using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkWell.Data;
using WorkWell.Enums;
using WorkWell.MVVM;
using WorkWell.Services;
using WorkWell.Views;
using WorkWell.Views.Admin;

namespace WorkWell.ViewModels
{
    internal class UpdateEmployeePageViewModel : ViewModelBase
    {
        private int employeeID;

        public List<string> Departments { get; } = new List<string> { "All", "Finance", "Marketing", "IT", "Sales", "Operations" };
        public List<string> Positions { get; } = new List<string> { "All", "Employee", "Manager", "Supervisor" };

        public int EmployeeID
        {
            get { return employeeID; }
            set
            {
                employeeID = value;
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

        private string salary;

        public string Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private string number;

        public string Number
        {
            get { return number; }
            set
            {
                number = value;
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

        private string selectedDepartment;

        public string SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                selectedDepartment = value;
                OnPropertyChanged();
                ChangeSubmitButton();
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

        private bool maleSelected;
        public bool MaleSelected
        {
            get => maleSelected;
            set
            {
                maleSelected = value;
                if (value) SelectedGender = Gender.MALE.ToString();
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private bool femaleSelected;
        public bool FemaleSelected
        {
            get => femaleSelected;
            set
            {
                femaleSelected = value;
                if (value) SelectedGender = Gender.FEMALE.ToString();
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        private string selectedGender;
        public string SelectedGender
        {
            get => selectedGender;
            set
            {
                selectedGender = value;
                OnPropertyChanged();
                ChangeSubmitButton();
            }
        }

        public void ChangeSubmitButton()
        {
            EnableSubmit = true;
        }

        public RelayCommand BackCommand => new RelayCommand(execute => FrameManagerService.SubFrame.Navigate(new EmployeeProfile(EmployeeID)));

        public RelayCommand UpdateEmployeeCommand => new RelayCommand(execute => UpdateEmployee());

        public UpdateEmployeePageViewModel(int employeeID)
        {
            this.employeeID = employeeID;
            LoadEmployeeData();
        }
        private void LoadEmployeeData()
        {
            using (var context = new AppDbContext())
            {
                var employee = context.Employees.Include(e => e.Department).Include(e => e.User).FirstOrDefault(e => e.EmployeeID == employeeID);

                if (employee != null)
                {
                    Name = employee.Name;
                    NIC = employee.NIC;
                    Salary = employee.Salary.ToString();
                    Number = employee.Phone;
                    Email = employee.User.Email;
                    SelectedDepartment = employee.Department.DepartmentName;
                    SelectedPosition = employee.Position;
                    MaleSelected = (employee.Gender == Gender.MALE.ToString());
                    FemaleSelected = (employee.Gender == Gender.FEMALE.ToString());

                }
                EnableSubmit = false;
            }
        }

        private void UpdateEmployee()
        {
            using (var context = new AppDbContext())
            {
                var employee = context.Employees.Include(e => e.User).FirstOrDefault(e => e.EmployeeID == employeeID);
                if (employee != null)
                {
                    employee.Name = Name;
                    employee.NIC = NIC;
                    employee.Salary = decimal.Parse(Salary);
                    employee.Phone = Number;
                    employee.User.Email = Email;
                    employee.Position = SelectedPosition;
                    var department = context.Departments.FirstOrDefault(d => d.DepartmentName == SelectedDepartment);
                    if (department != null)
                    {
                        employee.DepartmentID = department.DepartmentID;
                    }
                    context.SaveChanges();
                    MessageBox.Show("Employee updated successfully");
                }
                FrameManagerService.SubFrame.Navigate(new EmployeePage());
            }
        }
    }
}
