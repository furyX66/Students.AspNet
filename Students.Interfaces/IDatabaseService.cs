using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{
    Task<Student> EditStudent(Student student, int[] subjectIdDst, int fieldIdDst);

    Task<Student> CreateStudent(Student student, int[] subjectIdDst, int fieldIdDst);
    Task<Student> DisplayStudent(int? id);

    bool CheckStudentExist(int? id);

    Task<bool> StudentDeleteConfirm(int id);

    Task<Student> EditStudent(int? id);

    Task<List<Student>> IndexStudents();

    Task<Student> StudentDetails(int? id);


    Student CreateStudent();

    Task<Subject> SubjectDetails(int? id);

    Task<Subject> EditSubject(int? id);

    Task<Subject> DeleteSubject(int? id);

    Task<bool> SubjectDeleteConfirm(int? id);

    bool CheckSubjectExist(int? id);

    Task<Subject> EditSubject(int id, Subject subject);

    Task<Subject> CreateSubject(Subject subject);
    Task<List<FieldOfStudies>> IndexFieldOfStudy();
    Task<FieldOfStudies> CreateFieldOfStudies(FieldOfStudies fieldOfStudies);
    Task<FieldOfStudies> DetailsFieldOfStudies(int? id);
    Task<FieldOfStudies> EditFieldOfStudies(int? id);
    Task<FieldOfStudies> EditFieldOfStudies(int id, FieldOfStudies fieldOfStudies);
    Task<FieldOfStudies> DeleteFieldOfStudies(int? id);
    Task<bool> FieldOfStudiesDeleteConfirm(int? id);
    bool CheckFieldOfStudiesExist(int? id);
    Task<List<Student>> IndexStudent();
    Task<List<Subject>> IndexSubject();
    Task<List<Book>> IndexBook();
    Task<Book> DetailsBook(int? id);
    Task<Book> CreateBook(Book book);
    Task<Book> EditBook(int? id);
    Task<Book> EditBook(int id, Book book);
    Task<Book> DeleteBook(int? id);
    Task<bool> DeleteBookConfirmed(int? id);
    bool CheckBookExist(int? id);
    Task<List<LectureHall>> IndexHall();
    Task<LectureHall> CreateHall(LectureHall hall);
    Task<LectureHall> DetailsHall(int? id);
    Task<LectureHall> EditHall(int? id);
    Task<LectureHall> EditHall(int id, LectureHall hall);
    Task<LectureHall> DeleteHall(int? id);
    Task<bool> DeleteHallConfirmed(int? id);
    bool CheckHallExist(int? id);

}