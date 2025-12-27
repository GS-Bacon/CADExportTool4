using CADExportTool.Core.Enums;
using CADExportTool.Core.Interfaces;
using CADExportTool.Core.Models;
using CADExportTool.Services;

namespace CADExportTool.Tests.Integration;

/// <summary>
/// SolidWorksを使用した統合テスト
/// 実行には SolidWorks がインストールされている必要があります
/// </summary>
[TestClass]
[TestCategory("Integration")]
public sealed class SolidWorksExportTests
{
    private static readonly string TestDataPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "..", "..", "..", "..", "testdata");

    private static readonly string InputPath = Path.Combine(TestDataPath, "input");
    private static readonly string OutputPath = Path.Combine(TestDataPath, "output");

    private ISolidWorksService? _solidWorksService;
    private IExportService? _exportService;

    [TestInitialize]
    public void Setup()
    {
        // 出力ディレクトリをクリーンアップ
        if (Directory.Exists(OutputPath))
        {
            Directory.Delete(OutputPath, true);
        }
        Directory.CreateDirectory(OutputPath);

        // サービスを初期化
        _solidWorksService = new SolidWorksService();
        var thumbnailService = new ThumbnailService();
        var zipService = new ZipService();
        var converterFactory = new FileConverterFactory();

        _exportService = new ExportService(
            _solidWorksService,
            thumbnailService,
            zipService,
            converterFactory);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _solidWorksService?.Dispose();
    }

    [TestMethod]
    [Ignore("SolidWorksがインストールされている環境でのみ実行")]
    public async Task ExportDrawing_ToPdf_CreatesFile()
    {
        // Arrange
        var drawingPath = Path.Combine(InputPath, "sample.SLDDRW");
        SkipIfFileNotExists(drawingPath);

        var files = new List<CadFileItem> { CadFileItem.FromPath(drawingPath) };
        var options = new ExportOptions
        {
            ExportPdf = true,
            OutputFolderOption = FolderOption.CustomFolder,
            CustomFolderPath = OutputPath
        };

        // Act
        var result = await _exportService!.ExportAsync(files, options);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Export failed: {string.Join(", ", result.Errors)}");
        Assert.AreEqual(1, result.SuccessCount);

        var pdfPath = Path.Combine(OutputPath, "PDF", "sample.pdf");
        Assert.IsTrue(File.Exists(pdfPath), $"PDF file not found at {pdfPath}");
    }

    [TestMethod]
    [Ignore("SolidWorksがインストールされている環境でのみ実行")]
    public async Task ExportPart_ToStep_CreatesFile()
    {
        // Arrange
        var partPath = Path.Combine(InputPath, "sample.SLDPRT");
        SkipIfFileNotExists(partPath);

        var files = new List<CadFileItem> { CadFileItem.FromPath(partPath) };
        var options = new ExportOptions
        {
            ExportStep = true,
            OutputFolderOption = FolderOption.CustomFolder,
            CustomFolderPath = OutputPath
        };

        // Act
        var result = await _exportService!.ExportAsync(files, options);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Export failed: {string.Join(", ", result.Errors)}");
        Assert.AreEqual(1, result.SuccessCount);

        var stepPath = Path.Combine(OutputPath, "STEP", "sample.step");
        Assert.IsTrue(File.Exists(stepPath), $"STEP file not found at {stepPath}");
    }

    [TestMethod]
    [Ignore("SolidWorksがインストールされている環境でのみ実行")]
    public async Task ExportMultipleFormats_CreatesAllFiles()
    {
        // Arrange
        var partPath = Path.Combine(InputPath, "sample.SLDPRT");
        SkipIfFileNotExists(partPath);

        var files = new List<CadFileItem> { CadFileItem.FromPath(partPath) };
        var options = new ExportOptions
        {
            ExportIgs = true,
            ExportStep = true,
            Export3mf = true,
            OutputFolderOption = FolderOption.CustomFolder,
            CustomFolderPath = OutputPath
        };

        // Act
        var result = await _exportService!.ExportAsync(files, options);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Export failed: {string.Join(", ", result.Errors)}");
        Assert.AreEqual(3, result.SuccessCount);
    }

    [TestMethod]
    [Ignore("SolidWorksがインストールされている環境でのみ実行")]
    public async Task ExportWithThumbnail_CreatesThumbnailFile()
    {
        // Arrange
        var partPath = Path.Combine(InputPath, "sample.SLDPRT");
        SkipIfFileNotExists(partPath);

        var files = new List<CadFileItem> { CadFileItem.FromPath(partPath) };
        var options = new ExportOptions
        {
            ExportStep = true,
            CreateThumbnail = true,
            OutputFolderOption = FolderOption.CustomFolder,
            CustomFolderPath = OutputPath
        };

        // Act
        var result = await _exportService!.ExportAsync(files, options);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        var thumbnailPath = Path.Combine(OutputPath, "Thumbnail", "sample.png");
        Assert.IsTrue(File.Exists(thumbnailPath), $"Thumbnail not found at {thumbnailPath}");
    }

    private static void SkipIfFileNotExists(string path)
    {
        if (!File.Exists(path))
        {
            Assert.Inconclusive($"Test file not found: {path}. Place test files in testdata/input/");
        }
    }
}
