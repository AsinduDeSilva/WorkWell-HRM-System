using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkWell.Data;
using WorkWell.Enums;
using WorkWell.Models;
using WorkWell.MVVM;
using WorkWell.Services;
using WorkWell.Views.Admin;

namespace WorkWell.ViewModels
{
    public class DepartmentOption : ViewModelBase
    {
        public string Name { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }
    }

    internal class AddHRManagerViewModel : ViewModelBase
    {
        private AppDbContext context;

        public AddHRManagerViewModel()
        {
            context = new AppDbContext();
            hrManagers = context.HRManagers.Include(hrm => hrm.User).ToList();
            MaleSelected = true;
            LoadDepartments();
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
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
            }
        }

        private bool maleSelected;

        public bool MaleSelected
        {
            get { return maleSelected; }
            set
            {
                maleSelected = value;
                OnPropertyChanged();
            }
        }

        public bool Finance { get; set; }
        public bool IT { get; set; }
        public bool Marketing { get; set; }
        public bool Sales { get; set; }
        public bool Operations { get; set; }

        public RelayCommand ResetFormCommand => new RelayCommand(execute => ResetForm());

        public RelayCommand AddHRManagerCommand => new RelayCommand(execute => AddHRManager());

        public void ResetForm()
        {
            FrameManagerService.SubFrame.Navigate(new AddHRManagerPage());
        }

        public ICollection<Department> departments = new List<Department>();
        
        private List<HRManager> hrManagers;

        public List<HRManager> HrManagers
        {
            get { return hrManagers; }
            set
            {
                hrManagers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DepartmentOption> DepartmentOptions { get; set; } = [];

        private void LoadDepartments()
        {
            using (var context = new AppDbContext())
            {
                var departments = context.Departments.Select(d => d.DepartmentName).ToList();
                DepartmentOptions = new ObservableCollection<DepartmentOption>(
                    departments.Select(d => new DepartmentOption { Name = d })
                );
            }
        }


        public void AddHRManager()
        {
            foreach (var hrm in HrManagers)
            {
                if (hrm.User.Email.ToString().ToLower() == email.ToLower())
                {
                    MessageBox.Show("Email already exists");
                    return;
                }
            }
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(NIC) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }
            if (NIC.Length != 10)
            {
                MessageBox.Show("NIC must be 10 characters");
                return;
            }
            if (Phone.Length != 10 || Phone[0] == 0)
            {
                MessageBox.Show("Invalid Phone Number");
                return;
            }
            if (!Email.Contains("@") || !Email.Contains("."))
            {
                MessageBox.Show("Invalid Email");
                return;
            }

            foreach (var item in DepartmentOptions)
            {
                if (item.IsSelected)
                {
                    departments.Add(context.Departments.FirstOrDefault(d => d.DepartmentName == item.Name));
                }
            }


            var hrManager = new HRManager
            {
                Name = Name,
                NIC = NIC,
                Phone = Phone,
                User = new User
                {
                    Email = email,
                    Role = RoleTypes.HRM.ToString(),
                    Password = PasswordHashingService.Hash($"{NIC}.123")
                },
                Departments = departments
            };

            context.HRManagers.Add(hrManager);
            context.SaveChanges();
            MessageBox.Show("HR Manager added successfully");
            FrameManagerService.SubFrame.Navigate(new HRManagerPage());
        }

            

    }
}
