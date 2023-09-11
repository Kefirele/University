using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                new Student { StudentId = 1, Name = "Wieńczysław", LastName = "Nowakowicz", PESEL="94010787161", BirthDate = new DateTime(1987, 05, 22),
                    Gender="Man", PlaceOfBirth="Rybnik", PlaceOfResidence="Rybnik", AddressLine1="Test", AddressLine2="Test",PostalCode="12345" },
                new Student { StudentId = 2, Name = "Wieńczysław", LastName = "Nowakowicz", PESEL="94010787161", BirthDate = new DateTime(1987, 05, 22),
                    Gender="Man", PlaceOfBirth="Rybnik", PlaceOfResidence="Rybnik", AddressLine1="Test", AddressLine2="Test",PostalCode="12345"},
                new Student { StudentId = 3, Name = "Wieńczysław", LastName = "Nowakowicz", PESEL="94010787161", BirthDate = new DateTime(1987, 05, 22),
                    Gender="Man", PlaceOfBirth="Rybnik", PlaceOfResidence="Rybnik", AddressLine1="Test", AddressLine2="Test",PostalCode="12345" }
            };
                List<Subject> subjects = new List<Subject>
            {
                new Subject { SubjectId = 1, Name = "Matematyka", Semester = "1", Lecturer = "Michalina Beldzik",CourseCode="1"
                ,Title="test",Instructor="test",Schedule="test",Description="test",Credits=1,Department="test"},
                new Subject { SubjectId = 2, Name = "Biologia", Semester = "2", Lecturer = "Halina Kopeć",CourseCode="1"
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
        public void Show_all_subjects()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                SubjectsViewModel subjectsViewModel = new SubjectsViewModel(context, _dialogService);
                bool hasData = subjectsViewModel.Subjects.Any();
                Assert.IsTrue(hasData);
            }
        }
        [TestMethod]
        public void Add_Subject()
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
                    Credits = 1,
                    Department = "test",
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1" && s.Title == "test"
                && s.Instructor == "test" && s.Schedule == "test" && s.Description == "test" && s.Credits == 1 && s.Department == "test");
                Assert.IsTrue(newSubjectExists);
            }
        }
        [TestMethod]
        public void Add_subject_With_Student()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                Random random = new Random();
                int toSkip = random.Next(0, context.Students.Count());
                Student student = context.Students.OrderBy(x => x.StudentId).Skip(toSkip).Take(1).FirstOrDefault();

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
                    Credits = 1,
                    Department = "test",
                    AssignedStudents = new ObservableCollection<Student>
            {
                student
            }
                };
                addSubjectViewModel.Save.Execute(null);

                bool newSubjectExists = context.Subjects.Any(s => s.Name == "Test123" && s.Semester == "1" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1" && s.Title == "test"
                && s.Instructor == "test" && s.Schedule == "test" && s.Description == "test" && s.Credits == 1 && s.Department =="test"&&  s.Students.Any());
                Assert.IsTrue(newSubjectExists);
            }
        }
        [TestMethod]
        public void Add_Subject_without_name()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddSubjectViewModel addSubjectViewModel = new AddSubjectViewModel(context, _dialogService)
                {
                    Semester = "2",
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

                bool newSubjectExists = context.Subjects.Any(s => s.Semester == "2" && s.Lecturer == "Michalina Beldzik" && s.CourseCode == "1" && s.Title == "test"
                && s.Instructor == "test" && s.Schedule == "test" && s.Description == "test" && s.Credits == 1 && s.Department == "test");
                Assert.IsFalse(newSubjectExists);
            }
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
    }
}
