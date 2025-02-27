using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAPIAPI
{
    /// <summary>
    /// ファイルのコンバーターへのアクセスを提供します。
    /// </summary>
    internal interface IConverter
    {
        /// <summary>
        /// 変換対称の拡張子
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

        /// <summary>
        /// ファイルを変換します
        /// </summary>
        /// <param name="FilePath">変換するファイルパス</param>
        /// <returns>変換後のフィルパス</returns>
        public string Convert(string FilePath);
    }
}
