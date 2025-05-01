using System.Windows.Controls;
using System.Windows.Input;
using WorkWell.Services;
using WorkWell.Session;

namespace WorkWell.Views.Employee
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    /// 
    public partial class EmployeeView : Page
    {
        public EmployeeView()
        {
            InitializeComponent();
            FrameManagerService.SubFrame = SubFrame;
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
            UserSession.Logout();
            FrameManagerService.MainFrame.Navigate(new Signin());
        }

    }
}
