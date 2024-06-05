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
        #region selectfile
        private void SelectFileListView_DragDrop(object sender, DragEventArgs e)
        {
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
        private void SelectFileListView_AddItem(string[] files)
        {
            foreach (string filename in files)
            {
                bool Directoryflag = System.IO.File.GetAttributes(filename).HasFlag(FileAttributes.Directory);
                if (!Directoryflag)
                {
                    string FileExtesion = Path.GetExtension(filename);
                    if (CADExportTool4.ExportOption.ExportExtensions.Contains(FileExtesion))
                    {
                        string[] item = { Path.GetFileName(filename), filename };
                        var Listitem = new ListViewItem(item);
                        switch (FileExtesion)
                        {
                            case ".SLDDRW":
                                Listitem.ForeColor = Color.OrangeRed;
                                break;
                            case ".SLDPRT":
                                Listitem.ForeColor = Color.DarkBlue;
                                break;
                        }
                        ListViewItem listViewItem = SelectFileListView.FindItemWithText(text:filename);
                        if (listViewItem == null)
                        {
                            this.SelectFileListView.Items.Add(Listitem);
                        }
                    }
                }
            }
            foreach (ColumnHeader ch in SelectFileListView.Columns)
            {
                ch.Width = -2;
            }
        }
        #endregion
    }
}
