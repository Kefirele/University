using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using University.Interfaces;

namespace University.Models
{
    public class Student : IStudent
    {
        public ObservableCollection<Subject> AssignedSubjects;

        public long StudentId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PESEL { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; } = null;
        public virtual ICollection<Subject>? Subjects { get; set; } = null;
        public string Gender { get; set; } = string.Empty;
        public string PlaceOfBirth { get; set; } = string.Empty;
        public string PlaceOfResidence { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}
