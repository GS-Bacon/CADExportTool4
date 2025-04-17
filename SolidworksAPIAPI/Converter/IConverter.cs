using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAPIAPI.Converter
{
    /// <summary>
    /// ファイルのコンバーターへのアクセスを提供します。
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// 変換対象となるファイル拡張子
        /// </summary>
        public List<string> SubjectExtension { get; }

        /// <summary>
        /// 変換後の拡張子
        /// </summary>
        public string OutputExtension { get; }

        /// <summary>
        /// 出力先のフォルダーパス
        /// </summary>
        public string OutputFolderPath { get; }

        public List<string> FilePath {  get; }

        /// <summary>
        /// ファイルを変換します
        /// </summary>
        /// <param name="FilePath">変換するファイルパス</param>
        /// <returns>変換後のフィルパス</returns>
        public List<string>? Convert();
    }
}
