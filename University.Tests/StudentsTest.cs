using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using University.Data;
using University.Interfaces;
using University.Models;
using University.Services;
using University.ViewModels;

namespace University.Tests;

[TestClass]
public class StudentsTest
{
    private IDialogService _dialogService;
    private DbContextOptions<UniversityContext> _options;

    [TestInitialize()]
    public void Initialize()
    {
        _options = new DbContextOptionsBuilder<UniversityContext>()
            .UseInMemoryDatabase(databaseName: "UniversityTestDB")
            .Options;
        SeedTestDB();
        _dialogService = new DialogService();
    }

    private void SeedTestDB()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            context.Database.EnsureDeleted();
            List<Student> students = new List<Student>
            {
                new Student { StudentId = 1, Name = "Wieñczys³aw", LastName = "Nowakowicz", PESEL="94010787161", BirthDate = new DateTime(1987, 05, 22),
                    Gender="Man", PlaceOfBirth="Rybnik", PlaceOfResidence="Rybnik", AddressLine1="Test", AddressLine2="Test",PostalCode="12345" },
                new Student { StudentId = 2, Name = "Wieñczys³aw", LastName = "Nowakowicz", PESEL="94010787161", BirthDate = new DateTime(1987, 05, 22),
                    Gender="Man", PlaceOfBirth="Rybnik", PlaceOfResidence="Rybnik", AddressLine1="Test", AddressLine2="Test",PostalCode="12345"},
                new Student { StudentId = 3, Name = "Wieñczys³aw", LastName = "Nowakowicz", PESEL="94010787161", BirthDate = new DateTime(1987, 05, 22),
                    Gender="Man", PlaceOfBirth="Rybnik", PlaceOfResidence="Rybnik", AddressLine1="Test", AddressLine2="Test",PostalCode="12345" }
            };
            List<Subject> subjects = new List<Subject>
            {
                new Subject { SubjectId = 1, Name = "Matematyka", Semester = "1", Lecturer = "Michalina Beldzik",CourseCode="1"
                ,Title="test",Instructor="test",Schedule="test",Description="test",Credits=1,Department="test"},
                new Subject { SubjectId = 2, Name = "Biologia", Semester = "2", Lecturer = "Halina Kopeæ",CourseCode="1"
                ,Title="test",Instructor="test",Schedule="test",Description="test",Credits=1,Department="test" },
                new Subject { SubjectId = 3, Name = "Chemia", Semester = "3", Lecturer = "Jan Nowak",CourseCode="1"
                ,Title="test",Instructor="test",Schedule="test",Description="test",Credits=1,Department="test" }
            };
            context.Students.AddRange(students);
            context.Subjects.AddRange(subjects);
            context.SaveChanges();
        }
    }

    [TestMethod]
    public void Show_all_students()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            StudentsViewModel studentsViewModel = new StudentsViewModel(context, _dialogService);
            bool hasData = studentsViewModel.Students.Any();
            Assert.IsTrue(hasData);
        }
    }

    [TestMethod]
    public void Add_Student_without_subjects()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                Gender = "Man",
                PlaceOfBirth = "Rybnik",
                PlaceOfResidence = "Rybnik",
                AddressLine1 = "Test",
                AddressLine2 = "Test",
                PostalCode = "12345"
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116" && s.Gender=="Man" && s.PlaceOfBirth=="Rybnik"
            && s.PlaceOfResidence=="Rybnik" && s.AddressLine1=="Test" && s.AddressLine2 == "Test" && s.PostalCode=="12345");
            Assert.IsTrue(newStudentExists);
        }
    }

    [TestMethod]
    public void Add_studend_with_subjects()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            Random random = new Random();
            int toSkip = random.Next(0, context.Subjects.Count());
            Subject subject = context.Subjects.OrderBy(x => x.SubjectId).Skip(toSkip).Take(1).FirstOrDefault();
            subject.IsSelected = true;

            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                Gender = "Man",
                PlaceOfBirth = "Rybnik",
                PlaceOfResidence = "Rybnik",
                AddressLine1 = "Test",
                AddressLine2 = "Test",
                PostalCode = "12345",
                AssignedSubjects = new ObservableCollection<Subject>
            {
                subject
            }
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116" && s.Gender == "Man" && s.PlaceOfBirth == "Rybnik"
            && s.PlaceOfResidence == "Rybnik" && s.AddressLine1 == "Test" && s.AddressLine2 == "Test" && s.PostalCode == "12345" && s.Subjects.Any());
            Assert.IsTrue(newStudentExists);
        }
    }

    [TestMethod]
    public void Add_Studend_without_name()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                Gender = "Man",
                PlaceOfBirth = "Rybnik",
                PlaceOfResidence = "Rybnik",
                AddressLine1 = "Test",
                AddressLine2 = "Test",
                PostalCode = "12345",
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.LastName == "Doe" && s.PESEL == "67111994116" && s.Gender == "Man" && s.PlaceOfBirth == "Rybnik"
            && s.PlaceOfResidence == "Rybnik" && s.AddressLine1 == "Test" && s.AddressLine2 == "Test" && s.PostalCode == "12345");
            Assert.IsFalse(newStudentExists);
        }
    }

    [TestMethod]
    public void Add_Studend_without_last_name()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                Gender = "Man",
                PlaceOfBirth = "Rybnik",
                PlaceOfResidence = "Rybnik",
                AddressLine1 = "Test",
                AddressLine2 = "Test",
                PostalCode = "12345",
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.PESEL == "67111994116" && s.Gender == "Man" && s.PlaceOfBirth == "Rybnik"
            && s.PlaceOfResidence == "Rybnik" && s.AddressLine1 == "Test" && s.AddressLine2 == "Test" && s.PostalCode == "12345");
            Assert.IsFalse(newStudentExists);
        }
    }

    [TestMethod]
    public void Add_Studend_without_PESEL()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1967, 12, 06),
                Gender = "Man",
                PlaceOfBirth = "Rybnik",
                PlaceOfResidence = "Rybnik",
                AddressLine1 = "Test",
                AddressLine2 = "Test",
                PostalCode = "12345",
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.Gender == "Man" && s.PlaceOfBirth == "Rybnik"
            && s.PlaceOfResidence == "Rybnik" && s.AddressLine1 == "Test" && s.AddressLine2 == "Test" && s.PostalCode == "12345");
            Assert.IsFalse(newStudentExists);
        }
    }
    [TestMethod]
    public void Add_Studend_without_Gender()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                PlaceOfBirth = "Rybnik",
                PlaceOfResidence = "Rybnik",
                AddressLine1 = "Test",
                AddressLine2 = "Test",
                PostalCode = "12345",
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116" && s.PlaceOfBirth == "Rybnik"
            && s.PlaceOfResidence == "Rybnik" && s.AddressLine1 == "Test" && s.AddressLine2 == "Test" && s.PostalCode == "12345");
            Assert.IsFalse(newStudentExists);
        }
    }
    [TestMethod]
    public void Add_Studend_without_PlaceOfBirth()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                Gender = "Man",
                PlaceOfResidence = "Rybnik",
                AddressLine1 = "Test",
                AddressLine2 = "Test",
                PostalCode = "12345",
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116" && s.Gender == "Man"
            && s.PlaceOfResidence == "Rybnik" && s.AddressLine1 == "Test" && s.AddressLine2 == "Test" && s.PostalCode == "12345");
            Assert.IsFalse(newStudentExists);
        }
    }
    [TestMethod]
    public void Add_Studend_without_PlaceOfResidence()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                Gender = "Man",
                PlaceOfBirth = "Rybnik",
                AddressLine1 = "Test",
                AddressLine2 = "Test",
                PostalCode = "12345",
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116" && s.Gender == "Man" && s.PlaceOfBirth == "Rybnik"
            && s.AddressLine1 == "Test" && s.AddressLine2 == "Test" && s.PostalCode == "12345");
            Assert.IsFalse(newStudentExists);
        }
    }
    [TestMethod]
    public void Add_Studend_without_AddressLine1()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                Gender = "Man",
                PlaceOfBirth = "Rybnik",
                PlaceOfResidence = "Rybnik",
                AddressLine2 = "Test",
                PostalCode = "12345",
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116" && s.Gender == "Man" && s.PlaceOfBirth == "Rybnik"
            && s.PlaceOfResidence == "Rybnik" && s.AddressLine2 == "Test" && s.PostalCode == "12345");
            Assert.IsFalse(newStudentExists);
        }
    }
    [TestMethod]
    public void Add_Studend_without_AddressLine2()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                Gender = "Man",
                PlaceOfBirth = "Rybnik",
                PlaceOfResidence = "Rybnik",
                AddressLine1 = "Test",
                PostalCode = "12345",
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116" && s.Gender == "Man" && s.PlaceOfBirth == "Rybnik"
            && s.PlaceOfResidence == "Rybnik" && s.AddressLine1 == "Test" && s.PostalCode == "12345");
            Assert.IsFalse(newStudentExists);
        }
    }
    [TestMethod]
    public void Add_Studend_without_PostalCode()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                Gender = "Man",
                PlaceOfBirth = "Rybnik",
                PlaceOfResidence = "Rybnik",
                AddressLine1 = "Test",
                AddressLine2 = "Test",
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116" && s.Gender == "Man" && s.PlaceOfBirth == "Rybnik"
            && s.PlaceOfResidence == "Rybnik" && s.AddressLine1 == "Test" && s.AddressLine2 == "Test");
            Assert.IsFalse(newStudentExists);
        }
    }
}
