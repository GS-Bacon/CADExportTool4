using System.Windows;
using CADExportTool.WPF.ViewModels;
using Prism.Regions;

namespace CADExportTool.WPF.Views;

public partial class MainWindow : Window
{
    public MainWindow(IRegionManager regionManager)
    {
        InitializeComponent();
        regionManager.RegisterViewWithRegion("ContentRegion", typeof(MainView));
    }
}
