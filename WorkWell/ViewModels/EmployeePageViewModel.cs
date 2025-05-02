using Microsoft.EntityFrameworkCore;
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
using WorkWell.Views;
using WorkWell.Views.Admin;

namespace WorkWell.ViewModels
{
    class EmployeePageViewModel : ViewModelBase
    {
        private AppDbContext context;
        private List<Employee> employees;
        private List<Employee> allEmployees;

        private List<string> departments;
        public List<string> Departments
        {
            get => departments;
            set
            {
                departments = value;
                OnPropertyChanged();
            }
        }

        public List<string> Positions { get; } = new List<string> { "All","Employee", "Manager", "Supervisor" };

        public List<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged();
            }
        }

        private string searchTerm;
        public string SearchTerm
        {
            get => searchTerm;
            set
            {
                searchTerm = value;
                OnPropertyChanged();
                Search();
            }
        }

        private Employee selectedEmployee;

        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                OnPropertyChanged();
                FrameManagerService.SubFrame.Navigate(new EmployeeProfile(selectedEmployee.EmployeeID));
            }
        }

        private string selectedDepartment = "All";
        public string SelectedDepartment
        {
            get => selectedDepartment;
            set
            {
                selectedDepartment = value;
                OnPropertyChanged();
                Search();
            }
        }

        private string selectedPosition = "All";
        public string SelectedPosition
        {
            get => selectedPosition;
            set
            {
                selectedPosition = value;
                OnPropertyChanged();
                Search();
            }
        }

        public EmployeePageViewModel()
        {
            context = new AppDbContext();
            allEmployees = context.Employees.Include(emp => emp.Department).ToList();
            Employees = new List<Employee>(allEmployees);
            LoadDepartments();

        }

        public RelayCommand AddEmployeeCommand => new RelayCommand(execute => AddEmployee());

        public RelayCommand SearchCommand => new RelayCommand(execute => Search());

        public RelayCommand ClearCommand => new RelayCommand(execute => ClearSearch());

        private void LoadDepartments()
        {
            using (var context = new AppDbContext())
            {
                Departments = context.Departments.Select(d => d.DepartmentName).ToList();
            }
        }

        public void AddEmployee()
        {
            FrameManagerService.SubFrame.Navigate(new AddEmployeePage());
        }

        public void ClearSearch()
        {
            FrameManagerService.SubFrame.Navigate(new EmployeePage());
        }

        public void Search()
        {
            var filtered = allEmployees.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                filtered = filtered.Where(e =>
                    (!string.IsNullOrEmpty(e.Name) && e.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (e.Department != null && e.Department.DepartmentName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)));
            }

            if (SelectedDepartment != "All")
            {
                filtered = filtered.Where(e =>
                    e.Department != null && e.Department.DepartmentName.Equals(SelectedDepartment, StringComparison.OrdinalIgnoreCase));
            }

            if (SelectedPosition != "All")
            {
                filtered = filtered.Where(e =>
                    !string.IsNullOrEmpty(e.Position) && e.Position.Equals(SelectedPosition, StringComparison.OrdinalIgnoreCase));
            }

            Employees = filtered.ToList(); // Update the property, not the field
        }

    }
}
