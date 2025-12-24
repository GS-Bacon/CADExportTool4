using CADExportTool.Core.Interfaces;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Runtime.InteropServices;

namespace CADExportTool.Services;

/// <summary>
/// SolidWorks接続管理サービス（シングルトン）
/// </summary>
public class SolidWorksService : ISolidWorksService
{
    private readonly Lazy<SldWorks> _application;
    private readonly object _lockObject = new();
    private bool _disposed;

    public SolidWorksService()
    {
        _application = new Lazy<SldWorks>(() => new SldWorks());
    }

    /// <summary>SolidWorksアプリケーション</summary>
    private SldWorks Application => _application.Value;

    /// <inheritdoc/>
    public bool IsConnected => _application.IsValueCreated;

    /// <inheritdoc/>
    public Task<bool> ConnectAsync()
    {
        return Task.Run(() =>
        {
            try
            {
                // アプリケーションにアクセスして接続を確立
                _ = Application;
                return true;
            }
            catch
            {
                return false;
            }
        });
    }

    /// <inheritdoc/>
    public Task<object?> OpenDrawingAsync(string filePath)
    {
        return Task.Run(() => OpenDocument(filePath, swDocumentTypes_e.swDocDRAWING));
    }

    /// <inheritdoc/>
    public Task<object?> OpenPartAsync(string filePath)
    {
        return Task.Run(() => OpenDocument(filePath, swDocumentTypes_e.swDocPART));
    }

    /// <inheritdoc/>
    public Task<object?> OpenAssemblyAsync(string filePath)
    {
        return Task.Run(() => OpenDocument(filePath, swDocumentTypes_e.swDocASSEMBLY));
    }

    private object? OpenDocument(string filePath, swDocumentTypes_e documentType)
    {
        lock (_lockObject)
        {
            try
            {
                int fileError = 0;
                int fileWarning = 0;

                var document = Application.OpenDoc6(
                    filePath,
                    (int)documentType,
                    (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                    "",
                    ref fileError,
                    ref fileWarning);

                // エラーコードをログ出力
                if (fileError != 0)
                {
                    var errorMessage = GetFileLoadErrorMessage(fileError);
                    System.Diagnostics.Debug.WriteLine($"OpenDoc6 Error: {errorMessage} (Code: {fileError}) - File: {filePath}");

                    // 重大なエラーの場合はnullを返す
                    if (IsCriticalFileLoadError(fileError))
                    {
                        System.Diagnostics.Debug.WriteLine($"Critical error occurred, returning null");
                        return null;
                    }
                }

                // 警告コードをログ出力
                if (fileWarning != 0)
                {
                    var warningMessage = GetFileLoadWarningMessage(fileWarning);
                    System.Diagnostics.Debug.WriteLine($"OpenDoc6 Warning: {warningMessage} (Code: {fileWarning}) - File: {filePath}");
                }

                return document;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception opening document: {ex.Message} - File: {filePath}");
                return null;
            }
        }
    }

    /// <summary>
    /// ファイル読み込みエラーコードからメッセージを取得
    /// </summary>
    private static string GetFileLoadErrorMessage(int errorCode)
    {
        // swFileLoadError_e の値
        return errorCode switch
        {
            0 => "成功",
            1 => "ファイルが見つかりません",
            2 => "ファイルを開けません（別のアプリケーションで使用中）",
            3 => "ファイルが不正です",
            4 => "古いバージョンのファイルです",
            5 => "新しいバージョンのファイルです",
            6 => "参照ファイルが見つかりません",
            7 => "参照ファイルのバージョンが異なります",
            8 => "メモリ不足",
            9 => "アクセスが拒否されました",
            10 => "ファイルが読み取り専用です",
            11 => "不明なファイル形式です",
            _ => $"不明なエラー (Code: {errorCode})"
        };
    }

    /// <summary>
    /// ファイル読み込み警告コードからメッセージを取得
    /// </summary>
    private static string GetFileLoadWarningMessage(int warningCode)
    {
        // swFileLoadWarning_e の値
        return warningCode switch
        {
            0 => "警告なし",
            1 => "アイドル状態ではありません",
            2 => "参照の解決に失敗しました",
            4 => "図面シートが不足しています",
            8 => "ビューが不足しています",
            16 => "バージョン不一致の参照があります",
            32 => "読み取り専用です",
            64 => "軽量モードで開きました",
            128 => "AutoRecoverファイルです",
            _ => $"不明な警告 (Code: {warningCode})"
        };
    }

    /// <summary>
    /// 重大なファイル読み込みエラーかどうかを判定
    /// </summary>
    private static bool IsCriticalFileLoadError(int errorCode)
    {
        return errorCode switch
        {
            1 => true,  // ファイルが見つからない
            3 => true,  // ファイルが不正
            8 => true,  // メモリ不足
            9 => true,  // アクセス拒否
            11 => true, // 不明なファイル形式
            _ => false
        };
    }

    /// <inheritdoc/>
    public Task<bool> SaveAsAsync(object document, string outputPath, string format)
    {
        return Task.Run(() =>
        {
            lock (_lockObject)
            {
                try
                {
                    if (document is not ModelDoc2 modelDoc)
                        return false;

                    var extension = modelDoc.Extension;
                    if (extension == null)
                        return false;

                    int error = 0;
                    int warning = 0;

                    var success = extension.SaveAs3(
                        outputPath,
                        (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                        (int)swSaveAsOptions_e.swSaveAsOptions_Silent,
                        null,
                        null,
                        ref error,
                        ref warning);

                    // エラーコードをログ出力
                    if (error != 0)
                    {
                        var errorMessage = GetFileSaveErrorMessage(error);
                        System.Diagnostics.Debug.WriteLine($"SaveAs3 Error: {errorMessage} (Code: {error}) - File: {outputPath}");
                    }

                    // 警告コードをログ出力
                    if (warning != 0)
                    {
                        var warningMessage = GetFileSaveWarningMessage(warning);
                        System.Diagnostics.Debug.WriteLine($"SaveAs3 Warning: {warningMessage} (Code: {warning}) - File: {outputPath}");
                    }

                    return success;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception saving document: {ex.Message} - File: {outputPath}");
                    return false;
                }
            }
        });
    }

    /// <summary>
    /// ファイル保存エラーコードからメッセージを取得
    /// </summary>
    private static string GetFileSaveErrorMessage(int errorCode)
    {
        // swFileSaveError_e の値
        return errorCode switch
        {
            0 => "成功",
            1 => "一般的なエラーが発生しました",
            2 => "ファイルが読み取り専用です",
            4 => "ファイルが別のアプリケーションで使用中です",
            8 => "ファイルの書き込みに失敗しました",
            16 => "ディスク容量が不足しています",
            32 => "ファイル名が無効です",
            64 => "ドキュメントが保存されていません",
            128 => "保存中にドキュメントが変更されました",
            _ => $"不明なエラー (Code: {errorCode})"
        };
    }

    /// <summary>
    /// ファイル保存警告コードからメッセージを取得
    /// </summary>
    private static string GetFileSaveWarningMessage(int warningCode)
    {
        // swFileSaveWarning_e の値
        return warningCode switch
        {
            0 => "警告なし",
            1 => "再構築が必要です",
            2 => "参照の更新が必要です",
            4 => "ビューの更新が必要です",
            _ => $"不明な警告 (Code: {warningCode})"
        };
    }

    /// <inheritdoc/>
    public void CloseDocument(string filePath)
    {
        lock (_lockObject)
        {
            try
            {
                Application.CloseDoc(filePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error closing document: {ex.Message}");
            }
        }
    }

    /// <inheritdoc/>
    public void CloseAllDocuments()
    {
        lock (_lockObject)
        {
            try
            {
                Application.CloseAllDocuments(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error closing all documents: {ex.Message}");
            }
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed) return;

        if (_application.IsValueCreated)
        {
            try
            {
                // 全ドキュメントを閉じる
                CloseAllDocuments();

                // SldWorksインスタンスを取得
                var sldWorks = _application.Value;
                if (sldWorks != null)
                {
                    // COMオブジェクトを解放
                    Marshal.ReleaseComObject(sldWorks);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during disposal: {ex.Message}");
            }
            finally
            {
                // ガベージコレクションを強制実行してCOMオブジェクトを確実に解放
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
