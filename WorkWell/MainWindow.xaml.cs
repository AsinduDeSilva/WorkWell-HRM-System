using System.Windows;
using WorkWell.Services;
using WorkWell.Views;
using WorkWell.Views.Admin;

namespace WorkWell;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        FrameManagerService.MainFrame = MainFrame;
        MainFrame.Navigate(new AdminView());
    }
}