namespace CADExportTool.Tests;

[TestClass]
public sealed class UpdateServiceTests
{
    [TestMethod]
    public void CompareVersions_SameVersion_ReturnsZero()
    {
        // Arrange
        var v1 = "v1.0.0";
        var v2 = "v1.0.0";

        // Act
        var result = CompareVersions(v1, v2);

        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void CompareVersions_NewerMajorVersion_ReturnsPositive()
    {
        // Arrange
        var v1 = "v2.0.0";
        var v2 = "v1.0.0";

        // Act
        var result = CompareVersions(v1, v2);

        // Assert
        Assert.IsTrue(result > 0);
    }

    [TestMethod]
    public void CompareVersions_NewerMinorVersion_ReturnsPositive()
    {
        // Arrange
        var v1 = "v1.1.0";
        var v2 = "v1.0.0";

        // Act
        var result = CompareVersions(v1, v2);

        // Assert
        Assert.IsTrue(result > 0);
    }

    [TestMethod]
    public void CompareVersions_NewerPatchVersion_ReturnsPositive()
    {
        // Arrange
        var v1 = "v1.0.1";
        var v2 = "v1.0.0";

        // Act
        var result = CompareVersions(v1, v2);

        // Assert
        Assert.IsTrue(result > 0);
    }

    [TestMethod]
    public void CompareVersions_OlderVersion_ReturnsNegative()
    {
        // Arrange
        var v1 = "v1.0.0";
        var v2 = "v2.0.0";

        // Act
        var result = CompareVersions(v1, v2);

        // Assert
        Assert.IsTrue(result < 0);
    }

    [TestMethod]
    public void CompareVersions_WithoutVPrefix_Works()
    {
        // Arrange
        var v1 = "1.1.0";
        var v2 = "v1.0.0";

        // Act
        var result = CompareVersions(v1, v2);

        // Assert
        Assert.IsTrue(result > 0);
    }

    [TestMethod]
    public void CompareVersions_NullVersions_ReturnsZero()
    {
        // Arrange & Act
        var result = CompareVersions(null, null);

        // Assert
        Assert.AreEqual(0, result);
    }

    // Copy of version comparison logic from UpdateService for testing
    private static int CompareVersions(string? version1, string? version2)
    {
        if (string.IsNullOrEmpty(version1) || string.IsNullOrEmpty(version2))
        {
            return 0;
        }

        var v1 = version1.TrimStart('v', 'V');
        var v2 = version2.TrimStart('v', 'V');

        var parts1 = v1.Split('.').Select(p => int.TryParse(p, out var n) ? n : 0).ToArray();
        var parts2 = v2.Split('.').Select(p => int.TryParse(p, out var n) ? n : 0).ToArray();

        var maxLength = Math.Max(parts1.Length, parts2.Length);

        for (int i = 0; i < maxLength; i++)
        {
            var p1 = i < parts1.Length ? parts1[i] : 0;
            var p2 = i < parts2.Length ? parts2[i] : 0;

            if (p1 > p2) return 1;
            if (p1 < p2) return -1;
        }

        return 0;
    }
}
