using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkWell.Views.Employee
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    /// 
    public partial class EmployeeView : Page
    {
        private Frame MainFrame;
        public EmployeeView(Frame MainFrame)
        {
            InitializeComponent();
            this.MainFrame = MainFrame;
            SubFrame.Navigate(new Profile());
        }

        private void txtLeaveRequestClick(object sender, MouseButtonEventArgs e)
        {
            SubFrame.Navigate(new LeaveRequest());
        }

        private void txtProfileClick(object sender, MouseButtonEventArgs e)
        {
            SubFrame.Navigate(new Profile());
        }

        private void txtPayrollClick(object sender, MouseButtonEventArgs e)
        {
            SubFrame.Navigate(new PayrollPage());
        }

        private void txtSettingsClick(object sender, MouseButtonEventArgs e)
        {
            SubFrame.Navigate(new Settings());
        }

        private void txtLogoutClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new Signin());
        }

    }
}
