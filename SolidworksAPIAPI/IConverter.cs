using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAPIAPI
{
    internal interface IConverter
    {
        public string InputExtension { get; }
        public List<string> ConvertExtensions { get; }
        public string OutputFolderPath {get;}
        public string Convert(string FilePath);
    }
}
