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
using WorkWell.Views;
using WorkWell.Views.Admin;
using WorkWell.Views.HR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkWell.ViewModels
{
    class EmployeeProfileViewModel : ViewModelBase
    {
        private AppDbContext context;

        private List<Employee> employees;

        public List<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged();
            }
        }

        private int employeeID;
        private string name;
        private string email;
        private string phone;
        private string nic;
        private string department;
        private string position;
        private decimal salary;
        private string gender;
        private int sickLeaves;
        private int casualLeaves;
        private int vacationLeaves;

        public int EmployeeID
        {
            get { return employeeID; }
            set
            {
                employeeID = value;
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

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
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

        public string Department
        {
            get { return department; }
            set
            {
                department = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        public decimal Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }

        public int SickLeaves
        {
            get { return sickLeaves; }
            set
            {
                sickLeaves = value;
                OnPropertyChanged();
            }
        }

        public int CasualLeaves
        {
            get { return casualLeaves; }
            set
            {
                casualLeaves = value;
                OnPropertyChanged();
            }
        }

        public int VacationLeaves
        {
            get { return vacationLeaves; }
            set
            {
                vacationLeaves = value;
                OnPropertyChanged();
            }
        }

        public EmployeeProfileViewModel(int employeeID)
        {
            context = new AppDbContext();
            Employees = context.Employees.Include(emp => emp.Department).Include(emp => emp.User).ToList();
            LoadEmployeeData(employeeID);
        }

        public RelayCommand UpdateCommand => new RelayCommand(execute => FrameManagerService.SubFrame.Navigate(new UpdateEmployeePage(EmployeeID)));
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteEmployee(EmployeeID));

        public RelayCommand PayrollCommand => new RelayCommand(execute => FrameManagerService.SubFrame.Navigate(new EmployeePayrollPage(EmployeeID, Name)));

        public void LoadEmployeeData(int employeeID)
        {
            var employee = Employees.FirstOrDefault(e => e.EmployeeID == employeeID);
            if (employee != null)
            {
                EmployeeID = employee.EmployeeID;
                Name = employee.Name;
                NIC = employee.NIC;
                Salary = employee.Salary;
                Phone = employee.Phone;
                Email = employee.User.Email;
                Department = employee.Department.DepartmentName;
                Position = employee.Position;
                Gender = employee.Gender;
                SickLeaves = employee.SickLeaves;
                VacationLeaves = employee.VacationLeaves;
                CasualLeaves = employee.CasualLeaves;
            }
        }

        public void DeleteEmployee(int employeeID)
        {
            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete this employee?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirmResult == MessageBoxResult.Yes)
            {
                var employee = context.Employees.FirstOrDefault(e => e.EmployeeID == employeeID);
                if (employee != null)
                {
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                    MessageBox.Show("Employee deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    FrameManagerService.SubFrame.Navigate(new EmployeePage());
                }
            }
        }

    }
}
