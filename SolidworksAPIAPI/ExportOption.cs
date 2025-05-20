using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAPIAPI
{
    public class ExportOption : IExportOption    {
        public List<string>? FilePaths { get; set; }

        public List<ExtensionPair>? Extensions { get; set; } = new List<ExtensionPair>();
    }
}