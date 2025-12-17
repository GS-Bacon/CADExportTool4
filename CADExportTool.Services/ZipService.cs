using System.IO.Compression;
using CADExportTool.Core.Interfaces;

namespace CADExportTool.Services;

/// <summary>
/// ZIP圧縮サービス
/// </summary>
public class ZipService : IZipService
{
    /// <inheritdoc/>
    public Task<bool> CreateZipAsync(
        IEnumerable<string> sourceFiles,
        string outputPath,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                // 出力先ディレクトリが存在しない場合は作成
                var outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // 既存のZIPファイルがある場合は削除
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }

                using var zipArchive = ZipFile.Open(outputPath, ZipArchiveMode.Create);

                foreach (var file in sourceFiles)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (File.Exists(file))
                    {
                        var entryName = Path.GetFileName(file);
                        zipArchive.CreateEntryFromFile(file, entryName, CompressionLevel.Optimal);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating zip: {ex.Message}");
                return false;
            }
        }, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> CreateZipFromFolderAsync(
        string sourceFolder,
        string outputPath,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                // 出力先ディレクトリが存在しない場合は作成
                var outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // 既存のZIPファイルがある場合は削除
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }

                ZipFile.CreateFromDirectory(sourceFolder, outputPath, CompressionLevel.Optimal, false);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating zip from folder: {ex.Message}");
                return false;
            }
        }, cancellationToken);
    }
}
