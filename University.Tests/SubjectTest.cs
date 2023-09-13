using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.Interfaces;
using University.Models;
using University.Services;
using University.ViewModels;

namespace University.Tests
{
    [TestClass]

    public class SubjectTest
    {
        private IDialogService _dialogService;
        private DbContextOptions<UniversityContext> _options;
        public string jsonFilePathStudent = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "studentTest.json");
        public string jsonFilePathSubject = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "subjectTest.json");
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
                List<Student> students = new List<Student>
    {
        new Student
        {
            StudentId = 1,
            Name = "Wieńczysław",
            LastName = "Nowakowicz",
            PESEL = "94010787161",
            BirthDate = new DateTime(1987, 05, 22),
            Gender = "Man",
            PlaceOfBirth = "Rybnik",
            PlaceOfResidence = "Rybnik",
            AddressLine1 = "Test",
            AddressLine2 = "Test",
            PostalCode = "12345"
        },
    };

                List<Subject> subjects = new List<Subject>
    {
        new Subject
        {
            SubjectId = 1,
            Name = "Matematyka",
            Semester = "1",
            Lecturer = "Michalina Beldzik",
            CourseCode = "1",
            Title = "test",
            Instructor = "test",
            Schedule = "test",
            Description = "test",
            Credits = 1,
            Department = "test"
        },
    };

                IDataAccessService<Student> studentDataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
                IDataAccessService<Subject> subjectDataAccessService = new JsonDataAccessService<Subject>(jsonFilePathSubject);

                studentDataAccessService.SaveData(students);
                subjectDataAccessService.SaveData(subjects);
            }
        }
        [TestMethod]
        public void Show_all_subjects()
        {
            IDataAccessService<Subject> dataAccessService = new JsonDataAccessService<Subject>(jsonFilePathSubject);
            var subjects = dataAccessService.LoadData();

            Console.WriteLine(jsonFilePathSubject);

            bool hasData = subjects.Any();
            Assert.IsTrue(hasData);
        }
        [TestMethod]
        public void Add_Subject()
        {
            IDataAccessService<Subject> dataAccessService = new JsonDataAccessService<Subject>(jsonFilePathSubject);
            var existingSubjects = dataAccessService.LoadData().ToList();

            existingSubjects.Add(new Subject
            {
                Name = "Test123",
                Semester = "1",
                Lecturer = "Michalina Beldzik",
                CourseCode = "1",
                Title = "test",
                Instructor = "test",
                Schedule = "test",
                Description = "test",
                Credits = 1,
                Department = "test"
            });

            dataAccessService.SaveData(existingSubjects);

            bool newSubjectInJson = existingSubjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1" && s.Title == "test"
            && s.Instructor == "test" && s.Schedule == "test" && s.Description == "test" && s.Credits == 1 && s.Department == "test");

            Assert.IsTrue(newSubjectInJson);

            Cleanup();
        }
        [TestMethod]
        public void Add_Subject_without_semester()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddSubjectViewModel addSubjectViewModel = new AddSubjectViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Lecturer = "Michalina Beldzik",
                    CourseCode = "1",
                    Title = "test",
                    Instructor = "test",
                    Schedule = "test",
                    Description = "test",
                    Credits = 1,
                    Department = "test",
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1" && s.Title == "test"
                && s.Instructor == "test" && s.Schedule == "test" && s.Description == "test" && s.Credits == 1 && s.Department == "test");
                Assert.IsFalse(newSubjectExists);
            }
        }
        [TestMethod]
        public void Add_Subject_without_Lecturer()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddSubjectViewModel addSubjectViewModel = new AddSubjectViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Semester = "1",
                    CourseCode = "1",
                    Title = "test",
                    Instructor = "test",
                    Schedule = "test",
                    Description = "test",
                    Credits = 1,
                    Department = "test",
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.CourseCode == "1" && s.Title == "test"
                && s.Instructor == "test" && s.Schedule == "test" && s.Description == "test" && s.Credits == 1 && s.Department == "test");
                Assert.IsFalse(newSubjectExists);
            }
        }
        [TestMethod]
        public void Add_Subject_without_CourseCode()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddSubjectViewModel addSubjectViewModel = new AddSubjectViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Semester = "1",
                    Lecturer = "Michalina Beldzik",
                    Title = "test",
                    Instructor = "test",
                    Schedule = "test",
                    Description = "test",
                    Credits = 1,
                    Department = "test",
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.Lecturer == "Michalina Beldzik" && s.Title == "test"
                && s.Instructor == "test" && s.Schedule == "test" && s.Description == "test" && s.Credits == 1 && s.Department == "test");
                Assert.IsFalse(newSubjectExists);
            }
        }
        [TestMethod]
        public void Add_Subject_without_title()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddSubjectViewModel addSubjectViewModel = new AddSubjectViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Semester = "1",
                    Lecturer = "Michalina Beldzik",
                    CourseCode = "1",
                    Instructor = "test",
                    Schedule = "test",
                    Description = "test",
                    Credits = 1,
                    Department = "test",
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1"
                && s.Instructor == "test" && s.Schedule == "test" && s.Description == "test" && s.Credits == 1 && s.Department == "test");
                Assert.IsFalse(newSubjectExists);
            }
        }
        [TestMethod]
        public void Add_Subject_without_instructor()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddSubjectViewModel addSubjectViewModel = new AddSubjectViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Semester = "1",
                    Lecturer = "Michalina Beldzik",
                    CourseCode = "1",
                    Title = "test",
                    Schedule = "test",
                    Description = "test",
                    Credits = 1,
                    Department = "test",
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1" && s.Title == "test"
                && s.Schedule == "test" && s.Description == "test" && s.Credits == 1 && s.Department == "test");
                Assert.IsFalse(newSubjectExists);
            }
        }
        [TestMethod]
        public void Add_Subject_without_schedule()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddSubjectViewModel addSubjectViewModel = new AddSubjectViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Semester = "1",
                    Lecturer = "Michalina Beldzik",
                    CourseCode = "1",
                    Title = "test",
                    Instructor = "test",
                    Description = "test",
                    Credits = 1,
                    Department = "test",
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1" && s.Title == "test"
                && s.Instructor == "test" && s.Description == "test" && s.Credits == 1 && s.Department == "test");
                Assert.IsFalse(newSubjectExists);
            }
        }
        [TestMethod]
        public void Add_Subject_without_description()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddSubjectViewModel addSubjectViewModel = new AddSubjectViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Semester = "1",
                    Lecturer = "Michalina Beldzik",
                    CourseCode = "1",
                    Title = "test",
                    Instructor = "test",
                    Schedule = "test",
                    Credits = 1,
                    Department = "test",
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1" && s.Title == "test"
                && s.Instructor == "test" && s.Schedule == "test" && s.Credits == 1 && s.Department == "test");
                Assert.IsFalse(newSubjectExists);
            }
        }
        [TestMethod]
        public void Add_Subject_without_minus_credits()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddSubjectViewModel addSubjectViewModel = new AddSubjectViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Semester = "1",
                    Lecturer = "Michalina Beldzik",
                    CourseCode = "1",
                    Title = "test",
                    Instructor = "test",
                    Schedule = "test",
                    Description = "test",
                    Credits = -1,
                    Department = "test",
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1" && s.Title == "test"
                && s.Instructor == "test" && s.Schedule == "test" && s.Description == "test" && s.Credits == -1 && s.Department == "test");
                Assert.IsFalse(newSubjectExists);
            }
        }
        [TestMethod]
        public void Add_Subject_without_departament()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddSubjectViewModel addSubjectViewModel = new AddSubjectViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Semester = "1",
                    Lecturer = "Michalina Beldzik",
                    CourseCode = "1",
                    Title = "test",
                    Instructor = "test",
                    Schedule = "test",
                    Description = "test",
                    Credits = 1
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1" && s.Title == "test"
                && s.Instructor == "test" && s.Schedule == "test" && s.Description == "test" && s.Credits == 1);
                Assert.IsFalse(newSubjectExists);
            }
        }
        private void Cleanup()
        {
            IDataAccessService<Subject> dataAccessService = new JsonDataAccessService<Subject>(jsonFilePathSubject);
            var existingSubjects = dataAccessService.LoadData().ToList();

            // Usuń nowo dodany przedmiot, jeśli istnieje w danych JSON.
            var subjectToRemove = existingSubjects.FirstOrDefault(s => s.Name == "Test123");
            if (subjectToRemove != null)
            {
                existingSubjects.Remove(subjectToRemove);
                dataAccessService.SaveData(existingSubjects);
            }
        }
    }
}
