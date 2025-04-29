using System.Windows;
using WorkWell.Views;
using WorkWell.Views.Admin;
using WorkWell.Views.Employee;
using WorkWell.Views.HR;

namespace WorkWell;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new HRManagerView(MainFrame));
    }
}