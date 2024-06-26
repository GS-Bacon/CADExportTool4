namespace CADExportTool4
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.SelectFileListView = new System.Windows.Forms.ListView();
            this.filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Extension = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OpenFileDialogButton = new System.Windows.Forms.Button();
            this.SelectResetButton = new System.Windows.Forms.Button();
            this.ExoportOptionGroupBox = new System.Windows.Forms.GroupBox();
            this.PostprocessingOptionGroupBox = new System.Windows.Forms.GroupBox();
            this.ZipOptionGroupBox = new System.Windows.Forms.GroupBox();
            this.CreateZipFolderCheckBox = new System.Windows.Forms.CheckBox();
            this.ZipSameFolederRadioButton = new System.Windows.Forms.RadioButton();
            this.ZipOtherFolderListBox = new System.Windows.Forms.ListBox();
            this.ZipLowerFolderComboBox = new System.Windows.Forms.ComboBox();
            this.ZipOtherFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.ZipOtherFolderButton = new System.Windows.Forms.Button();
            this.ZipLowerFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.ZipOptionCheckBox = new System.Windows.Forms.CheckBox();
            this.SeparateByExtensionCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OtherFolderListBox = new System.Windows.Forms.ListBox();
            this.SameFolderWarningLabel = new System.Windows.Forms.Label();
            this.SameFolderLabel = new System.Windows.Forms.Label();
            this.OtherFolderButton = new System.Windows.Forms.Button();
            this.OtherFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.LowerFolderComboBox = new System.Windows.Forms.ComboBox();
            this.LowerFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.SameFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.PartExportOptionGroupBox = new System.Windows.Forms.GroupBox();
            this.STLCheckBox = new System.Windows.Forms.CheckBox();
            this.IGSCheckBox = new System.Windows.Forms.CheckBox();
            this.StepCheckBox = new System.Windows.Forms.CheckBox();
            this.DrawExportOptionGroupBox = new System.Windows.Forms.GroupBox();
            this.DXFCheckBox = new System.Windows.Forms.CheckBox();
            this.PDFCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SelectFileGroupBox = new System.Windows.Forms.GroupBox();
            this.StartExportGroupBox = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.StartExportButton = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SelectFileOpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ExoportOptionGroupBox.SuspendLayout();
            this.PostprocessingOptionGroupBox.SuspendLayout();
            this.ZipOptionGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.PartExportOptionGroupBox.SuspendLayout();
            this.DrawExportOptionGroupBox.SuspendLayout();
            this.SelectFileGroupBox.SuspendLayout();
            this.StartExportGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectFileListView
            // 
            this.SelectFileListView.AllowDrop = true;
            this.SelectFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.filename,
            this.path,
            this.Extension});
            this.SelectFileListView.HideSelection = false;
            this.SelectFileListView.Location = new System.Drawing.Point(6, 52);
            this.SelectFileListView.Name = "SelectFileListView";
            this.SelectFileListView.Size = new System.Drawing.Size(384, 676);
            this.SelectFileListView.TabIndex = 0;
            this.SelectFileListView.UseCompatibleStateImageBehavior = false;
            this.SelectFileListView.View = System.Windows.Forms.View.Details;
            this.SelectFileListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.SelectFileListView_DragDrop);
            this.SelectFileListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.SelectFileListView_DragEnter);
            this.SelectFileListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectFileListView_KeyDown);
            // 
            // filename
            // 
            this.filename.Text = "ファイル名";
            // 
            // path
            // 
            this.path.Text = "パス";
            // 
            // Extension
            // 
            this.Extension.Text = "拡張子";
            // 
            // OpenFileDialogButton
            // 
            this.OpenFileDialogButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.OpenFileDialogButton.Location = new System.Drawing.Point(330, 18);
            this.OpenFileDialogButton.Name = "OpenFileDialogButton";
            this.OpenFileDialogButton.Size = new System.Drawing.Size(60, 28);
            this.OpenFileDialogButton.TabIndex = 1;
            this.OpenFileDialogButton.Text = "参照";
            this.OpenFileDialogButton.UseVisualStyleBackColor = true;
            this.OpenFileDialogButton.Click += new System.EventHandler(this.OpenFileDialogButton_Click);
            // 
            // SelectResetButton
            // 
            this.SelectResetButton.Location = new System.Drawing.Point(245, 18);
            this.SelectResetButton.Name = "SelectResetButton";
            this.SelectResetButton.Size = new System.Drawing.Size(79, 28);
            this.SelectResetButton.TabIndex = 1;
            this.SelectResetButton.Text = "選択リセット";
            this.SelectResetButton.UseVisualStyleBackColor = true;
            this.SelectResetButton.Click += new System.EventHandler(this.SelectResetButton_Click);
            // 
            // ExoportOptionGroupBox
            // 
            this.ExoportOptionGroupBox.Controls.Add(this.PostprocessingOptionGroupBox);
            this.ExoportOptionGroupBox.Controls.Add(this.groupBox2);
            this.ExoportOptionGroupBox.Controls.Add(this.PartExportOptionGroupBox);
            this.ExoportOptionGroupBox.Controls.Add(this.DrawExportOptionGroupBox);
            this.ExoportOptionGroupBox.Controls.Add(this.button1);
            this.ExoportOptionGroupBox.Enabled = false;
            this.ExoportOptionGroupBox.Location = new System.Drawing.Point(419, 12);
            this.ExoportOptionGroupBox.Name = "ExoportOptionGroupBox";
            this.ExoportOptionGroupBox.Size = new System.Drawing.Size(479, 653);
            this.ExoportOptionGroupBox.TabIndex = 2;
            this.ExoportOptionGroupBox.TabStop = false;
            this.ExoportOptionGroupBox.Text = "出力オプション";
            // 
            // PostprocessingOptionGroupBox
            // 
            this.PostprocessingOptionGroupBox.Controls.Add(this.ZipOptionGroupBox);
            this.PostprocessingOptionGroupBox.Controls.Add(this.ZipOptionCheckBox);
            this.PostprocessingOptionGroupBox.Controls.Add(this.SeparateByExtensionCheckBox);
            this.PostprocessingOptionGroupBox.Location = new System.Drawing.Point(7, 341);
            this.PostprocessingOptionGroupBox.Name = "PostprocessingOptionGroupBox";
            this.PostprocessingOptionGroupBox.Size = new System.Drawing.Size(466, 306);
            this.PostprocessingOptionGroupBox.TabIndex = 5;
            this.PostprocessingOptionGroupBox.TabStop = false;
            this.PostprocessingOptionGroupBox.Text = "後処理オプション";
            // 
            // ZipOptionGroupBox
            // 
            this.ZipOptionGroupBox.Controls.Add(this.CreateZipFolderCheckBox);
            this.ZipOptionGroupBox.Controls.Add(this.ZipSameFolederRadioButton);
            this.ZipOptionGroupBox.Controls.Add(this.ZipOtherFolderListBox);
            this.ZipOptionGroupBox.Controls.Add(this.ZipLowerFolderComboBox);
            this.ZipOptionGroupBox.Controls.Add(this.ZipOtherFolderRadioButton);
            this.ZipOptionGroupBox.Controls.Add(this.ZipOtherFolderButton);
            this.ZipOptionGroupBox.Controls.Add(this.ZipLowerFolderRadioButton);
            this.ZipOptionGroupBox.Enabled = false;
            this.ZipOptionGroupBox.Location = new System.Drawing.Point(7, 63);
            this.ZipOptionGroupBox.Name = "ZipOptionGroupBox";
            this.ZipOptionGroupBox.Size = new System.Drawing.Size(453, 237);
            this.ZipOptionGroupBox.TabIndex = 6;
            this.ZipOptionGroupBox.TabStop = false;
            this.ZipOptionGroupBox.Text = "Zipフォルダ保存オプション";
            // 
            // CreateZipFolderCheckBox
            // 
            this.CreateZipFolderCheckBox.AutoSize = true;
            this.CreateZipFolderCheckBox.ForeColor = System.Drawing.Color.Gray;
            this.CreateZipFolderCheckBox.Location = new System.Drawing.Point(6, 156);
            this.CreateZipFolderCheckBox.Name = "CreateZipFolderCheckBox";
            this.CreateZipFolderCheckBox.Size = new System.Drawing.Size(260, 16);
            this.CreateZipFolderCheckBox.TabIndex = 6;
            this.CreateZipFolderCheckBox.Text = "直下の\"Zip\"フォルダに保存する (ない場合は作る)";
            this.CreateZipFolderCheckBox.UseVisualStyleBackColor = true;
            this.CreateZipFolderCheckBox.CheckedChanged += new System.EventHandler(this.CreateZipFolderCheckBox_CheckedChanged);
            // 
            // ZipSameFolederRadioButton
            // 
            this.ZipSameFolederRadioButton.AutoSize = true;
            this.ZipSameFolederRadioButton.Checked = true;
            this.ZipSameFolederRadioButton.Location = new System.Drawing.Point(6, 18);
            this.ZipSameFolederRadioButton.Name = "ZipSameFolederRadioButton";
            this.ZipSameFolederRadioButton.Size = new System.Drawing.Size(187, 16);
            this.ZipSameFolederRadioButton.TabIndex = 1;
            this.ZipSameFolederRadioButton.TabStop = true;
            this.ZipSameFolederRadioButton.Text = "出力フォルダと同じ場所に生成する";
            this.ZipSameFolederRadioButton.UseVisualStyleBackColor = true;
            this.ZipSameFolederRadioButton.CheckedChanged += new System.EventHandler(this.SameFolederZipRadioButton_CheckedChanged);
            // 
            // ZipOtherFolderListBox
            // 
            this.ZipOtherFolderListBox.Enabled = false;
            this.ZipOtherFolderListBox.FormattingEnabled = true;
            this.ZipOtherFolderListBox.ItemHeight = 12;
            this.ZipOtherFolderListBox.Location = new System.Drawing.Point(26, 110);
            this.ZipOtherFolderListBox.Name = "ZipOtherFolderListBox";
            this.ZipOtherFolderListBox.Size = new System.Drawing.Size(356, 40);
            this.ZipOtherFolderListBox.TabIndex = 5;
            // 
            // ZipLowerFolderComboBox
            // 
            this.ZipLowerFolderComboBox.Enabled = false;
            this.ZipLowerFolderComboBox.FormattingEnabled = true;
            this.ZipLowerFolderComboBox.Location = new System.Drawing.Point(26, 62);
            this.ZipLowerFolderComboBox.Name = "ZipLowerFolderComboBox";
            this.ZipLowerFolderComboBox.Size = new System.Drawing.Size(356, 20);
            this.ZipLowerFolderComboBox.TabIndex = 1;
            // 
            // ZipOtherFolderRadioButton
            // 
            this.ZipOtherFolderRadioButton.AutoSize = true;
            this.ZipOtherFolderRadioButton.ForeColor = System.Drawing.Color.Gray;
            this.ZipOtherFolderRadioButton.Location = new System.Drawing.Point(6, 88);
            this.ZipOtherFolderRadioButton.Name = "ZipOtherFolderRadioButton";
            this.ZipOtherFolderRadioButton.Size = new System.Drawing.Size(132, 16);
            this.ZipOtherFolderRadioButton.TabIndex = 1;
            this.ZipOtherFolderRadioButton.Text = "他のフォルダに保存する";
            this.ZipOtherFolderRadioButton.UseVisualStyleBackColor = true;
            this.ZipOtherFolderRadioButton.CheckedChanged += new System.EventHandler(this.ZipOtherFolderRadioButton_CheckedChanged);
            // 
            // ZipOtherFolderButton
            // 
            this.ZipOtherFolderButton.Enabled = false;
            this.ZipOtherFolderButton.Location = new System.Drawing.Point(390, 110);
            this.ZipOtherFolderButton.Name = "ZipOtherFolderButton";
            this.ZipOtherFolderButton.Size = new System.Drawing.Size(57, 28);
            this.ZipOtherFolderButton.TabIndex = 3;
            this.ZipOtherFolderButton.Text = "参照";
            this.ZipOtherFolderButton.UseVisualStyleBackColor = true;
            this.ZipOtherFolderButton.Click += new System.EventHandler(this.ZipOtherFolderButton_Click);
            // 
            // ZipLowerFolderRadioButton
            // 
            this.ZipLowerFolderRadioButton.AutoSize = true;
            this.ZipLowerFolderRadioButton.ForeColor = System.Drawing.Color.Gray;
            this.ZipLowerFolderRadioButton.Location = new System.Drawing.Point(6, 40);
            this.ZipLowerFolderRadioButton.Name = "ZipLowerFolderRadioButton";
            this.ZipLowerFolderRadioButton.Size = new System.Drawing.Size(183, 16);
            this.ZipLowerFolderRadioButton.TabIndex = 1;
            this.ZipLowerFolderRadioButton.Text = "同じ場所にあるフォルダに保存する";
            this.ZipLowerFolderRadioButton.UseVisualStyleBackColor = true;
            this.ZipLowerFolderRadioButton.CheckedChanged += new System.EventHandler(this.ZipLowerFolderRadioButton_CheckedChanged);
            // 
            // ZipOptionCheckBox
            // 
            this.ZipOptionCheckBox.AutoSize = true;
            this.ZipOptionCheckBox.ForeColor = System.Drawing.Color.Gray;
            this.ZipOptionCheckBox.Location = new System.Drawing.Point(7, 41);
            this.ZipOptionCheckBox.Name = "ZipOptionCheckBox";
            this.ZipOptionCheckBox.Size = new System.Drawing.Size(177, 16);
            this.ZipOptionCheckBox.TabIndex = 0;
            this.ZipOptionCheckBox.Text = "部品ごとのZipファイルを生成する";
            this.ZipOptionCheckBox.UseVisualStyleBackColor = true;
            this.ZipOptionCheckBox.CheckedChanged += new System.EventHandler(this.ZipOptionCheckBox_CheckedChanged);
            // 
            // SeparateByExtensionCheckBox
            // 
            this.SeparateByExtensionCheckBox.AutoSize = true;
            this.SeparateByExtensionCheckBox.ForeColor = System.Drawing.Color.Gray;
            this.SeparateByExtensionCheckBox.Location = new System.Drawing.Point(7, 19);
            this.SeparateByExtensionCheckBox.Name = "SeparateByExtensionCheckBox";
            this.SeparateByExtensionCheckBox.Size = new System.Drawing.Size(171, 16);
            this.SeparateByExtensionCheckBox.TabIndex = 0;
            this.SeparateByExtensionCheckBox.Text = "拡張子ごとにフォルダーを分ける";
            this.SeparateByExtensionCheckBox.UseVisualStyleBackColor = true;
            this.SeparateByExtensionCheckBox.CheckedChanged += new System.EventHandler(this.SeparateByExtensionCheckBox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OtherFolderListBox);
            this.groupBox2.Controls.Add(this.SameFolderWarningLabel);
            this.groupBox2.Controls.Add(this.SameFolderLabel);
            this.groupBox2.Controls.Add(this.OtherFolderButton);
            this.groupBox2.Controls.Add(this.OtherFolderRadioButton);
            this.groupBox2.Controls.Add(this.LowerFolderComboBox);
            this.groupBox2.Controls.Add(this.LowerFolderRadioButton);
            this.groupBox2.Controls.Add(this.SameFolderRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(7, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(466, 178);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "出力フォルダ選択";
            // 
            // OtherFolderListBox
            // 
            this.OtherFolderListBox.FormattingEnabled = true;
            this.OtherFolderListBox.ItemHeight = 12;
            this.OtherFolderListBox.Location = new System.Drawing.Point(21, 125);
            this.OtherFolderListBox.Name = "OtherFolderListBox";
            this.OtherFolderListBox.Size = new System.Drawing.Size(368, 40);
            this.OtherFolderListBox.TabIndex = 5;
            // 
            // SameFolderWarningLabel
            // 
            this.SameFolderWarningLabel.AutoSize = true;
            this.SameFolderWarningLabel.Location = new System.Drawing.Point(149, 23);
            this.SameFolderWarningLabel.Name = "SameFolderWarningLabel";
            this.SameFolderWarningLabel.Size = new System.Drawing.Size(0, 12);
            this.SameFolderWarningLabel.TabIndex = 4;
            // 
            // SameFolderLabel
            // 
            this.SameFolderLabel.AutoSize = true;
            this.SameFolderLabel.Location = new System.Drawing.Point(19, 38);
            this.SameFolderLabel.Name = "SameFolderLabel";
            this.SameFolderLabel.Size = new System.Drawing.Size(0, 12);
            this.SameFolderLabel.TabIndex = 4;
            // 
            // OtherFolderButton
            // 
            this.OtherFolderButton.Enabled = false;
            this.OtherFolderButton.Location = new System.Drawing.Point(397, 125);
            this.OtherFolderButton.Name = "OtherFolderButton";
            this.OtherFolderButton.Size = new System.Drawing.Size(57, 28);
            this.OtherFolderButton.TabIndex = 3;
            this.OtherFolderButton.Text = "参照";
            this.OtherFolderButton.UseVisualStyleBackColor = true;
            this.OtherFolderButton.Click += new System.EventHandler(this.OtherFolderButton_Click);
            // 
            // OtherFolderRadioButton
            // 
            this.OtherFolderRadioButton.AutoSize = true;
            this.OtherFolderRadioButton.ForeColor = System.Drawing.Color.Gray;
            this.OtherFolderRadioButton.Location = new System.Drawing.Point(6, 102);
            this.OtherFolderRadioButton.Name = "OtherFolderRadioButton";
            this.OtherFolderRadioButton.Size = new System.Drawing.Size(132, 16);
            this.OtherFolderRadioButton.TabIndex = 2;
            this.OtherFolderRadioButton.Text = "他のフォルダに保存する";
            this.OtherFolderRadioButton.UseVisualStyleBackColor = true;
            this.OtherFolderRadioButton.CheckedChanged += new System.EventHandler(this.OtherFolderRadioButton_CheckedChanged);
            // 
            // LowerFolderComboBox
            // 
            this.LowerFolderComboBox.Enabled = false;
            this.LowerFolderComboBox.FormattingEnabled = true;
            this.LowerFolderComboBox.Location = new System.Drawing.Point(21, 75);
            this.LowerFolderComboBox.Name = "LowerFolderComboBox";
            this.LowerFolderComboBox.Size = new System.Drawing.Size(368, 20);
            this.LowerFolderComboBox.TabIndex = 1;
            // 
            // LowerFolderRadioButton
            // 
            this.LowerFolderRadioButton.AutoSize = true;
            this.LowerFolderRadioButton.ForeColor = System.Drawing.Color.Gray;
            this.LowerFolderRadioButton.Location = new System.Drawing.Point(6, 53);
            this.LowerFolderRadioButton.Name = "LowerFolderRadioButton";
            this.LowerFolderRadioButton.Size = new System.Drawing.Size(183, 16);
            this.LowerFolderRadioButton.TabIndex = 0;
            this.LowerFolderRadioButton.Text = "同じ場所にあるフォルダに保存する";
            this.LowerFolderRadioButton.UseVisualStyleBackColor = true;
            this.LowerFolderRadioButton.CheckedChanged += new System.EventHandler(this.LowerFolderRadioButton_CheckedChanged);
            // 
            // SameFolderRadioButton
            // 
            this.SameFolderRadioButton.AutoSize = true;
            this.SameFolderRadioButton.Checked = true;
            this.SameFolderRadioButton.Location = new System.Drawing.Point(6, 19);
            this.SameFolderRadioButton.Name = "SameFolderRadioButton";
            this.SameFolderRadioButton.Size = new System.Drawing.Size(143, 16);
            this.SameFolderRadioButton.TabIndex = 0;
            this.SameFolderRadioButton.TabStop = true;
            this.SameFolderRadioButton.Text = "同じフォルダ内に保存する";
            this.SameFolderRadioButton.UseVisualStyleBackColor = true;
            this.SameFolderRadioButton.CheckedChanged += new System.EventHandler(this.SameFolderRadioButton_CheckedChanged);
            // 
            // PartExportOptionGroupBox
            // 
            this.PartExportOptionGroupBox.Controls.Add(this.STLCheckBox);
            this.PartExportOptionGroupBox.Controls.Add(this.IGSCheckBox);
            this.PartExportOptionGroupBox.Controls.Add(this.StepCheckBox);
            this.PartExportOptionGroupBox.Location = new System.Drawing.Point(7, 104);
            this.PartExportOptionGroupBox.Name = "PartExportOptionGroupBox";
            this.PartExportOptionGroupBox.Size = new System.Drawing.Size(466, 46);
            this.PartExportOptionGroupBox.TabIndex = 3;
            this.PartExportOptionGroupBox.TabStop = false;
            this.PartExportOptionGroupBox.Text = "図面出力形式";
            // 
            // STLCheckBox
            // 
            this.STLCheckBox.AutoSize = true;
            this.STLCheckBox.Location = new System.Drawing.Point(107, 19);
            this.STLCheckBox.Name = "STLCheckBox";
            this.STLCheckBox.Size = new System.Drawing.Size(39, 16);
            this.STLCheckBox.TabIndex = 1;
            this.STLCheckBox.Text = ".stl";
            this.STLCheckBox.UseVisualStyleBackColor = true;
            // 
            // IGSCheckBox
            // 
            this.IGSCheckBox.AutoSize = true;
            this.IGSCheckBox.Location = new System.Drawing.Point(60, 19);
            this.IGSCheckBox.Name = "IGSCheckBox";
            this.IGSCheckBox.Size = new System.Drawing.Size(41, 16);
            this.IGSCheckBox.TabIndex = 0;
            this.IGSCheckBox.Text = ".igs";
            this.IGSCheckBox.UseVisualStyleBackColor = true;
            // 
            // StepCheckBox
            // 
            this.StepCheckBox.AutoSize = true;
            this.StepCheckBox.Location = new System.Drawing.Point(6, 19);
            this.StepCheckBox.Name = "StepCheckBox";
            this.StepCheckBox.Size = new System.Drawing.Size(48, 16);
            this.StepCheckBox.TabIndex = 0;
            this.StepCheckBox.Text = ".step";
            this.StepCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawExportOptionGroupBox
            // 
            this.DrawExportOptionGroupBox.Controls.Add(this.DXFCheckBox);
            this.DrawExportOptionGroupBox.Controls.Add(this.PDFCheckBox);
            this.DrawExportOptionGroupBox.Location = new System.Drawing.Point(7, 52);
            this.DrawExportOptionGroupBox.Name = "DrawExportOptionGroupBox";
            this.DrawExportOptionGroupBox.Size = new System.Drawing.Size(466, 46);
            this.DrawExportOptionGroupBox.TabIndex = 2;
            this.DrawExportOptionGroupBox.TabStop = false;
            this.DrawExportOptionGroupBox.Text = "図面出力形式";
            // 
            // DXFCheckBox
            // 
            this.DXFCheckBox.AutoSize = true;
            this.DXFCheckBox.Location = new System.Drawing.Point(54, 19);
            this.DXFCheckBox.Name = "DXFCheckBox";
            this.DXFCheckBox.Size = new System.Drawing.Size(42, 16);
            this.DXFCheckBox.TabIndex = 0;
            this.DXFCheckBox.Text = ".dxf";
            this.DXFCheckBox.UseVisualStyleBackColor = true;
            // 
            // PDFCheckBox
            // 
            this.PDFCheckBox.AutoSize = true;
            this.PDFCheckBox.Location = new System.Drawing.Point(6, 19);
            this.PDFCheckBox.Name = "PDFCheckBox";
            this.PDFCheckBox.Size = new System.Drawing.Size(42, 16);
            this.PDFCheckBox.TabIndex = 0;
            this.PDFCheckBox.Text = ".pdf";
            this.PDFCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(377, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "オプションリセット";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SelectFileGroupBox
            // 
            this.SelectFileGroupBox.Controls.Add(this.SelectResetButton);
            this.SelectFileGroupBox.Controls.Add(this.SelectFileListView);
            this.SelectFileGroupBox.Controls.Add(this.OpenFileDialogButton);
            this.SelectFileGroupBox.Location = new System.Drawing.Point(13, 12);
            this.SelectFileGroupBox.Name = "SelectFileGroupBox";
            this.SelectFileGroupBox.Size = new System.Drawing.Size(400, 740);
            this.SelectFileGroupBox.TabIndex = 3;
            this.SelectFileGroupBox.TabStop = false;
            this.SelectFileGroupBox.Text = "ファイル選択";
            // 
            // StartExportGroupBox
            // 
            this.StartExportGroupBox.Controls.Add(this.progressBar1);
            this.StartExportGroupBox.Controls.Add(this.label4);
            this.StartExportGroupBox.Controls.Add(this.StartExportButton);
            this.StartExportGroupBox.Controls.Add(this.button4);
            this.StartExportGroupBox.Enabled = false;
            this.StartExportGroupBox.Location = new System.Drawing.Point(419, 671);
            this.StartExportGroupBox.Name = "StartExportGroupBox";
            this.StartExportGroupBox.Size = new System.Drawing.Size(479, 81);
            this.StartExportGroupBox.TabIndex = 4;
            this.StartExportGroupBox.TabStop = false;
            this.StartExportGroupBox.Text = "groupBox4";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 46);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(466, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "label1";
            // 
            // StartExportButton
            // 
            this.StartExportButton.Location = new System.Drawing.Point(286, 12);
            this.StartExportButton.Name = "StartExportButton";
            this.StartExportButton.Size = new System.Drawing.Size(90, 28);
            this.StartExportButton.TabIndex = 3;
            this.StartExportButton.Text = "変換開始";
            this.StartExportButton.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(383, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 28);
            this.button4.TabIndex = 3;
            this.button4.Text = "キャンセル";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // SelectFileOpenFileDialog1
            // 
            this.SelectFileOpenFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 761);
            this.Controls.Add(this.StartExportGroupBox);
            this.Controls.Add(this.ExoportOptionGroupBox);
            this.Controls.Add(this.SelectFileGroupBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ExoportOptionGroupBox.ResumeLayout(false);
            this.PostprocessingOptionGroupBox.ResumeLayout(false);
            this.PostprocessingOptionGroupBox.PerformLayout();
            this.ZipOptionGroupBox.ResumeLayout(false);
            this.ZipOptionGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.PartExportOptionGroupBox.ResumeLayout(false);
            this.PartExportOptionGroupBox.PerformLayout();
            this.DrawExportOptionGroupBox.ResumeLayout(false);
            this.DrawExportOptionGroupBox.PerformLayout();
            this.SelectFileGroupBox.ResumeLayout(false);
            this.StartExportGroupBox.ResumeLayout(false);
            this.StartExportGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView SelectFileListView;
        private System.Windows.Forms.Button OpenFileDialogButton;
        private System.Windows.Forms.Button SelectResetButton;
        private System.Windows.Forms.ColumnHeader filename;
        private System.Windows.Forms.ColumnHeader path;
        private System.Windows.Forms.GroupBox ExoportOptionGroupBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox DrawExportOptionGroupBox;
        private System.Windows.Forms.GroupBox SelectFileGroupBox;
        private System.Windows.Forms.CheckBox DXFCheckBox;
        private System.Windows.Forms.CheckBox PDFCheckBox;
        private System.Windows.Forms.GroupBox PartExportOptionGroupBox;
        private System.Windows.Forms.CheckBox STLCheckBox;
        private System.Windows.Forms.CheckBox IGSCheckBox;
        private System.Windows.Forms.CheckBox StepCheckBox;
        private System.Windows.Forms.GroupBox StartExportGroupBox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button StartExportButton;
        private System.Windows.Forms.Button button4;
        public System.Windows.Forms.ColumnHeader Extension;
        private System.Windows.Forms.GroupBox PostprocessingOptionGroupBox;
        private System.Windows.Forms.ListBox ZipOtherFolderListBox;
        private System.Windows.Forms.RadioButton ZipOtherFolderRadioButton;
        private System.Windows.Forms.Button ZipOtherFolderButton;
        private System.Windows.Forms.RadioButton ZipLowerFolderRadioButton;
        private System.Windows.Forms.RadioButton ZipSameFolederRadioButton;
        private System.Windows.Forms.ComboBox ZipLowerFolderComboBox;
        private System.Windows.Forms.CheckBox ZipOptionCheckBox;
        private System.Windows.Forms.CheckBox SeparateByExtensionCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox OtherFolderListBox;
        private System.Windows.Forms.Label SameFolderWarningLabel;
        private System.Windows.Forms.Label SameFolderLabel;
        private System.Windows.Forms.Button OtherFolderButton;
        private System.Windows.Forms.RadioButton OtherFolderRadioButton;
        private System.Windows.Forms.ComboBox LowerFolderComboBox;
        private System.Windows.Forms.RadioButton LowerFolderRadioButton;
        private System.Windows.Forms.RadioButton SameFolderRadioButton;
        private System.Windows.Forms.GroupBox ZipOptionGroupBox;
        private System.Windows.Forms.OpenFileDialog SelectFileOpenFileDialog1;
        private System.Windows.Forms.CheckBox CreateZipFolderCheckBox;
    }
}

