using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAPIAPI.Converter
{
    public class DrawConverter : IConverter
    {
        public List<string> SubjectExtension { get; set; } = [".SLDDRW"];

        public string OutputExtension { get; set; }
        public DrawConverter(string outputExtension)
        {
            OutputExtension = outputExtension;
        }
        public string? Convert(string OutoputFolderPath, string FilePath)
        {
            //各拡張子について処理
            foreach (string Extension in SubjectExtension)
            {
                //対象の拡張子だけに処理
                if (Path.GetExtension(FilePath) == Extension)
                {
                    //Null check
                    if (OpenCadFile.OpenDrawCadFile(FilePath) is ModelDoc2 draw)
                    {
                        ModelDocExtension SolidworksModelExtension = default;
                        int FileErro = 0;
                        int FileWarning = 0;
                        bool bRet;

                        SolidworksModelExtension = draw.Extension;
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
                        return exportFilePath;
                    }

                }
            }
            return null;

        }
    }
}
