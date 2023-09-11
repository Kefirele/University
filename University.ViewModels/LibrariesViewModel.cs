using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels
{
    public class LibrariesViewModel : ViewModelBase
    {
        private readonly UniversityContext _context;
        private readonly IDialogService _dialogService;

        private bool? dialogResult;
        public bool? DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                dialogResult = value;
            }
        }

        private ObservableCollection<Library>? libraries = null;
        public ObservableCollection<Library>? Libraries
        {
            get
            {
                if (libraries is null)
                {
                    libraries = new ObservableCollection<Library>();
                    return libraries;
                }
                return libraries;
            }
            set
            {
                libraries = value;
                OnPropertyChanged(nameof(Libraries));
            }
        }
        private ICommand? _add = null;
        public ICommand? Add
        {
            get
            {
                if (_add is null)
                {
                    _add = new RelayCommand<object>(AddNewLibrary);
                }
                return _add;
            }
        }

        private void AddNewLibrary(object? obj)
        {
            var instance = MainWindowViewModel.Instance();
            if (instance is not null)
            {
                instance.LibrariesSubView = new AddLibraryViewModel(_context, _dialogService);
            }
        }
        private ICommand? _edit;
        public ICommand? Edit
        {
            get
            {
                if (_edit is null)
                {
                    _edit = new RelayCommand<object>(EditLibraries);
                }
                return _edit;
            }
        }

        private void EditLibraries(object? obj)
        {
            if (obj is not null)
            {
                long libraryId = (long)obj;
                EditLibraryViewModel editLibraryViewModel = new EditLibraryViewModel(_context, _dialogService)
                {
                    LibraryId = libraryId
                };
                var instance = MainWindowViewModel.Instance();
                if (instance is not null)
                {
                    instance.LibrariesSubView = editLibraryViewModel;
                }
            }
        }
        private ICommand? _remove = null;
        public ICommand? Remove
        {
            get
            {
                if (_remove is null)
                {
                    _remove = new RelayCommand<object>(RemoveLibrary);
                }
                return _remove;
            }
        }

        private void RemoveLibrary(object? obj)
        {
            if (obj is not null)
            {
                long libraryId = (long)obj;
                Library? library = _context.Libraries.Find(libraryId);
                if (library is not null)
                {
                    DialogResult = _dialogService.Show(library.Name);
                    if (DialogResult == false)
                    {
                        return;
                    }

                    _context.Libraries.Remove(library);
                    _context.SaveChanges();
                }
            }
        }
        public LibrariesViewModel(UniversityContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;

            _context.Database.EnsureCreated();
            _context.Libraries.Load();
            Libraries = _context.Libraries.Local.ToObservableCollection();
        }
    }
}
