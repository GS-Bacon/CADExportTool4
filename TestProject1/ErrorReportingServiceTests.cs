namespace TestProject1;

[TestClass]
public sealed class ErrorReportingServiceTests
{
    [TestMethod]
    public void TruncateString_ShortString_ReturnsOriginal()
    {
        // Arrange
        var input = "Short string";
        var maxLength = 50;

        // Act
        var result = TruncateString(input, maxLength);

        // Assert
        Assert.AreEqual(input, result);
    }

    [TestMethod]
    public void TruncateString_LongString_TruncatesWithEllipsis()
    {
        // Arrange
        var input = "This is a very long string that exceeds the maximum length";
        var maxLength = 20;

        // Act
        var result = TruncateString(input, maxLength);

        // Assert
        Assert.AreEqual(20, result.Length);
        Assert.IsTrue(result.EndsWith("..."));
    }

    [TestMethod]
    public void TruncateString_ExactLength_ReturnsOriginal()
    {
        // Arrange
        var input = "Exactly20Char!!!!!!!";
        var maxLength = 20;

        // Act
        var result = TruncateString(input, maxLength);

        // Assert
        Assert.AreEqual(input, result);
    }

    [TestMethod]
    public void TruncateString_EmptyString_ReturnsEmpty()
    {
        // Arrange
        var input = "";
        var maxLength = 20;

        // Act
        var result = TruncateString(input, maxLength);

        // Assert
        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void TruncateString_NullString_ReturnsNull()
    {
        // Arrange
        string? input = null;
        var maxLength = 20;

        // Act
        var result = TruncateString(input!, maxLength);

        // Assert
        Assert.IsNull(result);
    }

    // Copy of TruncateString logic from ErrorReportingService for testing
    private static string TruncateString(string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str) || str.Length <= maxLength)
        {
            return str;
        }

        return str[..(maxLength - 3)] + "...";
    }
}
