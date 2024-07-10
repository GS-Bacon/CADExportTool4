using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace CADExportTool4
{
    internal class ExportCADFile
    {
        public ExportOption option;
        private SldWorks SolidworksApp = new SldWorks();
        public ExportCADFile(ExportOption option)
        {
            this.option = option;
        }
        public void Export()
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
                        }
                        SolidworksApp.CloseDoc(fileoptions.itempath);
                        break;
                    case (".SLDPRT"):
                    case (".SLDASM"):
                        PartDoc part = OpenPartCADFile(fileoptions.itempath);
                        foreach (string exportpath in fileoptions.exportpath)
                        {
                            ExportPartCADFile(part, exportpath);
                        }
                        SolidworksApp.CloseDoc(fileoptions.itempath);
                        break;
                }
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
                bool bRet = false;
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

    }
}
