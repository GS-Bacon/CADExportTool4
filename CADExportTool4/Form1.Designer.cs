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
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SameFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.LowerFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.LowerFolderComboBox = new System.Windows.Forms.ComboBox();
            this.OtherFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.OtherFolderButton = new System.Windows.Forms.Button();
            this.SameFolderLabel = new System.Windows.Forms.Label();
            this.SameFolderWarningLabel = new System.Windows.Forms.Label();
            this.OtherFolderListBox = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ExoportOptionGroupBox.SuspendLayout();
            this.PartExportOptionGroupBox.SuspendLayout();
            this.DrawExportOptionGroupBox.SuspendLayout();
            this.SelectFileGroupBox.SuspendLayout();
            this.StartExportGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.SelectFileListView.Size = new System.Drawing.Size(384, 609);
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
            this.ExoportOptionGroupBox.Controls.Add(this.groupBox3);
            this.ExoportOptionGroupBox.Controls.Add(this.groupBox2);
            this.ExoportOptionGroupBox.Controls.Add(this.PartExportOptionGroupBox);
            this.ExoportOptionGroupBox.Controls.Add(this.DrawExportOptionGroupBox);
            this.ExoportOptionGroupBox.Controls.Add(this.button1);
            this.ExoportOptionGroupBox.Enabled = false;
            this.ExoportOptionGroupBox.Location = new System.Drawing.Point(419, 12);
            this.ExoportOptionGroupBox.Name = "ExoportOptionGroupBox";
            this.ExoportOptionGroupBox.Size = new System.Drawing.Size(451, 575);
            this.ExoportOptionGroupBox.TabIndex = 2;
            this.ExoportOptionGroupBox.TabStop = false;
            this.ExoportOptionGroupBox.Text = "出力オプション";
            // 
            // PartExportOptionGroupBox
            // 
            this.PartExportOptionGroupBox.Controls.Add(this.STLCheckBox);
            this.PartExportOptionGroupBox.Controls.Add(this.IGSCheckBox);
            this.PartExportOptionGroupBox.Controls.Add(this.StepCheckBox);
            this.PartExportOptionGroupBox.Location = new System.Drawing.Point(7, 104);
            this.PartExportOptionGroupBox.Name = "PartExportOptionGroupBox";
            this.PartExportOptionGroupBox.Size = new System.Drawing.Size(438, 46);
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
            this.DrawExportOptionGroupBox.Size = new System.Drawing.Size(438, 46);
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
            this.button1.Location = new System.Drawing.Point(349, 18);
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
            this.SelectFileGroupBox.Size = new System.Drawing.Size(400, 667);
            this.SelectFileGroupBox.TabIndex = 3;
            this.SelectFileGroupBox.TabStop = false;
            this.SelectFileGroupBox.Text = "ファイル選択";
            // 
            // StartExportGroupBox
            // 
            this.StartExportGroupBox.Controls.Add(this.progressBar1);
            this.StartExportGroupBox.Controls.Add(this.label4);
            this.StartExportGroupBox.Controls.Add(this.button5);
            this.StartExportGroupBox.Controls.Add(this.button4);
            this.StartExportGroupBox.Location = new System.Drawing.Point(419, 587);
            this.StartExportGroupBox.Name = "StartExportGroupBox";
            this.StartExportGroupBox.Size = new System.Drawing.Size(451, 86);
            this.StartExportGroupBox.TabIndex = 4;
            this.StartExportGroupBox.TabStop = false;
            this.StartExportGroupBox.Text = "groupBox4";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 46);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(438, 23);
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
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(259, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 28);
            this.button5.TabIndex = 3;
            this.button5.Text = "変換開始";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(355, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 28);
            this.button4.TabIndex = 3;
            this.button4.Text = "キャンセル";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(7, 19);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(80, 16);
            this.checkBox6.TabIndex = 0;
            this.checkBox6.Text = "checkBox6";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(7, 41);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(80, 16);
            this.checkBox7.TabIndex = 0;
            this.checkBox7.Text = "checkBox6";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(41, 154);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 1;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(21, 64);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(88, 16);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "radioButton4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(21, 98);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(88, 16);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "radioButton4";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(21, 132);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(88, 16);
            this.radioButton7.TabIndex = 1;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "radioButton4";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "label1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(342, 202);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 28);
            this.button3.TabIndex = 3;
            this.button3.Text = "参照";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(21, 180);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(88, 16);
            this.radioButton6.TabIndex = 1;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "radioButton4";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(21, 202);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(315, 28);
            this.listBox2.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBox2);
            this.groupBox3.Controls.Add(this.radioButton6);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.radioButton7);
            this.groupBox3.Controls.Add(this.radioButton5);
            this.groupBox3.Controls.Add(this.radioButton4);
            this.groupBox3.Controls.Add(this.comboBox2);
            this.groupBox3.Controls.Add(this.checkBox7);
            this.groupBox3.Controls.Add(this.checkBox6);
            this.groupBox3.Location = new System.Drawing.Point(7, 325);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(438, 244);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // SameFolderRadioButton
            // 
            this.SameFolderRadioButton.AutoSize = true;
            this.SameFolderRadioButton.Location = new System.Drawing.Point(6, 19);
            this.SameFolderRadioButton.Name = "SameFolderRadioButton";
            this.SameFolderRadioButton.Size = new System.Drawing.Size(143, 16);
            this.SameFolderRadioButton.TabIndex = 0;
            this.SameFolderRadioButton.TabStop = true;
            this.SameFolderRadioButton.Text = "同じフォルダ内に保存する";
            this.SameFolderRadioButton.UseVisualStyleBackColor = true;
            this.SameFolderRadioButton.CheckedChanged += new System.EventHandler(this.SameFolderRadioButton_CheckedChanged);
            // 
            // LowerFolderRadioButton
            // 
            this.LowerFolderRadioButton.AutoSize = true;
            this.LowerFolderRadioButton.Location = new System.Drawing.Point(6, 53);
            this.LowerFolderRadioButton.Name = "LowerFolderRadioButton";
            this.LowerFolderRadioButton.Size = new System.Drawing.Size(183, 16);
            this.LowerFolderRadioButton.TabIndex = 0;
            this.LowerFolderRadioButton.TabStop = true;
            this.LowerFolderRadioButton.Text = "同じ場所にあるフォルダに保存する";
            this.LowerFolderRadioButton.UseVisualStyleBackColor = true;
            // 
            // LowerFolderComboBox
            // 
            this.LowerFolderComboBox.FormattingEnabled = true;
            this.LowerFolderComboBox.Location = new System.Drawing.Point(21, 75);
            this.LowerFolderComboBox.Name = "LowerFolderComboBox";
            this.LowerFolderComboBox.Size = new System.Drawing.Size(121, 20);
            this.LowerFolderComboBox.TabIndex = 1;
            // 
            // OtherFolderRadioButton
            // 
            this.OtherFolderRadioButton.AutoSize = true;
            this.OtherFolderRadioButton.Location = new System.Drawing.Point(6, 102);
            this.OtherFolderRadioButton.Name = "OtherFolderRadioButton";
            this.OtherFolderRadioButton.Size = new System.Drawing.Size(88, 16);
            this.OtherFolderRadioButton.TabIndex = 2;
            this.OtherFolderRadioButton.TabStop = true;
            this.OtherFolderRadioButton.Text = "radioButton1";
            this.OtherFolderRadioButton.UseVisualStyleBackColor = true;
            // 
            // OtherFolderButton
            // 
            this.OtherFolderButton.Location = new System.Drawing.Point(342, 125);
            this.OtherFolderButton.Name = "OtherFolderButton";
            this.OtherFolderButton.Size = new System.Drawing.Size(90, 28);
            this.OtherFolderButton.TabIndex = 3;
            this.OtherFolderButton.Text = "参照";
            this.OtherFolderButton.UseVisualStyleBackColor = true;
            // 
            // SameFolderLabel
            // 
            this.SameFolderLabel.AutoSize = true;
            this.SameFolderLabel.Location = new System.Drawing.Point(155, 21);
            this.SameFolderLabel.Name = "SameFolderLabel";
            this.SameFolderLabel.Size = new System.Drawing.Size(0, 12);
            this.SameFolderLabel.TabIndex = 4;
            // 
            // SameFolderWarningLabel
            // 
            this.SameFolderWarningLabel.AutoSize = true;
            this.SameFolderWarningLabel.Location = new System.Drawing.Point(19, 38);
            this.SameFolderWarningLabel.Name = "SameFolderWarningLabel";
            this.SameFolderWarningLabel.Size = new System.Drawing.Size(0, 12);
            this.SameFolderWarningLabel.TabIndex = 4;
            // 
            // OtherFolderListBox
            // 
            this.OtherFolderListBox.FormattingEnabled = true;
            this.OtherFolderListBox.ItemHeight = 12;
            this.OtherFolderListBox.Location = new System.Drawing.Point(21, 125);
            this.OtherFolderListBox.Name = "OtherFolderListBox";
            this.OtherFolderListBox.Size = new System.Drawing.Size(315, 28);
            this.OtherFolderListBox.TabIndex = 5;
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
            this.groupBox2.Size = new System.Drawing.Size(438, 161);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "出力フォルダ選択";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 691);
            this.Controls.Add(this.StartExportGroupBox);
            this.Controls.Add(this.ExoportOptionGroupBox);
            this.Controls.Add(this.SelectFileGroupBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ExoportOptionGroupBox.ResumeLayout(false);
            this.PartExportOptionGroupBox.ResumeLayout(false);
            this.PartExportOptionGroupBox.PerformLayout();
            this.DrawExportOptionGroupBox.ResumeLayout(false);
            this.DrawExportOptionGroupBox.PerformLayout();
            this.SelectFileGroupBox.ResumeLayout(false);
            this.StartExportGroupBox.ResumeLayout(false);
            this.StartExportGroupBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.ColumnHeader Extension;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox OtherFolderListBox;
        private System.Windows.Forms.Label SameFolderWarningLabel;
        private System.Windows.Forms.Label SameFolderLabel;
        private System.Windows.Forms.Button OtherFolderButton;
        private System.Windows.Forms.RadioButton OtherFolderRadioButton;
        private System.Windows.Forms.ComboBox LowerFolderComboBox;
        private System.Windows.Forms.RadioButton LowerFolderRadioButton;
        private System.Windows.Forms.RadioButton SameFolderRadioButton;
    }
}

