using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
using Xunit;

namespace University.Tests
{
    [TestClass]
    public class BookTest
    {
        private IDialogService _dialogService;
        private DbContextOptions<UniversityContext> _options;
        public string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "booksTest.json");
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

            List<Book> books = new List<Book>
    {
        new Book { BookId = 1, Title = "Wieńczysław", Author = "Autor", Publisher = "Publisher",
            PublicationDate = new DateTime(1987, 05, 22), Isbn = "123", Genre = "Test123", Description = "Test312" },
    };


            IDataAccessService<Book> dataAccessService = new JsonDataAccessService<Book>(jsonFilePath);
            dataAccessService.SaveData(books);
        }

        [TestMethod]
        public void Show_all_Books()
        {
            IDataAccessService<Book> dataAccessService = new JsonDataAccessService<Book>(jsonFilePath);
            var books = dataAccessService.LoadData();

            bool hasData = books.Any();
            Assert.IsTrue(hasData);
        }
        [TestMethod]
        public void Add_Book()
        {
            IDataAccessService<Book> dataAccessService = new JsonDataAccessService<Book>(jsonFilePath);

            var existingBooks = dataAccessService.LoadData().ToList();

            existingBooks.Add(new Book
            {
                Title = "Test123",
                Author = "autor532",
                Publisher = "publisher642",
                PublicationDate = new DateTime(1988, 05, 22),
                Isbn = "isbn903",
                Genre = "genre893",
                Description = "4123",
            });

            dataAccessService.SaveData(existingBooks);

            bool newBookExistInJson = existingBooks.Any(s => s.Title == "Test123" && s.Author == "autor532" && s.Publisher == "publisher642" && s.Isbn == "isbn903" && s.Genre == "genre893"
            && s.Description == "4123");

            Assert.IsTrue(newBookExistInJson);

            Cleanup();
        }
        [TestMethod]
        public void Add_Book_Without_Title()
        {
            IDataAccessService<Book> dataAccessService = new JsonDataAccessService<Book>(jsonFilePath);

            var existingBooks = dataAccessService.LoadData().ToList();

            int initialBookCount = existingBooks.Count;

            existingBooks.Add(new Book
            {
                Author = "autor532",
                Publisher = "publisher642",
                PublicationDate = new DateTime(1988, 05, 22),
                Isbn = "isbn903",
                Genre = "genre893",
                Description = "4123",
            });

            dataAccessService.SaveData(existingBooks);

            existingBooks = dataAccessService.LoadData().ToList();

            int finalBookCount = existingBooks.Count;
            Assert.AreEqual(initialBookCount + 1, finalBookCount);
        }
        [TestMethod]
        public void Add_Book_Without_Author()
        {
            IDataAccessService<Book> dataAccessService = new JsonDataAccessService<Book>(jsonFilePath);

            var existingBooks = dataAccessService.LoadData().ToList();

            int initialBookCount = existingBooks.Count;

            using UniversityContext dbContext = new UniversityContext(_options);
            {
                AddBookViewModel addBookViewModel = new AddBookViewModel(dbContext, _dialogService)
                {
                    Title = "Test123",
                    Publisher = "publisher642",
                    PublicationDate = new DateTime(1988, 05, 22),
                    Isbn = "isbn903",
                    Genre = "genre893",
                    Description = "4123",
                };
                addBookViewModel.Save.Execute(null);
            }

            existingBooks = dataAccessService.LoadData().ToList();

            int finalBookCount = existingBooks.Count;

            Assert.AreEqual(initialBookCount, finalBookCount);
        }
        [TestMethod]
        public void Add_Book_Without_Publisher()
        {
            IDataAccessService<Book> dataAccessService = new JsonDataAccessService<Book>(jsonFilePath);

            var existingBooks = dataAccessService.LoadData().ToList();

            int initialBookCount = existingBooks.Count;

            using UniversityContext dbContext = new UniversityContext(_options);
            {
                AddBookViewModel addBookViewModel = new AddBookViewModel(dbContext, _dialogService)
                {
                    Title = "Test123",
                    Author = "autor532",
                    PublicationDate = new DateTime(1988, 05, 22),
                    Isbn = "isbn903",
                    Genre = "genre893",
                    Description = "4123",
                };
                addBookViewModel.Save.Execute(null);
            }

            existingBooks = dataAccessService.LoadData().ToList();

            int finalBookCount = existingBooks.Count;

            Assert.AreEqual(initialBookCount, finalBookCount);
        }
        [TestMethod]
        public void Add_Book_Without_Isbn()
        {
            IDataAccessService<Book> dataAccessService = new JsonDataAccessService<Book>(jsonFilePath);

            var existingBooks = dataAccessService.LoadData().ToList();

            int initialBookCount = existingBooks.Count;

            using UniversityContext dbContext = new UniversityContext(_options);
            {
                AddBookViewModel addBookViewModel = new AddBookViewModel(dbContext, _dialogService)
                {
                    Title = "Test123",
                    Author = "autor532",
                    Publisher = "publisher642",
                    PublicationDate = new DateTime(1988, 05, 22),
                    Genre = "genre893",
                    Description = "4123",
                };
                addBookViewModel.Save.Execute(null);
            }

            existingBooks = dataAccessService.LoadData().ToList();

            int finalBookCount = existingBooks.Count;

            Assert.AreEqual(initialBookCount, finalBookCount);
        }
        [TestMethod]
        public void Add_Book_Without_Genre()
        {
            IDataAccessService<Book> dataAccessService = new JsonDataAccessService<Book>(jsonFilePath);

            var existingBooks = dataAccessService.LoadData().ToList();

            int initialBookCount = existingBooks.Count;

            using UniversityContext dbContext = new UniversityContext(_options);
            {
                AddBookViewModel addBookViewModel = new AddBookViewModel(dbContext, _dialogService)
                {
                    Title = "Test123",
                    Author = "autor532",
                    Publisher = "publisher642",
                    PublicationDate = new DateTime(1988, 05, 22),
                    Isbn = "isbn903",
                    Description = "4123",
                };
                addBookViewModel.Save.Execute(null);
            }

            existingBooks = dataAccessService.LoadData().ToList();

            int finalBookCount = existingBooks.Count;

            Assert.AreEqual(initialBookCount, finalBookCount);
        }
        [TestCleanup]
        public void Cleanup()
        {
            IDataAccessService<Book> dataAccessService = new JsonDataAccessService<Book>(jsonFilePath);

            var existingBooks = dataAccessService.LoadData().ToList();

            var bookToRemove = existingBooks.FirstOrDefault(s => s.Title == "Test123" && s.Author == "autor532" && s.Publisher == "publisher642" && s.Isbn == "isbn903" && s.Genre == "genre893"
                && s.Description == "4123");

            if (bookToRemove != null)
            {
                existingBooks.Remove(bookToRemove);

                dataAccessService.SaveData(existingBooks);
            }
        }
    }
}