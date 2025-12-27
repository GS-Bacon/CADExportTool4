using CADExportTool.Core.Enums;
using CADExportTool.Core.Models;

namespace CADExportTool.Tests.Models;

[TestClass]
public sealed class CadFileItemTests
{
    [TestMethod]
    public void FromPath_DrawingFile_SetsCorrectProperties()
    {
        // Arrange
        var path = @"C:\Test\Sample.SLDDRW";

        // Act
        var result = CadFileItem.FromPath(path);

        // Assert
        Assert.AreEqual("Sample.SLDDRW", result.FileName);
        Assert.AreEqual(path, result.FullPath);
        Assert.AreEqual(CadFileType.Drawing, result.FileType);
    }

    [TestMethod]
    public void FromPath_PartFile_SetsCorrectFileType()
    {
        // Arrange
        var path = @"C:\Test\Sample.SLDPRT";

        // Act
        var result = CadFileItem.FromPath(path);

        // Assert
        Assert.AreEqual(CadFileType.Part, result.FileType);
    }

    [TestMethod]
    public void FromPath_AssemblyFile_SetsCorrectFileType()
    {
        // Arrange
        var path = @"C:\Test\Sample.SLDASM";

        // Act
        var result = CadFileItem.FromPath(path);

        // Assert
        Assert.AreEqual(CadFileType.Assembly, result.FileType);
    }

    [TestMethod]
    public void FromPath_LowercaseExtension_RecognizesFileType()
    {
        // Arrange
        var path = @"C:\Test\sample.slddrw";

        // Act
        var result = CadFileItem.FromPath(path);

        // Assert
        Assert.AreEqual(CadFileType.Drawing, result.FileType);
    }

    [TestMethod]
    public void FromPath_UnknownExtension_SetsUnknownType()
    {
        // Arrange
        var path = @"C:\Test\sample.txt";

        // Act
        var result = CadFileItem.FromPath(path);

        // Assert
        Assert.AreEqual(CadFileType.Unknown, result.FileType);
    }

    [TestMethod]
    public void GetFileType_AllSupportedExtensions_ReturnsCorrectType()
    {
        // Assert
        Assert.AreEqual(CadFileType.Drawing, CadFileItem.GetFileType(".SLDDRW"));
        Assert.AreEqual(CadFileType.Part, CadFileItem.GetFileType(".SLDPRT"));
        Assert.AreEqual(CadFileType.Assembly, CadFileItem.GetFileType(".SLDASM"));
        Assert.AreEqual(CadFileType.Unknown, CadFileItem.GetFileType(".PDF"));
    }

    [TestMethod]
    public void IsSelected_DefaultValue_IsFalse()
    {
        // Arrange & Act
        var item = new CadFileItem();

        // Assert
        Assert.IsFalse(item.IsSelected);
    }

    [TestMethod]
    public void IsSelected_CanBeSet()
    {
        // Arrange
        var item = new CadFileItem();

        // Act
        item.IsSelected = true;

        // Assert
        Assert.IsTrue(item.IsSelected);
    }
}
