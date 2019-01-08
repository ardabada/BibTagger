namespace BibTagger
{
    partial class ExtraParamsForm
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
            this.facePlusPlusRadioBtn = new System.Windows.Forms.RadioButton();
            this.haarCascadeRadioBtn = new System.Windows.Forms.RadioButton();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.faceRecognitionGroupBox = new System.Windows.Forms.GroupBox();
            this.preservedFacesBtn = new System.Windows.Forms.RadioButton();
            this.areaDetectionGroupBox = new System.Windows.Forms.GroupBox();
            this.bibAreaBothRadioBtn = new System.Windows.Forms.RadioButton();
            this.edgesRadioBtn = new System.Windows.Forms.RadioButton();
            this.swtRadioBtn = new System.Windows.Forms.RadioButton();
            this.digitsGroupBox = new System.Windows.Forms.GroupBox();
            this.digitsBothRadioBtn = new System.Windows.Forms.RadioButton();
            this.tesseractRadioBtn = new System.Windows.Forms.RadioButton();
            this.neuralRadioBtn = new System.Windows.Forms.RadioButton();
            this.delayGroupbox = new System.Windows.Forms.GroupBox();
            this.delayLbl = new System.Windows.Forms.Label();
            this.delayTrackbar = new System.Windows.Forms.TrackBar();
            this.stepsGroupBox = new System.Windows.Forms.GroupBox();
            this.stepDetectionAndRecognitionRadioBtn = new System.Windows.Forms.RadioButton();
            this.stepFacialRadioBtn = new System.Windows.Forms.RadioButton();
            this.stepBothRadioBtn = new System.Windows.Forms.RadioButton();
            this.faceRecognitionGroupBox.SuspendLayout();
            this.areaDetectionGroupBox.SuspendLayout();
            this.digitsGroupBox.SuspendLayout();
            this.delayGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayTrackbar)).BeginInit();
            this.stepsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // facePlusPlusRadioBtn
            // 
            this.facePlusPlusRadioBtn.AutoSize = true;
            this.facePlusPlusRadioBtn.Checked = true;
            this.facePlusPlusRadioBtn.Location = new System.Drawing.Point(10, 22);
            this.facePlusPlusRadioBtn.Name = "facePlusPlusRadioBtn";
            this.facePlusPlusRadioBtn.Size = new System.Drawing.Size(117, 19);
            this.facePlusPlusRadioBtn.TabIndex = 1;
            this.facePlusPlusRadioBtn.TabStop = true;
            this.facePlusPlusRadioBtn.Text = "Using Face++ API";
            this.facePlusPlusRadioBtn.UseVisualStyleBackColor = true;
            // 
            // haarCascadeRadioBtn
            // 
            this.haarCascadeRadioBtn.AutoSize = true;
            this.haarCascadeRadioBtn.Location = new System.Drawing.Point(10, 47);
            this.haarCascadeRadioBtn.Name = "haarCascadeRadioBtn";
            this.haarCascadeRadioBtn.Size = new System.Drawing.Size(137, 19);
            this.haarCascadeRadioBtn.TabIndex = 2;
            this.haarCascadeRadioBtn.Text = "Using HAAR Cascade";
            this.haarCascadeRadioBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(369, 339);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 3;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(288, 339);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // faceRecognitionGroupBox
            // 
            this.faceRecognitionGroupBox.Controls.Add(this.preservedFacesBtn);
            this.faceRecognitionGroupBox.Controls.Add(this.haarCascadeRadioBtn);
            this.faceRecognitionGroupBox.Controls.Add(this.facePlusPlusRadioBtn);
            this.faceRecognitionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.faceRecognitionGroupBox.Name = "faceRecognitionGroupBox";
            this.faceRecognitionGroupBox.Size = new System.Drawing.Size(167, 99);
            this.faceRecognitionGroupBox.TabIndex = 5;
            this.faceRecognitionGroupBox.TabStop = false;
            this.faceRecognitionGroupBox.Text = "Face recognition";
            // 
            // preservedFacesBtn
            // 
            this.preservedFacesBtn.AutoSize = true;
            this.preservedFacesBtn.Location = new System.Drawing.Point(10, 72);
            this.preservedFacesBtn.Name = "preservedFacesBtn";
            this.preservedFacesBtn.Size = new System.Drawing.Size(139, 19);
            this.preservedFacesBtn.TabIndex = 3;
            this.preservedFacesBtn.Text = "Preserved faces data";
            this.preservedFacesBtn.UseVisualStyleBackColor = true;
            // 
            // areaDetectionGroupBox
            // 
            this.areaDetectionGroupBox.Controls.Add(this.bibAreaBothRadioBtn);
            this.areaDetectionGroupBox.Controls.Add(this.edgesRadioBtn);
            this.areaDetectionGroupBox.Controls.Add(this.swtRadioBtn);
            this.areaDetectionGroupBox.Location = new System.Drawing.Point(12, 117);
            this.areaDetectionGroupBox.Name = "areaDetectionGroupBox";
            this.areaDetectionGroupBox.Size = new System.Drawing.Size(167, 105);
            this.areaDetectionGroupBox.TabIndex = 6;
            this.areaDetectionGroupBox.TabStop = false;
            this.areaDetectionGroupBox.Text = "BIB area detection method";
            // 
            // bibAreaBothRadioBtn
            // 
            this.bibAreaBothRadioBtn.AutoSize = true;
            this.bibAreaBothRadioBtn.Location = new System.Drawing.Point(10, 72);
            this.bibAreaBothRadioBtn.Name = "bibAreaBothRadioBtn";
            this.bibAreaBothRadioBtn.Size = new System.Drawing.Size(50, 19);
            this.bibAreaBothRadioBtn.TabIndex = 3;
            this.bibAreaBothRadioBtn.Text = "Both";
            this.bibAreaBothRadioBtn.UseVisualStyleBackColor = true;
            // 
            // edgesRadioBtn
            // 
            this.edgesRadioBtn.AutoSize = true;
            this.edgesRadioBtn.Checked = true;
            this.edgesRadioBtn.Location = new System.Drawing.Point(10, 47);
            this.edgesRadioBtn.Name = "edgesRadioBtn";
            this.edgesRadioBtn.Size = new System.Drawing.Size(90, 19);
            this.edgesRadioBtn.TabIndex = 2;
            this.edgesRadioBtn.TabStop = true;
            this.edgesRadioBtn.Text = "Using edges";
            this.edgesRadioBtn.UseVisualStyleBackColor = true;
            // 
            // swtRadioBtn
            // 
            this.swtRadioBtn.AutoSize = true;
            this.swtRadioBtn.Location = new System.Drawing.Point(10, 22);
            this.swtRadioBtn.Name = "swtRadioBtn";
            this.swtRadioBtn.Size = new System.Drawing.Size(83, 19);
            this.swtRadioBtn.TabIndex = 1;
            this.swtRadioBtn.Text = "Using SWT";
            this.swtRadioBtn.UseVisualStyleBackColor = true;
            // 
            // digitsGroupBox
            // 
            this.digitsGroupBox.Controls.Add(this.digitsBothRadioBtn);
            this.digitsGroupBox.Controls.Add(this.tesseractRadioBtn);
            this.digitsGroupBox.Controls.Add(this.neuralRadioBtn);
            this.digitsGroupBox.Location = new System.Drawing.Point(185, 117);
            this.digitsGroupBox.Name = "digitsGroupBox";
            this.digitsGroupBox.Size = new System.Drawing.Size(259, 105);
            this.digitsGroupBox.TabIndex = 7;
            this.digitsGroupBox.TabStop = false;
            this.digitsGroupBox.Text = "Digits recognition";
            // 
            // digitsBothRadioBtn
            // 
            this.digitsBothRadioBtn.AutoSize = true;
            this.digitsBothRadioBtn.Location = new System.Drawing.Point(10, 72);
            this.digitsBothRadioBtn.Name = "digitsBothRadioBtn";
            this.digitsBothRadioBtn.Size = new System.Drawing.Size(50, 19);
            this.digitsBothRadioBtn.TabIndex = 3;
            this.digitsBothRadioBtn.Text = "Both";
            this.digitsBothRadioBtn.UseVisualStyleBackColor = true;
            // 
            // tesseractRadioBtn
            // 
            this.tesseractRadioBtn.AutoSize = true;
            this.tesseractRadioBtn.Location = new System.Drawing.Point(10, 47);
            this.tesseractRadioBtn.Name = "tesseractRadioBtn";
            this.tesseractRadioBtn.Size = new System.Drawing.Size(76, 19);
            this.tesseractRadioBtn.TabIndex = 2;
            this.tesseractRadioBtn.Text = "Tesseract";
            this.tesseractRadioBtn.UseVisualStyleBackColor = true;
            // 
            // neuralRadioBtn
            // 
            this.neuralRadioBtn.AutoSize = true;
            this.neuralRadioBtn.Checked = true;
            this.neuralRadioBtn.Location = new System.Drawing.Point(10, 22);
            this.neuralRadioBtn.Name = "neuralRadioBtn";
            this.neuralRadioBtn.Size = new System.Drawing.Size(109, 19);
            this.neuralRadioBtn.TabIndex = 1;
            this.neuralRadioBtn.TabStop = true;
            this.neuralRadioBtn.Text = "Neural network";
            this.neuralRadioBtn.UseVisualStyleBackColor = true;
            // 
            // delayGroupbox
            // 
            this.delayGroupbox.Controls.Add(this.delayLbl);
            this.delayGroupbox.Controls.Add(this.delayTrackbar);
            this.delayGroupbox.Location = new System.Drawing.Point(185, 12);
            this.delayGroupbox.Name = "delayGroupbox";
            this.delayGroupbox.Size = new System.Drawing.Size(259, 99);
            this.delayGroupbox.TabIndex = 8;
            this.delayGroupbox.TabStop = false;
            this.delayGroupbox.Text = "Delay";
            // 
            // delayLbl
            // 
            this.delayLbl.Location = new System.Drawing.Point(190, 19);
            this.delayLbl.Name = "delayLbl";
            this.delayLbl.Size = new System.Drawing.Size(63, 33);
            this.delayLbl.TabIndex = 1;
            this.delayLbl.Text = "500 ms";
            this.delayLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // delayTrackbar
            // 
            this.delayTrackbar.LargeChange = 100;
            this.delayTrackbar.Location = new System.Drawing.Point(10, 22);
            this.delayTrackbar.Maximum = 2000;
            this.delayTrackbar.Minimum = 300;
            this.delayTrackbar.Name = "delayTrackbar";
            this.delayTrackbar.Size = new System.Drawing.Size(188, 45);
            this.delayTrackbar.SmallChange = 10;
            this.delayTrackbar.TabIndex = 0;
            this.delayTrackbar.TickFrequency = 100;
            this.delayTrackbar.Value = 500;
            this.delayTrackbar.ValueChanged += new System.EventHandler(this.delayTrackbar_ValueChanged);
            // 
            // stepsGroupBox
            // 
            this.stepsGroupBox.Controls.Add(this.stepBothRadioBtn);
            this.stepsGroupBox.Controls.Add(this.stepFacialRadioBtn);
            this.stepsGroupBox.Controls.Add(this.stepDetectionAndRecognitionRadioBtn);
            this.stepsGroupBox.Location = new System.Drawing.Point(12, 228);
            this.stepsGroupBox.Name = "stepsGroupBox";
            this.stepsGroupBox.Size = new System.Drawing.Size(432, 105);
            this.stepsGroupBox.TabIndex = 9;
            this.stepsGroupBox.TabStop = false;
            this.stepsGroupBox.Text = "Steps";
            // 
            // stepDetectionAndRecognitionRadioBtn
            // 
            this.stepDetectionAndRecognitionRadioBtn.AutoSize = true;
            this.stepDetectionAndRecognitionRadioBtn.Location = new System.Drawing.Point(10, 22);
            this.stepDetectionAndRecognitionRadioBtn.Name = "stepDetectionAndRecognitionRadioBtn";
            this.stepDetectionAndRecognitionRadioBtn.Size = new System.Drawing.Size(304, 19);
            this.stepDetectionAndRecognitionRadioBtn.TabIndex = 2;
            this.stepDetectionAndRecognitionRadioBtn.Text = "BIB detection and recognition with selected method";
            this.stepDetectionAndRecognitionRadioBtn.UseVisualStyleBackColor = true;
            // 
            // stepFacialRadioBtn
            // 
            this.stepFacialRadioBtn.AutoSize = true;
            this.stepFacialRadioBtn.Location = new System.Drawing.Point(10, 47);
            this.stepFacialRadioBtn.Name = "stepFacialRadioBtn";
            this.stepFacialRadioBtn.Size = new System.Drawing.Size(232, 19);
            this.stepFacialRadioBtn.TabIndex = 3;
            this.stepFacialRadioBtn.Text = "Facial detection of unresolved images";
            this.stepFacialRadioBtn.UseVisualStyleBackColor = true;
            // 
            // stepBothRadioBtn
            // 
            this.stepBothRadioBtn.AutoSize = true;
            this.stepBothRadioBtn.Checked = true;
            this.stepBothRadioBtn.Location = new System.Drawing.Point(10, 72);
            this.stepBothRadioBtn.Name = "stepBothRadioBtn";
            this.stepBothRadioBtn.Size = new System.Drawing.Size(50, 19);
            this.stepBothRadioBtn.TabIndex = 4;
            this.stepBothRadioBtn.TabStop = true;
            this.stepBothRadioBtn.Text = "Both";
            this.stepBothRadioBtn.UseVisualStyleBackColor = true;
            // 
            // ExtraParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 368);
            this.Controls.Add(this.stepsGroupBox);
            this.Controls.Add(this.delayGroupbox);
            this.Controls.Add(this.digitsGroupBox);
            this.Controls.Add(this.areaDetectionGroupBox);
            this.Controls.Add(this.faceRecognitionGroupBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtraParamsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Additional params";
            this.faceRecognitionGroupBox.ResumeLayout(false);
            this.faceRecognitionGroupBox.PerformLayout();
            this.areaDetectionGroupBox.ResumeLayout(false);
            this.areaDetectionGroupBox.PerformLayout();
            this.digitsGroupBox.ResumeLayout(false);
            this.digitsGroupBox.PerformLayout();
            this.delayGroupbox.ResumeLayout(false);
            this.delayGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayTrackbar)).EndInit();
            this.stepsGroupBox.ResumeLayout(false);
            this.stepsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton facePlusPlusRadioBtn;
        private System.Windows.Forms.RadioButton haarCascadeRadioBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.GroupBox faceRecognitionGroupBox;
        private System.Windows.Forms.GroupBox areaDetectionGroupBox;
        private System.Windows.Forms.RadioButton bibAreaBothRadioBtn;
        private System.Windows.Forms.RadioButton edgesRadioBtn;
        private System.Windows.Forms.RadioButton swtRadioBtn;
        private System.Windows.Forms.GroupBox digitsGroupBox;
        private System.Windows.Forms.RadioButton digitsBothRadioBtn;
        private System.Windows.Forms.RadioButton tesseractRadioBtn;
        private System.Windows.Forms.RadioButton neuralRadioBtn;
        private System.Windows.Forms.GroupBox delayGroupbox;
        private System.Windows.Forms.Label delayLbl;
        private System.Windows.Forms.TrackBar delayTrackbar;
        private System.Windows.Forms.RadioButton preservedFacesBtn;
        private System.Windows.Forms.GroupBox stepsGroupBox;
        private System.Windows.Forms.RadioButton stepBothRadioBtn;
        private System.Windows.Forms.RadioButton stepFacialRadioBtn;
        private System.Windows.Forms.RadioButton stepDetectionAndRecognitionRadioBtn;
    }
}