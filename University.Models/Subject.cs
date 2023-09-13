using System;
using System.Collections.Generic;
using University.Interfaces;

namespace University.Models
{
    public class Subject : ISubject
    {
        public long SubjectId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string Lecturer { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? Credits { get; set; } = null;
        public string Department { get; set; } = string.Empty;
        public virtual ICollection<Subject>? Prerequisite { get; set; } = null;
        public string PrerequisiteName { get; set; } = string.Empty;
        public bool IsSelected { get; set; } = false;
        public virtual ICollection<Student>? Students { get; set; } = null;
        public virtual ICollection<FacultyMember>? FacultyMembers { get; set; } = null;
    }
}
