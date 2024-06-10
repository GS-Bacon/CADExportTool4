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
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "CADデータファイル|*.SLDDRW;*.SLDPRT|すべてのファイル|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectFileListView_AddItem(openFileDialog1.FileNames);
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
                        string[] item = { Path.GetFileName(filename), filename ,FileExtesion};
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
            if (this.SelectFileListView.Items.Count==0)
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
                    DrawExportOptionGroupBox.Enabled=true;
                   
                }
                else DrawExportOptionGroupBox.Enabled = false;

                ListViewItem partlistViewItem = SelectFileListView.FindItemWithText(text: ".SLDPRT");
                if (partlistViewItem != null)
                {
                    //.SLDDRWがある時は図面出力オプションを有効に
                    PartExportOptionGroupBox.Enabled = true;
                }
                else PartExportOptionGroupBox.Enabled = false;

                if(this.SameFolderRadioButton.Checked)
                {
                    HashSet<string> FolderNameList = new HashSet<string>();
                    foreach (ListViewItem filename in this.SelectFileListView.Items)
                    {
                        FolderNameList.Add(Path.GetDirectoryName(filename.SubItems[1].Text));
                    }
                    if (FolderNameList.ToList().Count() == 1)
                    {
                        this.SameFolderWarningLabel.ResetText();
                        this.SameFolderLabel.Text = FolderNameList.ToList()[0];
                    }
                    else
                    {
                        this.SameFolderLabel.Text = FolderNameList.ToList()[0];
                        this.SameFolderWarningLabel.ForeColor = Color.Red;
                        this.SameFolderWarningLabel.Text = "複数フォルダーから選択されています";
                    }
                }
            }
        }
        #endregion

        private void SameFolderRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SameFolderRadioButton.Checked == true)
            {
                this.SameFolderRadioButton.ForeColor = Color.Black;
                HashSet<string> FolderNameList = new HashSet<string>();
                foreach (ListViewItem filename in this.SelectFileListView.Items)
                {
                    FolderNameList.Add(Path.GetDirectoryName(filename.SubItems[1].Text));
                }
                if (FolderNameList.ToList().Count() == 1)
                {
                    this.SameFolderLabel.Text = FolderNameList.ToList()[0];
                }
                else
                {
                    this.SameFolderLabel.Text = FolderNameList.ToList()[0];
                    this.SameFolderWarningLabel.ForeColor= Color.Red;
                    this.SameFolderWarningLabel.Text = "複数フォルダーから選択されています";
                }
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
                this.LowerFolderRadioButton.ForeColor= Color.Black;
                this.LowerFolderComboBox.Items.Clear();
                this.LowerFolderComboBox.Enabled = true;
                HashSet<string> FolderNameList= new HashSet<string>();
                foreach (ListViewItem filename in this.SelectFileListView.Items)
                {
                    FolderNameList.Add(Path.GetDirectoryName(filename.SubItems[1].Text));
                }
                foreach (string Names in FolderNameList)
                {
                    string[]dirs=Directory.GetDirectories(Names);
                    this.LowerFolderComboBox.Items.AddRange(dirs);
                }
            }
            else
            {
                this.LowerFolderRadioButton.ForeColor=Color.Gray;
                this.LowerFolderComboBox.Enabled = false;
            }
        }
    }
}
