namespace SMZ_CADExportTool
{
    partial class CADExportTool
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FilePath_ListView = new ListView();
            FileName = new ColumnHeader();
            FullPath = new ColumnHeader();
            DrawFileOption_GroupBox = new GroupBox();
            DXF_CheckBox = new CheckBox();
            PDF_CheckBox = new CheckBox();
            PartFileOption_GroupBox = new GroupBox();
            ThreeMF_CheckBox = new CheckBox();
            STEP_CheckBox = new CheckBox();
            IGS_CheckBox = new CheckBox();
            ExportFolder_GroupBox = new GroupBox();
            OtherFolder_Button = new Button();
            OtherFolder_ListBox = new ListBox();
            LowerFolder_ComboBox = new ComboBox();
            OtherFolder_RadioButton = new RadioButton();
            LowerFolder_RadioButton = new RadioButton();
            SameFolder_RadioButton = new RadioButton();
            FilePath_Button = new Button();
            ZipOption_GroupBox = new GroupBox();
            CreateZipFolder_CheckBox = new CheckBox();
            ZipOtherFolder_ListBox = new ListBox();
            ZipOtherFolder_RadioButton = new RadioButton();
            ZipOtherFolder_Button = new Button();
            ZipLowerFolder_ComboBox = new ComboBox();
            ZipLowerFolder_RadioButton = new RadioButton();
            ZipSameFolder_RadioButton = new RadioButton();
            Zip_GroupBox = new GroupBox();
            ZipOption_CheckBox = new CheckBox();
            Other_GroupBox = new GroupBox();
            CreateThumbnail_CheckBox = new CheckBox();
            StartExport_Button = new Button();
            Break_Button = new Button();
            StartExport_ProgressBar = new ProgressBar();
            FilePath_OpenFileDialog = new OpenFileDialog();
            TaskLabel = new Label();
            DrawFileOption_GroupBox.SuspendLayout();
            PartFileOption_GroupBox.SuspendLayout();
            ExportFolder_GroupBox.SuspendLayout();
            ZipOption_GroupBox.SuspendLayout();
            Zip_GroupBox.SuspendLayout();
            Other_GroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // FilePath_ListView
            // 
            FilePath_ListView.AllowDrop = true;
            FilePath_ListView.Columns.AddRange(new ColumnHeader[] { FileName, FullPath });
            FilePath_ListView.Location = new Point(12, 41);
            FilePath_ListView.Name = "FilePath_ListView";
            FilePath_ListView.Size = new Size(324, 610);
            FilePath_ListView.TabIndex = 1;
            FilePath_ListView.UseCompatibleStateImageBehavior = false;
            FilePath_ListView.View = View.Details;
            FilePath_ListView.DragDrop += FilePath_ListView_DragDrop;
            FilePath_ListView.DragEnter += FilePath_ListView_DragEnter;
            FilePath_ListView.KeyDown += FilePath_ListView_KeyDown;
            // 
            // FileName
            // 
            FileName.Text = "FileName";
            FileName.Width = 90;
            // 
            // FullPath
            // 
            FullPath.Text = "FullPath";
            // 
            // DrawFileOption_GroupBox
            // 
            DrawFileOption_GroupBox.Controls.Add(DXF_CheckBox);
            DrawFileOption_GroupBox.Controls.Add(PDF_CheckBox);
            DrawFileOption_GroupBox.Location = new Point(342, 12);
            DrawFileOption_GroupBox.Name = "DrawFileOption_GroupBox";
            DrawFileOption_GroupBox.Size = new Size(391, 45);
            DrawFileOption_GroupBox.TabIndex = 2;
            DrawFileOption_GroupBox.TabStop = false;
            DrawFileOption_GroupBox.Text = "図面ファイル出力オプション";
            // 
            // DXF_CheckBox
            // 
            DXF_CheckBox.AutoSize = true;
            DXF_CheckBox.Location = new Point(59, 22);
            DXF_CheckBox.Name = "DXF_CheckBox";
            DXF_CheckBox.Size = new Size(46, 19);
            DXF_CheckBox.TabIndex = 1;
            DXF_CheckBox.Text = ".dxf";
            DXF_CheckBox.UseVisualStyleBackColor = true;
            // 
            // PDF_CheckBox
            // 
            PDF_CheckBox.AutoSize = true;
            PDF_CheckBox.Location = new Point(6, 22);
            PDF_CheckBox.Name = "PDF_CheckBox";
            PDF_CheckBox.Size = new Size(47, 19);
            PDF_CheckBox.TabIndex = 0;
            PDF_CheckBox.Text = ".pdf";
            PDF_CheckBox.UseVisualStyleBackColor = true;
            // 
            // PartFileOption_GroupBox
            // 
            PartFileOption_GroupBox.Controls.Add(ThreeMF_CheckBox);
            PartFileOption_GroupBox.Controls.Add(STEP_CheckBox);
            PartFileOption_GroupBox.Controls.Add(IGS_CheckBox);
            PartFileOption_GroupBox.Location = new Point(342, 63);
            PartFileOption_GroupBox.Name = "PartFileOption_GroupBox";
            PartFileOption_GroupBox.Size = new Size(391, 49);
            PartFileOption_GroupBox.TabIndex = 3;
            PartFileOption_GroupBox.TabStop = false;
            PartFileOption_GroupBox.Text = "パーツファイル出力オプション";
            // 
            // ThreeMF_CheckBox
            // 
            ThreeMF_CheckBox.AutoSize = true;
            ThreeMF_CheckBox.Location = new Point(113, 22);
            ThreeMF_CheckBox.Name = "ThreeMF_CheckBox";
            ThreeMF_CheckBox.Size = new Size(49, 19);
            ThreeMF_CheckBox.TabIndex = 2;
            ThreeMF_CheckBox.Text = ".3mf";
            ThreeMF_CheckBox.UseVisualStyleBackColor = true;
            // 
            // STEP_CheckBox
            // 
            STEP_CheckBox.AutoSize = true;
            STEP_CheckBox.Location = new Point(56, 22);
            STEP_CheckBox.Name = "STEP_CheckBox";
            STEP_CheckBox.Size = new Size(51, 19);
            STEP_CheckBox.TabIndex = 1;
            STEP_CheckBox.Text = ".step";
            STEP_CheckBox.UseVisualStyleBackColor = true;
            // 
            // IGS_CheckBox
            // 
            IGS_CheckBox.AutoSize = true;
            IGS_CheckBox.Location = new Point(6, 22);
            IGS_CheckBox.Name = "IGS_CheckBox";
            IGS_CheckBox.Size = new Size(44, 19);
            IGS_CheckBox.TabIndex = 0;
            IGS_CheckBox.Text = ".igs";
            IGS_CheckBox.UseVisualStyleBackColor = true;
            // 
            // ExportFolder_GroupBox
            // 
            ExportFolder_GroupBox.Controls.Add(OtherFolder_Button);
            ExportFolder_GroupBox.Controls.Add(OtherFolder_ListBox);
            ExportFolder_GroupBox.Controls.Add(LowerFolder_ComboBox);
            ExportFolder_GroupBox.Controls.Add(OtherFolder_RadioButton);
            ExportFolder_GroupBox.Controls.Add(LowerFolder_RadioButton);
            ExportFolder_GroupBox.Controls.Add(SameFolder_RadioButton);
            ExportFolder_GroupBox.Location = new Point(342, 118);
            ExportFolder_GroupBox.Name = "ExportFolder_GroupBox";
            ExportFolder_GroupBox.Size = new Size(391, 168);
            ExportFolder_GroupBox.TabIndex = 4;
            ExportFolder_GroupBox.TabStop = false;
            ExportFolder_GroupBox.Text = "出力フォルダオプション";
            // 
            // OtherFolder_Button
            // 
            OtherFolder_Button.Location = new Point(325, 126);
            OtherFolder_Button.Name = "OtherFolder_Button";
            OtherFolder_Button.Size = new Size(60, 23);
            OtherFolder_Button.TabIndex = 5;
            OtherFolder_Button.Text = "参照";
            OtherFolder_Button.UseVisualStyleBackColor = true;
            OtherFolder_Button.Click += OtherFolder_Button_Click;
            // 
            // OtherFolder_ListBox
            // 
            OtherFolder_ListBox.FormattingEnabled = true;
            OtherFolder_ListBox.HorizontalScrollbar = true;
            OtherFolder_ListBox.ItemHeight = 15;
            OtherFolder_ListBox.Location = new Point(6, 126);
            OtherFolder_ListBox.Name = "OtherFolder_ListBox";
            OtherFolder_ListBox.Size = new Size(313, 34);
            OtherFolder_ListBox.TabIndex = 4;
            // 
            // LowerFolder_ComboBox
            // 
            LowerFolder_ComboBox.FormattingEnabled = true;
            LowerFolder_ComboBox.Location = new Point(5, 72);
            LowerFolder_ComboBox.Name = "LowerFolder_ComboBox";
            LowerFolder_ComboBox.Size = new Size(314, 23);
            LowerFolder_ComboBox.TabIndex = 3;
            // 
            // OtherFolder_RadioButton
            // 
            OtherFolder_RadioButton.AutoSize = true;
            OtherFolder_RadioButton.ForeColor = Color.Gray;
            OtherFolder_RadioButton.Location = new Point(5, 101);
            OtherFolder_RadioButton.Name = "OtherFolder_RadioButton";
            OtherFolder_RadioButton.Size = new Size(84, 19);
            OtherFolder_RadioButton.TabIndex = 2;
            OtherFolder_RadioButton.TabStop = true;
            OtherFolder_RadioButton.Text = "選択フォルダ";
            OtherFolder_RadioButton.UseVisualStyleBackColor = true;
            OtherFolder_RadioButton.CheckedChanged += OtherFolder_RadioButton_CheckedChanged;
            // 
            // LowerFolder_RadioButton
            // 
            LowerFolder_RadioButton.AutoSize = true;
            LowerFolder_RadioButton.ForeColor = Color.Gray;
            LowerFolder_RadioButton.Location = new Point(6, 47);
            LowerFolder_RadioButton.Name = "LowerFolder_RadioButton";
            LowerFolder_RadioButton.Size = new Size(102, 19);
            LowerFolder_RadioButton.TabIndex = 1;
            LowerFolder_RadioButton.TabStop = true;
            LowerFolder_RadioButton.Text = "直下のフォルダー";
            LowerFolder_RadioButton.UseVisualStyleBackColor = true;
            LowerFolder_RadioButton.CheckedChanged += LowerFolder_RadioButton_CheckedChanged;
            // 
            // SameFolder_RadioButton
            // 
            SameFolder_RadioButton.AutoSize = true;
            SameFolder_RadioButton.ForeColor = Color.Gray;
            SameFolder_RadioButton.Location = new Point(6, 22);
            SameFolder_RadioButton.Name = "SameFolder_RadioButton";
            SameFolder_RadioButton.Size = new Size(101, 19);
            SameFolder_RadioButton.TabIndex = 0;
            SameFolder_RadioButton.TabStop = true;
            SameFolder_RadioButton.Text = "同じフォルダー内";
            SameFolder_RadioButton.UseVisualStyleBackColor = true;
            SameFolder_RadioButton.CheckedChanged += SameFolder_RadioButton_CheckedChanged;
            // 
            // FilePath_Button
            // 
            FilePath_Button.Location = new Point(276, 12);
            FilePath_Button.Name = "FilePath_Button";
            FilePath_Button.Size = new Size(60, 23);
            FilePath_Button.TabIndex = 6;
            FilePath_Button.Text = "参照";
            FilePath_Button.UseVisualStyleBackColor = true;
            FilePath_Button.Click += FilePath_Button_Click;
            // 
            // ZipOption_GroupBox
            // 
            ZipOption_GroupBox.Controls.Add(CreateZipFolder_CheckBox);
            ZipOption_GroupBox.Controls.Add(ZipOtherFolder_ListBox);
            ZipOption_GroupBox.Controls.Add(ZipOtherFolder_RadioButton);
            ZipOption_GroupBox.Controls.Add(ZipOtherFolder_Button);
            ZipOption_GroupBox.Controls.Add(ZipLowerFolder_ComboBox);
            ZipOption_GroupBox.Controls.Add(ZipLowerFolder_RadioButton);
            ZipOption_GroupBox.Controls.Add(ZipSameFolder_RadioButton);
            ZipOption_GroupBox.Enabled = false;
            ZipOption_GroupBox.Location = new Point(6, 47);
            ZipOption_GroupBox.Name = "ZipOption_GroupBox";
            ZipOption_GroupBox.Size = new Size(379, 191);
            ZipOption_GroupBox.TabIndex = 7;
            ZipOption_GroupBox.TabStop = false;
            ZipOption_GroupBox.Text = "Zipオプション";
            // 
            // CreateZipFolder_CheckBox
            // 
            CreateZipFolder_CheckBox.AutoSize = true;
            CreateZipFolder_CheckBox.Location = new Point(6, 166);
            CreateZipFolder_CheckBox.Name = "CreateZipFolder_CheckBox";
            CreateZipFolder_CheckBox.Size = new Size(141, 19);
            CreateZipFolder_CheckBox.TabIndex = 8;
            CreateZipFolder_CheckBox.Text = "直下にZipフォルダを作る";
            CreateZipFolder_CheckBox.UseVisualStyleBackColor = true;
            // 
            // ZipOtherFolder_ListBox
            // 
            ZipOtherFolder_ListBox.Enabled = false;
            ZipOtherFolder_ListBox.FormattingEnabled = true;
            ZipOtherFolder_ListBox.ItemHeight = 15;
            ZipOtherFolder_ListBox.Location = new Point(6, 126);
            ZipOtherFolder_ListBox.Name = "ZipOtherFolder_ListBox";
            ZipOtherFolder_ListBox.Size = new Size(301, 34);
            ZipOtherFolder_ListBox.TabIndex = 5;
            // 
            // ZipOtherFolder_RadioButton
            // 
            ZipOtherFolder_RadioButton.AutoSize = true;
            ZipOtherFolder_RadioButton.ForeColor = Color.Gray;
            ZipOtherFolder_RadioButton.Location = new Point(6, 101);
            ZipOtherFolder_RadioButton.Name = "ZipOtherFolder_RadioButton";
            ZipOtherFolder_RadioButton.Size = new Size(92, 19);
            ZipOtherFolder_RadioButton.TabIndex = 4;
            ZipOtherFolder_RadioButton.TabStop = true;
            ZipOtherFolder_RadioButton.Text = "選択フォルダー";
            ZipOtherFolder_RadioButton.UseVisualStyleBackColor = true;
            ZipOtherFolder_RadioButton.CheckedChanged += ZipOtherFolder_RadioButton_CheckedChanged;
            // 
            // ZipOtherFolder_Button
            // 
            ZipOtherFolder_Button.Enabled = false;
            ZipOtherFolder_Button.Location = new Point(313, 126);
            ZipOtherFolder_Button.Name = "ZipOtherFolder_Button";
            ZipOtherFolder_Button.Size = new Size(60, 23);
            ZipOtherFolder_Button.TabIndex = 3;
            ZipOtherFolder_Button.Text = "参照";
            ZipOtherFolder_Button.UseVisualStyleBackColor = true;
            ZipOtherFolder_Button.Click += ZipOtherFolder_Button_Click;
            // 
            // ZipLowerFolder_ComboBox
            // 
            ZipLowerFolder_ComboBox.Enabled = false;
            ZipLowerFolder_ComboBox.FormattingEnabled = true;
            ZipLowerFolder_ComboBox.Location = new Point(6, 72);
            ZipLowerFolder_ComboBox.Name = "ZipLowerFolder_ComboBox";
            ZipLowerFolder_ComboBox.Size = new Size(301, 23);
            ZipLowerFolder_ComboBox.TabIndex = 2;
            // 
            // ZipLowerFolder_RadioButton
            // 
            ZipLowerFolder_RadioButton.AutoSize = true;
            ZipLowerFolder_RadioButton.ForeColor = Color.Gray;
            ZipLowerFolder_RadioButton.Location = new Point(6, 47);
            ZipLowerFolder_RadioButton.Name = "ZipLowerFolder_RadioButton";
            ZipLowerFolder_RadioButton.Size = new Size(102, 19);
            ZipLowerFolder_RadioButton.TabIndex = 1;
            ZipLowerFolder_RadioButton.TabStop = true;
            ZipLowerFolder_RadioButton.Text = "直下のフォルダー";
            ZipLowerFolder_RadioButton.UseVisualStyleBackColor = true;
            ZipLowerFolder_RadioButton.CheckedChanged += ZipLowerFolder_RadioButton_CheckedChanged;
            // 
            // ZipSameFolder_RadioButton
            // 
            ZipSameFolder_RadioButton.AutoSize = true;
            ZipSameFolder_RadioButton.ForeColor = Color.Gray;
            ZipSameFolder_RadioButton.Location = new Point(6, 22);
            ZipSameFolder_RadioButton.Name = "ZipSameFolder_RadioButton";
            ZipSameFolder_RadioButton.Size = new Size(101, 19);
            ZipSameFolder_RadioButton.TabIndex = 0;
            ZipSameFolder_RadioButton.TabStop = true;
            ZipSameFolder_RadioButton.Text = "同じフォルダー内";
            ZipSameFolder_RadioButton.UseVisualStyleBackColor = true;
            ZipSameFolder_RadioButton.CheckedChanged += ZipSameFolder_RadioButton_CheckedChanged;
            // 
            // Zip_GroupBox
            // 
            Zip_GroupBox.Controls.Add(ZipOption_CheckBox);
            Zip_GroupBox.Controls.Add(ZipOption_GroupBox);
            Zip_GroupBox.Location = new Point(342, 292);
            Zip_GroupBox.Name = "Zip_GroupBox";
            Zip_GroupBox.Size = new Size(391, 247);
            Zip_GroupBox.TabIndex = 9;
            Zip_GroupBox.TabStop = false;
            Zip_GroupBox.Text = "Zip処理オプション";
            // 
            // ZipOption_CheckBox
            // 
            ZipOption_CheckBox.AutoSize = true;
            ZipOption_CheckBox.Location = new Point(6, 22);
            ZipOption_CheckBox.Name = "ZipOption_CheckBox";
            ZipOption_CheckBox.Size = new Size(201, 19);
            ZipOption_CheckBox.TabIndex = 0;
            ZipOption_CheckBox.Text = "ファイル名ごとにZipファイルを生成する";
            ZipOption_CheckBox.UseVisualStyleBackColor = true;
            ZipOption_CheckBox.CheckedChanged += ZipOption_CheckBox_CheckedChanged;
            // 
            // Other_GroupBox
            // 
            Other_GroupBox.Controls.Add(CreateThumbnail_CheckBox);
            Other_GroupBox.Location = new Point(342, 545);
            Other_GroupBox.Name = "Other_GroupBox";
            Other_GroupBox.Size = new Size(391, 48);
            Other_GroupBox.TabIndex = 10;
            Other_GroupBox.TabStop = false;
            Other_GroupBox.Text = "そのほか";
            // 
            // CreateThumbnail_CheckBox
            // 
            CreateThumbnail_CheckBox.AutoSize = true;
            CreateThumbnail_CheckBox.Location = new Point(6, 22);
            CreateThumbnail_CheckBox.Name = "CreateThumbnail_CheckBox";
            CreateThumbnail_CheckBox.Size = new Size(130, 19);
            CreateThumbnail_CheckBox.TabIndex = 0;
            CreateThumbnail_CheckBox.Text = "サムネ画像を取得する";
            CreateThumbnail_CheckBox.UseVisualStyleBackColor = true;
            // 
            // StartExport_Button
            // 
            StartExport_Button.Location = new Point(619, 624);
            StartExport_Button.Name = "StartExport_Button";
            StartExport_Button.Size = new Size(54, 27);
            StartExport_Button.TabIndex = 11;
            StartExport_Button.Text = "変換";
            StartExport_Button.UseVisualStyleBackColor = true;
            StartExport_Button.Click += StartExport_Button_Click;
            // 
            // Break_Button
            // 
            Break_Button.Location = new Point(678, 624);
            Break_Button.Name = "Break_Button";
            Break_Button.Size = new Size(54, 27);
            Break_Button.TabIndex = 12;
            Break_Button.Text = "中断";
            Break_Button.UseVisualStyleBackColor = true;
            Break_Button.Click += Break_Button_Click;
            // 
            // StartExport_ProgressBar
            // 
            StartExport_ProgressBar.Location = new Point(342, 628);
            StartExport_ProgressBar.Name = "StartExport_ProgressBar";
            StartExport_ProgressBar.Size = new Size(271, 23);
            StartExport_ProgressBar.TabIndex = 13;
            // 
            // FilePath_OpenFileDialog
            // 
            FilePath_OpenFileDialog.FileName = "openFileDialog1";
            FilePath_OpenFileDialog.Multiselect = true;
            // 
            // TaskLabel
            // 
            TaskLabel.AutoSize = true;
            TaskLabel.Font = new Font("Yu Gothic UI", 8F);
            TaskLabel.Location = new Point(347, 596);
            TaskLabel.Name = "TaskLabel";
            TaskLabel.Size = new Size(48, 13);
            TaskLabel.TabIndex = 14;
            TaskLabel.Text = "処理待ち";
            // 
            // CADExportTool
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(744, 663);
            Controls.Add(TaskLabel);
            Controls.Add(StartExport_ProgressBar);
            Controls.Add(Break_Button);
            Controls.Add(StartExport_Button);
            Controls.Add(Other_GroupBox);
            Controls.Add(Zip_GroupBox);
            Controls.Add(FilePath_Button);
            Controls.Add(ExportFolder_GroupBox);
            Controls.Add(PartFileOption_GroupBox);
            Controls.Add(DrawFileOption_GroupBox);
            Controls.Add(FilePath_ListView);
            Name = "CADExportTool";
            Text = "Form1";
            DrawFileOption_GroupBox.ResumeLayout(false);
            DrawFileOption_GroupBox.PerformLayout();
            PartFileOption_GroupBox.ResumeLayout(false);
            PartFileOption_GroupBox.PerformLayout();
            ExportFolder_GroupBox.ResumeLayout(false);
            ExportFolder_GroupBox.PerformLayout();
            ZipOption_GroupBox.ResumeLayout(false);
            ZipOption_GroupBox.PerformLayout();
            Zip_GroupBox.ResumeLayout(false);
            Zip_GroupBox.PerformLayout();
            Other_GroupBox.ResumeLayout(false);
            Other_GroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ColumnHeader FileName;
        private ColumnHeader FullPath;
        private GroupBox DrawFileOption_GroupBox;
        private GroupBox PartFileOption_GroupBox;
        private GroupBox ExportFolder_GroupBox;
        private Button FilePath_Button;
        private GroupBox Zip_GroupBox;
        private GroupBox Other_GroupBox;
        private OpenFileDialog FilePath_OpenFileDialog;
        private Label TaskLabel;
        public ListView FilePath_ListView;
        public CheckBox DXF_CheckBox;
        public CheckBox PDF_CheckBox;
        public CheckBox ThreeMF_CheckBox;
        public CheckBox STEP_CheckBox;
        public CheckBox IGS_CheckBox;
        public RadioButton OtherFolder_RadioButton;
        public RadioButton LowerFolder_RadioButton;
        public RadioButton SameFolder_RadioButton;
        public Button OtherFolder_Button;
        public ListBox OtherFolder_ListBox;
        public ComboBox LowerFolder_ComboBox;
        public GroupBox ZipOption_GroupBox;
        public RadioButton ZipLowerFolder_RadioButton;
        public RadioButton ZipSameFolder_RadioButton;
        public ListBox ZipOtherFolder_ListBox;
        public RadioButton ZipOtherFolder_RadioButton;
        public Button ZipOtherFolder_Button;
        public ComboBox ZipLowerFolder_ComboBox;
        public CheckBox ZipOption_CheckBox;
        public CheckBox CreateZipFolder_CheckBox;
        public CheckBox CreateThumbnail_CheckBox;
        public Button StartExport_Button;
        public Button Break_Button;
        public ProgressBar StartExport_ProgressBar;
    }
}
