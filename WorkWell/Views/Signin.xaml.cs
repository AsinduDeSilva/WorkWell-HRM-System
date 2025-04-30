using System.Windows;
using System.Windows.Controls;
using WorkWell.ViewModels;

namespace WorkWell.Views
{
    /// <summary>
    /// Interaction logic for Signin.xaml
    /// </summary>
    public partial class Signin : Page
    {
        public Signin()
        {
            InitializeComponent();
            DataContext = new SigninViewModel();
        }


        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is SigninViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }
    }
}
