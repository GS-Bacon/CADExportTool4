using CADExportTool.Core.Enums;
using CADExportTool.Core.Interfaces;
using CADExportTool.Core.Models;
using CADExportTool.Services;
using Moq;

namespace CADExportTool.Tests.Services;

[TestClass]
public sealed class ExportServiceTests
{
    private Mock<ISolidWorksService> _mockSolidWorks = null!;
    private Mock<IThumbnailService> _mockThumbnail = null!;
    private Mock<IZipService> _mockZip = null!;
    private Mock<IFileConverterFactory> _mockConverterFactory = null!;
    private Mock<IFileConverter> _mockConverter = null!;
    private ExportService _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockSolidWorks = new Mock<ISolidWorksService>();
        _mockThumbnail = new Mock<IThumbnailService>();
        _mockZip = new Mock<IZipService>();
        _mockConverterFactory = new Mock<IFileConverterFactory>();
        _mockConverter = new Mock<IFileConverter>();

        // デフォルト: SolidWorks接続成功
        _mockSolidWorks.Setup(x => x.ConnectAsync()).ReturnsAsync(true);

        // デフォルト: コンバーター取得成功
        _mockConverterFactory
            .Setup(x => x.GetConverter(It.IsAny<CadFileType>()))
            .Returns(_mockConverter.Object);

        _sut = new ExportService(
            _mockSolidWorks.Object,
            _mockThumbnail.Object,
            _mockZip.Object,
            _mockConverterFactory.Object);
    }

    #region CalculateTotalTasks Tests

    [TestMethod]
    public void CalculateTotalTasks_NoFiles_ReturnsZero()
    {
        // Arrange
        var files = new List<CadFileItem>();
        var options = new ExportOptions();

        // Act
        var result = _sut.CalculateTotalTasks(files, options);

        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void CalculateTotalTasks_DrawingWithPdf_ReturnsOne()
    {
        // Arrange
        var files = new List<CadFileItem>
        {
            CreateDrawingFile("test.SLDDRW")
        };
        var options = new ExportOptions { ExportPdf = true };

        // Act
        var result = _sut.CalculateTotalTasks(files, options);

        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void CalculateTotalTasks_DrawingWithPdfAndDxf_ReturnsTwo()
    {
        // Arrange
        var files = new List<CadFileItem>
        {
            CreateDrawingFile("test.SLDDRW")
        };
        var options = new ExportOptions { ExportPdf = true, ExportDxf = true };

        // Act
        var result = _sut.CalculateTotalTasks(files, options);

        // Assert
        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void CalculateTotalTasks_PartWithAllFormats_ReturnsThree()
    {
        // Arrange
        var files = new List<CadFileItem>
        {
            CreatePartFile("test.SLDPRT")
        };
        var options = new ExportOptions
        {
            ExportIgs = true,
            ExportStep = true,
            Export3mf = true
        };

        // Act
        var result = _sut.CalculateTotalTasks(files, options);

        // Assert
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void CalculateTotalTasks_WithThumbnail_IncludesThumbnailCount()
    {
        // Arrange
        var files = new List<CadFileItem>
        {
            CreateDrawingFile("test1.SLDDRW"),
            CreatePartFile("test2.SLDPRT")
        };
        var options = new ExportOptions
        {
            ExportPdf = true,
            ExportStep = true,
            CreateThumbnail = true
        };

        // Act
        var result = _sut.CalculateTotalTasks(files, options);

        // Assert
        // 1 PDF + 1 STEP + 2 thumbnails = 4
        Assert.AreEqual(4, result);
    }

    [TestMethod]
    public void CalculateTotalTasks_MixedFiles_CalculatesCorrectly()
    {
        // Arrange
        var files = new List<CadFileItem>
        {
            CreateDrawingFile("draw1.SLDDRW"),
            CreateDrawingFile("draw2.SLDDRW"),
            CreatePartFile("part1.SLDPRT"),
            CreateAssemblyFile("assy1.SLDASM")
        };
        var options = new ExportOptions
        {
            ExportPdf = true,   // 2 drawings
            ExportDxf = true,   // 2 drawings
            ExportStep = true,  // 1 part + 1 assembly
            CreateThumbnail = true // 4 files
        };

        // Act
        var result = _sut.CalculateTotalTasks(files, options);

        // Assert
        // 2 PDF + 2 DXF + 2 STEP + 4 thumbnails = 10
        Assert.AreEqual(10, result);
    }

    #endregion

    #region ExportAsync Tests

    [TestMethod]
    public async Task ExportAsync_SolidWorksConnectionFailed_ReturnsError()
    {
        // Arrange
        _mockSolidWorks.Setup(x => x.ConnectAsync()).ReturnsAsync(false);
        var files = new List<CadFileItem> { CreateDrawingFile("test.SLDDRW") };
        var options = new ExportOptions { ExportPdf = true };

        // Act
        var result = await _sut.ExportAsync(files, options);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsTrue(result.Errors.Any(e => e.Contains("SolidWorks")));
    }

    [TestMethod]
    public async Task ExportAsync_EmptyFileList_ReturnsSuccess()
    {
        // Arrange
        var files = new List<CadFileItem>();
        var options = new ExportOptions { ExportPdf = true };

        // Act
        var result = await _sut.ExportAsync(files, options);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.ProcessedCount);
    }

    [TestMethod]
    public async Task ExportAsync_CancellationRequested_SetsCancelledFlag()
    {
        // Arrange
        var files = new List<CadFileItem>
        {
            CreateDrawingFile("test1.SLDDRW"),
            CreateDrawingFile("test2.SLDDRW")
        };
        var options = new ExportOptions { ExportPdf = true };
        var cts = new CancellationTokenSource();

        // 変換を遅延させてキャンセルを可能にする
        _mockConverter
            .Setup(x => x.ConvertAsync(
                It.IsAny<ISolidWorksService>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<ExportFormat>(),
                It.IsAny<CancellationToken>()))
            .Returns<ISolidWorksService, string, string, ExportFormat, CancellationToken>(
                async (_, _, _, _, ct) =>
                {
                    cts.Cancel(); // 最初の変換中にキャンセル
                    await Task.Delay(10, ct);
                    return "output.pdf";
                });

        // Act
        var result = await _sut.ExportAsync(files, options, null, cts.Token);

        // Assert
        Assert.IsTrue(result.WasCancelled);
    }

    [TestMethod]
    public async Task ExportAsync_SuccessfulConversion_IncrementsSuccessCount()
    {
        // Arrange
        var files = new List<CadFileItem> { CreateDrawingFile("test.SLDDRW") };
        var options = new ExportOptions { ExportPdf = true };

        _mockConverter
            .Setup(x => x.ConvertAsync(
                It.IsAny<ISolidWorksService>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<ExportFormat>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync("/output/test.pdf");

        // Act
        var result = await _sut.ExportAsync(files, options);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(1, result.SuccessCount);
        Assert.AreEqual(0, result.FailedCount);
    }

    [TestMethod]
    public async Task ExportAsync_FailedConversion_IncrementsFailedCount()
    {
        // Arrange
        var files = new List<CadFileItem> { CreateDrawingFile("test.SLDDRW") };
        var options = new ExportOptions { ExportPdf = true };

        _mockConverter
            .Setup(x => x.ConvertAsync(
                It.IsAny<ISolidWorksService>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<ExportFormat>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((string?)null);

        // Act
        var result = await _sut.ExportAsync(files, options);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(0, result.SuccessCount);
        Assert.AreEqual(1, result.FailedCount);
        Assert.IsTrue(result.Errors.Any(e => e.Contains("test.SLDDRW")));
    }

    [TestMethod]
    public async Task ExportAsync_ReportsProgress()
    {
        // Arrange
        var files = new List<CadFileItem> { CreateDrawingFile("test.SLDDRW") };
        var options = new ExportOptions { ExportPdf = true };
        var progressReports = new List<ExportProgress>();

        _mockConverter
            .Setup(x => x.ConvertAsync(
                It.IsAny<ISolidWorksService>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<ExportFormat>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync("/output/test.pdf");

        var progress = new Progress<ExportProgress>(p => progressReports.Add(p));

        // Act
        await _sut.ExportAsync(files, options, progress);

        // 少し待ってProgressの非同期報告を受け取る
        await Task.Delay(100);

        // Assert
        Assert.IsTrue(progressReports.Count > 0);
        Assert.IsTrue(progressReports.Any(p => p.StatusMessage.Contains("PDF")));
    }

    #endregion

    #region Helper Methods

    private static CadFileItem CreateDrawingFile(string fileName)
    {
        return new CadFileItem
        {
            FileName = fileName,
            FullPath = $"/test/{fileName}",
            FileType = CadFileType.Drawing
        };
    }

    private static CadFileItem CreatePartFile(string fileName)
    {
        return new CadFileItem
        {
            FileName = fileName,
            FullPath = $"/test/{fileName}",
            FileType = CadFileType.Part
        };
    }

    private static CadFileItem CreateAssemblyFile(string fileName)
    {
        return new CadFileItem
        {
            FileName = fileName,
            FullPath = $"/test/{fileName}",
            FileType = CadFileType.Assembly
        };
    }

    #endregion
}
