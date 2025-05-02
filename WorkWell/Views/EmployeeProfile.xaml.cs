using System.Windows.Controls;
using WorkWell.ViewModels;

namespace WorkWell.Views
{
    
    public partial class EmployeeProfile : Page
    {
        public EmployeeProfile(int employeeID)
        {
            InitializeComponent();
            DataContext = new EmployeeProfileViewModel(employeeID);
        }
    }
}
