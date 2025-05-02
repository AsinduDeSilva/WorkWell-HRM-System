using System.Windows;
using System.Windows.Input;
using WorkWell.Data;
using WorkWell.Models;
using WorkWell.MVVM;
using WorkWell.Services;
using WorkWell.Views.Admin;

namespace WorkWell.ViewModels
{
    class HRMProfilePageViewModel : ViewModelBase
    {
        private int hrManagerID;
        private string name;
        private string contact;
        private string nic;
        private string email;

        public int HRManagerID
        {
            get { return hrManagerID; }
            set { 
                hrManagerID = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged();
            }
        }

        public string Contact
        {
            get { return contact; }
            set 
            { 
                contact = value;
                OnPropertyChanged();
            }
        }

        public string NIC
        {
            get { return nic; }
            set 
            {
                nic = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand => new RelayCommand(execute => NavigateToUpdateProfilePage());
        public ICommand DeleteCommand => new RelayCommand(execute => DeleteHRManager());

        private AppDbContext context;
        private HRManager hrManager;

        public HRMProfilePageViewModel(int hrManagerId)
        {
            context = new AppDbContext();

            HRManagerID = hrManagerId;

            hrManager = context.HRManagers.Find(HRManagerID);
            context.Entry(hrManager).Reference(hrm => hrm.User).Load();

            Name = hrManager.Name;
            Contact = hrManager.Phone;
            NIC = hrManager.NIC;
            Email = hrManager.User.Email;


        }

        public void NavigateToUpdateProfilePage()
        {
            FrameManagerService.SubFrame.Navigate(new UpdateHRManagerPage(HRManagerID));
        }

        public void DeleteHRManager()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this HR Manager?", "Delete HR Manager", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (hrManager != null)
                {
                    context.HRManagers.Remove(hrManager);
                    context.SaveChanges();
                    MessageBox.Show("HR Manager deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    FrameManagerService.SubFrame.Navigate(new HRManagerPage());
                }
            }
        }

    }
}
