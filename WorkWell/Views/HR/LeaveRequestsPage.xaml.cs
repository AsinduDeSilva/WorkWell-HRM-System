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
            // Load dummy data
            var dummyData = new List<LeaveRequest>
            {
                new LeaveRequest { EmployeeName = "John Doe", EmployeeId = "E001", Date = "2025-04-23", Reason = "Medical Leave" },
                new LeaveRequest { EmployeeName = "Jane Smith", EmployeeId = "E002", Date = "2025-04-25", Reason = "Vacation" },
                new LeaveRequest { EmployeeName = "David Wick", EmployeeId = "E003", Date = "2025-04-28", Reason = "Personal Leave" }
            };
            LeaveRequestsGrid.ItemsSource = dummyData;
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
