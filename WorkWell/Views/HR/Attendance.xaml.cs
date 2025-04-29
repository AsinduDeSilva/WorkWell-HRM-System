using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WorkWell.Views.HR
{
    public partial class Attendance : Page
    {
        public Attendance()
        {
            InitializeComponent();

            // Dummy data
            var dummyData = new List<AttendanceRecord>
            {
                new AttendanceRecord { EmployeeId = "E001", EmployeeName = "John Doe", Date = DateTime.Today, ArriveTime = "09:00", DepartureTime = "17:00" },
                new AttendanceRecord { EmployeeId = "E002", EmployeeName = "Jane Smith", Date = DateTime.Today, ArriveTime = "09:15", DepartureTime = "17:05" },
                new AttendanceRecord { EmployeeId = "E003", EmployeeName = "David Wick", Date = DateTime.Today, ArriveTime = "08:50", DepartureTime = "16:45" }
            };

            AttendanceGrid.ItemsSource = dummyData;
        }

        private void SubmitAttendance_Click(object sender, RoutedEventArgs e)
        {
            var records = AttendanceGrid.ItemsSource as List<AttendanceRecord>;

            if (records == null) return;

            foreach (var record in records)
            {
                // You can process or save this to DB
                Console.WriteLine($"{record.EmployeeId} | {record.EmployeeName} | {record.Date:d} | {record.ArriveTime} - {record.DepartureTime}");
            }

            MessageBox.Show("Attendance submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class AttendanceRecord
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? Date { get; set; }
        public string ArriveTime { get; set; }
        public string DepartureTime { get; set; }
    }
}
