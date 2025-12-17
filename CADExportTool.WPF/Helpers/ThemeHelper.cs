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
        var theme = _paletteHelper.GetTheme();
        theme.SetBaseTheme(isDark ? BaseTheme.Dark : BaseTheme.Light);
        _paletteHelper.SetTheme(theme);
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
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
    }

    /// <summary>
    /// システムテーマ変更イベントの監視を停止
    /// </summary>
    public static void StopWatchingSystemTheme()
    {
        SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
    }

    private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (e.Category == UserPreferenceCategory.General)
        {
            Application.Current?.Dispatcher.Invoke(SyncWithSystemTheme);
        }
    }
}
