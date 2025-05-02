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

        private AppDbContext context;

        public LeaveRequestHRViewModel()
        {
            context = new AppDbContext();
            LoadPendingLeaveRequests();
            ApproveCommand = new RelayCommand(ApproveLeave);
            RejectCommand = new RelayCommand(RejectLeave);
        }

        private void LoadPendingLeaveRequests()
        {
            {
                var requests = context.Leaves
                    .Include(l => l.Employee) 
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
                    switch (leave.LeaveType)
                    {
                        case "MEDICAL":
                            leave.Employee.SickLeaves--;
                            break;
                        case "CASUAL":
                            leave.Employee.CasualLeaves--;
                            break;
                        case "VACATION":
                            leave.Employee.VacationLeaves--;    
                            break;
                    }  
                    context.SaveChanges();
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
                {
                    var dbLeave = context.Leaves.Find(leave.LeaveID);
                    if (dbLeave != null)
                    {
                        dbLeave.Status = status;
                        context.SaveChanges();
                        LoadPendingLeaveRequests(); 
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