﻿using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels
{
    public class AddFacultyMemberViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly UniversityContext _context;
        private readonly IDialogService _dialogService;

        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        return "Name is Required";
                    }
                }
                if (columnName == "Age")
                {
                    if (!int.TryParse(Age.ToString(), out int ageValue) || ageValue <= 0)
                    {
                        return "Enter a valid positive number for Age";
                    }
                }
                if (columnName == "Gender")
                {
                    if (string.IsNullOrEmpty(Gender))
                    {
                        return "Gender is Required";
                    }
                }
                if (columnName == "Department")
                {
                    if (string.IsNullOrEmpty(Department))
                    {
                        return "Department is Required";
                    }
                }
                if (columnName == "Position")
                {
                    if (string.IsNullOrEmpty(Position))
                    {
                        return "Position is Required";
                    }
                }
                if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                    {
                        return "Email is Required";
                    }
                }
                if (columnName == "OfficeRoomNumber")
                {
                    if (string.IsNullOrEmpty(OfficeRoomNumber))
                    {
                        return "OfficeRoomNumber is Required";
                    }
                }

                return string.Empty;
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private string _gender = string.Empty;
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private string _department = string.Empty;
        public string Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                OnPropertyChanged(nameof(Department));
            }
        }
        private string _position = string.Empty;
        public string Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
        private string _email = string.Empty;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _officeRoomNumber = string.Empty;
        public string OfficeRoomNumber
        {
            get
            {
                return _officeRoomNumber;
            }
            set
            {
                _officeRoomNumber = value;
                OnPropertyChanged(nameof(OfficeRoomNumber));
            }
        }

        private string _response = string.Empty;
        public string Response
        {
            get
            {
                return _response;
            }
            set
            {
                _response = value;
                OnPropertyChanged(nameof(Response));
            }
        }


        private ICommand? _back = null;
        public ICommand? Back
        {
            get
            {
                if (_back is null)
                {
                    _back = new RelayCommand<object>(NavigateBack);
                }
                return _back;
            }
        }
        private ObservableCollection<Subject>? _assignedSubjects = null;
        public ObservableCollection<Subject>? AssignedSubjects
        {
            get
            {
                if (_assignedSubjects is null)
                {
                    _assignedSubjects = LoadSubjects();
                    return _assignedSubjects;
                }
                return _assignedSubjects;
            }
            set
            {
                _assignedSubjects = value;
                OnPropertyChanged(nameof(AssignedSubjects));
            }
        }
        private void NavigateBack(object? obj)
        {
            var instance = MainWindowViewModel.Instance();
            if (instance is not null)
            {
                instance.FacultyMembersSubView = new FacultyMembersViewModel(_context, _dialogService);
            }
        }

        private ICommand? _save = null;
        public ICommand? Save
        {
            get
            {
                if (_save is null)
                {
                    _save = new RelayCommand<object>(SaveData);
                }
                return _save;
            }
        }

        private void SaveData(object? obj)
        {
            if (!IsValid())
            {
                Response = "Please complete all required fields";
                return;
            }

            FacultyMember facultyMember = new FacultyMember
            {
                Name = this.Name,
                Age = this.Age,
                Gender = this.Gender,
                Department = this.Department,
                Position = this.Position,
                Email = this.Email,
                OfficeRoomNumber = this.OfficeRoomNumber,
                Subjects = AssignedSubjects?.Where(s => s.IsSelected).ToList()
            };

            _context.FacultyMembers.Add(facultyMember);
            _context.SaveChanges();

            Response = "Data Saved";
        }

        public AddFacultyMemberViewModel(UniversityContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;
        }
        private ObservableCollection<Subject> LoadSubjects()
        {
            _context.Database.EnsureCreated();
            _context.Subjects.Load();
            return _context.Subjects.Local.ToObservableCollection();
        }
        private bool IsValid()
        {
            string[] properties = { "Name", "Age", "Gender", "Department", "Position", "Email", "OfficeRoomNumber" };
            foreach (string property in properties)
            {
                if (!string.IsNullOrEmpty(this[property]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
