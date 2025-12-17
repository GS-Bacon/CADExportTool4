using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAPIAPI.Converter
{
    public class OpenCadFile
    {
        public static ModelDoc2? OpenAssemblyCadFile(string FilePath)
        {
            SldWorks SolidworksApp = new SldWorks();
            try
            {
                ModelDoc2 SolidworksDocument;
                int FileErro = 0;
                int FileWarning = 0;
                SolidworksDocument = SolidworksApp.OpenDoc6(
                        FilePath,
                        (int)swDocumentTypes_e.swDocASSEMBLY,
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

        public static ModelDoc2? OpenDrawCadFile(string FilePath)
        {
            SldWorks SolidworksApp = new SldWorks();
            try
            {
                ModelDoc2 SolidworksDocument;
                int FileErro = 0;
                int FileWarning = 0;
                SolidworksDocument = SolidworksApp.OpenDoc6(
                        FilePath,
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

        public static PartDoc? OpenPartCadFile(string filePath)
        {
            SldWorks SolidworksApp = new SldWorks();
            try
            {
                PartDoc? SolidworksDocumen;
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
    }
}
