using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels
{
    public class AddBookViewModel : ViewModelBase, IDataErrorInfo
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
                if (columnName == "Title")
                {
                    if (string.IsNullOrEmpty(Title))
                    {
                        return "Title is Required";
                    }
                }
                if (columnName == "Author")
                {
                    if (string.IsNullOrEmpty(Author))
                    {
                        return "Author is Required";
                    }
                }
                if (columnName == "Publisher")
                {
                    if (string.IsNullOrEmpty(Publisher))
                    {
                        return "Publisher is Required";
                    }
                }
                if (columnName == "PublicationDate")
                {
                    if (PublicationDate is null)
                    {
                        return "PublicationDate is Required";
                    }
                }
                if (columnName == "Isbn")
                {
                    if (string.IsNullOrEmpty(Isbn))
                    {
                        return "Isbn is Required";
                    }
                }
                if (columnName == "Genre")
                {
                    if (string.IsNullOrEmpty(Genre))
                    {
                        return "Genre is Required";
                    }
                }
                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                    {
                        return "Description is Required";
                    }
                }

                return string.Empty;
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

        private string _author = string.Empty;
        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        private string _publisher = string.Empty;
        public string Publisher
        {
            get
            {
                return _publisher;
            }
            set
            {
                _publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }

        private DateTime? _publicationDate = null;
        public DateTime? PublicationDate
        {
            get
            {
                return _publicationDate;
            }
            set
            {
                _publicationDate = value;
                OnPropertyChanged(nameof(PublicationDate));
            }
        }
        private string _isbn = string.Empty;
        public string Isbn
        {
            get
            {
                return _isbn;
            }
            set
            {
                _isbn = value;
                OnPropertyChanged(nameof(Isbn));
            }
        }
        private string _genre = string.Empty;
        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
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
        private ObservableCollection<Library>? _assignedLibraries = null;
        public ObservableCollection<Library>? AssignedLibraries
        {
            get
            {
                if (_assignedLibraries is null)
                {
                    _assignedLibraries = LoadLibraries();
                    return _assignedLibraries;
                }
                return _assignedLibraries;
            }
            set
            {
                _assignedLibraries = value;
                OnPropertyChanged(nameof(AssignedLibraries));
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

        private void NavigateBack(object? obj)
        {
            var instance = MainWindowViewModel.Instance();
            if (instance is not null)
            {
                instance.BooksSubView = new BooksViewModel(_context, _dialogService);
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

            Book book = new Book
            {
                Title = this.Title,
                Author = this.Author,
                Publisher = this.Publisher,
                PublicationDate = this.PublicationDate,
                Isbn = this.Isbn,
                Genre = this.Genre,
                Description = this.Description,
                Libraries = AssignedLibraries?.Where(s => s.IsSelected).ToList()
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            Response = "Data Saved";
        }

        public AddBookViewModel(UniversityContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;
        }
        private ObservableCollection<Library> LoadLibraries()
        {
            _context.Database.EnsureCreated();
            _context.Libraries.Load();
            return _context.Libraries.Local.ToObservableCollection();
        }
        private bool IsValid()
        {
            string[] properties = { "Title", "Author", "Publisher", "PublicationDate", "Isbn", "Genre", "Description" };
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
