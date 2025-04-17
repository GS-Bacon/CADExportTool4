using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAPIAPI
{
    public interface IExportOption
    {
        /// <summary>
        /// 元のファイルパス
        /// </summary>
        public List<string>? FilePaths { get; }

        /// <summary>
        /// 変換する拡張子と保存フォルダのパスのリスト
        /// </summary>
        public List<ExtensionPair>? Extensions { get; }

    }

    /// <summary>
    /// 変換後の拡張子と出力フォルダーパスのペア
    /// </summary>
    public record ExtensionPair
    {
        /// <summary>
        /// 拡張子
        /// </summary>
        public string OutputExtension { get; }

        /// <summary>
        /// 保存フォルダのパス
        /// </summary>
        public string FolderPath { get; }

        /// <summary>
        /// 拡張子と保存フォルダのペア
        /// </summary>
        /// <param name="outputextension">出力する拡張子</param>
        /// <param name="folderPath">保存するフォルダーパス</param>
        public ExtensionPair(string outputextension, string folderPath)
        {
            this.OutputExtension = outputextension;
            this.FolderPath = folderPath;
        }
    }
}
