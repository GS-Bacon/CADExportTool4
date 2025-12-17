using CADExportTool.Core.Interfaces;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

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

                return document;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error opening document: {ex.Message}");
                return null;
            }
        }
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

                    return success;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error saving document: {ex.Message}");
                    return false;
                }
            }
        });
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
                CloseAllDocuments();
            }
            catch
            {
                // Ignore disposal errors
            }
        }

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
