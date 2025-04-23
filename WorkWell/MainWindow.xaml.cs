using System.Windows;
using WorkWell.Views.Employee;

namespace WorkWell;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        //MainFrame.Navigate(new Profile());
        //MainFrame.Navigate(new LeaveRequest());
        MainFrame.Navigate(new Settings());
    }
}