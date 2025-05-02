using System.Windows.Controls;
using WorkWell.ViewModels;

namespace WorkWell.Views.Admin
{
    /// <summary>
    /// Interaction logic for HRMProfilePage.xaml
    /// </summary>
    public partial class HRMProfilePage : Page
    {
        public HRMProfilePage(int hrManagerId)
        {
            InitializeComponent();
            DataContext = new HRMProfilePageViewModel(hrManagerId);  
        }
    }
}
