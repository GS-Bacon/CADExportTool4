using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAPIAPI.Converter
{
    public class PartConverter : IConverter
    {
        /// <summary>
        /// 変換対象となるファイル拡張子
        /// </summary>
        public List<string> SubjectExtension { get; } = [".SLDPRT", ".SLDASM"];

        /// <summary>
        /// 変換後の拡張子
        /// </summary>
        public string OutputExtension { get; }

        /// <summary>
        /// 出力するフォルダパス
        /// </summary>
        public string OutputFolderPath { get; }

        public List<string> FilePath { get; } = new List<string>();
        public PartConverter(string outputExtension, string outoputFolderPath, List<string> filePath)
        {
            OutputExtension = outputExtension;
            OutputFolderPath = outoputFolderPath;
            FilePath = filePath;
        }

        public List<string>? Convert()
        {
            List<string>? result = null;
            //各拡張子について処理
            foreach (string Extension in SubjectExtension)
            {
                foreach (string File in FilePath)
                {
                    //対象の拡張子だけに処理
                    if (Path.GetExtension(File) == Extension)
                    {
                        //Null check
                        if (OpenCadFile.OpenPartCadFile(File) is ModelDoc2 part)
                        {
                            ModelDocExtension SolidworksModelExtension = default;
                            int FileErro = 0;
                            int FileWarning = 0;
                            bool bRet;

                            SolidworksModelExtension = part.Extension;
                            string exportFilePath = Path.Combine(OutputFolderPath, Path.ChangeExtension(Path.GetFileName(File), OutputExtension));

                            bRet = SolidworksModelExtension.SaveAs3(
                                exportFilePath,
                                (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                                (int)swSaveAsOptions_e.swSaveAsOptions_Silent,
                                null,
                                null,
                                ref FileErro,
                                ref FileWarning
                                );
                            result.Add(exportFilePath);
                        }
                    }
                }

            }
            return result;
        }
    }
}