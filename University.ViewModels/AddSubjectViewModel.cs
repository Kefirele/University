using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels;

public class AddSubjectViewModel : ViewModelBase, IDataErrorInfo
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
            if (columnName == "Semester")
            {
                if (string.IsNullOrEmpty(Semester))
                {
                    return "Semester is Required";
                }
            }
            if (columnName == "Lecturer")
            {
                if (string.IsNullOrEmpty(Lecturer))
                {
                    return "Lecturer is Required";
                }
            }
            if (columnName == "CourseCode")
            {
                if (string.IsNullOrEmpty(CourseCode))
                {
                    return "CourseCode is Required";
                }
            }
            if (columnName == "Title")
            {
                if (string.IsNullOrEmpty(Title))
                {
                    return "Title is Required";
                }
            }
            if (columnName == "Instructor")
            {
                if (string.IsNullOrEmpty(Instructor))
                {
                    return "Instructor is Required";
                }
            }
            if (columnName == "Schedule")
            {
                if (string.IsNullOrEmpty(Schedule))
                {
                    return "Schedule is Required";
                }
            }
            if (columnName == "Description")
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return "Description is Required";
                }
            }
            if (columnName == "Credits")
            {
                if (!int.TryParse(Credits.ToString(), out int creditsValue) || creditsValue <= -1)
                {
                    return "Enter a valid positive number for Credits";
                }
            }
            if (columnName == "Department")
            {
                if (string.IsNullOrEmpty(Department))
                {
                    return "Department is Required";
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

    private string _semester = string.Empty;
    public string Semester
    {
        get
        {
            return _semester;
        }
        set
        {
            _semester = value;
            OnPropertyChanged(nameof(Semester));
        }
    }

    private string _lecturer = string.Empty;
    public string Lecturer
    {
        get
        {
            return _lecturer;
        }
        set
        {
            _lecturer = value;
            OnPropertyChanged(nameof(Lecturer));
        }
    }
    private string _courseCode = string.Empty;
    public string CourseCode
    {
        get
        {
            return _courseCode;
        }
        set
        {
            _courseCode = value;
            OnPropertyChanged(nameof(CourseCode));
        }
    }

    private string _title = string.Empty;
    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    private string _instructor = string.Empty;
    public string Instructor
    {
        get
        {
            return _instructor;
        }
        set
        {
            _instructor = value;
            OnPropertyChanged(nameof(Instructor));
        }
    }
    private string _schedule = string.Empty;
    public string Schedule
    {
        get
        {
            return _schedule;
        }
        set
        {
            _schedule = value;
            OnPropertyChanged(nameof(Schedule));
        }
    }
    private string _description = string.Empty;
    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }
    private int _credits;
    public int Credits
    {
        get
        {
            return _credits;
        }
        set
        {
            _credits = value;
            OnPropertyChanged(nameof(Credits));
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
    private ObservableCollection<Subject>? _availablePrerequisites = null;
    public ObservableCollection<Subject> AvailablePrerequisites
    {
        get
        {
            if (_availablePrerequisites is null)
            {
                _availablePrerequisites = LoadSubject();
                return _availablePrerequisites;
            }
            return _availablePrerequisites;
        }
        set
        {
            _availablePrerequisites = value;
            OnPropertyChanged(nameof(AvailablePrerequisites));
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

    private ObservableCollection<Student>? _availableStudents = null;
    public ObservableCollection<Student> AvailableStudents
    {
        get
        {
            if (_availableStudents is null)
            {
                _availableStudents = LoadStudents();
                return _availableStudents;
            }
            return _availableStudents;
        }
        set
        {
            _availableStudents = value;
            OnPropertyChanged(nameof(AvailableStudents));
        }
    }

    private ObservableCollection<Student>? _assignedStudents = null;
    public ObservableCollection<Student> AssignedStudents
    {
        get
        {
            if (_assignedStudents is null)
            {
                _assignedStudents = new ObservableCollection<Student>();
                return _assignedStudents;
            }
            return _assignedStudents;
        }
        set
        {
            _assignedStudents = value;
            OnPropertyChanged(nameof(AssignedStudents));
        }
    }
    private ObservableCollection<FacultyMember>? _availableFacutlyMembers = null;
    public ObservableCollection<FacultyMember> AvailableFacultyMembers
    {
        get
        {
            if (_availableFacutlyMembers is null)
            {
                _availableFacutlyMembers = LoadFacultyMembers();
                return _availableFacutlyMembers;
            }
            return _availableFacutlyMembers;
        }
        set
        {
            _availableFacutlyMembers = value;
            OnPropertyChanged(nameof(AvailableFacultyMembers));
        }
    }

    private ObservableCollection<FacultyMember>? _assignedFacultyMembers = null;
    public ObservableCollection<FacultyMember> AssignedFacultyMembers
    {
        get
        {
            if (_assignedFacultyMembers is null)
            {
                _assignedFacultyMembers = new ObservableCollection<FacultyMember>();
                return _assignedFacultyMembers;
            }
            return _assignedFacultyMembers;
        }
        set
        {
            _assignedFacultyMembers = value;
            OnPropertyChanged(nameof(AssignedFacultyMembers));
        }
    }

    private ICommand? _back = null;
    public ICommand Back
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

    private void NavigateBack(object? obj)
    {
        var instance = MainWindowViewModel.Instance();
        if (instance is not null)
        {
            instance.SubjectsSubView = new SubjectsViewModel(_context, _dialogService);
        }
    }

    private ICommand? _addStudentButton;
    public ICommand AddStudentButton
    {
        get
        {
            if (_addStudentButton is null)
            {
                _addStudentButton = new RelayCommand<object>(AddStudent);
            }
            return _addStudentButton;
        }
    }
    private ICommand? _addFacultyMemberButton;
    public ICommand AddFacultyMemberButton
    {
        get
        {
            if (_addFacultyMemberButton is null)
            {
                _addFacultyMemberButton = new RelayCommand<object>(AddFacultyMember);
            }
            return _addFacultyMemberButton;
        }
    }

    private void AddStudent(object? obj)
    {
        if (obj is Student student)
        {

            if (AssignedStudents.Contains(student))
            {
                return;
            }
            AssignedStudents.Add(student);
        }
    }
    private void AddFacultyMember(object? obj)
    {
        if (obj is FacultyMember facultyMember)
        {

            if (AssignedFacultyMembers.Contains(facultyMember))
            {
                return;
            }
            AssignedFacultyMembers.Add(facultyMember);
        }
    }

    private ICommand? _removeStudenButton = null;
    public ICommand? RemoveStudentButton
    {
        get
        {
            if (_removeStudenButton is null)
            {
                _removeStudenButton = new RelayCommand<object>(RemoveStudent);
            }
            return _removeStudenButton;
        }
    }
    private ICommand? _removeFacultyMemberButton = null;
    public ICommand? RemoveFacultyMemberButton
    {
        get
        {
            if (_removeFacultyMemberButton is null)
            {
                _removeFacultyMemberButton = new RelayCommand<object>(RemoveFacultyMember);
            }
            return _removeFacultyMemberButton;
        }
    }

    private void RemoveStudent(object? obj)
    {
        if (obj is Student student)
        {
            AssignedStudents.Remove(student);
        }
    }
    private void RemoveFacultyMember(object? obj)
    {
        if (obj is FacultyMember facultyMember)
        {
            AssignedFacultyMembers.Remove(facultyMember);
        }
    }

    private ICommand? _save = null;
    public ICommand Save
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

        Subject subject = new Subject
        {
            Name = this.Name,
            Semester = this.Semester,
            Lecturer = this.Lecturer,
            CourseCode = this.CourseCode,
            Title = this.Title,
            Instructor = this.Instructor,
            Schedule = this.Schedule,
            Description = this.Description,
            Credits = this.Credits,
            Department = this.Department,
            Prerequisite = AvailablePrerequisites?.Where(s => s.IsSelected).ToList(),
            Students = AssignedStudents,
            FacultyMembers = this.AssignedFacultyMembers
        };

        _context.Subjects.Add(subject);
        _context.SaveChanges();

        Response = "Data Saved";
    }

    public AddSubjectViewModel(UniversityContext context, IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;
    }
    private ObservableCollection<Subject> LoadSubject()
    {
        _context.Database.EnsureCreated();
        _context.Subjects.Load();
        return _context.Subjects.Local.ToObservableCollection();
    }

    private ObservableCollection<Student> LoadStudents()
    {
        _context.Database.EnsureCreated();
        _context.Students.Load();
        return _context.Students.Local.ToObservableCollection();
    }
    private ObservableCollection<FacultyMember> LoadFacultyMembers()
    {
        _context.Database.EnsureCreated();
        _context.FacultyMembers.Load();
        return _context.FacultyMembers.Local.ToObservableCollection();
    }

    private bool IsValid()
    {
        string[] properties = { "Name", "Semester", "Lecturer", "CourseCode", "Title", "Instructor", "Schedule", "Description", "Credits", "Department" };
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
