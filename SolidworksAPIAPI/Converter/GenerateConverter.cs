using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAPIAPI.Converter
{
    public class GenerateConverter
    {
        public List<IConverter>? Generate(IExportOption exportOption)
        {
            List<IConverter> converter = new List<IConverter>();
            foreach (ExtensionPair extension in exportOption.Extensions)
            {
                switch (extension.OutputExtension)
                {
                    case ".pdf":
                        IConverter converter1 = new DrawConverter(extension.OutputExtension, extension.FolderPath, exportOption.FilePaths);
                        converter.Add(converter1);
                        break;
                    case ".dxf":
                        IConverter converter2 = new DrawConverter(extension.OutputExtension, extension.FolderPath, exportOption.FilePaths);
                        converter.Add(converter2);
                        break;
                    case ".igs":
                        IConverter converter3 = new PartConverter(extension.OutputExtension, extension.FolderPath, exportOption.FilePaths);
                        converter.Add(converter3);
                        break;
                    case ".step":
                        IConverter converter4 = new PartConverter(extension.OutputExtension, extension.FolderPath, exportOption.FilePaths);
                        converter.Add(converter4);
                        break;
                    case ".3MF":
                        IConverter converter5 = new PartConverter(extension.OutputExtension, extension.FolderPath, exportOption.FilePaths);
                        converter.Add(converter5);
                        break;
                }
            }
            return converter;
        }
    }
}