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
        public List<string> SubjectExtension { get; } = [".SLDPRT"];

        /// <summary>
        /// 変換後の拡張子
        /// </summary>
        public string OutputExtension { get; }
        public PartConverter(string outputExtension)
        {
            OutputExtension = outputExtension;
        }
        public string Convert(string OutoputFolderPath, string FilePath)
        {
            //各拡張子について処理
            foreach (string Extension in SubjectExtension)
            {

                //対象の拡張子だけに処理
                if (Path.GetExtension(FilePath) == Extension)
                {
                    //Null check
                    if (OpenCadFile.OpenPartCadFile(FilePath) is ModelDoc2 part)
                    {
                        ModelDocExtension SolidworksModelExtension = default;
                        int FileErro = 0;
                        int FileWarning = 0;
                        bool bRet;

                        SolidworksModelExtension = part.Extension;
                        string exportFilePath = Path.Combine(OutoputFolderPath, Path.ChangeExtension(Path.GetFileName(FilePath), OutputExtension));

                        bRet = SolidworksModelExtension.SaveAs3(
                            exportFilePath,
                            (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                            (int)swSaveAsOptions_e.swSaveAsOptions_Silent,
                            null,
                            null,
                            ref FileErro,
                            ref FileWarning
                            );
                        SldWorks SolidworksApp = new SldWorks();
                        SolidworksApp.CloseDoc(FilePath);
                        return exportFilePath;
                    }

                }

            }
            return null;

        }
    }
}