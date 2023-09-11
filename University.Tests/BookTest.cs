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
    public class BookTest
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
                List<Book> books = new List<Book>
            {
                new Book { BookId = 1, Title = "Wieńczysław", Author = "Autor", Publisher = "Publisher",
                    PublicationDate=new DateTime(1987, 05, 22), Isbn="123", Genre="Test123", Description="Test312"},
            };
                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
        [TestMethod]
        public void Show_all_Books()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                BooksViewModel booksViewModel = new BooksViewModel(context, _dialogService);
                bool hasData = booksViewModel.Books.Any();
                Assert.IsTrue(hasData);
            }
        }
        [TestMethod]
        public void Add_Book()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddBookViewModel addBookViewModel = new AddBookViewModel(context, _dialogService)
                {
                    Title = "Test123",
                    Author = "autor532",
                    Publisher = "publisher642",
                    PublicationDate = new DateTime(1988, 05, 22),
                    Isbn = "isbn903",
                    Genre = "genre893",
                    Description = "4123",
                };
                addBookViewModel.Save.Execute(null);

                bool newBookExist = context.Books.Any(s => s.Title == "Test123" && s.Author == "autor532" && s.Publisher == "publisher642" && s.Isbn == "isbn903" && s.Genre == "genre893"
                && s.Description == "4123");
                Assert.IsTrue(newBookExist);
            }
        }
        [TestMethod]
        public void Add_Book_Without_Title()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddBookViewModel addBookViewModel = new AddBookViewModel(context, _dialogService)
                {
                    Author = "autor532",
                    Publisher = "publisher642",
                    PublicationDate = new DateTime(1988, 05, 22),
                    Isbn = "isbn903",
                    Genre = "genre893",
                    Description = "4123",
                };
                addBookViewModel.Save.Execute(null);

                bool newBookExist = context.Books.Any(s => s.Author == "autor532" && s.Publisher == "publisher642" && s.Isbn == "isbn903" && s.Genre == "genre893"
                && s.Description == "4123");
                Assert.IsFalse(newBookExist);
            }
        }
        [TestMethod]
        public void Add_Book_Without_Author()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddBookViewModel addBookViewModel = new AddBookViewModel(context, _dialogService)
                {
                    Title = "Test123",
                    Publisher = "publisher642",
                    PublicationDate = new DateTime(1988, 05, 22),
                    Isbn = "isbn903",
                    Genre = "genre893",
                    Description = "4123",
                };
                addBookViewModel.Save.Execute(null);

                bool newBookExist = context.Books.Any(s => s.Title == "Test123"&& s.Publisher == "publisher642" && s.Isbn == "isbn903" && s.Genre == "genre893"
                && s.Description == "4123");
                Assert.IsFalse(newBookExist);
            }
        }
        [TestMethod]
        public void Add_Book_Without_Publisher()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddBookViewModel addBookViewModel = new AddBookViewModel(context, _dialogService)
                {
                    Title = "Test123",
                    Author = "autor532",
                    PublicationDate = new DateTime(1988, 05, 22),
                    Isbn = "isbn903",
                    Genre = "genre893",
                    Description = "4123",
                };
                addBookViewModel.Save.Execute(null);

                bool newBookExist = context.Books.Any(s => s.Title == "Test123" && s.Author == "autor532"&& s.Isbn == "isbn903" && s.Genre == "genre893"
                && s.Description == "4123");
                Assert.IsFalse(newBookExist);
            }
        }
        [TestMethod]
        public void Add_Book_Without_Isbn()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddBookViewModel addBookViewModel = new AddBookViewModel(context, _dialogService)
                {
                    Title = "Test123",
                    Author = "autor532",
                    Publisher = "publisher642",
                    PublicationDate = new DateTime(1988, 05, 22),
                    Genre = "genre893",
                    Description = "4123",
                };
                addBookViewModel.Save.Execute(null);

                bool newBookExist = context.Books.Any(s => s.Title == "Test123" && s.Author == "autor532" && s.Publisher == "publisher642" && s.Genre == "genre893"
                && s.Description == "4123");
                Assert.IsFalse(newBookExist);
            }
        }
        [TestMethod]
        public void Add_Book_Without_Genre()
        {
            using UniversityContext context = new UniversityContext(_options);
            {
                AddBookViewModel addBookViewModel = new AddBookViewModel(context, _dialogService)
                {
                    Title = "Test123",
                    Author = "autor532",
                    Publisher = "publisher642",
                    PublicationDate = new DateTime(1988, 05, 22),
                    Isbn = "isbn903",
                    Description = "4123",
                };
                addBookViewModel.Save.Execute(null);

                bool newBookExist = context.Books.Any(s => s.Title == "Test123" && s.Author == "autor532" && s.Publisher == "publisher642" && s.Isbn == "isbn903"
                && s.Description == "4123");
                Assert.IsFalse(newBookExist);
            }
        }
    }
}
