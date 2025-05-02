using System.Windows.Input;
using WorkWell.Data;
using WorkWell.MVVM;
using WorkWell.Session;
using WorkWell.Models;
using System.Windows;
using System.Collections.ObjectModel;

namespace WorkWell.ViewModels
{
    class AttendanceViewModel :ViewModelBase
    {
        private ObservableCollection<AttendanceRecord> employeeAttenDanceList;

        public ObservableCollection<AttendanceRecord> EmployeeAttenDanceList
        {
            get { return employeeAttenDanceList; }
            set
            {
                employeeAttenDanceList = value;
                OnPropertyChanged();
            }
        }

        private AppDbContext context;

        public ICommand SubmitCommand => new RelayCommand(execute => SubmitAttendance());

        public AttendanceViewModel()
        {
            context = new AppDbContext();
            EmployeeAttenDanceList = new ObservableCollection<AttendanceRecord>();
            LoadData();
        }

        public void LoadData()
        {
            HRManager hrManager = UserSession.CurrentUser.HRManager;
            context.Entry(hrManager).Collection(hrm => hrm.Departments).Load();

            foreach (var department in hrManager.Departments)
            {
                context.Entry(department).Collection(d => d.Employees).Load();
                foreach (var employee in department.Employees)
                {
                    EmployeeAttenDanceList.Add(new AttendanceRecord
                    {
                        EmployeeId = employee.EmployeeID.ToString(),
                        EmployeeName = employee.Name,
                        Date = DateOnly.FromDateTime(DateTime.Today),
                        ArriveTime = "",
                        DepartureTime = ""
                    });
                }
            }
        }

        private void SubmitAttendance()
        {
            foreach (var record in employeeAttenDanceList)
            {
                if (!IsValidTime(record.ArriveTime) || !IsValidTime(record.DepartureTime)) return;

                context.Attendances.Add(new Attendance()
                {
                    EmployeeID = int.Parse(record.EmployeeId),
                    Date = record.Date.Value,
                    CheckInTime = TimeOnly.Parse(record.ArriveTime),
                    CheckOutTime = TimeOnly.Parse(record.DepartureTime)
                });
            }
            context.SaveChanges();
            MessageBox.Show("Attendance submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsValidTime(string timeString)
        {
            if (TimeOnly.TryParse(timeString, out TimeOnly time))
            {
                return true;
            }
            else
            {
                MessageBox.Show($"Invalid time format: {timeString}");
                return false;
            }
        }
        
    }
    


    class AttendanceRecord
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateOnly? Date { get; set; }
        public string ArriveTime { get; set; }
        public string DepartureTime { get; set; }
    }
}
