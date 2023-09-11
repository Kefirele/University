using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels
{
    public class AddLibraryViewModel : ViewModelBase, IDataErrorInfo
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
                if (columnName == "Adress")
                {
                    if (string.IsNullOrEmpty(Adress))
                    {
                        return "Adress is Required";
                    }
                }
                if (columnName == "NumberOfFloors")
                {
                    if (!int.TryParse(NumberOfFloors.ToString(), out int numberOfFloorsValue) || numberOfFloorsValue <= -1)
                    {
                        return "Enter a valid positive number for NumberOfFloors";
                    }
                }
                if (columnName == "NumberOfRooms")
                {
                    if (!int.TryParse(NumberOfRooms.ToString(), out int numberOfRoomsValue) || numberOfRoomsValue <= -1)
                    {
                        return "Enter a valid positive number for NumberOfRooms";
                    }
                }
                if (columnName == "Librarian")
                {
                    if (string.IsNullOrEmpty(Librarian))
                    {
                        return "Librarian is Required";
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

        private string _adress = string.Empty;
        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                OnPropertyChanged(nameof(Adress));
            }
        }

        private int _numberOfFloors;
        public int NumberOfFloors
        {
            get
            {
                return _numberOfFloors;
            }
            set
            {
                _numberOfFloors = value;
                OnPropertyChanged(nameof(NumberOfFloors));
            }
        }
        private int _numberOfRooms;
        public int NumberOfRooms
        {
            get
            {
                return _numberOfRooms;
            }
            set
            {
                _numberOfRooms = value;
                OnPropertyChanged(nameof(NumberOfRooms));
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

        private ObservableCollection<Library>? _facilities = null;
        public ObservableCollection<Library> AvailableFacalities
        {
            get
            {
                if (_facilities is null)
                {
                    _facilities = LoadLibrary();
                    return _facilities;
                }
                return _facilities;
            }
            set
            {
                _facilities = value;
                OnPropertyChanged(nameof(AvailableFacalities));
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
        private string _librarian = string.Empty;
        public string Librarian
        {
            get
            {
                return _librarian;
            }
            set
            {
                _librarian = value;
                OnPropertyChanged(nameof(Librarian));
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
        private ObservableCollection<Book>? _availableBooks = null;
        public ObservableCollection<Book> AvailableBooks
        {
            get
            {
                if (_availableBooks is null)
                {
                    _availableBooks = LoadBooks();
                    return _availableBooks;
                }
                return _availableBooks;
            }
            set
            {
                _availableBooks = value;
                OnPropertyChanged(nameof(AvailableBooks));
            }
        }
        private ObservableCollection<Book>? _assignedBooks = null;
        public ObservableCollection<Book> AssignedBooks
        {
            get
            {
                if (_assignedBooks is null)
                {
                    _assignedBooks = new ObservableCollection<Book>();
                    return _assignedBooks;
                }
                return _assignedBooks;
            }
            set
            {
                _assignedBooks = value;
                OnPropertyChanged(nameof(AssignedBooks));
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
                instance.LibrariesSubView = new LibrariesViewModel(_context, _dialogService);
            }
        }
        private ICommand? _add;
        public ICommand Add
        {
            get
            {
                if (_add is null)
                {
                    _add = new RelayCommand<object>(AddBook);
                }
                return _add;
            }
        }
        private void AddBook(object? obj)
        {
            if (obj is Book book)
            {

                if (AssignedBooks.Contains(book))
                {
                    return;
                }
                AssignedBooks.Add(book);
            }
        }
        private ICommand? _remove = null;
        public ICommand? Remove
        {
            get
            {
                if (_remove is null)
                {
                    _remove = new RelayCommand<object>(RemoveBook);
                }
                return _remove;
            }
        }
        private void RemoveBook(object? obj)
        {
            if (obj is Book book)
            {
                AssignedBooks.Remove(book);
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

            Library library = new Library
            {
                Name = this.Name,
                Adress = this.Adress,
                NumberOfFloors = this.NumberOfFloors,
                NumberOfRooms = this.NumberOfRooms,
                Librarian = this.Librarian,
                Description = this.Description,
                //Facilities = AvailableFacalities?.Where(s => s.IsSelected).ToList(),
                Books = AssignedBooks,
            };

            _context.Libraries.Add(library);
            _context.SaveChanges();

            Response = "Data Saved";
        }
        public AddLibraryViewModel(UniversityContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;
        }
        private ObservableCollection<Library> LoadLibrary()
        {
            _context.Database.EnsureCreated();
            _context.Libraries.Load();
            return _context.Libraries.Local.ToObservableCollection();
        }

        private ObservableCollection<Book> LoadBooks()
        {
            _context.Database.EnsureCreated();
            _context.Books.Load();
            return _context.Books.Local.ToObservableCollection();
        }
        private bool IsValid()
        {
            string[] properties = { "Name", "Adress", "NumberOfFloors", "NumberOfRooms", "Librarian"};
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
