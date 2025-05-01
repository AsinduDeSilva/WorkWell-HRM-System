using System.Windows.Controls;
using WorkWell.ViewModels;

namespace WorkWell.Views.Employee
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
            DataContext = new ProfileViewModel();
        }

    
    }
}
