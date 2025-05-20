using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using SolidworksAPIAPI;
namespace SMZ_CADExportTool
{
    public partial class CADExportTool : Form
    {
        public CADExportTool()
        {
            InitializeComponent();
            DrawFileOption_GroupBox.Enabled = false;
            PartFileOption_GroupBox.Enabled = false;
            ExportFolder_GroupBox.Enabled = false;
            Zip_GroupBox.Enabled = false;
            Other_GroupBox.Enabled = false;
            StartExport_Button.Enabled = false;
        }
        #region FilePath ListView
        private void FilePath_ListView_DragEnter(object sender, DragEventArgs e)
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

        private void FilePath_ListView_DragDrop(object sender, DragEventArgs e)
        {
            //�h���b�v�����A�C�e����ǉ�
            FilePathListView_AddItems((string[])e.Data.GetData(DataFormats.FileDrop, false));
        }
        /// <summary>
        /// �T���l���擾����
        /// </summary>
        /// <param name="path"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private Bitmap CreateThumbnail(string path, int scale)
        {
            // �t�@�C�������݂����ꍇ
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                ShellFile shellFile = ShellFile.FromFilePath(path);
                Bitmap bmp = shellFile.Thumbnail.Bitmap;
                int w = (int)(bmp.Width * scale);
                int h = (int)(bmp.Height * scale);
                return bmp;
            }
            return null;
        }

        private void FilePath_ListView_KeyDown(object sender, KeyEventArgs e)
        {
            //Delete�L�[��������폜
            if (e.KeyData == Keys.Delete)
            {
                if (this.FilePath_ListView.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in this.FilePath_ListView.SelectedItems)
                    {
                        int index = item.Index;
                        this.FilePath_ListView.Items.RemoveAt(index);
                    }
                }
            }
            ChangeFilePathListView();
        }

        private void FilePath_Button_Click(object sender, EventArgs e)
        {
            FilePath_OpenFileDialog.Multiselect = true;
            FilePath_OpenFileDialog.Filter = "CAD�f�[�^�t�@�C��|*.SLDDRW;*.SLDPRT|���ׂẴt�@�C��|*.*";
            if (FilePath_OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilePathListView_AddItems(FilePath_OpenFileDialog.FileNames);
            }
        }

        /// <summary>
        /// ListView�ɃA�C�e�������������ɒǉ�����
        /// </summary>
        /// <param name="filePath"></param>
        private void FilePathListView_AddItems(string[] filePath)
        {
            DrawFileOption_GroupBox.Enabled = false;
            PartFileOption_GroupBox.Enabled = false;
            foreach (string files in filePath)
            {

                //�d���͒e��
                ListViewItem listViewItem = null;
                foreach (ListViewItem items in FilePath_ListView.Items)
                {
                    if (items.SubItems[0].Text == files) listViewItem = items;
                    break;
                }
                if (listViewItem == null)
                {
                    string[] item = { Path.GetFileNameWithoutExtension(files), files };
                    var Listitem = new ListViewItem(item);
                    //�g���q�ɂ���ĕ����F��ς���
                    switch (Path.GetExtension(files))
                    {
                        case ".SLDDRW":
                            Listitem.ForeColor = Color.Red;
                            this.FilePath_ListView.Items.Add(Listitem);
                            break;
                        case ".SLDPRT":
                            Listitem.ForeColor = Color.Blue;
                            this.FilePath_ListView.Items.Add(Listitem);
                            break;
                        case ".SLDASM":
                            Listitem.ForeColor = Color.DarkBlue;
                            this.FilePath_ListView.Items.Add(Listitem);
                            break;
                        default:
                            Listitem.ForeColor = Color.DarkGray;
                            break;

                    }
                }

            }
            foreach (ColumnHeader ch in FilePath_ListView.Columns)
            {
                ch.Width = -2;
            }
            ChangeFilePathListView();
        }
        /// <summary>
        /// �K�v�ȃI�v�V���������g����悤�ɂ���
        /// </summary>
        private void ChangeFilePathListView()
        {
            bool optionFlag = false;
            bool partFlag = false;
            bool drawFlag = false;
            foreach (ListViewItem item in FilePath_ListView.Items)
            {
                switch (Path.GetExtension(item.SubItems[1].Text))
                {
                    case ".SLDDRW":
                        drawFlag = true;
                        optionFlag = true;
                        break;
                    case ".SLDPRT":
                        partFlag = true;
                        optionFlag = true;
                        break;
                    case ".SLDASM":
                        partFlag = true;
                        optionFlag = true;
                        break;
                }
                if (drawFlag && partFlag) break;
            }
            DrawFileOption_GroupBox.Enabled = drawFlag;
            PartFileOption_GroupBox.Enabled = partFlag;
            ExportFolder_GroupBox.Enabled = optionFlag;
            Zip_GroupBox.Enabled = optionFlag;
            Other_GroupBox.Enabled = optionFlag;
            StartExport_Button.Enabled = optionFlag;
        }
        #endregion


        #region ExportOption
        /// <summary>
        /// �����t�H���_�[�ɕۑ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SameFolder_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SameFolder_RadioButton.Checked)
            {
                SameFolder_RadioButton.ForeColor = Color.Black;
            }
            else
            {
                SameFolder_RadioButton.ForeColor = Color.Gray;
            }
        }
        private void LowerFolder_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.LowerFolder_RadioButton.Checked)
            {
                this.LowerFolder_RadioButton.ForeColor = Color.Black;
                this.LowerFolder_ComboBox.Items.Clear();
                this.LowerFolder_ComboBox.Enabled = true;
                //�d���Ȃ����X�g���쐬
                HashSet<string> FolderNameList = new HashSet<string>();
                foreach (ListViewItem filename in this.FilePath_ListView.Items)
                {
                    //�f�B���N�g����ǉ�
                    FolderNameList.Add(Path.GetDirectoryName(filename.SubItems[1].Text));
                }

                foreach (string Names in FolderNameList)
                {
                    //�����ɂ���t�H���_��ǉ�
                    string[] dirs = Directory.GetDirectories(Names);
                    foreach (string Names2 in dirs)
                    {
                        this.LowerFolder_ComboBox.Items.Add(Path.GetFileName(Names2));
                    }
                }
            }
            else
            {
                this.LowerFolder_RadioButton.ForeColor = Color.Gray;
                this.LowerFolder_ComboBox.Enabled = false;
                this.LowerFolder_ComboBox.Items.Clear();
            }
        }



        private void OtherFolder_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (OtherFolder_RadioButton.Checked)
            {
                OtherFolder_RadioButton.ForeColor = Color.Black;
                OtherFolder_ListBox.Items.Clear();
                OtherFolder_ListBox.Enabled = true;
                OtherFolder_Button.Enabled = true;
            }
            else
            {
                OtherFolder_RadioButton.ForeColor = Color.Gray;
                OtherFolder_ListBox.Items.Clear();
                OtherFolder_ListBox.Enabled = false;
                OtherFolder_Button.Enabled = false;
            }
        }

        private void OtherFolder_Button_Click(object sender, EventArgs e)
        {

            CommonOpenFileDialog commonFileDialog = new CommonOpenFileDialog();
            commonFileDialog.IsFolderPicker = true;
            commonFileDialog.InitialDirectory = Path.GetDirectoryName(FilePath_ListView.Items[0].SubItems[1].Text);
            if (commonFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.OtherFolder_ListBox.Items.Clear();
                OtherFolder_ListBox.Items.Add(commonFileDialog.FileName);
            }
        }
        #endregion

        #region ZipOption
        private void ZipOption_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ZipOption_GroupBox.Enabled = ZipOption_CheckBox.Checked;
        }

        private void ZipSameFolder_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ZipSameFolder_RadioButton.Checked)
            {
                ZipSameFolder_RadioButton.ForeColor = Color.Black;
            }
            else
            {
                ZipSameFolder_RadioButton.ForeColor = Color.Gray;
            }
        }

        private void ZipLowerFolder_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            OtherFolder_ListBox.Items.Clear();
            if (ZipLowerFolder_RadioButton.Checked == true)
            {
                ZipLowerFolder_RadioButton.ForeColor = Color.Black;
                ZipLowerFolder_ComboBox.Items.Clear();
                ZipLowerFolder_ComboBox.Enabled = true;
                //�d���Ȃ����X�g���쐬
                HashSet<string> FolderNameList = new HashSet<string>();
                foreach (ListViewItem filename in FilePath_ListView.Items)
                {
                    //�f�B���N�g����ǉ�
                    FolderNameList.Add(Path.GetDirectoryName(filename.SubItems[1].Text));
                }

                foreach (string Names in FolderNameList)
                {
                    //�����ɂ���t�H���_��ǉ�
                    string[] dirs = Directory.GetDirectories(Names);
                    foreach (string Names2 in dirs)
                    {
                        ZipLowerFolder_ComboBox.Items.Add(Path.GetFileName(Names2));
                    }
                }
            }
            else
            {
                ZipLowerFolder_RadioButton.ForeColor = Color.Gray;
                ZipLowerFolder_ComboBox.Enabled = false;
                ZipLowerFolder_ComboBox.Items.Clear();
            }
        }

        private void ZipOtherFolder_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ZipOtherFolder_RadioButton.Checked)
            {
                ZipOtherFolder_RadioButton.ForeColor = Color.Black;
                ZipOtherFolder_ListBox.Items.Clear();
                ZipOtherFolder_ListBox.Enabled = true;
                ZipOtherFolder_Button.Enabled = true;
            }
            else
            {
                ZipOtherFolder_RadioButton.ForeColor = Color.Gray;
                ZipOtherFolder_ListBox.Items.Clear();
                ZipOtherFolder_ListBox.Enabled = false;
                ZipOtherFolder_Button.Enabled = false;
            }
        }

        private void ZipOtherFolder_Button_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog commonFileDialog = new CommonOpenFileDialog();
            commonFileDialog.IsFolderPicker = true;
            commonFileDialog.InitialDirectory = Path.GetDirectoryName(FilePath_ListView.Items[0].SubItems[1].Text);
            if (commonFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ZipOtherFolder_ListBox.Items.Clear();
                ZipOtherFolder_ListBox.Items.Add(commonFileDialog.FileName);
            }
        }
        #endregion

        #region ExportStartEnd
        Thread thread1 = null;
        private void StartExport_Button_Click(object sender, EventArgs e)
        {
            string? errorText = GetErroGUI();
            if (errorText == null)
            {
                //GUI�𖳌�������
                this.GuiOnOff(false);
                string TopFolderPath = "";
                //�o�͐�̃t�H���_�[���擾����
                //�I�v�V�����ɂ���ĉ��ʃt�H���_�[��I������
                if (this.SameFolder_RadioButton.Checked)
                {
                    //�g�b�v�t�H���_�[���擾
                    TopFolderPath = Path.GetDirectoryName(this.FilePath_ListView.Items[0].SubItems[1].Text);
                }
                else if (this.LowerFolder_RadioButton.Checked)
                {
                    //�����t�H���_�[
                    TopFolderPath = this.LowerFolder_ComboBox.Items[0].ToString();
                }
                else if (this.OtherFolder_RadioButton.Checked)
                {
                    //���̃t�H���_�[
                    TopFolderPath = this.ZipOtherFolder_ListBox.Items[0].ToString();
                }

                List<string> filePaths = new List<string>();
                foreach (ListViewItem item in this.FilePath_ListView.Items)
                {
                    filePaths.Add(item.SubItems[1].Text);
                }
                ExportFile exportFile = new ExportFile(this);
                thread1 = new Thread(() => exportFile.Export(progressBar: this.StartExport_ProgressBar, filePaths, TopFolderPath));
                thread1.Start();
                this.TaskLabel.Text = "�����҂�";
                this.StartExport_ProgressBar.Value = 0;
                GuiOnOff(true);
            }
            else
            {
                TaskLabel.Text = errorText;
            }
        }
        private void Break_Button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("�ϊ������𒆒f���܂����H", "���f�_�C�A���O", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                thread1.Interrupt();
                thread1.Join();
                this.TaskLabel.Text = "�����𒆒f���܂���";
                this.Invoke(new CADExportTool.TaskCount(this.InvokeProgressBar), 0);
                this.StartExport_ProgressBar.Update();
                this.GuiOnOff(true);
            }
        }

        #endregion

        #region Utility
        private void GuiOnOff(bool Flag)
        {
            this.FilePath_ListView.Enabled = Flag;
            this.FilePath_Button.Enabled = Flag;
            this.ExportFolder_GroupBox.Enabled = Flag;
            this.Zip_GroupBox.Enabled = Flag;
            this.Other_GroupBox.Enabled = Flag;
            foreach (ListViewItem item in FilePath_ListView.Items)
            {
                if (Path.GetExtension(item.SubItems[1].Text).Equals(".SLDPRT", StringComparison.OrdinalIgnoreCase) |
                    Path.GetExtension(item.SubItems[1].Text).Equals(".SLDASM", StringComparison.OrdinalIgnoreCase))
                {
                    this.PartFileOption_GroupBox.Enabled = Flag;
                }
                if (Path.GetExtension(item.SubItems[1].Text).Equals(".SLDDRW", StringComparison.OrdinalIgnoreCase))
                {
                    this.DrawFileOption_GroupBox.Enabled = Flag;
                }
            }
        }
        /// <summary>
        /// ���[�U�[����̃G���[��\��
        /// </summary>
        /// <returns></returns>
        private string? GetErroGUI()
        {
            if (LowerFolder_RadioButton.Checked)
            {
                if (LowerFolder_ComboBox.SelectedItem == null)
                {
                    return ExportFolder_GroupBox.Text + "/" + LowerFolder_RadioButton.Text + "�̃t�H���_�[��I�����Ă�������";
                }
            }
            if (OtherFolder_RadioButton.Checked)
            {
                if (OtherFolder_ListBox.Items.Count <= 0)
                {
                    return ExportFolder_GroupBox.Text + "/" + OtherFolder_RadioButton.Text + "�̃t�H���_�[��I�����Ă�������";
                }
            }

            if (ZipLowerFolder_RadioButton.Checked)
            {
                if (ZipLowerFolder_ComboBox.SelectedItem == null)
                {
                    return ZipOption_GroupBox.Text + "/" + ZipLowerFolder_RadioButton.Text + "�̃t�H���_�[��I�����Ă�������";
                }
            }
            if (ZipOtherFolder_RadioButton.Checked)
            {
                if (ZipOtherFolder_ListBox.Items.Count <= 0)
                {
                    return ZipOption_GroupBox.Text + "/" + ZipOtherFolder_RadioButton.Text + "�̃t�H���_�[��I�����Ă�������";
                }
            }
            if (this.PDF_CheckBox.Checked |
                this.DXF_CheckBox.Checked |
                this.STEP_CheckBox.Checked |
                this.IGS_CheckBox.Checked |
                this.ThreeMF_CheckBox.Checked
                &&
                this.LowerFolder_RadioButton.Checked |
                this.OtherFolder_RadioButton.Checked |
                this.SameFolder_RadioButton.Checked)
            {
                return null;
            }


            else
            {
                return "�o�͐�t�H���_�[��I�����Ă�������";

            }
        }
        public void InvokeProgressBar(int counter)
        {
            this.StartExport_ProgressBar.Value = counter;
        }
        public void SetMaxProgressBar(int counter)
        {
            this.StartExport_ProgressBar.Maximum = counter;
        }
        public void SetMinProgressBar(int counter)
        {
            this.StartExport_ProgressBar.Minimum = counter;
        }

        public delegate void TaskCount(int counter);
        public delegate void LabelText(string text);
        public void SetLabel(String label)
        {
            this.TaskLabel.Text = label;
        }
        #endregion

    }
}
