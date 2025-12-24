using System.Windows;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace CADExportTool.WPF.Helpers;

/// <summary>
/// OSテーマとMaterial Designテーマを同期するヘルパークラス
/// </summary>
public static class ThemeHelper
{
    private static readonly PaletteHelper _paletteHelper = new();
    private static bool _isWatching;
    private static readonly object _lockObject = new();

    /// <summary>
    /// 現在のOSテーマがダークモードかどうかを取得
    /// </summary>
    public static bool IsSystemDarkTheme()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");

            if (key?.GetValue("AppsUseLightTheme") is int value)
            {
                return value == 0; // 0 = Dark, 1 = Light
            }
        }
        catch
        {
            // レジストリアクセスに失敗した場合はライトテーマをデフォルトとする
        }

        return false;
    }

    /// <summary>
    /// アプリケーションテーマを設定
    /// </summary>
    public static void SetTheme(bool isDark)
    {
        try
        {
            var theme = _paletteHelper.GetTheme();
            theme.SetBaseTheme(isDark ? BaseTheme.Dark : BaseTheme.Light);
            _paletteHelper.SetTheme(theme);
        }
        catch
        {
            // テーマ設定に失敗しても続行
        }
    }

    /// <summary>
    /// OSテーマに合わせてアプリケーションテーマを同期
    /// </summary>
    public static void SyncWithSystemTheme()
    {
        SetTheme(IsSystemDarkTheme());
    }

    /// <summary>
    /// システムテーマ変更イベントを監視開始
    /// </summary>
    public static void StartWatchingSystemTheme()
    {
        lock (_lockObject)
        {
            if (_isWatching) return;
            SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
            _isWatching = true;
        }
    }

    /// <summary>
    /// システムテーマ変更イベントの監視を停止
    /// </summary>
    public static void StopWatchingSystemTheme()
    {
        lock (_lockObject)
        {
            if (!_isWatching) return;
            try
            {
                SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
            }
            catch
            {
                // 解除に失敗しても続行
            }
            finally
            {
                _isWatching = false;
            }
        }
    }

    private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (e.Category == UserPreferenceCategory.General)
        {
            try
            {
                Application.Current?.Dispatcher.Invoke(SyncWithSystemTheme);
            }
            catch
            {
                // アプリケーション終了時など、Dispatcher呼び出しに失敗する場合がある
            }
        }
    }
}
