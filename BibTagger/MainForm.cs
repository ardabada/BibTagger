using CSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Logger;
using BibCore;
using FaceSearchModel = BibCore.FaceApi.Models.FaceSearchModel;
using System.Windows.Threading;

namespace BibTagger
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            listBoxLog = new ListBoxLog(logListBox);

            ProgressReport.OnProgressChanged += ProgressReport_OnProgressChanged;

            LogManager.OnLog += LogManager_OnLog;
        }

        #region Logging

        public ListBoxLog listBoxLog;
        
        public bool UseLogging
        {
            get { return logCheckbox.Checked; }
        }

        private void LogManager_OnLog(object sender, LogEventArgs e)
        {
            listBoxLog.Log(e.Level, e.FinalMessage);
        }

        #endregion

        #region Private variables

        string baseDirectory = string.Empty;
        string dataFilePath = string.Empty;
        string saveReportPath = string.Empty;
        string saveDataPath = string.Empty;

        ExtraParams _params = new ExtraParams();

        CsvRow dataHeader = null;
        int bibColumnIndex = 0;

        WorkStatus status = WorkStatus.ReadyToStart;
        string[] extensions = new string[] { ".jpg", ".jpeg", ".bmp", ".png" };

        #endregion


        private void dirBrowseBtn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(folderBrowserDialog.SelectedPath))
                {
                    baseDirectory = folderBrowserDialog.SelectedPath;
                    saveReportPath = Path.Combine(baseDirectory, "report.csv");
                    saveDataPath = Path.Combine(baseDirectory, "detection.json");
                }
            }

            photosDirPathLbl.Text = baseDirectory;
            LogManager.SetDirectory(baseDirectory);
        }


        bool isValidDataFile(string path)
        {
            return Path.GetExtension(path).ToLower() == ".csv";
        }
        private void dataBrowseBtn_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(baseDirectory) || !Directory.Exists(baseDirectory))
            //{
            //    MessageBox.Show("Please select base directory first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            if (dataFileOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (isValidDataFile(dataFileOpenFileDialog.FileName))
                {
                    dataFilePath = dataFileOpenFileDialog.FileName;
                    //saveReportPath = Path.Combine(Path.GetDirectoryName(baseDirectory), "report.csv");
                    //saveDataPath = Path.Combine(Path.GetDirectoryName(baseDirectory), "detection.json");
                }
                else MessageBox.Show("Bad data file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dataPathLbl.Text = Path.GetFileName(dataFilePath);
            tryGetBibColumn();
        }
        void tryGetBibColumn()
        {
            if (File.Exists(dataFilePath))
            {
                dataHeader = CsvManager.ReadHeader(dataFilePath);
                int index = 0;
                for (int i = 0; i < dataHeader.Columns.Count; i++)
                {
                    var val = dataHeader.Columns[i].ToLower();
                    if (val == "bib")
                    {
                        index = i;
                        break;
                    }
                }

                bibColumnIndex = index;
                showBibColumn();
            }
        }
        void showBibColumn()
        {
            if (dataHeader.Columns.Count > bibColumnIndex)
                bibColumnLbl.Text = dataHeader.Columns[bibColumnIndex] + " (column " + (bibColumnIndex + 1) + ")";
        }

        private void bibColumnSelectBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists(dataFilePath))
            {
                MessageBox.Show("Select data file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BibColumnsForm columns = new BibColumnsForm();
            columns.Header = dataHeader;
            columns.SelectedIndex = bibColumnIndex;
            if (columns.ShowDialog() == DialogResult.OK)
            {
                bibColumnIndex = columns.SelectedIndex;
            }
            showBibColumn();
        }

        private void additionalParamsBtn_Click(object sender, EventArgs e)
        {
            ExtraParamsForm epf = new ExtraParamsForm();
            epf.Params = _params;
            if (epf.ShowDialog() == DialogResult.OK)
            {
                _params = epf.Params;
            }
        }

        private void workTriggerBtn_Click(object sender, EventArgs e)
        {
            switch (status)
            {
                case WorkStatus.ReadyToStart:
                    start();
                    break;
                case WorkStatus.PendingStop:
                    return;
                case WorkStatus.Working:
                    requestStop();
                    break;
            }
        }

        void start()
        {
            if (mainBackgroundWorker.IsBusy)
                return;

            if (!File.Exists(dataFilePath) || !Directory.Exists(baseDirectory))
            {
                MessageBox.Show("Please select valid base directory and data file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            controlsPanel.Enabled = false;
            workTriggerBtn.Enabled = true;
            workTriggerBtn.Text = "Stop";
            status = WorkStatus.Working;
            LogManager.CanLog = UseLogging;
            LogManager.ClearLog(); //TODO: clear log if requested
            mainBackgroundWorker.RunWorkerAsync();
        }
        void resetState()
        {
            controlsPanel.Enabled = true;
            currentProgressBar.Value = 0;
            currentProgressBar.Style = ProgressBarStyle.Blocks;
            workTriggerBtn.Text = "Start";
            workTriggerBtn.Enabled = true;
            status = WorkStatus.ReadyToStart;
            ProgressReport.Reset();
        }
        void requestStop()
        {
            workTriggerBtn.Enabled = false;
            workTriggerBtn.Text = "Pending stop";
            status = WorkStatus.PendingStop;
            currentProgressBar.Style = ProgressBarStyle.Marquee;
            CsvManager.CancelOperation();
            mainBackgroundWorker.CancelAsync();
        }

        private void mainBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            #region Initialization

            BibDetector.Cleanup();
            
            ProgressReport.Directory = baseDirectory;
            ProgressReport.UseSubdirs = includeSubdirsCheckbox.Checked;
            ProgressReport.DataFile = dataFilePath;
            ProgressReport.Operation = CurrentOperation.Start;

            ProgressReport.Operation = CurrentOperation.ListingAllPhotos;
            ProgressReport.TotalPhotos = -1;

            List<string> files = filesInDirectory(baseDirectory);
            ProgressReport.TotalPhotos = files.Count;
            ProgressReport.Operation = CurrentOperation.PhotoListingDone;

            ProgressReport.Operation = CurrentOperation.ReadingParticipantsData;
            ProgressReport.TotalParticipants = -1;

            List<CsvRow> data = CsvManager.ReadAllWithoutHeader(dataFilePath);
            ProgressReport.TotalParticipants = data.Count;
            ProgressReport.Operation = CurrentOperation.ParticipantsDataReadingDone;

            ProgressReport.Operation = CurrentOperation.ProcessingParticipantsBibs;
            List<string> participantsBibs = new List<string>();
            foreach (var part in data)
            {
                if (mainBackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                participantsBibs.Add(part.Columns[bibColumnIndex]);
            }
            ProgressReport.Operation = CurrentOperation.ProcessingParticipantsBibsDone;
            
            List<CsvRow> report = new List<CsvRow>();
            List<TaggableFace> processedFaces = new List<TaggableFace>();
            TaggableResult tagResult = new TaggableResult(baseDirectory);
            
            if (useSavedProgressCheckbox.Checked)
            {
                ProgressReport.Operation = CurrentOperation.ReadingSavedProgress;
                var savedData = readSavedProgress();
                report = savedData.Item1;
                tagResult = savedData.Item2;
            }

            ProgressReport.Configuration = _params;

            #endregion

            #region First step procession: detecting bibs

            if (ProgressReport.Configuration.DetectionSteps == DetectionSteps.DetectionAndRecognition || ProgressReport.Configuration.DetectionSteps == DetectionSteps.Both)
            {
                ProgressReport.Operation = CurrentOperation.ProcessingPhotos;

                for (int i = 0; i < files.Count; i++)
                {
                    if (mainBackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    var photo = files[i];

                    if (tagResult.PhotoExists(photo))
                        continue;

                    ProgressReport.CurrentPhoto = Path.GetFileName(photo);
                    ProgressReport.CurrentPhotoIndex = i;

                    if (i != 0)
                        Thread.Sleep(_params.Delay);

                    TaggableImage tagImage = new TaggableImage(baseDirectory, photo);

                    var faces = BibDetector.GetPossibleBibs(photo, _params.FaceRecognition, _params.BibArea, _params.DigitsRecognition, baseDirectory);
                    if (faces == null)
                    {
                        LogManager.Error("Force break and cancellation of bib procession.");
                        break;
                    }
                    else processedFaces.AddRange(faces);

                    CsvRow row = new CsvRow();
                    row.Add(photo);
                    int actualBibs = 0;
                    int totalNum = 0;
                    int resolvedFaces = 0;
                    foreach (var face in faces)
                    {
                        List<string> nums = new List<string>();
                        bool faceResolved = false;
                        foreach (var num in face.NumericBibs)
                        {
                            totalNum++;
                            if (participantsBibs.Contains(num))
                            {
                                faceResolved = true;
                                ProgressReport.DetectedBibs++;
                                actualBibs++;
                                nums.Add(num);
                            }
                        }
                        if (faceResolved)
                        {
                            row.Add(nums.ToArray());
                            string tag = nums.First();
                            tagImage.AddFace(face.FaceId, tag, face.DetectedBibs);
                            resolvedFaces++;
                        }
                        else tagImage.AddFace(face.FaceId, face.DetectedBibs);
                    }

                    //recognized numbers - RN
                    //existing numbers - EN
                    //detected faces - DF
                    //resolved faces - RF
                    string logMsg = string.Format("RN: {0}; EN: {1}; DF: {2}; RF: {3}", totalNum, actualBibs, faces.Length, resolvedFaces);

                    if (actualBibs == 0)
                    {
                        ProgressReport.UnresolvedImages++;
                        row.Add("UNRESOLVED");
                    }
                    else ProgressReport.ResolvedImages++;

                    LogManager.Info(logMsg);

                    report.Add(row);
                    CsvManager.SaveCsv(report, saveReportPath);
                    tagResult.AddImage(tagImage);
                    tagResult.Save(saveDataPath);

                    ProgressReport.CurrentPhotoIndex = i + 1;

                    //TODO: iteration cleanup
                    //BibDetector.IterationCleanup();
                }

            }
            #endregion

            #region Second step procession: train faces

            if (ProgressReport.Configuration.DetectionSteps == DetectionSteps.FacialUnresolved || ProgressReport.Configuration.DetectionSteps == DetectionSteps.Both)
            {
                tagResult = readSavedProgress().Item2;
                ProgressReport.Operation = CurrentOperation.TrainingFaces;
                Dictionary<string, string> idsAndTags = new Dictionary<string, string>();
                for (int i = 0; i < tagResult.Images.Count; i++)
                {
                    //TODO: set current photo index
                    var image = tagResult.Images[i];
                    for (int j = 0; j < image.Data.Count; j++)
                    {
                        if (mainBackgroundWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }

                        var face = image.Data[j];
                        if (face.Resolved)
                        {
                            idsAndTags.Add(face.FaceId, face.ResolvedBib);
                        }
                    }
                }

                //train resolved faces
                LogManager.Info("Starting training resolved faces: " + idsAndTags.Count);
                //BibDetector.StudyFaces(idsAndTags, ProgressReport.Configuration.Delay);

                ProgressReport.Operation = CurrentOperation.SearchingUnresolvedFaces;
                for (int i = 0; i < tagResult.Images.Count; i++)
                {
                    //TODO: set current photo index
                    var image = tagResult.Images[i];
                    for (int j = 0; j < image.Data.Count; j++)
                    {
                        var face = image.Data[j];
                        if (!face.Resolved)
                        {
                            Thread.Sleep(ProgressReport.Configuration.Delay);
                            //search unresolved photo
                            List<FaceSearchModel> samePeople = BibDetector.SearchFace(face.FaceId);
                            if (samePeople != null && samePeople.Count > 0)
                            {
                                int bestIndex = 0;
                                for (int k = bestIndex + 1; i < samePeople.Count; i++)
                                {
                                    if (samePeople[i].Confidence > samePeople[bestIndex].Confidence)
                                        bestIndex = i;
                                }
                                FaceSearchModel bestMatch = samePeople[bestIndex];
                                face.Resolved = true;
                                face.ResolvedBib = bestMatch.Tag;
                                tagResult.Save(saveDataPath);
                                LogManager.Info("Match \"" + face.FaceId + "\" with BIB \"" + face.ResolvedBib + "\"");
                            }
                            else LogManager.Warning("No matches for \"" + face.FaceId + "\"");
                        }
                    }
                }
            }
            #endregion

            //TODO: saving csv file after detecting each bib number to prevent cancelling and loosing results
            CsvManager.SaveCsv(report, saveReportPath);
        }
        
        
        private Tuple<List<CsvRow>, TaggableResult> readSavedProgress()
        {
            TaggableResult tagResult = TaggableResult.Load(saveDataPath);
            List<CsvRow> report = CsvManager.ReadAll(saveReportPath);

            return new Tuple<List<CsvRow>, TaggableResult>(report, tagResult);
        }

        private void ProgressReport_OnProgressChanged(object sender, EventArgs e)
        {
            reportProgress();
        }

        void reportProgress()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { reportProgress(); });
                return;
            }

            currentOperationLbl.Text = ProgressReport.OperationText;
            if (ProgressReport.TotalPhotos > 0)
                processedPhotosLbl.Text = ProgressReport.CurrentPhotoIndex + "/" + ProgressReport.TotalPhotos;
            resolvedPhotosLbl.Text = ProgressReport.ResolvedImages.ToString();
            unresolvedPhotosLbl.Text = ProgressReport.UnresolvedImages.ToString();

            if (ProgressReport.CurrentProgress <= -1)
                currentProgressBar.Style = ProgressBarStyle.Marquee;
            else
            {
                currentProgressBar.Style = ProgressBarStyle.Blocks;
                currentProgressBar.Value = ProgressReport.CurrentProgress;
            }

            if (ProgressReport.TotalProgress <= -1)
                totalProgressBar.Style = ProgressBarStyle.Marquee;
            else
            {
                totalProgressBar.Style = ProgressBarStyle.Blocks;
                totalProgressBar.Value = ProgressReport.TotalProgress;
            }
            currentProgressLbl.Text = ProgressReport.CurrentProgress + "%";
            totalProgressLbl.Text = ProgressReport.TotalProgress + "%";
        }

        private void mainBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //currentOperationLbl.Text = ProgressReport.Operation;
            //if (ProgressReport.TotalPhotos < -1)
            //    totalPhotosLbl.Text = "0";
            //else if (ProgressReport.TotalPhotos == -1)
            //    totalPhotosLbl.Text = "Calculating..";
            //else totalPhotosLbl.Text = ProgressReport.TotalPhotos.ToString();

            //if (ProgressReport.TotalParticipants < -1)
            //    totalParticipantsLbl.Text = "0";
            //else if (ProgressReport.TotalParticipants == -1)
            //    totalParticipantsLbl.Text = "Calculating..";
            //else totalParticipantsLbl.Text = ProgressReport.TotalParticipants.ToString();

            //currentPhotoLbl.Text = ProgressReport.CurrentPhoto;

            //currentProgressLbl.Text = ProgressReport.CurrentProgress.ToString() + "%";
            //if (!string.IsNullOrEmpty(ProgressReport.CurrentPhoto))
            //    currentProgressLbl.Text += " (" + ProgressReport.CurrentPhotoIndex + " of " + ProgressReport.TotalPhotos + ")";

            //detectedBibsLbl.Text = ProgressReport.DetectedBibs.ToString();
            //unresolvedImagesLbl.Text = ProgressReport.UnresolvedImages.ToString();

            //progressBar.Value = ProgressReport.CurrentProgress;
        }
        private void mainBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            resetState();
            if (e.Cancelled)
            {
                LogManager.Info("Bib tagging stopped user.");
                MessageBox.Show("Parsing stopped by user.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                LogManager.Info("Bib tagging completed.");
                MessageBox.Show("Bib tagging completed. See results in report file.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        List<string> filesInDirectory(string dir)
        {
            List<string> result = new List<string>();
            try
            {
                DirectoryInfo d = new DirectoryInfo(dir);
                FileInfo[] files = null;
                try
                {
                    files = d.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                }
                catch { }
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (mainBackgroundWorker.CancellationPending)
                            break;
                        string ext = Path.GetExtension(file.FullName).ToLower();
                        if (extensions.Contains(ext))
                            result.Add(file.FullName);
                    }
                }
            }
            catch { }

            if (includeSubdirsCheckbox.Checked)
            {
                try
                {
                    foreach (var s_dir in Directory.GetDirectories(dir))
                    {
                        if (mainBackgroundWorker.CancellationPending)
                            break;
                        result.AddRange(filesInDirectory(s_dir));
                    }
                }
                catch { }
            }
            return result;
        }
    }
}
