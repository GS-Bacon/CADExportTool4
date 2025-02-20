using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAPIAPI
{
    public class PdfConverter : IConverter
    {
        public string InputExtension { get; } = ".pdf";

        public List<string> ConvertExtensions { get; } = [".SLDDRW" ];

        public string OutputFolderPath { get; }

        public string Convert(string FilePath)
        {
            throw new NotImplementedException();
        }
    }
}
