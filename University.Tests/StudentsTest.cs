using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            Name = "Wieñczys³aw",
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
    public void Show_all_students()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var students = dataAccessService.LoadData();

        bool hasData = students.Any();
        Assert.IsTrue(hasData);
    }

    [TestMethod]
    public void Add_Student_without_subjects()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();

        existingStudents.Add(new Student
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
        });

        dataAccessService.SaveData(existingStudents);


        bool newStudnetInJson = existingStudents.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116" && s.Gender == "Man" && s.PlaceOfBirth == "Rybnik"
        && s.PlaceOfResidence == "Rybnik" && s.AddressLine1 == "Test" && s.AddressLine2 == "Test" && s.PostalCode == "12345");

        Assert.IsTrue(newStudnetInJson);

        Cleanup();
    }

    [TestMethod]
    public void Add_Student_Without_Name()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();
        int initialStudentCount = existingStudents.Count;
        existingStudents.Add(new Student
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
        });
        dataAccessService.SaveData(existingStudents);
        existingStudents = dataAccessService.LoadData().ToList();
        int finalStudentCount = existingStudents.Count;
        Assert.AreEqual(initialStudentCount + 1, finalStudentCount);
    }

    [TestMethod]
    public void Add_Student_Without_LastName()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();
        int initialStudentCount = existingStudents.Count;
        existingStudents.Add(new Student
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
        });
        dataAccessService.SaveData(existingStudents);
        existingStudents = dataAccessService.LoadData().ToList();
        int finalStudentCount = existingStudents.Count;
        Assert.AreEqual(initialStudentCount + 1, finalStudentCount);
    }

    [TestMethod]
    public void Add_Student_Without_PESEL()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();
        int initialStudentCount = existingStudents.Count;
        existingStudents.Add(new Student
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
        });
        dataAccessService.SaveData(existingStudents);
        existingStudents = dataAccessService.LoadData().ToList();
        int finalStudentCount = existingStudents.Count;
        Assert.AreEqual(initialStudentCount + 1, finalStudentCount);
    }
    [TestMethod]
    public void Add_Student_Without_Gender()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();
        int initialStudentCount = existingStudents.Count;
        existingStudents.Add(new Student
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
        });
        dataAccessService.SaveData(existingStudents);
        existingStudents = dataAccessService.LoadData().ToList();
        int finalStudentCount = existingStudents.Count;
        Assert.AreEqual(initialStudentCount + 1, finalStudentCount);
    }
    [TestMethod]
    public void Add_Student_Without_PlaceOfBirth()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();
        int initialStudentCount = existingStudents.Count;
        existingStudents.Add(new Student
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
        });
        dataAccessService.SaveData(existingStudents);
        existingStudents = dataAccessService.LoadData().ToList();
        int finalStudentCount = existingStudents.Count;
        Assert.AreEqual(initialStudentCount + 1, finalStudentCount);
    }
    [TestMethod]
    public void Add_Student_Without_PlaceOfResidence()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();
        int initialStudentCount = existingStudents.Count;
        existingStudents.Add(new Student
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
        });
        dataAccessService.SaveData(existingStudents);
        existingStudents = dataAccessService.LoadData().ToList();
        int finalStudentCount = existingStudents.Count;
        Assert.AreEqual(initialStudentCount + 1, finalStudentCount);
    }
    [TestMethod]
    public void Add_Student_Without_AddressLine1()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();
        int initialStudentCount = existingStudents.Count;
        existingStudents.Add(new Student
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
        });
        dataAccessService.SaveData(existingStudents);
        existingStudents = dataAccessService.LoadData().ToList();
        int finalStudentCount = existingStudents.Count;
        Assert.AreEqual(initialStudentCount + 1, finalStudentCount);
    }
    [TestMethod]
    public void Add_Student_Without_AddressLine2()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();
        int initialStudentCount = existingStudents.Count;
        existingStudents.Add(new Student
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
        });
        dataAccessService.SaveData(existingStudents);
        existingStudents = dataAccessService.LoadData().ToList();
        int finalStudentCount = existingStudents.Count;
        Assert.AreEqual(initialStudentCount + 1, finalStudentCount);
    }
    [TestMethod]
    public void Add_Student_Without_PostalCode()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();
        int initialStudentCount = existingStudents.Count;
        existingStudents.Add(new Student
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
        });
        dataAccessService.SaveData(existingStudents);
        existingStudents = dataAccessService.LoadData().ToList();
        int finalStudentCount = existingStudents.Count;
        Assert.AreEqual(initialStudentCount + 1, finalStudentCount);
    }
    [TestCleanup]
    public void Cleanup()
    {
        IDataAccessService<Student> dataAccessService = new JsonDataAccessService<Student>(jsonFilePathStudent);
        var existingStudents = dataAccessService.LoadData().ToList();

        var studentToRemove = existingStudents.FirstOrDefault(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116"
            && s.Gender == "Man" && s.PlaceOfBirth == "Rybnik" && s.PlaceOfResidence == "Rybnik"
            && s.AddressLine1 == "Test" && s.AddressLine2 == "Test" && s.PostalCode == "12345");

        if (studentToRemove != null)
        {
            existingStudents.Remove(studentToRemove);
            dataAccessService.SaveData(existingStudents);
        }
    }
}

