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
        public HRManagerView()
        {
            InitializeComponent();
            MainFrame.Navigate(new HRManagerPage());
        }
    }
}
