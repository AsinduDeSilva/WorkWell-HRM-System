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
using WorkWell.ViewModels;

namespace WorkWell.Views.Admin
{
    /// <summary>
    /// Interaction logic for UpdateHRManagerPage.xaml
    /// </summary>
    public partial class UpdateHRManagerPage : Page
    {
        public UpdateHRManagerPage(int hrManagerID)
        {
            InitializeComponent();
            DataContext = new UpdateHRManagerViewModel(hrManagerID);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
