using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWell.Data;
using WorkWell.Enums;
using WorkWell.Models;
using WorkWell.MVVM;
using WorkWell.Services;
using WorkWell.Views.Admin;

namespace WorkWell.ViewModels
{
    public class NewView
    {
        public int HRManagerID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string NIC { get; set; }
        public string Departments { get; set; }
    }
    internal class HRManagerPageViewModel : ViewModelBase
    {
        public RelayCommand AddHRManagerCommand => new RelayCommand(execute => AddHRManager());

        private AppDbContext context;

        private List<HRManager> hrManagers;

        public List<HRManager> HrManagers
        {
            get { return hrManagers; }
            set
            {
                hrManagers = value;
                OnPropertyChanged();
            }
        }

        private List<NewView> newList = new List<NewView>();

        public List<NewView> NewList
        {
            get { return newList; }
            set
            {
                newList = value;
                OnPropertyChanged();
            }
        }

        private NewView selectedItem;

        public NewView SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
                FrameManagerService.SubFrame.Navigate(new UpdateHRManagerPage(selectedItem.HRManagerID));
            }
        }


        public HRManagerPageViewModel()
        {
            context = new AppDbContext();
            hrManagers = context.HRManagers.Include(hrm => hrm.Departments).ToList();

            hrManagers.ForEach(hrm =>
            {
                ICollection<Department> departments = hrm.Departments;
                string departmentNames = string.Join(", ", departments.Select(d => d.DepartmentName));
                NewView NewItem = new NewView
                {
                    HRManagerID = hrm.HRManagerID,
                    Name = hrm.Name,
                    Phone = hrm.Phone,
                    NIC = hrm.NIC,
                    Departments = departmentNames
                };
                newList.Add(NewItem);
            });
        }


        public void AddHRManager()
        {
            FrameManagerService.SubFrame.Navigate(new AddHRManagerPage());
        }
    }
}
