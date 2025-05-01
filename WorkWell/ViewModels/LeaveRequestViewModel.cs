using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkWell.Data;
using WorkWell.Enums;
using WorkWell.Models;
using WorkWell.MVVM;
using WorkWell.Session; // Add this for DbContext if you have it in your project

namespace WorkWell.ViewModels
{

    
    public class LeaveRequestViewModel : ViewModelBase
    {
        private int _employeeId;
        private string _employeeName;
        private ComboBoxItem _selectedLeaveType;
        private DateOnly _date = DateOnly.FromDateTime(DateTime.Today); // Initialize with current date
        private ObservableCollection<Leave> _leaveHistory;


        public ObservableCollection<Leave> LeaveHistory
        {
            get => _leaveHistory;
            set { _leaveHistory = value; OnPropertyChanged(); }
        }

        public int EmployeeId
        {
            get => _employeeId;
            set { _employeeId = value; OnPropertyChanged(); }
        }

        public string EmployeeName
        {
            get => _employeeName;
            set { _employeeName = value; OnPropertyChanged(); }
        }

        public ComboBoxItem SelectedLeaveType
        {
            get => _selectedLeaveType;
            set { _selectedLeaveType = value; OnPropertyChanged(); }
        }

        public DateOnly Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DateForBinding)); // Update the DateTime binding
            }
        }

        public DateTime DateForBinding
        {
            get => Date.ToDateTime(TimeOnly.MinValue); // Convert DateOnly → DateTime
            set => Date = DateOnly.FromDateTime(value); // Convert DateTime → DateOnly
        }

        public ICommand SubmitCommand => new RelayCommand(execute => SubmitLeave());


        public LeaveRequestViewModel()
        {
            Date = DateOnly.FromDateTime(DateTime.Today);
            this.EmployeeId = UserSession.CurrentUser.Employee.EmployeeID;
            this.EmployeeName = UserSession.CurrentUser.Employee.Name;
            LoadLeaveHistory(); // Load history when ViewModel initializes
        }

        private void LoadLeaveHistory()
        {
            using (var context = new AppDbContext())
            {
                var history = context.Leaves
                    .Where(l => l.EmployeeID == this.EmployeeId)
                    .OrderByDescending(l => l.Date)
                    .ToList();

                LeaveHistory = new ObservableCollection<Leave>(history);
            }
        }

        private void SubmitLeave()
        {
            if (!ValidateSubmission()) return;


            try
            {
                using (var context = new AppDbContext())
                {
                    var newLeave = new Leave
                    {
                        EmployeeID = this.EmployeeId,
                        LeaveType = this.SelectedLeaveType.Content.ToString(),
                        Date = this.Date,
                        Status = LeaveStatus.PENDING.ToString()
                    };

                    context.Leaves.Add(newLeave);
                    context.SaveChanges();

                    MessageBox.Show("Leave request submitted successfully!");
                    ClearForm();
                    LoadLeaveHistory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting leave: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            // Reset form fields if needed
            SelectedLeaveType = default;
            Date = DateOnly.FromDateTime(DateTime.Today);
        }

        private bool ValidateSubmission()
        {
            if (EmployeeId <= 0)
            {
                MessageBox.Show("Invalid Employee ID");
                return false;
            }   

            if (string.IsNullOrEmpty(EmployeeName))
            {
                MessageBox.Show("Employee name is required");
                return false;
            }

            if (Date < DateOnly.FromDateTime(DateTime.Today))
            {
                MessageBox.Show("Cannot request leave for past dates");
                return false;
            }

            return true;
        }

    }


}
