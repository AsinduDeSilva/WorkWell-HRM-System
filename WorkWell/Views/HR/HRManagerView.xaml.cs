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
using WorkWell.Views.Admin;

namespace WorkWell.Views.HR
{
    /// <summary>
    /// Interaction logic for HRManagerView.xaml
    /// </summary>
    public partial class HRManagerView : Page
    {
        private Frame MainFrame;
        public HRManagerView(Frame MainFrame)
        {
            InitializeComponent();
            this.MainFrame = MainFrame;
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
            MainFrame.Navigate(new Signin());
        }
    }
}
