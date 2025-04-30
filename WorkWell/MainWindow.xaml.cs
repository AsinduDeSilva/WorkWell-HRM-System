using System.Windows;
using WorkWell.Services;
using WorkWell.Views;

namespace WorkWell;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        FrameManagerService.MainFrame = MainFrame;
        MainFrame.Navigate(new Signin());
    }
}