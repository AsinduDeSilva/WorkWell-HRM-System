using Microsoft.Identity.Client;
using System.Windows;
using WorkWell.Data;
using WorkWell.MVVM;
using WorkWell.Services;
using WorkWell.Session;
using WorkWell.Views;

namespace WorkWell.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        private string currentPassword;

        public string CurrentPassword
        {
            get
            {
                return currentPassword;
            }
            set
            {
                currentPassword = value;
                OnPropertyChanged();
            }
        }

        private string newPassword;

        public string NewPassword
        {
            get
            {
                return newPassword;
            }
            set
            {
                newPassword = value;
                OnPropertyChanged();
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                confirmPassword = value; OnPropertyChanged();
            }
        }

        public RelayCommand ChangePasswordCommand => new RelayCommand(execute => ChangePassword());
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());

        private readonly AppDbContext context;

        public SettingsViewModel()
        {
            context = new AppDbContext();
        }
        public void ChangePassword()
        {
            if (string.IsNullOrWhiteSpace(CurrentPassword) ||
                string.IsNullOrWhiteSpace(NewPassword) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            var user = context.Users.Find(UserSession.CurrentUser.UserID);
            if (user == null ||  !PasswordHashingService.Verify(CurrentPassword, user.Password) )
            {
                MessageBox.Show("Current password is incorrect.");
                return;
            }

            user.Password = PasswordHashingService.Hash(NewPassword);
            context.SaveChanges();

            MessageBox.Show("Password changed successfully.");
            FrameManagerService.MainFrame.Navigate(new Signin());
        }

        public void Cancel()
        {
            CurrentPassword = string.Empty;
            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;
        }

    }
}
