namespace University.Interfaces
{
    public interface ISubject
    {
        long SubjectId { get; set; }
        string Name { get; set; }
        string Semester { get; set; }
        string Lecturer { get; set; }
        string CourseCode { get; set; }
        string Title { get; set; }
        string Instructor { get; set; }
        string Schedule { get; set; }
        string Description { get; set; }
        int? Credits { get; set; }
        string Department { get; set; }
        string PrerequisiteName { get; set; }
        bool IsSelected { get; set; }
    }
}
