using SolidworksAPIAPI;
using SolidworksAPIAPI.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace SMZ_CADExportTool
{
    /// <summary>
    /// ファイル変換を行うクラス
    /// </summary>
    public class ExportFile
    {
        private IExportOption ExportOption = new SolidworksAPIAPI.ExportOption();
        public CADExportTool ExportTool;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form"></param>
        public ExportFile(CADExportTool exportTool)
        {
            this.ExportTool = exportTool;
        }

        /// <summary>
        /// ファイルを変換する
        /// </summary>
        /// <param name="progressBar">出力状況を表示するためのプログレスバー</param>
        public void Export(System.Windows.Forms.ProgressBar? progressBar, List<string> FilePaths, string TopFolderPath)
        {
            try
            {

                //各種Converterを生成する
                DrawConverter PDFConverter = new DrawConverter(".pdf");
                DrawConverter DXFConverter = new DrawConverter(".dxf");
                PartConverter IGSConverter = new PartConverter(".igs");
                PartConverter STEPConverter = new PartConverter(".step");
                PartConverter ThreeMFConverter = new PartConverter(".3MF");

                //タスク数カウント用
                int DrawExtension = 0;
                int PartExtension = 0;

                //各拡張子ごとのフォルダーをない場合は作成する
                if (ExportTool.PDF_CheckBox.Checked)
                {
                    //PDFフォルダーが存在しない場合は作成する
                    if (!Directory.Exists(Path.Combine(TopFolderPath, "PDF")))
                    {
                        Directory.CreateDirectory(Path.Combine(TopFolderPath, "PDF"));
                    }
                    DrawExtension++;
                }
                if (ExportTool.DXF_CheckBox.Checked)
                {
                    //DXFフォルダーが存在しない場合は作成する
                    if (!Directory.Exists(Path.Combine(TopFolderPath, "DXF")))
                    {
                        Directory.CreateDirectory(Path.Combine(TopFolderPath, "DXF"));
                    }
                    DrawExtension++;
                }
                if (ExportTool.IGS_CheckBox.Checked)
                {
                    //IGSフォルダーが存在しない場合は作成する
                    if (!Directory.Exists(Path.Combine(TopFolderPath, "IGS")))
                    {
                        Directory.CreateDirectory(Path.Combine(TopFolderPath, "IGS"));
                    }
                    PartExtension++;
                }
                if (ExportTool.STEP_CheckBox.Checked)
                {
                    //STEPフォルダーが存在しない場合は作成する
                    if (!Directory.Exists(Path.Combine(TopFolderPath, "STEP")))
                    {
                        Directory.CreateDirectory(Path.Combine(TopFolderPath, "STEP"));
                    }
                    PartExtension++;
                }
                if (ExportTool.ThreeMF_CheckBox.Checked)
                {
                    //3MFフォルダーが存在しない場合は作成する
                    if (!Directory.Exists(Path.Combine(TopFolderPath, "3MF")))
                    {
                        Directory.CreateDirectory(Path.Combine(TopFolderPath, "3MF"));
                    }
                    PartExtension++;
                }
                //Thubnailフォルダーが存在しない場合は作成する
                if (!Directory.Exists(Path.Combine(TopFolderPath, "Thumbnail")))
                {
                    Directory.CreateDirectory(Path.Combine(TopFolderPath, "Thumbnail"));
                }

                //タスク数をカウントする
                int TaskCount = 0;
                TaskCount += PartExtension * FilePaths.Count(file => Path.GetExtension(file).Equals(".SLDPRT", StringComparison.OrdinalIgnoreCase));
                TaskCount += PartExtension * FilePaths.Count(file => Path.GetExtension(file).Equals(".SLDASM", StringComparison.OrdinalIgnoreCase));
                TaskCount += DrawExtension * FilePaths.Count(file => Path.GetExtension(file).Equals(".SLDDRW", StringComparison.OrdinalIgnoreCase));
                if (ExportTool.PDF_CheckBox.Checked || ExportTool.DXF_CheckBox.Checked)
                {
                    TaskCount += FilePaths.Count(file => Path.GetExtension(file).Equals(".SLDDRW", StringComparison.OrdinalIgnoreCase));
                }
                if (ExportTool.IGS_CheckBox.Checked || ExportTool.STEP_CheckBox.Checked || ExportTool.ThreeMF_CheckBox.Checked)
                {
                    TaskCount += FilePaths.Count(file => Path.GetExtension(file).Equals(".SLDPRT", StringComparison.OrdinalIgnoreCase));
                    TaskCount += FilePaths.Count(file => Path.GetExtension(file).Equals(".SLDASM", StringComparison.OrdinalIgnoreCase));
                }
                //タスク数が0の場合は処理を終了する
                if (TaskCount == 0)
                {
                    MessageBox.Show("変換するファイルがありません。");
                    return;
                }
                //プログレスバーの最大値を設定する
                if (progressBar != null)
                {
                    ExportTool.Invoke(new CADExportTool.TaskCount(ExportTool.InvokeProgressBar), 0);
                    ExportTool.Invoke(new CADExportTool.TaskCount(ExportTool.SetMaxProgressBar), TaskCount);
                    ExportTool.Invoke(new CADExportTool.TaskCount(ExportTool.SetMinProgressBar), 0);
                }

                //各ファイルを変換する

                foreach (string file in FilePaths)
                {
                    switch (Path.GetExtension(file))
                    {
                        case ".SLDDRW":
                            if (ExportTool.PDF_CheckBox.Checked)
                            {
                                ExportTool.Invoke(new CADExportTool.LabelText(ExportTool.SetLabel), $"{ExportTool.StartExport_ProgressBar.Value}/{ExportTool.StartExport_ProgressBar.Maximum} 出力中：{Path.GetFileName(file)}");
                                PDFConverter.Convert(Path.Combine(TopFolderPath, "PDF"), file);
                                if (progressBar != null)
                                {
                                    ExportTool.Invoke(new CADExportTool.TaskCount(ExportTool.InvokeProgressBar), ExportTool.StartExport_ProgressBar.Value + 1);
                                }
                            }
                            if (ExportTool.DXF_CheckBox.Checked)
                            {
                                ExportTool.Invoke(new CADExportTool.LabelText(ExportTool.SetLabel), $"{ExportTool.StartExport_ProgressBar.Value}/{ExportTool.StartExport_ProgressBar.Maximum} 出力中：{Path.GetFileName(file)}");
                                DXFConverter.Convert(Path.Combine(TopFolderPath, "dxf"), file);
                                if (progressBar != null)
                                {
                                    ExportTool.Invoke(new CADExportTool.TaskCount(ExportTool.InvokeProgressBar), ExportTool.StartExport_ProgressBar.Value + 1);
                                }
                            }
                            break;
                        case ".SLDPRT":
                        case ".SLDASM":
                            if (ExportTool.IGS_CheckBox.Checked)
                            {
                                ExportTool.Invoke(new CADExportTool.LabelText(ExportTool.SetLabel), $"{ExportTool.StartExport_ProgressBar.Value}/{ExportTool.StartExport_ProgressBar.Maximum} 出力中：{Path.GetFileName(file)}");
                                IGSConverter.Convert(Path.Combine(TopFolderPath, "IGS"), file);
                                if (progressBar != null)
                                {
                                    ExportTool.Invoke(new CADExportTool.TaskCount(ExportTool.InvokeProgressBar), ExportTool.StartExport_ProgressBar.Value + 1);
                                }
                            }
                            if (ExportTool.STEP_CheckBox.Checked)
                            {
                                ExportTool.Invoke(new CADExportTool.LabelText(ExportTool.SetLabel), $"{ExportTool.StartExport_ProgressBar.Value}/{ExportTool.StartExport_ProgressBar.Maximum} 出力中：{Path.GetFileName(file)}");
                                STEPConverter.Convert(Path.Combine(TopFolderPath, "STEP"), file);
                                if (progressBar != null)
                                {
                                    ExportTool.Invoke(new CADExportTool.TaskCount(ExportTool.InvokeProgressBar), ExportTool.StartExport_ProgressBar.Value + 1);
                                }
                            }
                            if (ExportTool.ThreeMF_CheckBox.Checked)
                            {
                                ExportTool.Invoke(new CADExportTool.LabelText(ExportTool.SetLabel), $"{ExportTool.StartExport_ProgressBar.Value}/{ExportTool.StartExport_ProgressBar.Maximum} 出力中：{Path.GetFileName(file)}");
                                ThreeMFConverter.Convert(Path.Combine(TopFolderPath, "3MF"), file);
                                if (progressBar != null)
                                {
                                    ExportTool.Invoke(new CADExportTool.TaskCount(ExportTool.InvokeProgressBar), ExportTool.StartExport_ProgressBar.Value + 1);
                                }
                            }
                            break;
                    }
                    if (ExportTool.CreateThumbnail_CheckBox.Checked)
                    {
                        SolidworksAPIAPI.GetThumbnail getThumbnail = new();

                        ExportTool.Invoke(new CADExportTool.LabelText(ExportTool.SetLabel), $"{ExportTool.StartExport_ProgressBar.Value}/{ExportTool.StartExport_ProgressBar.Maximum} サムネイルを出力：{Path.GetFileName(file)}");
                        getThumbnail.GetAllImg(Path.Combine(TopFolderPath, "Thumbnail"), file);
                        if (progressBar != null)
                        {
                            ExportTool.Invoke(new CADExportTool.TaskCount(ExportTool.InvokeProgressBar), ExportTool.StartExport_ProgressBar.Value + 1);
                        }
                    }
                }
            }
            catch (ThreadInterruptedException)
            {
            }

        }
    }
}
