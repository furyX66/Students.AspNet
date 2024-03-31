using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{
    bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst);

    Task<Student> DisplayStudent(int? id);

    bool CheckStudentExist(int? id);

    Task<bool> StudentDeleteConfirm(int id);

    Task<Student> EditStudent(int? id);

    Task<List<Student>> IndexStudents();

    Task<Student> StudentDetails(int? id);

    Task<bool> CreateStudent(int id, string name, int age, string major, int[] subjectIdDst);

    Student CreateStudent();

    Task<Subject> SubjectDetails(int? id);

    Task<Subject> EditSubject(int? id);

    Task<Subject> DeleteSubject(int? id);

    Task<bool> SubjectDeleteConfirm(int? id);

    bool CheckSubjectExist(int? id);

    Task<Subject> EditSubject(int id, Subject subject);

    Task<Subject> CreateSubject(Subject subject);
}