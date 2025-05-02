using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkWell.Data;
using WorkWell.Enums;
using WorkWell.Models;
using WorkWell.MVVM;
using WorkWell.Services;
using WorkWell.Views;
using WorkWell.Views.Admin;

namespace WorkWell.ViewModels
{
    class AddEmployeePageViewModel: ViewModelBase
    {
        private AppDbContext context;

        private List<Employee> employees;

        public List<string> Departments { get; } = new List<string> {"Finance", "Marketing", "IT", "Sales", "Operations" };
        public List<string> Positions { get; } = new List<string> {"Employee", "Manager", "Supervisor" };

        public List<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged();
            }
        }

        private string name;
        private string phone;
        private string nic;
        private string email;
        private decimal salary;

        public decimal Salary
        {
            get { return salary; }
            set
            {
                salary = value;
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
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
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

        private string selectedDepartment;

        public string SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                selectedDepartment = value;
                OnPropertyChanged();
            }
        }

        private string selectedPosition;

        public string SelectedPosition
        {
            get { return selectedPosition; }
            set
            {
                selectedPosition = value;
                OnPropertyChanged();
            }
        }

        private bool maleSelected = true;

        public bool MaleSelected
        {
            get { return maleSelected; }
            set
            {
                maleSelected = value;
                OnPropertyChanged();
            }
        }

        public AddEmployeePageViewModel()
        {
            context = new AppDbContext();
            Employees = context.Employees.Include(emp => emp.Department).Include(emp => emp.User).ToList();
        }
        public RelayCommand AddEmployeeCommand => new RelayCommand(execute => AddEmployee());

        public RelayCommand ResetCommand => new RelayCommand(execute => FrameManagerService.SubFrame.Navigate(new AddEmployeePage()));
        private void AddEmployee()
        {
            foreach (var emp in Employees)
            {
                if (emp.User.Email.ToLower() == Email)
                {
                    MessageBox.Show("Employee already exists");
                    return;
                }
            }
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(NIC) || string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            var department = context.Departments.FirstOrDefault(d => d.DepartmentName == SelectedDepartment);
            string gender;

            if (department == null) 
            {
                MessageBox.Show("Please select a department");
                return;
            }

            if (maleSelected == true)
            {
                gender = Gender.MALE.ToString();
            }
            else
            {
                gender = Gender.FEMALE.ToString();
            }

            var employee = new Employee
            {
                Name = Name,
                Phone = Phone,
                NIC = NIC,
                Salary = Salary,
                Position = SelectedPosition,
                Department = department,
                SickLeaves = 15,
                CasualLeaves = 15,
                VacationLeaves = 15,
                User = new User
                {
                    Email = Email,
                    Role = "Employee",
                    Password = $"{NIC}.123"
                },
                Gender = gender
            };
            context.Employees.Add(employee);
            context.SaveChanges();
            MessageBox.Show("Employee added successfully");
            FrameManagerService.SubFrame.Navigate(new EmployeePage());
        }
    }
}
