using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CADExportTool4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region selectfile //元ファイル選択イベント

        private void SelectFileListView_DragDrop(object sender, DragEventArgs e)
        {
            //ドロップしたアイテムを追加
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            SelectFileListView_AddItem(files);
        }

        private void SelectFileListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void SelectResetButton_Click(object sender, EventArgs e)
        {
            SelectFileListView.Items.Clear();
            SelectFileListVew_Changed();
        }


        private void OpenFileDialogButton_Click(object sender, EventArgs e)
        {
            SelectFileOpenFileDialog1.Multiselect = true;
            SelectFileOpenFileDialog1.Filter = "CADデータファイル|*.SLDDRW;*.SLDPRT|すべてのファイル|*.*";
            if (SelectFileOpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectFileListView_AddItem(SelectFileOpenFileDialog1.FileNames);
            }

        }
        /// <summary>
        /// SelectFileListViewにアイテムを追加する
        /// </summary>
        /// <param name="files">追加するフルパスのリスト</param>
        private void SelectFileListView_AddItem(string[] files)
        {
            foreach (string filename in files)
            {
                //フォルダは弾く
                bool Directoryflag = System.IO.File.GetAttributes(filename).HasFlag(FileAttributes.Directory);
                if (!Directoryflag)
                {
                    //ExportOptionで指定した拡張子以外のファイルは弾く
                    string FileExtesion = Path.GetExtension(filename);
                    if (CADExportTool4.ExportOption.FileExtensions.Contains(FileExtesion))
                    {
                        string[] item = { Path.GetFileName(filename), filename, FileExtesion };
                        var Listitem = new ListViewItem(item);
                        //拡張子によって文字の色を分ける
                        switch (FileExtesion)
                        {
                            case ".SLDDRW":
                                Listitem.ForeColor = Color.OrangeRed;
                                break;
                            case ".SLDPRT":
                                Listitem.ForeColor = Color.DarkBlue;
                                break;
                        }
                        //重複は弾く
                        ListViewItem listViewItem = SelectFileListView.FindItemWithText(text: filename);
                        if (listViewItem == null)
                        {
                            this.SelectFileListView.Items.Add(Listitem);
                        }
                    }
                }
            }
            //列幅を調整
            foreach (ColumnHeader ch in SelectFileListView.Columns)
            {
                ch.Width = -2;
            }
            SelectFileListVew_Changed();
        }

        private void SelectFileListView_KeyDown(object sender, KeyEventArgs e)
        {
            //Deleteキー押したら削除
            if (e.KeyData == Keys.Delete)
            {
                if (this.SelectFileListView.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in this.SelectFileListView.SelectedItems)
                    {
                        int index = item.Index;
                        this.SelectFileListView.Items.RemoveAt(index);
                    }
                }
            }
            SelectFileListVew_Changed();
        }
        /// <summary>
        /// SelectFileListViewが変更されたときに実行するイベント
        /// イベント化出来たらやりたい
        /// </summary>
        private void SelectFileListVew_Changed()
        {
            if (this.SelectFileListView.Items.Count == 0)
            {
                //リストが空の時はオプション選択を無効に
                this.StartExportGroupBox.Enabled = false;
                this.ExoportOptionGroupBox.Enabled = false;
            }
            else
            {
                this.StartExportGroupBox.Enabled = true;
                this.ExoportOptionGroupBox.Enabled = true;
                ListViewItem drawlistViewItem = SelectFileListView.FindItemWithText(text: ".SLDDRW");
                if (drawlistViewItem != null)
                {
                    //.SLDDRWがある時は図面出力オプションを有効に
                    DrawExportOptionGroupBox.Enabled = true;

                }
                else DrawExportOptionGroupBox.Enabled = false;

                ListViewItem partlistViewItem = SelectFileListView.FindItemWithText(text: ".SLDPRT");
                if (partlistViewItem != null)
                {
                    //.SLDDRWがある時は図面出力オプションを有効に
                    PartExportOptionGroupBox.Enabled = true;
                }
                else PartExportOptionGroupBox.Enabled = false;

                if (this.SameFolderRadioButton.Checked)
                {
                    SetSameFolderLabelText();
                }
            }
        }
        /// <summary>
        /// フォルダ選択のオプション用ラベルを設定する
        /// </summary>
        private void SetSameFolderLabelText()
        {
            HashSet<string> FolderNameList = new HashSet<string>();
            foreach (ListViewItem filename in this.SelectFileListView.Items)
            {
                FolderNameList.Add(Path.GetDirectoryName(filename.SubItems[1].Text));
            }
            if (FolderNameList.ToList().Count() == 1)
            {
                this.SameFolderWarningLabel.ResetText();
                if (FolderNameList.ToList()[0].Length >= 30)
                {
                    this.SameFolderLabel.Text = "..\\" + Path.GetFileName(Path.GetDirectoryName(FolderNameList.ToList()[0]));
                }
                else this.SameFolderLabel.Text = FolderNameList.ToList()[0];
            }
            else
            {
                if (FolderNameList.ToList()[0].Length >= 30)
                {
                    this.SameFolderLabel.Text = "..\\" + Path.GetFileName(Path.GetDirectoryName(FolderNameList.ToList()[0]));
                }
                else this.SameFolderLabel.Text = FolderNameList.ToList()[0];
                this.SameFolderWarningLabel.ForeColor = Color.Red;
                this.SameFolderWarningLabel.Text = "複数フォルダーから選択されています";
            }
        }
        #endregion

        #region 保存フォルダ選択オプション
        private void SameFolderRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SameFolderRadioButton.Checked == true)
            {
                this.SameFolderRadioButton.ForeColor = Color.Black;
                SetSameFolderLabelText();
            }
            else
            {
                this.SameFolderRadioButton.ForeColor = Color.Gray;
                this.SameFolderWarningLabel.ResetText();
                this.SameFolderLabel.ResetText();
            }
        }

        private void LowerFolderRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            LowerFolderWaringLabel.ResetText();
            if (this.LowerFolderRadioButton.Checked == true)
            {
                this.LowerFolderRadioButton.ForeColor = Color.Black;
                this.LowerFolderComboBox.Items.Clear();
                this.LowerFolderComboBox.Enabled = true;
                //重複ないリストを作成
                HashSet<string> FolderNameList = new HashSet<string>();
                foreach (ListViewItem filename in this.SelectFileListView.Items)
                {
                    //ディレクトリを追加
                    FolderNameList.Add(Path.GetDirectoryName(filename.SubItems[1].Text));
                }

                foreach (string Names in FolderNameList)
                {
                    //直下にあるフォルダを追加
                    string[] dirs = Directory.GetDirectories(Names);
                    foreach (string Names2 in dirs)
                    {
                        this.LowerFolderComboBox.Items.Add(Path.GetFileName(Names2));
                    }
                }
            }
            else
            {
                this.LowerFolderRadioButton.ForeColor = Color.Gray;
                this.LowerFolderComboBox.Enabled = false;
            }
        }

        private void OtherFolderRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.OtherFolderRadioButton.Checked == true)
            {
                this.OtherFolderRadioButton.ForeColor = Color.Black;
                this.OtherFolderListBox.Enabled = true;
                this.OtherFolderButton.Enabled = true;
            }
            else
            {
                this.OtherFolderRadioButton.ForeColor = Color.Gray;
                this.OtherFolderListBox.Enabled = false;
                this.OtherFolderButton.Enabled = false;
            }
        }

        private void OtherFolderButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog commonFileDialog = new CommonOpenFileDialog();
            commonFileDialog.IsFolderPicker = true;
            commonFileDialog.InitialDirectory = Path.GetDirectoryName(SelectFileListView.Items[0].SubItems[1].Text);
            if (commonFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.OtherFolderListBox.Items.Clear();
                OtherFolderListBox.Items.Add(commonFileDialog.FileName);
            }
        }
        #endregion

        #region 後処理オプション
        private void SeparateByExtensionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SeparateByExtensionCheckBox.Checked == true)
            {
                this.SeparateByExtensionCheckBox.ForeColor = Color.Black;
            }
            else
            {
                this.SeparateByExtensionCheckBox.ForeColor = Color.Gray;
            }
        }

        private void ZipOptionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ZipOptionCheckBox.Checked == true)
            {
                this.ZipOptionGroupBox.Enabled = true;
                this.ZipOptionCheckBox.ForeColor = Color.Black;
            }
            else
            {
                this.ZipOptionGroupBox.Enabled = false;
                this.ZipOptionCheckBox.ForeColor = Color.Gray;
            }
        }
        private void SameFolederZipRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ZipSameFolederRadioButton.Checked == true)
            {
                this.ZipSameFolederRadioButton.ForeColor = Color.Black;
            }
            else
            {
                this.ZipSameFolederRadioButton.ForeColor = Color.Gray;
            }
        }

        private void ZipLowerFolderRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ZipLowerFolderRadioButton.Checked == true)
            {
                this.ZipLowerFolderRadioButton.ForeColor = Color.Black;
                this.ZipLowerFolderComboBox.Enabled = true;
                this.ZipLowerFolderComboBox.Items.Clear();
                HashSet<string> FolderNameList = new HashSet<string>();
                foreach (ListViewItem filename in this.SelectFileListView.Items)
                {
                    //ディレクトリを追加
                    FolderNameList.Add(Path.GetDirectoryName(filename.SubItems[1].Text));
                }

                foreach (string Names in FolderNameList)
                {
                    //直下にあるフォルダを追加
                    string[] dirs = Directory.GetDirectories(Names);
                    foreach (string Names2 in dirs)
                    {
                        this.ZipLowerFolderComboBox.Items.Add(Path.GetFileName(Names2));
                    }
                }

            }
            else
            {
                this.ZipLowerFolderRadioButton.ForeColor = Color.Gray;
                this.ZipLowerFolderComboBox.Enabled = false;
            }
        }

        private void ZipOtherFolderRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ZipOtherFolderRadioButton.Checked == true)
            {
                this.ZipOtherFolderRadioButton.ForeColor = Color.Black;
                this.ZipOtherFolderListBox.Enabled = true;
                this.ZipOtherFolderButton.Enabled = true;
            }
            else
            {
                this.ZipOtherFolderRadioButton.ForeColor = Color.Gray;
                this.ZipOtherFolderListBox.Enabled = false;
                this.ZipOtherFolderButton.Enabled = false;
            }
        }

        private void ZipOtherFolderButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog commonFileDialog = new CommonOpenFileDialog();
            commonFileDialog.IsFolderPicker = true;
            commonFileDialog.InitialDirectory = Path.GetDirectoryName(SelectFileListView.Items[0].SubItems[1].Text);
            if (commonFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ZipOtherFolderListBox.Items.Clear();
                ZipOtherFolderListBox.Items.Add(commonFileDialog.FileName);
            }
        }

        private void CreateZipFolderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CreateZipFolderCheckBox.Checked == true)
            {
                this.CreateZipFolderCheckBox.ForeColor = Color.Black;
            }
            else
            {
                this.CreateZipFolderCheckBox.ForeColor = Color.Gray;
            }
        }
        #endregion

        public delegate void LabelText(string text);
        public void SetLabel(String label)
        {
            this.TaskNameLabel.Text = label;
        }
        public delegate void TaskCount(int counter);
        public void PlusCounter(int counter)
        {
            this.TaskProgressBar.Value = counter;
        }
        public delegate void OnOff(bool onoff);
        public void OnOffSwitch(bool onoff)
        {
            this.SelectFileGroupBox.Enabled = onoff;
            this.ExoportOptionGroupBox.Enabled = onoff;
        }
        Thread thread1 = null;
        private void StartExportButton_Click(object sender, EventArgs e)
        {
            ExportOption options = GetExportOpostion();

            if (options == null) return;
            else
            {
                int taskcounter = 0;
                foreach (var file in options.Fileoptions)
                {
                    taskcounter += file.exportpath.Count;
                    if (file.zippath != "")
                    {
                        taskcounter++;
                    }
                }
                TaskProgressBar.Maximum = taskcounter + 1;
                TaskProgressBar.Minimum = 0;
                TaskProgressBar.Value = 0;
                
                TaskProgressBar.Value = 1;
                TaskNameLabel.Text = $"{TaskProgressBar.Value}/{TaskProgressBar.Maximum} 開始処理中";
                SelectFileGroupBox.Enabled = false;
                ExoportOptionGroupBox.Enabled = false;
                ExportCADFile ec = new ExportCADFile(options, this);
                thread1= new Thread(new ThreadStart(ec.Export));
                thread1.Start();

            }
        }

        /// <summary>
        /// オプションを転記してExportOptionとして返す
        /// </summary>
        /// <returns></returns>
        private ExportOption GetExportOpostion()
        {
            #region 拡張子選択
            ///
            ExportOption options = new ExportOption();
            options.exoption["pdf"].check = this.PDFCheckBox.Checked;
            options.exoption["dxf"].check = this.DXFCheckBox.Checked;
            options.exoption["igs"].check = this.IGSCheckBox.Checked;
            options.exoption["step"].check = this.STEPCheckBox.Checked;
            options.exoption["stl"].check = this.STLCheckBox.Checked;

            bool extenSionFlag = false;
            foreach (ExtensionVariants extension in options.exoption.Values)
            {
                if (extension.check == true)
                {
                    extenSionFlag = true;
                    break;
                }
            }
            if (!extenSionFlag)
            {
                MessageBox.Show("出力する拡張子を選択してください");
                return null;
            }
            #endregion

            #region 出力フォルダを指定する
            string ExportFolderPath = Path.GetDirectoryName(this.SelectFileListView.Items[0].SubItems[1].Text); ;
            //同じフォルダの場合
            if (this.SameFolderRadioButton.Checked)
            {
                //一番上のファイルのフォルダを指定する
                ExportFolderPath = Path.GetDirectoryName(this.SelectFileListView.Items[0].SubItems[1].Text);
            }
            //選択した直下フォルダを指定する場合
            else if (this.LowerFolderRadioButton.Checked)
            {
                //未選択の場合は警告を出す
                if (LowerFolderComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("フォルダを選択してください");
                    LowerFolderWaringLabel.Text = "フォルダを選択してください";
                    return null;
                }
                else
                {
                    ExportFolderPath = Path.GetDirectoryName(this.SelectFileListView.Items[0].SubItems[1].Text) + "\\" + LowerFolderComboBox.Items[LowerFolderComboBox.SelectedIndex].ToString();
                }
            }
            //他のフォルダを指定する場合
            else if (this.OtherFolderRadioButton.Checked)
            {
                //未選択の場合は警告を出す
                if (this.OtherFolderListBox.Items.Count == 0)
                {
                    MessageBox.Show("フォルダを選択してください");
                    OtherFolderWarningLabel.Text = "フォルダを選択してください";
                    return null;
                }
                else
                {
                    ExportFolderPath = OtherFolderListBox.Items[0].ToString();
                }
            }
            #endregion

            //拡張子ごとにフォルダを作る
            foreach (ExtensionVariants ce in options.exoption.Values)
            {
                if (this.SeparateByExtensionCheckBox.Checked)
                {
                    bool folderflag = false;
                    foreach (string folderpath in ce.extensions)
                    {
                        //フォルダが存在していたらフォルダはつくらない
                        if (System.IO.Directory.Exists(ExportFolderPath + "\\" + folderpath))
                        {
                            folderflag = true;
                            ce.folderpath = ExportFolderPath + "\\" + folderpath;
                            break;
                        }
                    }
                    if (!folderflag)
                    {
                        if (ce.check)
                        {
                            string folderPath = ExportFolderPath + "\\" + ce.extensions[0];
                            Directory.CreateDirectory(folderPath);
                            ce.folderpath = folderPath;
                        }
                    }
                }
                else
                {
                    ce.folderpath = ExportFolderPath;
                }
            }
            #region Zip出力オプション
            bool CreateZip = this.ZipOptionCheckBox.Checked;
            bool CreateZipFolder = this.CreateZipFolderCheckBox.Checked;
            string ZipFolderPath = "";
            if (CreateZip)
            {
                string folder = "";
                if (this.ZipSameFolederRadioButton.Checked)
                {
                    folder = ExportFolderPath;
                }
                else if (this.ZipLowerFolderRadioButton.Checked)
                {
                    if (ZipLowerFolderComboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("Zipファイルを保存するフォルダを選択してください");
                        return null;
                    }
                    else
                    {
                        folder = Path.GetDirectoryName(this.SelectFileListView.Items[0].SubItems[1].Text) + "\\" + ZipLowerFolderComboBox.Items[ZipLowerFolderComboBox.SelectedIndex].ToString();
                    }
                }
                else if (this.ZipOtherFolderRadioButton.Checked)
                {
                    if (this.ZipOtherFolderListBox.Items.Count == 0)
                    {
                        MessageBox.Show("Zipファイルを保存するフォルダを選択してください");
                        return null;
                    }
                    else
                    {
                        folder = ZipOtherFolderListBox.Items[0].ToString();
                    }
                }
                if (this.CreateZipFolderCheckBox.Checked)
                {
                    folder = folder + "\\zip";
                    Directory.CreateDirectory(folder);
                }
                ZipFolderPath = folder;
                options.ZipFolderPath = ZipFolderPath;

            }

            #endregion

            //すべてのフォルダパスを記述する
            foreach (ListViewItem filepath in this.SelectFileListView.Items)
            {
                Fileoptions fileoptions = new Fileoptions();
                string ext = Path.GetExtension(filepath.SubItems[1].Text);
                //追加するかどうか
                bool addFlag = false;
                //名前を取得
                fileoptions.filename = Path.GetFileNameWithoutExtension(filepath.SubItems[1].Text);
                fileoptions.itempath = filepath.SubItems[1].Text;
                foreach (ExtensionVariants v in options.exoption.Values)
                {
                    //元ファイルの拡張子が一致して出力にチェックが入っている場合
                    if (v.parent_ex.Contains(ext) && v.check)
                    {
                        fileoptions.exportpath.Add(v.folderpath + "\\" + Path.ChangeExtension(Path.GetFileName(filepath.SubItems[1].Text), "." + v.name));
                        addFlag = true;
                    }
                }
                if (this.ZipOptionCheckBox.Checked)
                {
                    {
                        fileoptions.zippath = ZipFolderPath + "\\" + fileoptions.filename + ".zip";
                    }

                }
                if (addFlag)
                {
                    options.Fileoptions.Add(fileoptions);
                }
            }
            foreach (Fileoptions filepath in options.Fileoptions)
            {
                options.ZipFilePathList.Add(filepath.zippath);
            }

            Debug.WriteLine("ExportPath:" + ExportFolderPath.ToString());
            Debug.WriteLine("ZipFolderPath");
            return options;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PDFCheckBox.Checked = false;
            DXFCheckBox.Checked = false;
            IGSCheckBox.Checked = false;
            STEPCheckBox.Checked = false;
            STLCheckBox.Checked = false;
            SameFolderLabel.Text = string.Empty;
            LowerFolderComboBox.Items.Clear();
            ZipLowerFolderComboBox.Items.Clear();
            ZipOtherFolderListBox.Items.Clear();
        }

        private void TaskNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("変換処理を中断しますか？", "中断ダイアログ", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                thread1.Interrupt();
                thread1.Join();
                TaskNameLabel.Text = "処理を中断しました";
                TaskProgressBar.Value = 0;
                TaskProgressBar.Update();
                SelectFileGroupBox.Enabled = true;
                ExoportOptionGroupBox.Enabled = true;
                TaskProgressBar.Value = 0;
            }
        }
    }
}
