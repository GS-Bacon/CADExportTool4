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
        public List<Fileoptions> Fileoptions = new List<Fileoptions>();
        public Dictionary<string,ExtensionVariants> exoption = new Dictionary<string, ExtensionVariants>(){
            {"pdf",new ExtensionVariants() {name="pdf",extensions=new List<string>() {"pdf"} ,parent_ex=new List<string>(){".SLDDRW" } } },
            {"dxf",new ExtensionVariants() {name="dxf",extensions=new List<string>() {"dxf"} ,parent_ex=new List<string>(){".SLDDRW" }} },
            {"igs",new ExtensionVariants() {name="igs",extensions=new List<string>() {"igs","iges"},parent_ex=new List<string>(){".SLDPRT","SLDASM" } } },
            {"step",new ExtensionVariants() {name="step",extensions=new List<string>() {"step","stp"},parent_ex=new List<string>(){ ".SLDPRT", "SLDASM" } } },
            {"stl",new ExtensionVariants() {name="stl",extensions=new List<string>() {"stl"} ,parent_ex=new List<string>(){".SLDPRT", "SLDASM" }} },
            };
        public HashSet<string> ZipFilePathList= new HashSet<string>();
    }

    class Fileoptions
    {
        /// <summary>
        /// 拡張子なしファイル名
        /// </summary>
        public string filename { get; set; } = "";

        /// <summary>
        /// 元ファイルのパス
        /// </summary>
        public string itempath { get; set; } = "";

        /// <summary>
        /// 出力後のファイル一覧
        /// </summary>
        public List<string> exportpath { get; set; }= new List<string>();

        /// <summary>
        /// Zipフォルダパス
        /// </summary>
        public string zippath { get; set; } = "";
    }
    class ExtensionVariants
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string name;
        public List<string> extensions;
        public bool check=false;
        public string folderpath="";
        public List<string> parent_ex;
    }
}
