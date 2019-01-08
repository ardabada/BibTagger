namespace BibTagger
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.additionalParamsBtn = new System.Windows.Forms.Button();
            this.logCheckbox = new System.Windows.Forms.CheckBox();
            this.useSavedProgressCheckbox = new System.Windows.Forms.CheckBox();
            this.bibColumnLbl = new System.Windows.Forms.Label();
            this.bibColumnSelectBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.includeSubdirsCheckbox = new System.Windows.Forms.CheckBox();
            this.photosDirPathLbl = new System.Windows.Forms.Label();
            this.dirBrowseBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataPathLbl = new System.Windows.Forms.Label();
            this.dataBrowseBtn = new System.Windows.Forms.Button();
            this.workTriggerBtn = new System.Windows.Forms.Button();
            this.logListBox = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.dataFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.currentProgressBar = new System.Windows.Forms.ProgressBar();
            this.totalProgressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.currentProgressLbl = new System.Windows.Forms.Label();
            this.totalProgressLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.currentOperationLbl = new System.Windows.Forms.Label();
            this.processedPhotosLbl = new System.Windows.Forms.Label();
            this.resolvedPhotosLbl = new System.Windows.Forms.Label();
            this.unresolvedPhotosLbl = new System.Windows.Forms.Label();
            this.controlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.additionalParamsBtn);
            this.controlsPanel.Controls.Add(this.logCheckbox);
            this.controlsPanel.Controls.Add(this.useSavedProgressCheckbox);
            this.controlsPanel.Controls.Add(this.bibColumnLbl);
            this.controlsPanel.Controls.Add(this.bibColumnSelectBtn);
            this.controlsPanel.Controls.Add(this.label10);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Controls.Add(this.includeSubdirsCheckbox);
            this.controlsPanel.Controls.Add(this.photosDirPathLbl);
            this.controlsPanel.Controls.Add(this.dirBrowseBtn);
            this.controlsPanel.Controls.Add(this.label2);
            this.controlsPanel.Controls.Add(this.dataPathLbl);
            this.controlsPanel.Controls.Add(this.dataBrowseBtn);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(633, 156);
            this.controlsPanel.TabIndex = 0;
            // 
            // additionalParamsBtn
            // 
            this.additionalParamsBtn.Location = new System.Drawing.Point(517, 107);
            this.additionalParamsBtn.Name = "additionalParamsBtn";
            this.additionalParamsBtn.Size = new System.Drawing.Size(104, 41);
            this.additionalParamsBtn.TabIndex = 38;
            this.additionalParamsBtn.Text = "Additional parameters";
            this.additionalParamsBtn.UseVisualStyleBackColor = true;
            this.additionalParamsBtn.Click += new System.EventHandler(this.additionalParamsBtn_Click);
            // 
            // logCheckbox
            // 
            this.logCheckbox.AutoSize = true;
            this.logCheckbox.Checked = true;
            this.logCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logCheckbox.Location = new System.Drawing.Point(121, 129);
            this.logCheckbox.Name = "logCheckbox";
            this.logCheckbox.Size = new System.Drawing.Size(67, 19);
            this.logCheckbox.TabIndex = 37;
            this.logCheckbox.Text = "Logging";
            this.logCheckbox.UseVisualStyleBackColor = true;
            // 
            // useSavedProgressCheckbox
            // 
            this.useSavedProgressCheckbox.AutoSize = true;
            this.useSavedProgressCheckbox.Location = new System.Drawing.Point(121, 104);
            this.useSavedProgressCheckbox.Name = "useSavedProgressCheckbox";
            this.useSavedProgressCheckbox.Size = new System.Drawing.Size(132, 19);
            this.useSavedProgressCheckbox.TabIndex = 36;
            this.useSavedProgressCheckbox.Text = "Use saved progress";
            this.useSavedProgressCheckbox.UseVisualStyleBackColor = true;
            // 
            // bibColumnLbl
            // 
            this.bibColumnLbl.AutoEllipsis = true;
            this.bibColumnLbl.Location = new System.Drawing.Point(118, 82);
            this.bibColumnLbl.Name = "bibColumnLbl";
            this.bibColumnLbl.Size = new System.Drawing.Size(392, 19);
            this.bibColumnLbl.TabIndex = 35;
            // 
            // bibColumnSelectBtn
            // 
            this.bibColumnSelectBtn.Location = new System.Drawing.Point(517, 78);
            this.bibColumnSelectBtn.Name = "bibColumnSelectBtn";
            this.bibColumnSelectBtn.Size = new System.Drawing.Size(104, 23);
            this.bibColumnSelectBtn.TabIndex = 34;
            this.bibColumnSelectBtn.Text = "Select";
            this.bibColumnSelectBtn.UseVisualStyleBackColor = true;
            this.bibColumnSelectBtn.Click += new System.EventHandler(this.bibColumnSelectBtn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 15);
            this.label10.TabIndex = 33;
            this.label10.Text = "Bib column:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Photos directory:";
            // 
            // includeSubdirsCheckbox
            // 
            this.includeSubdirsCheckbox.AutoSize = true;
            this.includeSubdirsCheckbox.Checked = true;
            this.includeSubdirsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeSubdirsCheckbox.Location = new System.Drawing.Point(121, 31);
            this.includeSubdirsCheckbox.Name = "includeSubdirsCheckbox";
            this.includeSubdirsCheckbox.Size = new System.Drawing.Size(150, 19);
            this.includeSubdirsCheckbox.TabIndex = 27;
            this.includeSubdirsCheckbox.Text = "Include subdirectories";
            this.includeSubdirsCheckbox.UseVisualStyleBackColor = true;
            // 
            // photosDirPathLbl
            // 
            this.photosDirPathLbl.AutoEllipsis = true;
            this.photosDirPathLbl.Location = new System.Drawing.Point(118, 9);
            this.photosDirPathLbl.Name = "photosDirPathLbl";
            this.photosDirPathLbl.Size = new System.Drawing.Size(392, 19);
            this.photosDirPathLbl.TabIndex = 28;
            // 
            // dirBrowseBtn
            // 
            this.dirBrowseBtn.Location = new System.Drawing.Point(517, 5);
            this.dirBrowseBtn.Name = "dirBrowseBtn";
            this.dirBrowseBtn.Size = new System.Drawing.Size(104, 23);
            this.dirBrowseBtn.TabIndex = 29;
            this.dirBrowseBtn.Text = "Browse";
            this.dirBrowseBtn.UseVisualStyleBackColor = true;
            this.dirBrowseBtn.Click += new System.EventHandler(this.dirBrowseBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 30;
            this.label2.Text = "Data file:";
            // 
            // dataPathLbl
            // 
            this.dataPathLbl.AutoEllipsis = true;
            this.dataPathLbl.Location = new System.Drawing.Point(118, 53);
            this.dataPathLbl.Name = "dataPathLbl";
            this.dataPathLbl.Size = new System.Drawing.Size(392, 19);
            this.dataPathLbl.TabIndex = 31;
            // 
            // dataBrowseBtn
            // 
            this.dataBrowseBtn.Location = new System.Drawing.Point(517, 49);
            this.dataBrowseBtn.Name = "dataBrowseBtn";
            this.dataBrowseBtn.Size = new System.Drawing.Size(104, 23);
            this.dataBrowseBtn.TabIndex = 32;
            this.dataBrowseBtn.Text = "Browse";
            this.dataBrowseBtn.UseVisualStyleBackColor = true;
            this.dataBrowseBtn.Click += new System.EventHandler(this.dataBrowseBtn_Click);
            // 
            // workTriggerBtn
            // 
            this.workTriggerBtn.Location = new System.Drawing.Point(15, 162);
            this.workTriggerBtn.Name = "workTriggerBtn";
            this.workTriggerBtn.Size = new System.Drawing.Size(606, 23);
            this.workTriggerBtn.TabIndex = 39;
            this.workTriggerBtn.Text = "Start";
            this.workTriggerBtn.UseVisualStyleBackColor = true;
            this.workTriggerBtn.Click += new System.EventHandler(this.workTriggerBtn_Click);
            // 
            // logListBox
            // 
            this.logListBox.FormattingEnabled = true;
            this.logListBox.HorizontalScrollbar = true;
            this.logListBox.ItemHeight = 15;
            this.logListBox.Location = new System.Drawing.Point(15, 191);
            this.logListBox.Name = "logListBox";
            this.logListBox.ScrollAlwaysVisible = true;
            this.logListBox.Size = new System.Drawing.Size(606, 139);
            this.logListBox.TabIndex = 40;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // dataFileOpenFileDialog
            // 
            this.dataFileOpenFileDialog.Filter = "Participants info|*.csv";
            // 
            // mainBackgroundWorker
            // 
            this.mainBackgroundWorker.WorkerReportsProgress = true;
            this.mainBackgroundWorker.WorkerSupportsCancellation = true;
            this.mainBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mainBackgroundWorker_DoWork);
            this.mainBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.mainBackgroundWorker_ProgressChanged);
            this.mainBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mainBackgroundWorker_RunWorkerCompleted);
            // 
            // currentProgressBar
            // 
            this.currentProgressBar.Location = new System.Drawing.Point(103, 460);
            this.currentProgressBar.MarqueeAnimationSpeed = 5;
            this.currentProgressBar.Name = "currentProgressBar";
            this.currentProgressBar.Size = new System.Drawing.Size(518, 23);
            this.currentProgressBar.TabIndex = 41;
            // 
            // totalProgressBar
            // 
            this.totalProgressBar.Location = new System.Drawing.Point(103, 489);
            this.totalProgressBar.MarqueeAnimationSpeed = 5;
            this.totalProgressBar.Name = "totalProgressBar";
            this.totalProgressBar.Size = new System.Drawing.Size(518, 23);
            this.totalProgressBar.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 460);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 23);
            this.label3.TabIndex = 43;
            this.label3.Text = "Current:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // currentProgressLbl
            // 
            this.currentProgressLbl.Location = new System.Drawing.Point(63, 460);
            this.currentProgressLbl.Name = "currentProgressLbl";
            this.currentProgressLbl.Size = new System.Drawing.Size(37, 23);
            this.currentProgressLbl.TabIndex = 44;
            this.currentProgressLbl.Text = "0%";
            this.currentProgressLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // totalProgressLbl
            // 
            this.totalProgressLbl.Location = new System.Drawing.Point(63, 489);
            this.totalProgressLbl.Name = "totalProgressLbl";
            this.totalProgressLbl.Size = new System.Drawing.Size(37, 23);
            this.totalProgressLbl.TabIndex = 45;
            this.totalProgressLbl.Text = "0%";
            this.totalProgressLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 489);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 23);
            this.label6.TabIndex = 46;
            this.label6.Text = "Total:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 335);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 15);
            this.label4.TabIndex = 47;
            this.label4.Text = "Current operation:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 355);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 15);
            this.label5.TabIndex = 48;
            this.label5.Text = "Processed photos:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 375);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 15);
            this.label7.TabIndex = 49;
            this.label7.Text = "Resolved photos:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 395);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 15);
            this.label8.TabIndex = 50;
            this.label8.Text = "Unresolved photos:";
            // 
            // currentOperationLbl
            // 
            this.currentOperationLbl.AutoSize = true;
            this.currentOperationLbl.Location = new System.Drawing.Point(128, 335);
            this.currentOperationLbl.Name = "currentOperationLbl";
            this.currentOperationLbl.Size = new System.Drawing.Size(80, 15);
            this.currentOperationLbl.TabIndex = 51;
            this.currentOperationLbl.Text = "Pending start";
            // 
            // processedPhotosLbl
            // 
            this.processedPhotosLbl.AutoSize = true;
            this.processedPhotosLbl.Location = new System.Drawing.Point(128, 355);
            this.processedPhotosLbl.Name = "processedPhotosLbl";
            this.processedPhotosLbl.Size = new System.Drawing.Size(26, 15);
            this.processedPhotosLbl.TabIndex = 52;
            this.processedPhotosLbl.Text = "0/0";
            // 
            // resolvedPhotosLbl
            // 
            this.resolvedPhotosLbl.AutoSize = true;
            this.resolvedPhotosLbl.Location = new System.Drawing.Point(128, 375);
            this.resolvedPhotosLbl.Name = "resolvedPhotosLbl";
            this.resolvedPhotosLbl.Size = new System.Drawing.Size(14, 15);
            this.resolvedPhotosLbl.TabIndex = 53;
            this.resolvedPhotosLbl.Text = "0";
            // 
            // unresolvedPhotosLbl
            // 
            this.unresolvedPhotosLbl.AutoSize = true;
            this.unresolvedPhotosLbl.Location = new System.Drawing.Point(128, 395);
            this.unresolvedPhotosLbl.Name = "unresolvedPhotosLbl";
            this.unresolvedPhotosLbl.Size = new System.Drawing.Size(14, 15);
            this.unresolvedPhotosLbl.TabIndex = 54;
            this.unresolvedPhotosLbl.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 524);
            this.Controls.Add(this.unresolvedPhotosLbl);
            this.Controls.Add(this.resolvedPhotosLbl);
            this.Controls.Add(this.processedPhotosLbl);
            this.Controls.Add(this.currentOperationLbl);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.totalProgressLbl);
            this.Controls.Add(this.currentProgressLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.totalProgressBar);
            this.Controls.Add(this.currentProgressBar);
            this.Controls.Add(this.logListBox);
            this.Controls.Add(this.workTriggerBtn);
            this.Controls.Add(this.controlsPanel);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bib Tagger";
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.CheckBox logCheckbox;
        private System.Windows.Forms.CheckBox useSavedProgressCheckbox;
        private System.Windows.Forms.Label bibColumnLbl;
        private System.Windows.Forms.Button bibColumnSelectBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox includeSubdirsCheckbox;
        private System.Windows.Forms.Label photosDirPathLbl;
        private System.Windows.Forms.Button dirBrowseBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dataPathLbl;
        private System.Windows.Forms.Button dataBrowseBtn;
        private System.Windows.Forms.Button additionalParamsBtn;
        private System.Windows.Forms.Button workTriggerBtn;
        private System.Windows.Forms.ListBox logListBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog dataFileOpenFileDialog;
        private System.ComponentModel.BackgroundWorker mainBackgroundWorker;
        private System.Windows.Forms.ProgressBar currentProgressBar;
        private System.Windows.Forms.ProgressBar totalProgressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label currentProgressLbl;
        private System.Windows.Forms.Label totalProgressLbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label currentOperationLbl;
        private System.Windows.Forms.Label processedPhotosLbl;
        private System.Windows.Forms.Label resolvedPhotosLbl;
        private System.Windows.Forms.Label unresolvedPhotosLbl;
    }
}