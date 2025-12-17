using System.Windows;
using CADExportTool.Core.Interfaces;
using CADExportTool.Services;
using CADExportTool.WPF.ViewModels;
using CADExportTool.WPF.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace CADExportTool.WPF;

public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Singleton services
        containerRegistry.RegisterSingleton<ISolidWorksService, SolidWorksService>();

        // Transient services
        containerRegistry.Register<IExportService, ExportService>();
        containerRegistry.Register<IThumbnailService, ThumbnailService>();
        containerRegistry.Register<IZipService, ZipService>();

        // ViewModels
        containerRegistry.Register<MainViewModel>();

        // Navigation
        containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
    }
}
