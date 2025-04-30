using System.Windows.Controls;
using System.Windows.Input;
using WorkWell.Services;
using WorkWell.Views.Admin;

namespace WorkWell.Views.HR
{
    /// <summary>
    /// Interaction logic for HRManagerView.xaml
    /// </summary>
    public partial class HRManagerView : Page
    {
        public HRManagerView()
        {
            InitializeComponent();
            FrameManagerService.SubFrame = SubFrame;
            SubFrame.Navigate(new EmployeePage());
        }

        private void txtEmployeesClick(object sender, MouseButtonEventArgs e)
        {
            SubFrame.Navigate(new EmployeePage());
        }

        private void txtLeaveApprovalClick(object sender, MouseButtonEventArgs e)
        {
            SubFrame.Navigate(new LeaveRequestPage());
        }

        private void txtAttendanceClick(object sender, MouseButtonEventArgs e)
        {
            SubFrame.Navigate(new Attendance());
        }

        private void txtLogoutClick(object sender, MouseButtonEventArgs e)
        {
            FrameManagerService.MainFrame.Navigate(new Signin());
        }
    }
}
