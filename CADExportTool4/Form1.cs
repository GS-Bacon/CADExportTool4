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
        private void SelectFileListVew_Changed()
        {
            if (this.SelectFileListView.Items.Count==0)
            {
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
                    DrawExportOptionGroupBox.Enabled=true;
                   
                }
                else DrawExportOptionGroupBox.Enabled = false;

                ListViewItem partlistViewItem = SelectFileListView.FindItemWithText(text: ".SLDPRT");
                if (partlistViewItem != null)
                {
                    PartExportOptionGroupBox.Enabled = true;
                }
                else PartExportOptionGroupBox.Enabled = false;
            }
        }
        #endregion
    }
}
