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
                    if (CADExportTool4.ExportOption.ExportExtensions.Contains(FileExtesion))
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
            if(this.ZipOtherFolderRadioButton.Checked == true)
            {
                this.ZipOtherFolderRadioButton.ForeColor= Color.Black;
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
            if(CreateZipFolderCheckBox.Checked == true)
            {
                this.CreateZipFolderCheckBox.ForeColor= Color.Black;
            }
            else
            {
                this.CreateZipFolderCheckBox.ForeColor = Color.Gray;
            }
        }
    }
}
