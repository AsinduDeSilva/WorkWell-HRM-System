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

            DataTable table = new DataTable();
            table.Columns.Add("Manager ID", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Departments", typeof(string));

            table.Rows.Add("MN001", "Alice Johnson", "Finance");
            table.Rows.Add("MN002", "Bob Smith", "HR, Admin");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");
            table.Rows.Add("MN003", "Charlie Brown", "IT");

            // Bind to DataGrid
            DataGrid_HRManagers.ItemsSource = table.DefaultView;
        }

        private void Add_Employee_Click(object sender, RoutedEventArgs e)
        {

        }






    }
}
