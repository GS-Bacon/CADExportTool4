using SolidworksAPIAPI;
namespace TestProject2
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SolidworksAPIAPI.GetThumbnail getThumbnail = new SolidworksAPIAPI.GetThumbnail();
            getThumbnail.GetAllImg("C:\\Users\\bacon\\OneDrive\\ドキュメント\\SMZ\\Arma3.0\\02(Arma3.0チコニー用試作1号機)\\全体調整\\ステップ\\フロントステップ");
        }
    }
}
