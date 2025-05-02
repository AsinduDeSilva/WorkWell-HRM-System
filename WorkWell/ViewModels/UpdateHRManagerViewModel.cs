using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkWell.Data;
using WorkWell.Models;
using WorkWell.MVVM;
using WorkWell.Services;
using WorkWell.Views.Admin;

namespace WorkWell.ViewModels
{
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

        public UpdateHRManagerViewModel(int hrManagerID)
        {
            HrManagerID = hrManagerID;
            context = new AppDbContext();

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
                        if (hrm.DepartmentName.ToLower() == "Finance".ToLower())
                        {
                            Finance = true;
                        }
                        else if (hrm.DepartmentName.ToLower() == "IT".ToLower())
                        {
                            IT = true;
                        }
                        else if (hrm.DepartmentName.ToLower() == "Marketing".ToLower())
                        {
                            Marketing = true;
                        }
                        else if (hrm.DepartmentName.ToLower() == "Sales".ToLower())
                        {
                            Sales = true;
                        }
                        else if (hrm.DepartmentName.ToLower() == "Operations".ToLower())
                        {
                            Operations = true;
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
                if (Finance == true)
                {
                    departments.Add(context.Departments.FirstOrDefault(d => d.DepartmentName == "Finance"));
                }
                if (IT == true)
                {
                    departments.Add(context.Departments.FirstOrDefault(d => d.DepartmentName == "IT"));
                }
                if (Marketing == true)
                {
                    departments.Add(context.Departments.FirstOrDefault(d => d.DepartmentName == "Marketing"));
                }
                if (Sales == true)
                {
                    departments.Add(context.Departments.FirstOrDefault(d => d.DepartmentName == "Sales"));
                }
                if (Operations == true)
                {
                    departments.Add(context.Departments.FirstOrDefault(d => d.DepartmentName == "Operations"));
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
