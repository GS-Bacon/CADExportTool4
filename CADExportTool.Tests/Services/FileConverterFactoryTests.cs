using CADExportTool.Core.Enums;
using CADExportTool.Services;
using CADExportTool.Services.Converters;

namespace CADExportTool.Tests.Services;

[TestClass]
public sealed class FileConverterFactoryTests
{
    private FileConverterFactory _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _sut = new FileConverterFactory();
    }

    [TestMethod]
    public void GetConverter_Drawing_ReturnsDrawingConverter()
    {
        // Act
        var result = _sut.GetConverter(CadFileType.Drawing);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(DrawingConverter));
    }

    [TestMethod]
    public void GetConverter_Part_ReturnsPartConverter()
    {
        // Act
        var result = _sut.GetConverter(CadFileType.Part);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(PartConverter));
    }

    [TestMethod]
    public void GetConverter_Assembly_ReturnsAssemblyConverter()
    {
        // Act
        var result = _sut.GetConverter(CadFileType.Assembly);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(AssemblyConverter));
    }

    [TestMethod]
    public void GetConverter_Unknown_ThrowsNotSupportedException()
    {
        // Act & Assert
        Assert.ThrowsException<NotSupportedException>(() =>
        {
            _sut.GetConverter(CadFileType.Unknown);
        });
    }

    [TestMethod]
    public void GetConverter_SameType_ReturnsSameInstance()
    {
        // Act
        var first = _sut.GetConverter(CadFileType.Drawing);
        var second = _sut.GetConverter(CadFileType.Drawing);

        // Assert
        Assert.AreSame(first, second, "Factory should return cached instance");
    }

    [TestMethod]
    public void DrawingConverter_SupportsPdfAndDxf()
    {
        // Arrange
        var converter = _sut.GetConverter(CadFileType.Drawing);

        // Assert
        Assert.AreEqual(CadFileType.Drawing, converter.SupportedFileType);
        CollectionAssert.Contains(converter.SupportedFormats.ToList(), ExportFormat.Pdf);
        CollectionAssert.Contains(converter.SupportedFormats.ToList(), ExportFormat.Dxf);
    }

    [TestMethod]
    public void PartConverter_SupportsIgsStepAnd3mf()
    {
        // Arrange
        var converter = _sut.GetConverter(CadFileType.Part);

        // Assert
        Assert.AreEqual(CadFileType.Part, converter.SupportedFileType);
        CollectionAssert.Contains(converter.SupportedFormats.ToList(), ExportFormat.Igs);
        CollectionAssert.Contains(converter.SupportedFormats.ToList(), ExportFormat.Step);
        CollectionAssert.Contains(converter.SupportedFormats.ToList(), ExportFormat.ThreeMf);
    }

    [TestMethod]
    public void AssemblyConverter_SupportsIgsStepAnd3mf()
    {
        // Arrange
        var converter = _sut.GetConverter(CadFileType.Assembly);

        // Assert
        Assert.AreEqual(CadFileType.Assembly, converter.SupportedFileType);
        CollectionAssert.Contains(converter.SupportedFormats.ToList(), ExportFormat.Igs);
        CollectionAssert.Contains(converter.SupportedFormats.ToList(), ExportFormat.Step);
        CollectionAssert.Contains(converter.SupportedFormats.ToList(), ExportFormat.ThreeMf);
    }
}
