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

namespace WorkWell.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Page
    {
        private Frame MainFrame;
        public AdminView(Frame MainFrame)
        {
            InitializeComponent();
            this.MainFrame = MainFrame;
            SubFrame.Navigate(new EmployeePage());
        }

        private void txtLogoutClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new Signin());
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
