using System.Windows.Controls;
using WorkWell.ViewModels;

namespace WorkWell.Views.Employee
{
    public partial class PayrollPage : Page
    {
        public PayrollPage()
        {
            InitializeComponent();
            DataContext = new PayrollPageViewModel();
        }
    }
}
