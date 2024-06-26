using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADExportTool4
{
    /// <summary>
    /// 出力オプションをまとめるクラス
    /// </summary>
    class ExportOption
    {
        /// <summary>
        /// 選択できるファイルの拡張子一覧
        /// </summary>
        public static string[] FileExtensions = { ".SLDPRT", ".SLDDRW", ".SLDASM" };

        public List<string> FileList = new List<string>();

        public ExtensionOption pdf = new ExtensionOption() { FolderPath = new List<string>{ "pdf" } };
        public ExtensionOption dxf = new ExtensionOption() { FolderPath = new List<string> { "dxf" } };
        public ExtensionOption igs = new ExtensionOption() { FolderPath = new List<string> { "igs", "iges" } };
        public ExtensionOption step = new ExtensionOption() { FolderPath = new List<string> { "step" } };
        public ExtensionOption stl = new ExtensionOption() { FolderPath = new List<string> { "stl" } };

        /// <summary>
        /// エクスポートするフォルダのフルパス
        /// </summary>
        public string ExportFolderPath;

        /// <summary>
        /// 部品名ごとにフォルダを分けるかどうか
        /// </summary>
        public bool SeparatebyExtension = false;

        /// <summary>
        /// Zipにまとめるかどうか
        /// </summary>
        public bool CreateZip = false;

        /// <summary>
        /// "zip"フォルダを作るかどうか
        /// </summary>
        public bool CreateZipFolder = false;

        /// <summary>
        /// Zipフォルダフルパス
        /// </summary>
        public string ZipFolderPath;
    }

    class ExtensionOption
    {
        public bool check = false;
        public List<string> FolderPath;
    }
}
