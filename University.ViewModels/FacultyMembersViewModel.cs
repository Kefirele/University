﻿using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels
{
    public class FacultyMembersViewModel : ViewModelBase
    {
        private readonly UniversityContext _context;
        private readonly IDialogService _dialogService;

        private bool? _dialogResult = null;
        public bool? DialogResult
        {
            get
            {
                return _dialogResult;
            }
            set
            {
                _dialogResult = value;
            }
        }

        private ObservableCollection<FacultyMember>? _facultyMembers = null;
        public ObservableCollection<FacultyMember>? FacultyMembers
        {
            get
            {
                if (_facultyMembers is null)
                {
                    _facultyMembers = new ObservableCollection<FacultyMember>();
                    return _facultyMembers;
                }
                return _facultyMembers;
            }
            set
            {
                _facultyMembers = value;
                OnPropertyChanged(nameof(FacultyMembers));
            }
        }

        private ICommand? _add = null;
        public ICommand? Add
        {
            get
            {
                if (_add is null)
                {
                    _add = new RelayCommand<object>(AddNewFacultyMember);
                }
                return _add;
            }
        }
        
        private void AddNewFacultyMember(object? obj)
        {
            var instance = MainWindowViewModel.Instance();
            if (instance is not null)
            {
                instance.FacultyMembersSubView = new AddFacultyMemberViewModel(_context, _dialogService);
            }
        }
    
        private ICommand? _edit = null;
        public ICommand? Edit
        {
            get
            {
                if (_edit is null)
                {
                    _edit = new RelayCommand<object>(EditFacultyMember);
                }
                return _edit;
            }
        }

        private void EditFacultyMember(object? obj)
        {
            if (obj is not null)
            {
                long facultyId = (long)obj;
                EditFacultyMemberViewModel editFacultyMemberViewModel = new EditFacultyMemberViewModel(_context, _dialogService)
                {
                    FacultyId = facultyId
                };
                var instance = MainWindowViewModel.Instance();
                if (instance is not null)
                {
                    instance.FacultyMembersSubView = editFacultyMemberViewModel;
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
                    _remove = new RelayCommand<object>(RemoveFacultyMember);
                }
                return _remove;
            }
        }

        private void RemoveFacultyMember(object? obj)
        {
            if (obj is not null)
            {
                long facultyMemberId = (long)obj;
                FacultyMember? facultyMember = _context.FacultyMembers.Find(facultyMemberId);
                if (facultyMember is not null)
                {
                    DialogResult = _dialogService.Show(facultyMember.Name);
                    if (DialogResult == false)
                    {
                        return;
                    }

                    _context.FacultyMembers.Remove(facultyMember);
                    _context.SaveChanges();
                }
            }
        }

        public FacultyMembersViewModel(UniversityContext context, IDialogService dialogService)
        {
            _context = context;
            _dialogService = dialogService;

            _context.Database.EnsureCreated();
            _context.FacultyMembers.Load();
            FacultyMembers = _context.FacultyMembers.Local.ToObservableCollection();
        }
    }
}
