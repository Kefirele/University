using University.Models;
using Microsoft.EntityFrameworkCore;

namespace University.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<FacultyMember> FacultyMembers { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<Library> Libraries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("UniversityDb");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().Ignore(s => s.IsSelected);
            modelBuilder.Entity<Subject>().HasMany(s => s.Prerequisite).WithMany().UsingEntity(j => j.ToTable("SubjectPrerequisites"));
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    Name = "Wieńczysław",
                    LastName = "Nowakowicz",
                    PESEL = "90072853789",
                    BirthDate = new DateTime(1987, 05, 22),
                    Gender = "Man",
                    PlaceOfBirth = "Rybnik",
                    PlaceOfResidence = "Rybnik",
                    AddressLine1 = "Ulica",
                    AddressLine2 = "2",
                    PostalCode = "12345"
                },
                new Student
                {
                    StudentId = 2,
                    Name = "Stanisław",
                    LastName = "Nowakowicz",
                    PESEL = "02211197421",
                    BirthDate = new DateTime(2019, 06, 25),
                    Gender = "Man",
                    PlaceOfBirth = "Rybnik",
                    PlaceOfResidence = "Rybnik",
                    AddressLine1 = "Ulica",
                    AddressLine2 = "2",
                    PostalCode = "12345"
                },
                new Student
                {
                    StudentId = 3,
                    Name = "Eugenia",
                    LastName = "Nowakowicz",
                    PESEL = "78011876842",
                    BirthDate = new DateTime(2021, 06, 08),
                    Gender = "Woman",
                    PlaceOfBirth = "Rybnik",
                    PlaceOfResidence = "Rybnik",
                    AddressLine1 = "Ulica",
                    AddressLine2 = "2",
                    PostalCode = "12345"
                });

            modelBuilder.Entity<Subject>().HasData(
               new Subject
               {
                   SubjectId = 1,
                   Name = "Matematyka",
                   Semester = "1",
                   Lecturer = "Michalina Warszawa",
                   CourseCode = "123",
                   Title = "dr.",
                   Instructor = "Michalina Warszawa",
                   Schedule = "1",
                   Description = "test",
                   Credits = 1,
                   Department = "test",
                   PrerequisiteName = "Test"
               },
               new Subject
               {
                   SubjectId = 2,
                   Name = "Biologia",
                   Semester = "2",
                   Lecturer = "Halina Katowice",
                   CourseCode = "123",
                   Title = "dr.",
                   Instructor = "Michalina Warszawa",
                   Schedule = "1",
                   Description = "test",
                   Credits = 1,
                   Department = "test",
                   PrerequisiteName = "Test"
               },
               new Subject
               {
                   SubjectId = 3,
                   Name = "Chemia",
                   Semester = "3",
                   Lecturer = "Jan Nowak",
                   CourseCode = "123",
                   Title = "dr.",
                   Instructor = "Michalina Warszawa",
                   Schedule = "1",
                   Description = "test",
                   Credits = 1,
                   Department = "test",
                   PrerequisiteName = "Test"
               });
            modelBuilder.Entity<FacultyMember>().HasKey(fm => fm.FacultyId);
            modelBuilder.Entity<FacultyMember>().HasData(
               new FacultyMember
               {
                   FacultyId = 1,
                   Name = "Test",
                   Age = 12,
                   Gender = "Man",
                   Department = "School",
                   Position = "Test",
                   Email = "Test",
                   OfficeRoomNumber = "13" 
               },
               new FacultyMember
               {
                   FacultyId = 2,
                   Name = "Test2",
                   Age = 14,
                   Gender = "Woman",
                   Department = "School2",
                   Position = "Test2",
                   Email = "Test2",
                   OfficeRoomNumber = "14"
               },
               new FacultyMember
               {
                   FacultyId = 3,
                   Name = "Test3",
                   Age = 13,
                   Gender = "Woman",
                   Department = "School3",
                   Position = "Test3",
                   Email = "Test3",
                   OfficeRoomNumber = "15"
               }
            );
            modelBuilder.Entity<Book>().HasData(
               new Book
               {
                   BookId = 1,
                   Title = "Test",
                   Author = "Test",
                   Publisher = "Man",
                   PublicationDate = new DateTime(2021, 06, 08),
                   Isbn = "Test",
                   Genre = "Test",
                   Description = "13"
               },
               new Book
               {
                   BookId = 2,
                   Title = "Test",
                   Author = "Test",
                   Publisher = "Man",
                   PublicationDate = new DateTime(2021, 06, 08),
                   Isbn = "Test",
                   Genre = "Test",
                   Description = "13"
               },
               new Book
               {
                   BookId = 3,
                   Title = "Test",
                   Author = "Test",
                   Publisher = "Man",
                   PublicationDate = new DateTime(2021, 06, 08),
                   Isbn = "Test",
                   Genre = "Test",
                   Description = "13"
               }
            );
                        modelBuilder.Entity<Library>().HasData(
               new Library
               {
                   LibraryId = 1,
                   Name = "Test",
                   Adress = "Test",
                   NumberOfFloors = 1,
                   NumberOfRooms = 2,
                   Description = "Test",
                   Librarian = "Test",
               },
                new Library
                {
                    LibraryId = 2,
                    Name = "Test",
                    Adress = "Test",
                    NumberOfFloors = 1,
                    NumberOfRooms = 2,
                    Description = "Test",
                    Librarian = "Test",
                },
                new Library
                {
                    LibraryId = 3,
                    Name = "Test",
                    Adress = "Test",
                    NumberOfFloors = 1,
                    NumberOfRooms = 2,
                    Description = "Test",
                    Librarian = "Test",
                }
            );
        }
    }
}
