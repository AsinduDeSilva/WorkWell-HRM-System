using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using WorkWell.Data;
using WorkWell.Enums;
using WorkWell.Models;
using WorkWell.MVVM;
using WorkWell.Session;

namespace WorkWell.ViewModels.HR
{
    public class LeaveRequestHRViewModel : ViewModelBase
    {
        private ObservableCollection<Leave> _pendingLeaveRequests;
        public ObservableCollection<Leave> PendingLeaveRequests
        {
            get => _pendingLeaveRequests;
            set { _pendingLeaveRequests = value; OnPropertyChanged(); }
        }

        public ICommand ApproveCommand { get; }
        public ICommand RejectCommand { get; }

        public LeaveRequestHRViewModel()
        {
            LoadPendingLeaveRequests();
            ApproveCommand = new RelayCommand(ApproveLeave);
            RejectCommand = new RelayCommand(RejectLeave);
        }

        private void LoadPendingLeaveRequests()
        {
            using (var context = new AppDbContext())
            {
                var requests = context.Leaves
                    .Include(l => l.Employee) // Make sure to include Employee
                    .Where(l => l.Status == "PENDING")
                    .OrderBy(l => l.Date)
                    .ToList();

                PendingLeaveRequests = new ObservableCollection<Leave>(requests);
            }
        }

        private void ApproveLeave(object parameter)
        {
            if (parameter is Leave leave)
            {
                if (MessageBox.Show($"Approve leave request for {leave.Employee.Name}?",
                    "Confirm Approval", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    UpdateLeaveStatus(leave, "APPROVED");
                    switch ("")
                    {
                        case "MEDICAL":
                            UserSession.CurrentUser.Employee.SickLeaves--;
                            break;
                        case "CASUAL":
                            UserSession.CurrentUser.Employee.CasualLeaves--;
                            break;
                        case "VACATION":
                            UserSession.CurrentUser.Employee.VacationLeaves--;
                            break;
                    }
                }
            }
        }

        private void RejectLeave(object parameter)
        {
            if (parameter is Leave leave)
            {
                if (MessageBox.Show($"Reject leave request for {leave.Employee.Name}?",
                    "Confirm Rejection", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    UpdateLeaveStatus(leave, "REJECTED");
                }
            }
        }

        private void UpdateLeaveStatus(Leave leave, string status)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var dbLeave = context.Leaves.Find(leave.LeaveID);
                    if (dbLeave != null)
                    {
                        dbLeave.Status = status;
                        context.SaveChanges();
                        LoadPendingLeaveRequests(); // Refresh the list
                        MessageBox.Show($"Leave request {status.ToLower()} successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating leave: {ex.Message}");
            }
        }
    }
}