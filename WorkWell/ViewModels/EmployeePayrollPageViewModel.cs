using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Windows.Input;
using WorkWell.Data;
using WorkWell.Models;
using WorkWell.MVVM;

namespace WorkWell.ViewModels
{
    internal class EmployeePayrollPageViewModel : ViewModelBase
    {
        private readonly AppDbContext context;

        public EmployeePayrollPageViewModel(int employeeId, string name)
        {
            EmployeeId = employeeId;
            EmployeeName = name;
            context = new AppDbContext();
            SubmitCommand = new RelayCommand(execute => SavePayroll());
            LoadSalaryFromDatabase(employeeId);
        }

        private int employeeId;
        public int EmployeeId
        {
            get => employeeId;
            set { employeeId = value; OnPropertyChanged(); }
        }

        private string employeeName = string.Empty;
        public string EmployeeName
        {
            get => employeeName;
            set { employeeName = value; OnPropertyChanged(); }
        }

        private decimal basicSalary;
        public decimal BasicSalary
        {
            get => basicSalary;
            set { basicSalary = value; OnPropertyChanged(nameof(BasicSalary)); UpdateTotalSalary(); }
        }

        private decimal allowances;
        public decimal Allowances
        {
            get => allowances;
            set { allowances = value; OnPropertyChanged(); UpdateTotalSalary();  }
        }

        private decimal totalSalary;
        public decimal TotalSalary
        {
            get => totalSalary;
            set { totalSalary = value; OnPropertyChanged();}
        }

        private int month;
        public int Month
        {
            get => month;
            set { month = value; OnPropertyChanged();}
        }

        private int year;
        public int Year
        {
            get => year;
            set { year = value; OnPropertyChanged();}
        }

        private void UpdateTotalSalary()
        {
            TotalSalary = BasicSalary + Allowances;
        }

        public ICommand SubmitCommand { get; }

        private void SavePayroll()
        {
            try
            {
                var newPayroll = new Payroll
                {
                    EmployeeID = EmployeeId,
                    BaseSalary = BasicSalary,
                    Allowances = Allowances,
                    Month = Month,
                    Year = Year,
                };

                context.Payrolls.Add(newPayroll);
                context.SaveChanges();
                MessageBox.Show("Payroll submitted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting payroll: {ex.Message}");
            }
        }

        private void LoadSalaryFromDatabase(int employeeId)
        {
            var emp = context.Employees.FirstOrDefault(e => e.EmployeeID == employeeId);
            if (emp != null)
            {
                BasicSalary = emp.Salary;
            }
        }


    }
}
