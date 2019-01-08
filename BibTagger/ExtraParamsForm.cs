using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BibCore;

namespace BibTagger
{
    public partial class ExtraParamsForm : Form
    {
        public ExtraParamsForm()
        {
            InitializeComponent();
        }

        private ExtraParams _params = new ExtraParams();
        public ExtraParams Params
        {
            get { return _params; }
            set { _params = value; updateButtons(); }
        }

        void updateButtons()
        {
            delayTrackbar.Value = _params.Delay;

            switch (_params.FaceRecognition)
            {
                case FaceRecognitionMethod.FacePlusPlus:
                    facePlusPlusRadioBtn.Checked = true;
                    break;
                case FaceRecognitionMethod.HaarCascade:
                    haarCascadeRadioBtn.Checked = true;
                    break;
                case FaceRecognitionMethod.PreservedFacesData:
                    preservedFacesBtn.Checked = true;
                    break;
            }

            switch (_params.BibArea)
            {
                case BibAreaDetectionMethod.SWT:
                    swtRadioBtn.Checked = true;
                    break;
                case BibAreaDetectionMethod.Edges:
                    edgesRadioBtn.Checked = true;
                    break;
                case BibAreaDetectionMethod.Both:
                    bibAreaBothRadioBtn.Checked = true;
                    break;
            }

            switch (_params.DigitsRecognition)
            {
                case DigitsRecognitionMethod.Neural:
                    neuralRadioBtn.Checked = true;
                    break;
                case DigitsRecognitionMethod.Tesseract:
                    tesseractRadioBtn.Checked = true;
                    break;
                case DigitsRecognitionMethod.Both:
                    digitsBothRadioBtn.Checked = true;
                    break;
            }

            switch (_params.DetectionSteps)
            {
                case DetectionSteps.DetectionAndRecognition:
                    stepDetectionAndRecognitionRadioBtn.Checked = true;
                    break;
                case DetectionSteps.FacialUnresolved:
                    stepFacialRadioBtn.Checked = true;
                    break;
                case DetectionSteps.Both:
                    stepBothRadioBtn.Checked = true;
                    break;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (_params == null)
                _params = new ExtraParams();

            _params.Delay = delayTrackbar.Value;

            if (facePlusPlusRadioBtn.Checked)
                _params.FaceRecognition = FaceRecognitionMethod.FacePlusPlus;
            else if (haarCascadeRadioBtn.Checked)
                _params.FaceRecognition = FaceRecognitionMethod.HaarCascade;
            else _params.FaceRecognition = FaceRecognitionMethod.PreservedFacesData;

            if (swtRadioBtn.Checked)
                _params.BibArea = BibAreaDetectionMethod.SWT;
            else if (edgesRadioBtn.Checked)
                _params.BibArea = BibAreaDetectionMethod.Edges;
            else _params.BibArea = BibAreaDetectionMethod.Both;

            if (neuralRadioBtn.Checked)
                _params.DigitsRecognition = DigitsRecognitionMethod.Neural;
            else if (tesseractRadioBtn.Checked)
                _params.DigitsRecognition = DigitsRecognitionMethod.Tesseract;
            else _params.DigitsRecognition = DigitsRecognitionMethod.Both;

            if (stepDetectionAndRecognitionRadioBtn.Checked)
                _params.DetectionSteps = DetectionSteps.DetectionAndRecognition;
            else if (stepFacialRadioBtn.Checked)
                _params.DetectionSteps = DetectionSteps.FacialUnresolved;
            else _params.DetectionSteps = DetectionSteps.Both;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void delayTrackbar_ValueChanged(object sender, EventArgs e)
        {
            delayLbl.Text = delayTrackbar.Value + " ms";
        }
    }
}
