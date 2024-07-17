using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.IO.Compression;
using System.Windows.Forms;
using System.Data;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Threading;

namespace CADExportTool4
{
    internal class ExportCADFile
    {
        private SldWorks SolidworksApp = new SldWorks();
        private ExportOption option;
        public Form1 Form1;

        public ExportCADFile(ExportOption option, Form1 form1)
        {
            this.option = option;
            this.Form1 = form1;

        }
        public void Export()
        {
            try
            {
                foreach (Fileoptions fileoptions in this.option.Fileoptions)
                {

                    switch (Path.GetExtension(fileoptions.itempath))
                    {
                        case (".SLDDRW"):
                            ModelDoc2 draw = OpenDrawCADFile(fileoptions.itempath);
                            foreach (string exportpath in fileoptions.exportpath)
                            {
                                ExportDrawCADFile(draw, exportpath);
                                Form1.Invoke(new Form1.TaskCount(Form1.PlusCounter), Form1.TaskProgressBar.Value + 1);
                                Form1.Invoke(new Form1.LabelText(Form1.SetLabel), $"{Form1.TaskProgressBar.Value}/{Form1.TaskProgressBar.Maximum} 出力中：{fileoptions.filename}");
                            }
                            SolidworksApp.CloseDoc(fileoptions.itempath);
                            break;
                        case (".SLDPRT"):
                        case (".SLDASM"):
                            PartDoc part = OpenPartCADFile(fileoptions.itempath);
                            foreach (string exportpath in fileoptions.exportpath)
                            {
                                ExportPartCADFile(part, exportpath);
                                Form1.Invoke(new Form1.TaskCount(Form1.PlusCounter), Form1.TaskProgressBar.Value + 1);
                                Form1.Invoke(new Form1.LabelText(Form1.SetLabel), $"{Form1.TaskProgressBar.Value}/{Form1.TaskProgressBar.Maximum} 出力中：{fileoptions.filename}");
                            }
                            SolidworksApp.CloseDoc(fileoptions.itempath);
                            break;
                    }
                }
                MakeZip();
                GetUserLog();
                EndToast();
                Form1.Invoke(new Form1.TaskCount(Form1.PlusCounter), 0);
                Form1.Invoke(new Form1.LabelText(Form1.SetLabel), $"処理待ち");
                Form1.Invoke(new Form1.OnOff(Form1.OnOffSwitch), true);
            }
            catch (ThreadInterruptedException ex)
            {

            }
        }
        private ModelDoc2 OpenDrawCADFile(string filePath)
        {
            try
            {
                ModelDoc2 SolidworksDocument;
                int FileErro = 0;
                int FileWarning = 0;
                SolidworksDocument = (ModelDoc2)SolidworksApp.OpenDoc6(
                        filePath,
                        (int)swDocumentTypes_e.swDocDRAWING,
                        (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                        "",
                        ref FileErro,
                        ref FileWarning
                        );
                return SolidworksDocument;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        private string ExportDrawCADFile(ModelDoc2 doc2, string ExportFolderPath)
        {
            try
            {
                ModelDocExtension SolidworksModelExtension = default(ModelDocExtension);
                int FileErro = 0;
                int FileWarning = 0;
                bool bRet;

                SolidworksModelExtension = (ModelDocExtension)doc2.Extension;

                bRet = SolidworksModelExtension.SaveAs3(
                    ExportFolderPath,
                    (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                    (int)swSaveAsOptions_e.swSaveAsOptions_Silent,
                    null,
                    null,
                    ref FileErro,
                    ref FileWarning
                    );
                return ExportFolderPath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.ToString();
            }
        }
        private PartDoc OpenPartCADFile(string filePath)
        {
            try
            {
                PartDoc SolidworksDocumen = default(PartDoc);
                int FileError = 0;
                int FileWarning = 0;
                SolidworksDocumen = (PartDoc)SolidworksApp.OpenDoc6(
                    filePath,
                    (int)swDocumentTypes_e.swDocPART,
                    (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                    "",
                    ref FileError,
                    ref FileWarning
                    );
                return SolidworksDocumen;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        private string ExportPartCADFile(PartDoc part, string ExportFolderPath)
        {
            try
            {
                ModelDoc2 SolidworksModel;
                ModelDocExtension SolidworksModelExtension = default(ModelDocExtension);
                int FileError = 0;
                int FileWarning = 0;
                bool bRet;

                SolidworksModel = (ModelDoc2)part;
                SolidworksModelExtension = (ModelDocExtension)SolidworksModel.Extension;

                bRet = SolidworksModelExtension.SaveAs3(
                        ExportFolderPath,
                        (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                        (int)swSaveAsOptions_e.swSaveAsOptions_Silent,
                        null,
                        null,
                        ref FileError,
                        ref FileWarning
                        );
                return ExportFolderPath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        private void MakeZip()
        {
            foreach (string zipfile in this.option.ZipFilePathList)
            {
                if (System.IO.File.Exists(zipfile) && zipfile != "")
                {
                    System.IO.File.Delete(zipfile);
                }
            }
            foreach (Fileoptions fileoptions in this.option.Fileoptions)
            {
                if (fileoptions.zippath != "")
                {
                    Form1.Invoke(new Form1.LabelText(Form1.SetLabel), $"{Form1.TaskProgressBar.Value}/{Form1.TaskProgressBar.Maximum} Zipファイル作成中 {fileoptions.filename}");
                    using (ZipArchive archive = ZipFile.Open(fileoptions.zippath, ZipArchiveMode.Update))
                    {
                        foreach (string filename in fileoptions.exportpath)
                        {
                            archive.CreateEntryFromFile(filename, Path.GetFileName(filename), CompressionLevel.Optimal);
                        }
                    }
                    Form1.Invoke(new Form1.TaskCount(Form1.PlusCounter), Form1.TaskProgressBar.Value + 1);
                }
            }
        }
        private void GetUserLog()//使用したユーザー名と時刻をCSVファイルに保存
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(System.Environment.UserName);
            dataTable.Columns.Add(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            //CSV出力用変数の作成
            List<string> lines = new List<string>();

            //列名をカンマ区切りで1行に連結
            List<string> header = new List<string>();
            foreach (DataColumn dr in dataTable.Columns)
            {
                header.Add(dr.ColumnName);
            }
            lines.Add(string.Join(",", header));

            //列の値をカンマ区切りで1行に連結
            foreach (DataRow dr in dataTable.Rows)
            {
                lines.Add(string.Join(",", dr.ItemArray));
            }

            using (StreamWriter sw = new StreamWriter(@"log.csv", true,
                                          Encoding.GetEncoding("shift-jis")))
            {
                foreach (string line in lines)
                {
                    sw.WriteLine(line);
                }
            }
        }
        private void EndToast() //作業終了時にトースト通知を発行
        {
            new ToastContentBuilder()
                .AddText("CADデータ出力ツール")
                .AddText("作業が終了しました")
                .Show();
        }
    }
}
