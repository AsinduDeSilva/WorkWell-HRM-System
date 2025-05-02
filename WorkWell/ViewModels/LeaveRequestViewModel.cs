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
        private int _medicalLeaveBalance;
        private int _casualLeaveBalance;
        private int _vacationLeaveBalance;


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

        public int MedicalLeaveBalance
        {
            get => _medicalLeaveBalance;
            set { _medicalLeaveBalance = value; OnPropertyChanged(); }
        }

        public int CasualLeaveBalance
        {
            get => _casualLeaveBalance;
            set { _casualLeaveBalance = value; OnPropertyChanged(); }
        }

        public int VacationLeaveBalance
        {
            get => _vacationLeaveBalance;
            set { _vacationLeaveBalance = value; OnPropertyChanged(); }
        }

        public ICommand SubmitCommand => new RelayCommand(execute => SubmitLeave());


        public LeaveRequestViewModel()
        {
            Date = DateOnly.FromDateTime(DateTime.Today);
            this.EmployeeId = UserSession.CurrentUser.Employee.EmployeeID;
            this.EmployeeName = UserSession.CurrentUser.Employee.Name;
            LoadLeaveHistory(); // Load history when ViewModel initializes
            LoadLeaveBalances();
        }

        private void LoadLeaveBalances()
        {
            MedicalLeaveBalance = UserSession.CurrentUser.Employee.SickLeaves;
            CasualLeaveBalance = UserSession.CurrentUser.Employee.CasualLeaves;
            VacationLeaveBalance = UserSession.CurrentUser.Employee.VacationLeaves;
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

            if (!HasSufficientLeaveBalance())
            {
                MessageBox.Show($"Cannot apply for {SelectedLeaveType.Content.ToString()} leave. You have no remaining leaves of this type.");
                return;
            }

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

        private bool HasSufficientLeaveBalance()
        {
            var leaveType = SelectedLeaveType.Content.ToString();
            return leaveType switch
            {
                "MEDICAL" => MedicalLeaveBalance > 0,
                "CASUAL" => CasualLeaveBalance > 0,
                "VACATION" => VacationLeaveBalance > 0,
                _ => false
            };
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
