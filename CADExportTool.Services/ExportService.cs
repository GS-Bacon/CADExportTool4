using CADExportTool.Core.Enums;
using CADExportTool.Core.Interfaces;
using CADExportTool.Core.Models;

namespace CADExportTool.Services;

/// <summary>
/// エクスポートサービス
/// </summary>
public class ExportService : IExportService
{
    private readonly ISolidWorksService _solidWorksService;
    private readonly IThumbnailService _thumbnailService;
    private readonly IZipService _zipService;
    private readonly IFileConverterFactory _converterFactory;

    // 進捗カウンター（スレッドセーフのためlock使用）
    private int _currentTask;
    private readonly object _progressLock = new();

    public ExportService(
        ISolidWorksService solidWorksService,
        IThumbnailService thumbnailService,
        IZipService zipService,
        IFileConverterFactory converterFactory)
    {
        _solidWorksService = solidWorksService;
        _thumbnailService = thumbnailService;
        _zipService = zipService;
        _converterFactory = converterFactory;
    }

    /// <inheritdoc/>
    public async Task<ExportResult> ExportAsync(
        IEnumerable<CadFileItem> files,
        ExportOptions options,
        IProgress<ExportProgress>? progress = null,
        CancellationToken cancellationToken = default)
    {
        var result = new ExportResult();
        var fileList = files.ToList();
        var totalTasks = CalculateTotalTasks(fileList, options);
        _currentTask = 0;

        // SolidWorksに接続
        if (!await _solidWorksService.ConnectAsync())
        {
            result.Errors.Add("SolidWorksに接続できませんでした");
            return result;
        }

        try
        {
            foreach (var file in fileList)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var baseFolder = GetOutputFolder(file.FullPath, options);

                // ファイルタイプに応じて変換
                switch (file.FileType)
                {
                    case CadFileType.Drawing:
                        await ProcessDrawingAsync(file, options, baseFolder, result, progress, totalTasks, cancellationToken);
                        break;

                    case CadFileType.Part:
                        await ProcessPartAsync(file, options, baseFolder, result, progress, totalTasks, cancellationToken);
                        break;

                    case CadFileType.Assembly:
                        await ProcessAssemblyAsync(file, options, baseFolder, result, progress, totalTasks, cancellationToken);
                        break;
                }

                // サムネイル生成
                if (options.CreateThumbnail)
                {
                    await ProcessThumbnailAsync(file, baseFolder, result, progress, totalTasks, cancellationToken);
                }

                result.ProcessedCount++;
            }

            result.IsSuccess = result.FailedCount == 0;
        }
        catch (OperationCanceledException)
        {
            result.WasCancelled = true;
        }
        catch (Exception ex)
        {
            result.Errors.Add($"エクスポート中にエラーが発生しました: {ex.Message}");
        }

        return result;
    }

    private async Task ProcessDrawingAsync(
        CadFileItem file,
        ExportOptions options,
        string baseFolder,
        ExportResult result,
        IProgress<ExportProgress>? progress,
        int totalTasks,
        CancellationToken cancellationToken)
    {
        var converter = _converterFactory.GetConverter(CadFileType.Drawing);

        if (options.ExportPdf)
        {
            await ConvertFileAsync(converter, file, baseFolder, "PDF", ExportFormat.Pdf, result, progress, totalTasks, cancellationToken);
        }

        if (options.ExportDxf)
        {
            await ConvertFileAsync(converter, file, baseFolder, "DXF", ExportFormat.Dxf, result, progress, totalTasks, cancellationToken);
        }
    }

    private async Task ProcessPartAsync(
        CadFileItem file,
        ExportOptions options,
        string baseFolder,
        ExportResult result,
        IProgress<ExportProgress>? progress,
        int totalTasks,
        CancellationToken cancellationToken)
    {
        var converter = _converterFactory.GetConverter(CadFileType.Part);

        if (options.ExportIgs)
        {
            await ConvertFileAsync(converter, file, baseFolder, "IGS", ExportFormat.Igs, result, progress, totalTasks, cancellationToken);
        }

        if (options.ExportStep)
        {
            await ConvertFileAsync(converter, file, baseFolder, "STEP", ExportFormat.Step, result, progress, totalTasks, cancellationToken);
        }

        if (options.Export3mf)
        {
            await ConvertFileAsync(converter, file, baseFolder, "3MF", ExportFormat.ThreeMf, result, progress, totalTasks, cancellationToken);
        }
    }

    private async Task ProcessAssemblyAsync(
        CadFileItem file,
        ExportOptions options,
        string baseFolder,
        ExportResult result,
        IProgress<ExportProgress>? progress,
        int totalTasks,
        CancellationToken cancellationToken)
    {
        var converter = _converterFactory.GetConverter(CadFileType.Assembly);

        if (options.ExportIgs)
        {
            await ConvertFileAsync(converter, file, baseFolder, "IGS", ExportFormat.Igs, result, progress, totalTasks, cancellationToken);
        }

        if (options.ExportStep)
        {
            await ConvertFileAsync(converter, file, baseFolder, "STEP", ExportFormat.Step, result, progress, totalTasks, cancellationToken);
        }

        if (options.Export3mf)
        {
            await ConvertFileAsync(converter, file, baseFolder, "3MF", ExportFormat.ThreeMf, result, progress, totalTasks, cancellationToken);
        }
    }

    private async Task ConvertFileAsync(
        IFileConverter converter,
        CadFileItem file,
        string baseFolder,
        string formatName,
        ExportFormat format,
        ExportResult result,
        IProgress<ExportProgress>? progress,
        int totalTasks,
        CancellationToken cancellationToken)
    {
        int current;
        lock (_progressLock)
        {
            current = ++_currentTask;
        }

        ReportProgress(progress, current, totalTasks, file.FileName, $"{formatName}に変換中: {file.FileName}");

        var outputFolder = Path.Combine(baseFolder, formatName);
        var outputPath = await converter.ConvertAsync(_solidWorksService, file.FullPath, outputFolder, format, cancellationToken);

        if (outputPath != null)
        {
            result.OutputFiles.Add(outputPath);
            result.SuccessCount++;
        }
        else
        {
            result.FailedCount++;
            result.Errors.Add($"{file.FileName}の{formatName}変換に失敗しました");
        }
    }

    private async Task ProcessThumbnailAsync(
        CadFileItem file,
        string baseFolder,
        ExportResult result,
        IProgress<ExportProgress>? progress,
        int totalTasks,
        CancellationToken cancellationToken)
    {
        int current;
        lock (_progressLock)
        {
            current = ++_currentTask;
        }

        ReportProgress(progress, current, totalTasks, file.FileName, $"サムネイル取得中: {file.FileName}");

        var outputFolder = Path.Combine(baseFolder, "Thumbnail");
        var outputPath = await _thumbnailService.ExtractThumbnailAsync(file.FullPath, outputFolder, cancellationToken);

        if (outputPath != null)
        {
            result.OutputFiles.Add(outputPath);
        }
    }

    private string GetOutputFolder(string filePath, ExportOptions options)
    {
        var fileDir = Path.GetDirectoryName(filePath) ?? string.Empty;

        var outputFolder = options.OutputFolderOption switch
        {
            FolderOption.SameFolder => fileDir,
            FolderOption.SubFolder => Path.Combine(fileDir, options.SelectedSubFolder),
            FolderOption.CustomFolder => options.CustomFolderPath,
            _ => fileDir
        };

        // Path Traversal対策: 正規化して親ディレクトリ参照を解決
        var normalizedPath = Path.GetFullPath(outputFolder);

        // サブフォルダ選択時、元のファイルディレクトリ外へのアクセスを防止
        if (options.OutputFolderOption == FolderOption.SubFolder)
        {
            var normalizedFileDir = Path.GetFullPath(fileDir);
            if (!normalizedPath.StartsWith(normalizedFileDir, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Invalid subfolder path: attempting to access parent directory");
            }
        }

        return normalizedPath;
    }

    private void ReportProgress(IProgress<ExportProgress>? progress, int current, int total, string fileName, string message)
    {
        progress?.Report(new ExportProgress
        {
            Current = current,
            Total = total,
            CurrentFileName = fileName,
            StatusMessage = message
        });
    }

    /// <inheritdoc/>
    public int CalculateTotalTasks(IEnumerable<CadFileItem> files, ExportOptions options)
    {
        var fileList = files.ToList();
        var drawingCount = fileList.Count(f => f.FileType == CadFileType.Drawing);
        var partCount = fileList.Count(f => f.FileType == CadFileType.Part);
        var assemblyCount = fileList.Count(f => f.FileType == CadFileType.Assembly);

        var total = 0;

        // 図面フォーマット
        if (options.ExportPdf) total += drawingCount;
        if (options.ExportDxf) total += drawingCount;

        // パーツ/アセンブリフォーマット
        if (options.ExportIgs) total += partCount + assemblyCount;
        if (options.ExportStep) total += partCount + assemblyCount;
        if (options.Export3mf) total += partCount + assemblyCount;

        // サムネイル
        if (options.CreateThumbnail) total += fileList.Count;

        return total;
    }
}
