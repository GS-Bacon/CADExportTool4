using System.Windows;
using CADExportTool.Core.Interfaces;
using CADExportTool.Services;
using CADExportTool.WPF.Helpers;
using CADExportTool.WPF.Services;
using CADExportTool.WPF.ViewModels;
using CADExportTool.WPF.Views;
using MaterialDesignThemes.Wpf;
using Prism.DryIoc;
using Prism.Ioc;

namespace CADExportTool.WPF;

public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // OSテーマに同期してアプリのテーマを設定
        ThemeHelper.SyncWithSystemTheme();

        // OSテーマ変更の監視を開始
        ThemeHelper.StartWatchingSystemTheme();

        // 更新チェック
        await CheckForUpdatesAsync();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        try
        {
            // OSテーマ変更の監視を停止
            ThemeHelper.StopWatchingSystemTheme();
        }
        catch
        {
            // 終了処理中のエラーは無視
        }

        base.OnExit(e);
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Singleton services
        containerRegistry.RegisterSingleton<ISolidWorksService, SolidWorksService>();
        containerRegistry.RegisterSingleton<ISnackbarMessageQueue>(_ => new SnackbarMessageQueue(TimeSpan.FromSeconds(3)));
        containerRegistry.RegisterSingleton<IFileConverterFactory, FileConverterFactory>();
        containerRegistry.RegisterSingleton<IUpdateService, UpdateService>();
        containerRegistry.RegisterSingleton<IErrorReportingService, ErrorReportingService>();

        // Transient services
        containerRegistry.Register<IExportService, ExportService>();
        containerRegistry.Register<IThumbnailService, ThumbnailService>();
        containerRegistry.Register<IZipService, ZipService>();
        containerRegistry.Register<IToastNotificationService, ToastNotificationService>();

        // ViewModels
        containerRegistry.Register<MainViewModel>();

        // Navigation
        containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
    }

    private async Task CheckForUpdatesAsync()
    {
        try
        {
            var updateService = Container.Resolve<IUpdateService>();
            var result = await updateService.CheckForUpdateAsync();

            if (result.IsUpdateAvailable && !string.IsNullOrEmpty(result.DownloadUrl))
            {
                var message = $"新しいバージョン {result.LatestVersion} が利用可能です。\n現在のバージョン: {result.CurrentVersion}\n\n更新しますか？";

                var dialogResult = MessageBox.Show(
                    message,
                    "更新があります",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    await updateService.DownloadAndInstallUpdateAsync(result.DownloadUrl);
                }
            }
        }
        catch
        {
            // 更新チェックの失敗は無視
        }
    }
}
