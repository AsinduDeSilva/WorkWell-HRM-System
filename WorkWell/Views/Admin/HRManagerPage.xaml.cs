using System;
using System.Collections.Generic;
using System.Data;
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
using WorkWell.Models;
using WorkWell.ViewModels;

namespace WorkWell.Views.Admin
{
    /// <summary>
    /// Interaction logic for HRManagerPage.xaml
    /// </summary>
    public partial class HRManagerPage : Page
    {
        public HRManagerPage()
        {
            InitializeComponent();
            DataContext = new HRManagerPageViewModel();
        }
    }
}
