using WorkWell.Data;
using WorkWell.Models;
using WorkWell.MVVM;
using WorkWell.Session;

namespace WorkWell.ViewModels
{
    class ProfileViewModel : ViewModelBase
    {
        private int employeeId;
        private string name;
        private string nic;
        private string department;
        private string position;
        private string contact;
        private string gender;
        private string email;
        private decimal salary;
        private int sickLeaves;
        private int casualLeaves;
        private int vacationLeaves;

        public int EmployeeId
        {
            get { return employeeId; }
            set
            {
                employeeId = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string NIC
        {
            get { return nic; }
            set
            {
                nic = value;
                OnPropertyChanged();
            }
        }

        public string Department
        {
            get { return department; }
            set
            {
                department = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        public string Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public decimal Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged();
            }
        }

        public int SickLeaves
        {
            get { return sickLeaves; }
            set
            {
                sickLeaves = value;
                OnPropertyChanged();
            }
        }

        public int CasualLeaves
        {
            get { return casualLeaves; }
            set
            {
                casualLeaves = value;
                OnPropertyChanged();
            }
        }

        public int VacationLeaves
        {
            get { return vacationLeaves; }
            set
            {
                vacationLeaves = value;
                OnPropertyChanged();
            }
        }

        public ProfileViewModel()
        {
            User user = UserSession.CurrentUser;
            AppDbContext context = new AppDbContext();
            context.Entry(user.Employee).Reference(e => e.Department).Load();

            EmployeeId = user.Employee.EmployeeID;
            Name = user.Employee.Name;
            NIC = user.Employee.NIC;
            Department = user.Employee.Department?.DepartmentName ?? "N/A";
            Position = user.Employee.Position;
            Contact = user.Employee.Phone;
            Gender = user.Employee.Gender;
            Email = user.Email;
            Salary = user.Employee.Salary;
            SickLeaves = user.Employee.SickLeaves;
            CasualLeaves = user.Employee.CasualLeaves;
            VacationLeaves = user.Employee.VacationLeaves;
        }



    }
}
