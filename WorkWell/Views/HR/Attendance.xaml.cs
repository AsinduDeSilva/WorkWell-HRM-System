using System.Windows;
using System.Windows.Controls;
using WorkWell.ViewModels;

namespace WorkWell.Views.HR
{
    public partial class Attendance : Page
    {
        private static AttendanceViewModel viewModel;

        public Attendance()
        {
            InitializeComponent();

            if (viewModel == null) viewModel = new AttendanceViewModel();
            DataContext = viewModel;
        }
    }


}
