using System.Windows.Controls;
using WorkWell.ViewModels;
using WorkWell.Models;
using System.Windows.Input;
namespace WorkWell.Views.HR
{
    /// <summary>  
    /// Interaction logic for EmployeePage.xaml  
    /// </summary>  
    public partial class EmployeePayrollPage : Page
    {
        public EmployeePayrollPage(int employeeId, string name)
        {
            InitializeComponent();
            DataContext = new EmployeePayrollPageViewModel(employeeId, name);
        }
    }
}
