using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    public class FacultyMemberTest
    {
        private IDialogService _dialogService;
        private DbContextOptions<UniversityContext> _options;
        public string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "facultyMemberTest.json");

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

            List<FacultyMember> facultyMembers = new List<FacultyMember>
    {
         new FacultyMember { FacultyId = 1, Name = "Wieńczysław", Age = 10, Gender = "woman",
                    Department="test", Position="Rybnik", Email="Rybnik", OfficeRoomNumber="Test"},
    };


            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);
            dataAccessService.SaveData(facultyMembers);
        }
        [TestMethod]
        public void Show_all_facultyMembers()
        {
            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);
            var facultyMembers = dataAccessService.LoadData();

            bool hasData = facultyMembers.Any();
            Assert.IsTrue(hasData);
        }
        [TestMethod]
        public void Add_FacultyMember()
        {
            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);
            var existingFacultyMembers = dataAccessService.LoadData().ToList();
            existingFacultyMembers.Add(new FacultyMember
            {
                Name = "Test123",
                Age = 10,
                Gender = "Woman",
                Department = "Test1337",
                Position = "position",
                Email = "test@gmail.com",
                OfficeRoomNumber = "10",
            });
            dataAccessService.SaveData(existingFacultyMembers);

            bool newFacultyMemberExistInJson = existingFacultyMembers.Any(s => s.Name == "Test123" && s.Age == 10 && s.Gender == "Woman" && s.Department == "Test1337" && s.Position == "position"
                && s.Email == "test@gmail.com" && s.OfficeRoomNumber == "10");
            Assert.IsTrue(newFacultyMemberExistInJson);

            Cleanup();
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Name()
        {
            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);

            var existingFacultyMembers = dataAccessService.LoadData().ToList();

            int initialFacultyMemberCount = existingFacultyMembers.Count;

            existingFacultyMembers.Add(new FacultyMember
            {
                Age = 10,
                Gender = "Woman",
                Department = "Test1337",
                Position = "position",
                Email = "test@gmail.com",
                OfficeRoomNumber = "10",
            });

            dataAccessService.SaveData(existingFacultyMembers);

            existingFacultyMembers = dataAccessService.LoadData().ToList();

            int finalFacultyMemberCount = existingFacultyMembers.Count;
            Assert.AreEqual(initialFacultyMemberCount + 1, finalFacultyMemberCount);
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Age()
        {
            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);

            var existingFacultyMembers = dataAccessService.LoadData().ToList();

            int initialFacultyMemberCount = existingFacultyMembers.Count;

            existingFacultyMembers.Add(new FacultyMember
            {
                Name = "Test123",
                Gender = "Woman",
                Department = "Test1337",
                Position = "position",
                Email = "test@gmail.com",
                OfficeRoomNumber = "10",
            });

            dataAccessService.SaveData(existingFacultyMembers);

            existingFacultyMembers = dataAccessService.LoadData().ToList();

            int finalFacultyMemberCount = existingFacultyMembers.Count;
            Assert.AreEqual(initialFacultyMemberCount + 1, finalFacultyMemberCount);
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Gender()
        {
            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);

            var existingFacultyMembers = dataAccessService.LoadData().ToList();

            int initialFacultyMemberCount = existingFacultyMembers.Count;

            existingFacultyMembers.Add(new FacultyMember
            {
                Name = "Test123",
                Age = 10,
                Department = "Test1337",
                Position = "position",
                Email = "test@gmail.com",
                OfficeRoomNumber = "10",
            });

            dataAccessService.SaveData(existingFacultyMembers);

            existingFacultyMembers = dataAccessService.LoadData().ToList();

            int finalFacultyMemberCount = existingFacultyMembers.Count;
            Assert.AreEqual(initialFacultyMemberCount + 1, finalFacultyMemberCount);
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Department()
        {
            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);

            var existingFacultyMembers = dataAccessService.LoadData().ToList();

            int initialFacultyMemberCount = existingFacultyMembers.Count;

            existingFacultyMembers.Add(new FacultyMember
            {
                Name = "Test123",
                Age = 10,
                Gender = "Woman",
                Position = "position",
                Email = "test@gmail.com",
                OfficeRoomNumber = "10",
            });

            dataAccessService.SaveData(existingFacultyMembers);

            existingFacultyMembers = dataAccessService.LoadData().ToList();

            int finalFacultyMemberCount = existingFacultyMembers.Count;
            Assert.AreEqual(initialFacultyMemberCount + 1, finalFacultyMemberCount);
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Position()
        {
            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);

            var existingFacultyMembers = dataAccessService.LoadData().ToList();

            int initialFacultyMemberCount = existingFacultyMembers.Count;

            existingFacultyMembers.Add(new FacultyMember
            {
                Name = "Test123",
                Age = 10,
                Gender = "Woman",
                Department = "Test1337",
                Email = "test@gmail.com",
                OfficeRoomNumber = "10",
            });

            dataAccessService.SaveData(existingFacultyMembers);

            existingFacultyMembers = dataAccessService.LoadData().ToList();

            int finalFacultyMemberCount = existingFacultyMembers.Count;
            Assert.AreEqual(initialFacultyMemberCount + 1, finalFacultyMemberCount);
        }
        [TestMethod]
        public void Add_FacultyMember_Without_Email()
        {
            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);

            var existingFacultyMembers = dataAccessService.LoadData().ToList();

            int initialFacultyMemberCount = existingFacultyMembers.Count;

            existingFacultyMembers.Add(new FacultyMember
            {
                Name = "Test123",
                Age = 10,
                Gender = "Woman",
                Department = "Test1337",
                Position = "position",
                OfficeRoomNumber = "10",
            });

            dataAccessService.SaveData(existingFacultyMembers);

            existingFacultyMembers = dataAccessService.LoadData().ToList();

            int finalFacultyMemberCount = existingFacultyMembers.Count;
            Assert.AreEqual(initialFacultyMemberCount + 1, finalFacultyMemberCount);
        }
        [TestMethod]
        public void Add_FacultyMember_Without_OfficeRoom()
        {
            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);

            var existingFacultyMembers = dataAccessService.LoadData().ToList();

            int initialFacultyMemberCount = existingFacultyMembers.Count;

            existingFacultyMembers.Add(new FacultyMember
            {
                Name = "Test123",
                Age = 10,
                Gender = "Woman",
                Department = "Test1337",
                Position = "position",
                Email = "test@gmail.com"
            });

            dataAccessService.SaveData(existingFacultyMembers);

            existingFacultyMembers = dataAccessService.LoadData().ToList();

            int finalFacultyMemberCount = existingFacultyMembers.Count;
            Assert.AreEqual(initialFacultyMemberCount + 1, finalFacultyMemberCount);
        }
        [TestCleanup]
        public void Cleanup()
        {
            IDataAccessService<FacultyMember> dataAccessService = new JsonDataAccessService<FacultyMember>(jsonFilePath);

            var existingFacultyMember = dataAccessService.LoadData().ToList();

            var facultyMemberToRemove = existingFacultyMember.FirstOrDefault(s => s.Name == "Test123" && s.Age == 10 && s.Gender == "Woman" && s.Department == "Test1337" && s.Position == "position"
                && s.Email == "test@gmail.com");

            if (facultyMemberToRemove != null)
            {
                existingFacultyMember.Remove(facultyMemberToRemove);

                dataAccessService.SaveData(existingFacultyMember);
            }
        }
    }
}