using System.Windows;
using MaterialDesignThemes.Wpf;

namespace CADExportTool.WPF.Services;

/// <summary>
/// トースト通知サービス（Material Design Snackbar使用）
/// </summary>
public interface IToastNotificationService
{
    void ShowSuccess(string message);
    void ShowError(string message);
    void ShowInfo(string message);
}

/// <summary>
/// Snackbarを使用したトースト通知の実装
/// </summary>
public class ToastNotificationService : IToastNotificationService
{
    private readonly ISnackbarMessageQueue _messageQueue;

    public ToastNotificationService(ISnackbarMessageQueue messageQueue)
    {
        _messageQueue = messageQueue;
    }

    public void ShowSuccess(string message)
    {
        _messageQueue.Enqueue(message, "OK", _ => { }, null, false, true, TimeSpan.FromSeconds(5));
    }

    public void ShowError(string message)
    {
        _messageQueue.Enqueue($"エラー: {message}", "閉じる", _ => { }, null, false, true, TimeSpan.FromSeconds(8));
    }

    public void ShowInfo(string message)
    {
        _messageQueue.Enqueue(message, null, null, null, false, true, TimeSpan.FromSeconds(3));
    }
}
