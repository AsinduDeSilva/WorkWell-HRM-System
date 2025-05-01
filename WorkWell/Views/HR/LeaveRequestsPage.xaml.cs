using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WorkWell.Views.HR
{
    public partial class LeaveRequestPage : Page
    {
        public LeaveRequestPage()
        {
            InitializeComponent();
        }
    }

    public class LeaveRequest
    {
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string Date { get; set; }
        public string Reason { get; set; }
    }
}
