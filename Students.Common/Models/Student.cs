using Students.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Students.Common.Models;

public class Student
{
    public int Id { get; set; }
    public int? FieldOfStudyId { get; set; }

    [Required]
    [StringLength(100)]
    [NameSurname]
    public string Name { get; set; } = string.Empty;

    [Range(1, 100)]
    public int Age { get; set; }

    [Required]
    [StringLength(100)]
    public string Major { get; set; } = string.Empty;
    [PostalCode]
    public string PostalCode {  get; set; } = string.Empty;
    public FieldOfStudies? FieldOfStudies { get; set; }

    public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();

    [NotMapped]
    public List<Subject> AvailableSubjects { get; set; } = new List<Subject>();
    [NotMapped]
    public List<FieldOfStudies> AvailableFieldOfStudies { get; set; } = new List<FieldOfStudies>();

    public Student()
    {
    }

    public Student(string name, int age, string major, string postalCode)
    {
        Name = name;
        Age = age;
        Major = major;
        PostalCode = postalCode;
    }

    public void AddSubject(Subject subject)
    {
        var studentSubject = new StudentSubject
        {
            Student = this,
            Subject = subject
        };
        StudentSubjects.Add(studentSubject);
    }
    public void AddFieldOfStudy(FieldOfStudies fieldOfStudies)
    {
        FieldOfStudies = fieldOfStudies;
    }
}