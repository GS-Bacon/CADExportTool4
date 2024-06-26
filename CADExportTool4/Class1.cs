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
        public static string[] FileExtensions  = {".SLDPRT",".SLDDRW", ".SLDASM" };

        public PDF pdf;
        public DXF dXF;
        public IGS igs;
        public STEP step;
        public STL stl;

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

    /// <summary>
    /// PDF出力オプション
    /// </summary>
    class PDF
    {
        public PDF() { }
        public static string[] FolderPath = { "pdf"};
        public bool check=false;
    }
    class DXF
    {
        public DXF() { }
        public static string[] FolderPath = { "dxf"};
        public bool check = false;
    }
    class IGS
    {
        public IGS() { }
        public static string[] FolderPath = { "igs", "iges" };
        public bool check = false;
    }
    class STEP
    {
        public STEP() { }
        public static string[] FolderPath = { "step"};
        public bool check = false;
    }
    class STL
    {
        public STL() { }
        public static string[] FolderPath = { "stl"};
        public bool check = false;
    }
}
