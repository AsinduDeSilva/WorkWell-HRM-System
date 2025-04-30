using System.Windows;
using WorkWell.Data;
using WorkWell.Enums;
using WorkWell.MVVM;
using WorkWell.Services;
using WorkWell.Views.Admin;

namespace WorkWell.ViewModels
{
    class SigninViewModel : ViewModelBase
    {
        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand => new RelayCommand(execute => Login());

        private AppDbContext context;

        public SigninViewModel()
        {
            context = new AppDbContext();
        }
        public void Login()
        {
            foreach (var user in context.Users.ToList())
            {
                if (user.Email == email && user.Password == password)
                {
                    if (user.Role.Equals(RoleTypes.ADMIN.ToString()))
                    {
                        FrameManagerService.MainFrame.Navigate(new AdminView());
                    }
                    else if (user.Role.Equals(RoleTypes.HRM.ToString()))
                    {


                    }
                    else if (user.Role.Equals(RoleTypes.EMPLOYEE.ToString()))
                    {
                       
                    }
                    return;
                }
            }
            MessageBox.Show("Invalid email or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

