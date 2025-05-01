using System.Windows;
using System.Windows.Controls;
using WorkWell.ViewModels;

namespace WorkWell.Views.Employee
{
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            this.DataContext = new SettingsViewModel();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            txtCurrentPassword.Password = string.Empty;
            txtNewPassword.Password = string.Empty;
            txtConfirmPassword.Password = string.Empty;

            if (DataContext is SettingsViewModel viewModel)
            {
                viewModel.Cancel();
            }
        }

        private void txtCurrentPassword_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext is SettingsViewModel viewModel)
            {
                viewModel.CurrentPassword = txtCurrentPassword.Password;
            }
        }

        private void txtNewPassword_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext is SettingsViewModel viewModel)
            {
                viewModel.NewPassword = txtNewPassword.Password;
            }
        }

        private void txtConfirmPassword_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext is SettingsViewModel viewModel)
            {
                viewModel.ConfirmPassword = txtConfirmPassword.Password;
            }
        }
    }
}
