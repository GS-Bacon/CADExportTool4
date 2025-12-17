namespace CADExportTool.Core.Interfaces;

/// <summary>
/// SolidWorks接続管理サービス
/// </summary>
public interface ISolidWorksService : IDisposable
{
    /// <summary>SolidWorksに接続しているか</summary>
    bool IsConnected { get; }

    /// <summary>SolidWorksに接続</summary>
    Task<bool> ConnectAsync();

    /// <summary>図面ファイルを開く</summary>
    Task<object?> OpenDrawingAsync(string filePath);

    /// <summary>パーツファイルを開く</summary>
    Task<object?> OpenPartAsync(string filePath);

    /// <summary>アセンブリファイルを開く</summary>
    Task<object?> OpenAssemblyAsync(string filePath);

    /// <summary>ドキュメントを保存</summary>
    Task<bool> SaveAsAsync(object document, string outputPath, string format);

    /// <summary>ドキュメントを閉じる</summary>
    void CloseDocument(string filePath);

    /// <summary>すべてのドキュメントを閉じる</summary>
    void CloseAllDocuments();
}
