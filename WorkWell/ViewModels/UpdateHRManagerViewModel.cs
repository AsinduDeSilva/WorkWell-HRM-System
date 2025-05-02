using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using WorkWell.Data;
using WorkWell.Models;
using WorkWell.MVVM;
using WorkWell.Services;
using WorkWell.Views.Admin;

namespace WorkWell.ViewModels
{
    internal class DepartmentOptionUpdate 
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; } = false;
    }

    internal class UpdateHRManagerViewModel : ViewModelBase
    {
        private AppDbContext context;

        private int hrManagerID;

        public int HrManagerID
        {
            get { return hrManagerID; }
            set
            {
                hrManagerID = value;
                OnPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private string nic;

        public string NIC
        {
            get { return nic; }
            set
            {
                nic = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private bool finance;

        public bool Finance
        {
            get { return finance; }
            set
            {
                finance = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private bool marketing;

        public bool Marketing
        {
            get { return marketing; }
            set
            {
                marketing = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private bool it;

        public bool IT
        {
            get { return it; }
            set
            {
                it = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private bool sales;

        public bool Sales
        {
            get { return sales; }
            set
            {
                sales = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private bool operations;

        public bool Operations
        {
            get { return operations; }
            set
            {
                operations = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }


        private bool enableSubmit = false;

        public bool EnableSubmit
        {
            get { return enableSubmit; }
            set 
            { 
                enableSubmit = value; 
                OnPropertyChanged(); 
            }
        }

        public void ChangeSubmitButton()
        {
            EnableSubmit = true;
        }

        public ObservableCollection<DepartmentOptionUpdate> DepartmentOptionsUpdate { get; set; } = [];

        private void LoadDepartments()
        {
            using (var context = new AppDbContext())
            {
                var departments = context.Departments.Select(d => d.DepartmentName).ToList();
                DepartmentOptionsUpdate = new ObservableCollection<DepartmentOptionUpdate>(
                    departments.Select(d => new DepartmentOptionUpdate() { Name = d })
                );
            }
        }

        public UpdateHRManagerViewModel(int hrManagerID)
        {
            HrManagerID = hrManagerID;
            context = new AppDbContext();
            LoadDepartments();

            foreach (var hrManager in context.HRManagers.Include(hrm => hrm.User).Include(hrm => hrm.Departments).ToList())
            {
                if (hrManager.HRManagerID == HrManagerID)
                {
                    name = hrManager.Name;
                    nic = hrManager.NIC;
                    phone = hrManager.Phone;
                    email = hrManager.User.Email;
                    foreach (var hrm in hrManager.Departments)
                    {
                        DepartmentOptionUpdate match = DepartmentOptionsUpdate.FirstOrDefault(d => d.Name.Equals(hrm.DepartmentName, StringComparison.OrdinalIgnoreCase));
                        if (match != null)
                        {
                            match.IsSelected = true;

                        }
                    }
                    EnableSubmit = false;

                }
            }
        }

        public RelayCommand GoBackCommand => new RelayCommand(execute => GoBack());

        public RelayCommand UpdateHRManagerCommand => new RelayCommand(execute => UpdateHRManager());

        public void GoBack()
        {
            FrameManagerService.SubFrame.Navigate(new HRManagerPage());
        }

        public ICollection<Department> departments = new List<Department>();
        public void UpdateHRManager()
        {
            var hrManager = context.HRManagers.Include(hrm => hrm.User).Include(hrm => hrm.Departments).FirstOrDefault(hrm => hrm.HRManagerID == HrManagerID);
            if (hrManager != null)
            {
                foreach (var item in DepartmentOptionsUpdate)
                {
                    if (item.IsSelected)
                    {
                        departments.Add(context.Departments.FirstOrDefault(d => d.DepartmentName == item.Name));
                    }
                }
                hrManager.Name = Name;
                hrManager.Phone = Phone;
                hrManager.NIC = NIC;
                var user = context.Users.FirstOrDefault(u => u.UserID == hrManager.UserID);
                if (user != null)
                {
                    user.Email = Email;
                }
                hrManager.Departments = departments;

                context.SaveChanges();
                MessageBox.Show("HR Manager updated successfully");
                FrameManagerService.SubFrame.Navigate(new HRManagerPage());
            }
        }

 

    }
}
