using System.Windows;
using WorkWell.Services;
using WorkWell.Views;
using WorkWell.Views.Admin;

namespace WorkWell;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        //EmailService.SendEmail("asindudesilva2@gmail.com", "Test", "<h1>Hello!</h1><p>This is a test email sent from C# code.</p>");
        InitializeComponent();
        FrameManagerService.MainFrame = MainFrame;
        MainFrame.Navigate(new AdminView());
    }
}