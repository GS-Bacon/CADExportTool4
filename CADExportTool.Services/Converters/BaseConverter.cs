using CADExportTool.Core.Enums;
using CADExportTool.Core.Interfaces;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace CADExportTool.Services.Converters;

/// <summary>
/// コンバーター基底クラス
/// </summary>
public abstract class BaseConverter : IFileConverter
{
    /// <inheritdoc/>
    public abstract CadFileType SupportedFileType { get; }

    /// <inheritdoc/>
    public abstract IReadOnlyList<ExportFormat> SupportedFormats { get; }

    /// <inheritdoc/>
    public async Task<string?> ConvertAsync(
        ISolidWorksService solidWorksService,
        string filePath,
        string outputFolder,
        ExportFormat format,
        CancellationToken cancellationToken = default)
    {
        if (!SupportedFormats.Contains(format))
            return null;

        cancellationToken.ThrowIfCancellationRequested();

        // ドキュメントを開く
        var document = await OpenDocumentAsync(solidWorksService, filePath);
        if (document is not ModelDoc2 modelDoc)
            return null;

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            // 出力ファイルパスを生成
            var outputExtension = GetExtension(format);
            var outputFileName = Path.ChangeExtension(Path.GetFileName(filePath), outputExtension);
            var outputPath = Path.Combine(outputFolder, outputFileName);

            // 出力フォルダが存在しない場合は作成
            Directory.CreateDirectory(outputFolder);

            // 保存
            var success = await solidWorksService.SaveAsAsync(modelDoc, outputPath, outputExtension);

            return success ? outputPath : null;
        }
        finally
        {
            // ドキュメントを閉じる
            solidWorksService.CloseDocument(filePath);
        }
    }

    /// <summary>
    /// ドキュメントを開く
    /// </summary>
    protected abstract Task<object?> OpenDocumentAsync(ISolidWorksService solidWorksService, string filePath);

    /// <summary>
    /// フォーマットから拡張子を取得
    /// </summary>
    protected static string GetExtension(ExportFormat format) => format switch
    {
        ExportFormat.Pdf => ".pdf",
        ExportFormat.Dxf => ".dxf",
        ExportFormat.Igs => ".igs",
        ExportFormat.Step => ".step",
        ExportFormat.ThreeMf => ".3mf",
        _ => throw new ArgumentOutOfRangeException(nameof(format))
    };
}
