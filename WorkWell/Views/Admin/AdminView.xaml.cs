using System.Windows.Controls;
using System.Windows.Input;
using WorkWell.Services;


namespace WorkWell.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Page
    {
        public AdminView()
        {
            InitializeComponent();
            FrameManagerService.SubFrame = SubFrame;
            SubFrame.Navigate(new EmployeePage());
        }

        private void txtLogoutClick(object sender, MouseButtonEventArgs e)
        {
            FrameManagerService.MainFrame.Navigate(new Signin());
        }

        private void txtEmployeesClick(object sender, MouseButtonEventArgs e)
        {
            SubFrame.Navigate(new EmployeePage());
        }

        private void txtHRManagersClick(object sender, MouseButtonEventArgs e)
        {
            SubFrame.Navigate(new HRManagerPage());
        }
    }
}
