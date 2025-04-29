using System.Windows;
using WorkWell.Views;

namespace WorkWell;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new Signin());
    }
}