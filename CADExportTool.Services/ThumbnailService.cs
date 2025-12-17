using CADExportTool.Core.Interfaces;
using Microsoft.WindowsAPICodePack.Shell;

namespace CADExportTool.Services;

/// <summary>
/// サムネイル取得サービス
/// </summary>
public class ThumbnailService : IThumbnailService
{
    /// <inheritdoc/>
    public Task<string?> ExtractThumbnailAsync(
        string filePath,
        string outputFolder,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                // 出力フォルダが存在しない場合は作成
                Directory.CreateDirectory(outputFolder);

                var outputFileName = Path.ChangeExtension(Path.GetFileName(filePath), ".png");
                var outputPath = Path.Combine(outputFolder, outputFileName);

                using var shellFile = ShellFile.FromFilePath(filePath);
                var thumbnail = shellFile.Thumbnail.ExtraLargeBitmap;

                if (thumbnail != null)
                {
                    thumbnail.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                    return outputPath;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error extracting thumbnail: {ex.Message}");
                return null;
            }
        }, cancellationToken);
    }
}
