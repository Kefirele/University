using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    public class FacultyMemberTest
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
                List<FacultyMember> facultyMembers = new List<FacultyMember>
            {
                new FacultyMember { FacultyId = 1, Name = "Wieńczysław", Age = 10, Gender = "woman",
                    Department="test", Position="Rybnik", Email="Rybnik", OfficeRoomNumber="Test"},
            };
                context.FacultyMembers.AddRange(facultyMembers);
                context.SaveChanges();
            }
        }
        [TestMethod]
        public void Show_all_facultyMembers()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                FacultyMembersViewModel facultyMembersViewModel = new FacultyMembersViewModel(context, _dialogService);
                bool hasData = facultyMembersViewModel.FacultyMembers.Any();
                Assert.IsTrue(hasData);
            }
        }
        [TestMethod]
        public void Add_FacultyMember()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddFacultyMemberViewModel addFacultyMemberViewModel = new AddFacultyMemberViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Age = 10,
                    Gender = "Woman",
                    Department = "Test1337",
                    Position = "position",
                    Email = "test@gmail.com",
                    OfficeRoomNumber = "10",
                };
                addFacultyMemberViewModel.Save.Execute(null);

                bool newFacultyMemberExists = context.FacultyMembers.Any(s => s.Name == "Test123" && s.Age == 10 && s.Gender == "Woman" && s.Department == "Test1337" && s.Position == "position"
                && s.Email == "test@gmail.com" && s.OfficeRoomNumber == "10");
                Assert.IsTrue(newFacultyMemberExists);
            }
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Name()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddFacultyMemberViewModel addFacultyMemberViewModel = new AddFacultyMemberViewModel(context, _dialogService)
                {
                    Age = 10,
                    Gender = "Woman",
                    Department = "Test1337",
                    Position = "position",
                    Email = "test@gmail.com",
                    OfficeRoomNumber = "10",
                };
                addFacultyMemberViewModel.Save.Execute(null);

                bool newFacultyMemberExists = context.FacultyMembers.Any(s =>s.Age == 10 && s.Gender == "Woman" && s.Department == "Test1337" && s.Position == "position"
                && s.Email == "test@gmail.com" && s.OfficeRoomNumber == "10");
                Assert.IsFalse(newFacultyMemberExists);
            }
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Age()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddFacultyMemberViewModel addFacultyMemberViewModel = new AddFacultyMemberViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Gender = "Woman",
                    Department = "Test1337",
                    Position = "position",
                    Email = "test@gmail.com",
                    OfficeRoomNumber = "10",
                };
                addFacultyMemberViewModel.Save.Execute(null);

                bool newFacultyMemberExists = context.FacultyMembers.Any(s => s.Name == "Test123" && s.Gender == "Woman" && s.Department == "Test1337" && s.Position == "position"
                && s.Email == "test@gmail.com" && s.OfficeRoomNumber == "10");
                Assert.IsFalse(newFacultyMemberExists);
            }
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Gender()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddFacultyMemberViewModel addFacultyMemberViewModel = new AddFacultyMemberViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Age = 10,
                    Department = "Test1337",
                    Position = "position",
                    Email = "test@gmail.com",
                    OfficeRoomNumber = "10",
                };
                addFacultyMemberViewModel.Save.Execute(null);

                bool newFacultyMemberExists = context.FacultyMembers.Any(s => s.Name == "Test123" && s.Age == 10 && s.Department == "Test1337" && s.Position == "position"
                && s.Email == "test@gmail.com" && s.OfficeRoomNumber == "10");
                Assert.IsFalse(newFacultyMemberExists);
            }
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Department()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddFacultyMemberViewModel addFacultyMemberViewModel = new AddFacultyMemberViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Age = 10,
                    Gender = "Woman",
                    Position = "position",
                    Email = "test@gmail.com",
                    OfficeRoomNumber = "10",
                };
                addFacultyMemberViewModel.Save.Execute(null);

                bool newFacultyMemberExists = context.FacultyMembers.Any(s => s.Name == "Test123" && s.Age == 10 && s.Gender == "Woman" && s.Position == "position"
                && s.Email == "test@gmail.com" && s.OfficeRoomNumber == "10");
                Assert.IsFalse(newFacultyMemberExists);
            }
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Position()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddFacultyMemberViewModel addFacultyMemberViewModel = new AddFacultyMemberViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Age = 10,
                    Gender = "Woman",
                    Department = "Test1337",
                    Email = "test@gmail.com",
                    OfficeRoomNumber = "10",
                };
                addFacultyMemberViewModel.Save.Execute(null);

                bool newFacultyMemberExists = context.FacultyMembers.Any(s => s.Name == "Test123" && s.Age == 10 && s.Gender == "Woman" && s.Department == "Test1337"
                && s.Email == "test@gmail.com" && s.OfficeRoomNumber == "10");
                Assert.IsFalse(newFacultyMemberExists);
            }
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Email()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddFacultyMemberViewModel addFacultyMemberViewModel = new AddFacultyMemberViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Age = 10,
                    Gender = "Woman",
                    Department = "Test1337",
                    Position = "position",
                    OfficeRoomNumber = "10",
                };
                addFacultyMemberViewModel.Save.Execute(null);

                bool newFacultyMemberExists = context.FacultyMembers.Any(s => s.Name == "Test123" && s.Age == 10 && s.Gender == "Woman" && s.Department == "Test1337" && s.Position == "position"
                && s.OfficeRoomNumber == "10");
                Assert.IsFalse(newFacultyMemberExists);
            }
        }
        [TestMethod]
        public void Add_FacultyMember_Without_OfficeRoom()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddFacultyMemberViewModel addFacultyMemberViewModel = new AddFacultyMemberViewModel(context, _dialogService)
                {
                    Name = "Test123",
                    Age = 10,
                    Gender = "Woman",
                    Department = "Test1337",
                    Position = "position",
                    Email = "test@gmail.com"
                };
                addFacultyMemberViewModel.Save.Execute(null);

                bool newFacultyMemberExists = context.FacultyMembers.Any(s => s.Name == "Test123" && s.Age == 10 && s.Gender == "Woman" && s.Department == "Test1337" && s.Position == "position"
                && s.Email == "test@gmail.com");
                Assert.IsFalse(newFacultyMemberExists);
            }
        }
    }
}
