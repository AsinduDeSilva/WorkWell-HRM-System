using QuestPDF.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Windows;
using WorkWell.Data;
using WorkWell.MVVM;
using WorkWell.Session;


namespace WorkWell.ViewModels
{
    internal class PayrollPageViewModel : ViewModelBase
    {
        private int employeeId;
        private string employeeName;
        private int month;
        private int year;
        private decimal basicSalary;
        private decimal allowances;
        private decimal totalSalary;

        public int EmployeeId
        {
            get { return employeeId; }
            set
            {
                employeeId = value;
                OnPropertyChanged();
            }
        }

        public string EmployeeName
        {
            get { return employeeName; }
            set
            {
                employeeName = value;
                OnPropertyChanged();
            }

        }

        public int Month
        {
            get { return month; }
            set
            {
                month = value; 
                OnPropertyChanged();
            }
        }

        public int Year
        {
            get { return year; }
            set 
            {
                year = value; 
                OnPropertyChanged();
            }
        }

        public decimal BasicSalary
        {
            get { return basicSalary; }
            set
            {  
                basicSalary = value; 
                OnPropertyChanged(); 
            }
        }

        public decimal Allowances 
        {
            get { return allowances; }
            set
            {
                allowances = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalSalary
        {
            get { return totalSalary; }
            set
            {
                totalSalary = value;
                OnPropertyChanged();
            }
        }

        private AppDbContext context;

        public RelayCommand DownloadPayslipCommand => new RelayCommand(execute => DownloadPayslip());

        public PayrollPageViewModel()
        {

            QuestPDF.Settings.License = LicenseType.Community;

            context = new AppDbContext();
            EmployeeId = UserSession.CurrentUser.Employee.EmployeeID;
            EmployeeName = UserSession.CurrentUser.Employee.Name;

            LoadPayroll();

        }

        private void LoadPayroll()
        {
            using var context = new AppDbContext();

            var payroll = context.Payrolls
                .Where(p=> p.EmployeeID == UserSession.CurrentUser.Employee.EmployeeID)
                .OrderByDescending(p => p.Year)
                .ThenByDescending(p => p.Month)
                .FirstOrDefault();

            if (payroll != null)
            {
                Month = payroll.Month;
                Year = payroll.Year;
                BasicSalary = payroll.BaseSalary;
                Allowances = payroll.Allowances;
                TotalSalary = BasicSalary + Allowances;
            }
        }

        private void DownloadPayslip()
        {
            try
            {
                var doc = new PayslipDocument(EmployeeName, EmployeeId, Month, Year, BasicSalary, Allowances, TotalSalary);

                string filename = $"Payslip_{EmployeeName}_{Month}_{Year}.pdf";
                string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);

                doc.GeneratePdf(fullPath);

                Process.Start(new ProcessStartInfo
                {
                    FileName = fullPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}");
            }
        }



    }
    
}
