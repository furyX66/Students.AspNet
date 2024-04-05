using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{
    #region StudentMethods
    Task<List<Student>> IndexStudent();
    Task<Student> DetailsStudent(int? id);
    Student CreateStudent();
    Task<Student> CreateStudent(Student student, int[] subjectIdDst, int fieldIdDst);
    Task<Student> EditStudent(Student student, int[] subjectIdDst, int fieldIdDst);
    Task<Student> EditStudent(int? id);
    Task<Student> DeleteStudent(int? id);
    Task<bool> StudentDeleteConfirmed(int id);
    bool CheckStudentExist(int? id);
    #endregion
    #region SubjectMethods
    Task<List<Subject>> IndexSubject();
    Task<Subject> SubjectDetails(int? id);
    Task<Subject> CreateSubject(Subject subject);
    Task<Subject> EditSubject(int id, Subject subject);
    Task<Subject> EditSubject(int? id);
    Task<Subject> DeleteSubject(int? id);
    Task<bool> SubjectDeleteConfirmed(int? id);
    bool CheckSubjectExist(int? id);
    #endregion
    #region FieldOfStudiesMethods

    Task<List<FieldOfStudies>> IndexFieldOfStudy();
    Task<FieldOfStudies> DetailsFieldOfStudies(int? id);
    Task<FieldOfStudies> CreateFieldOfStudies(FieldOfStudies fieldOfStudies);
    Task<FieldOfStudies> EditFieldOfStudies(int? id);
    Task<FieldOfStudies> EditFieldOfStudies(int id, FieldOfStudies fieldOfStudies);
    Task<FieldOfStudies> DeleteFieldOfStudies(int? id);
    Task<bool> FieldOfStudiesDeleteConfirm(int? id);
    bool CheckFieldOfStudiesExist(int? id);
    #endregion
    #region BookMethods
    Task<List<Book>> IndexBook();
    Task<Book> DetailsBook(int? id);
    Task<Book> CreateBook(Book book);
    Task<Book> EditBook(int? id);
    Task<Book> EditBook(int id, Book book);
    Task<Book> DeleteBook(int? id);
    Task<bool> DeleteBookConfirmed(int? id);
    bool CheckBookExist(int? id);
    #endregion
    #region HallMethods
    Task<List<LectureHall>> IndexHall();
    Task<LectureHall> DetailsHall(int? id);
    Task<LectureHall> CreateHall(LectureHall hall);
    Task<LectureHall> EditHall(int? id);
    Task<LectureHall> EditHall(int id, LectureHall hall);
    Task<LectureHall> DeleteHall(int? id);
    Task<bool> DeleteHallConfirmed(int? id);
    bool CheckHallExist(int? id);
    #endregion
}