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

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // OSテーマに同期してアプリのテーマを設定
        ThemeHelper.SyncWithSystemTheme();

        // OSテーマ変更の監視を開始
        ThemeHelper.StartWatchingSystemTheme();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        // OSテーマ変更の監視を停止
        ThemeHelper.StopWatchingSystemTheme();

        base.OnExit(e);
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Singleton services
        containerRegistry.RegisterSingleton<ISolidWorksService, SolidWorksService>();
        containerRegistry.RegisterSingleton<ISnackbarMessageQueue>(_ => new SnackbarMessageQueue(TimeSpan.FromSeconds(3)));

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
}
