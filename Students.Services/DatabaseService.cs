using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;

namespace Students.Services;

public class DatabaseService : IDatabaseService
{
    #region Ctor and Properties

    private readonly StudentsContext _context;
    private readonly ILogger<DatabaseService> _logger;

    public DatabaseService(
        ILogger<DatabaseService> logger,
        StudentsContext context)
    {
        _logger = logger;
        _context = context;
    }

    #endregion // Ctor and Properties

    #region Public Methods

    #region StudentMethods
    public Student CreateStudent()
    {
        var listOfSubjects = _context.Subject
                .ToList();
        var listOfFields = _context.FieldOfStudies
                .ToList();
        var newStudent = new Student();
        newStudent.AvailableSubjects = listOfSubjects;
        newStudent.AvailableFieldOfStudies = listOfFields;
        return newStudent;
    } 
    public async Task<Student> StudentDetails(int? id)
    {
        var student = await _context.Student
          .Include(s => s.FieldOfStudies)
          .Include(s => s.StudentSubjects)
              .ThenInclude(ss => ss.Subject)
              .FirstOrDefaultAsync(m => m.Id == id);
        return student;
    }
    public async Task<Student> CreateStudent(Student student, int[] subjectIdDst, int fieldIdDst)
    {
        try         
        {
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();
            var availableSubjects = _context.Subject
                .Where(s => !subjectIdDst.Contains(s.Id))
                .ToList();
            var chosenFieldOfStudies = await _context.FieldOfStudies.FindAsync(fieldIdDst);
            student.FieldOfStudyId = fieldIdDst;

            student.AvailableSubjects = availableSubjects;
            student.FieldOfStudies = chosenFieldOfStudies;

            foreach (var chosenSubject in chosenSubjects)
            {
                student.AddSubject(chosenSubject);
            }

            _context.Add(student);
            await _context.SaveChangesAsync();
            {
                throw new Exception("An error occurred during saving data");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }
        return student;
    }
    public async Task<Student> EditStudent(Student student, int[] subjectIdDst, int fieldIdDst)
    {
        var existingStudent = await _context.Student.FindAsync(student.Id);
        if (existingStudent != null)
        {
            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Major = student.Major;
            existingStudent.PostalCode = student.PostalCode;

            var studentSubjects = await _context.StudentSubject
                .Where(ss => ss.StudentId == student.Id)
                .ToListAsync();
            _context.StudentSubject.RemoveRange(studentSubjects);

            var chosenSubjects = await _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToListAsync();

            var chosenFieldOfStudies = await _context.FieldOfStudies.FindAsync(fieldIdDst);
            existingStudent.FieldOfStudies = chosenFieldOfStudies;

            foreach (var subject in chosenSubjects)
            {
                var studentSubject = new StudentSubject
                {
                    Student = existingStudent,
                    Subject = subject
                };
                _context.StudentSubject.Add(studentSubject);
            }
            await _context.SaveChangesAsync();
        }

        return existingStudent;
    }
    public async Task<List<Student>> IndexStudent()
    {
        var result = await _context.Student.ToListAsync();
        return result;
    }
    public async Task<Student> EditStudent(int? id)
    {
        var student = await _context.Student.FindAsync(id);
        try
        {
            if (id != null)
            {
                if (student != null)
                {
                    var chosenSubjects = _context.StudentSubject
                        .Where(ss => ss.StudentId == id)
                        .Select(ss => ss.Subject)
                        .ToList();
                    var availableSubjects = _context.Subject
                        .Where(s => !chosenSubjects.Contains(s))
                        .ToList();
                    var avaliableFields = _context.FieldOfStudies
                        .ToList();
                    student.FieldOfStudies = await _context.FieldOfStudies.FindAsync(student.FieldOfStudyId);

                    student.StudentSubjects = _context.StudentSubject
                        .Where(x => x.StudentId == id)
                        .ToList();
                    student.AvailableSubjects = availableSubjects;
                    student.AvailableFieldOfStudies = avaliableFields;
                    return student;
                }
                else
                {
                    throw new Exception("An error occured");
                }
            }
            else
            {
                throw new Exception("An error occured");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
            throw new Exception("An error occured");
        }
    }
    public async Task<Student> DisplayStudent(int? id)
    {
        Student? student = null;
        try
        {
            student = _context.Student
                .FirstOrDefault(m => m.Id == id);
            if (student is not null)
            {
                var studentSubjects = await _context.StudentSubject.Where(ss => ss.StudentId == id).Include(ss => ss.Subject).ToListAsync();
                student.StudentSubjects = studentSubjects;
                student.FieldOfStudies = await _context.FieldOfStudies.FindAsync (student.FieldOfStudyId);  
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught in DisplayStudent: " + ex);
        }

        return student ?? throw new Exception("An error occured");
    }
    public async Task<List<Student>> IndexStudents()
    {
        var model = await _context.Student.ToListAsync();
        return model;
    }
    public async Task<bool> StudentDeleteConfirm(int id)
    {
        var result = false;
        var student = await _context.Student.FindAsync(id);
        if (student != null)
        {
            _context.Student.Remove(student);
        }
        var resultChecker = await _context.SaveChangesAsync();
        result = resultChecker > 0;
        return result;
    }
    public bool CheckStudentExist(int? id)
    {
        var result = _context.Student.Any(e => e.Id == id);
        return result;
    }
    #endregion
    #region SubjectMethods
    public async Task<List<Subject>> IndexSubject()
    {
        var result = await _context.Subject.ToListAsync();
        return result;
    }
    public async Task<Subject> SubjectDetails(int? id)
    {
        var subject = await _context.Subject
    .FirstOrDefaultAsync(m => m.Id == id);
        return subject ?? throw new Exception("An error occured");
    }
    public async Task<Subject> EditSubject(int? id)
    {
        var subject = await _context.Subject.FindAsync(id);
        return subject ?? throw new Exception("An error occured");
    }
    public async Task<Subject> EditSubject(int id, Subject subject)
    {
        _context.Update(subject);
        await _context.SaveChangesAsync();
        return subject;
    }
    public async Task<Subject> DeleteSubject(int? id)
    {
        var subject = await _context.Subject
            .FirstOrDefaultAsync(m => m.Id == id);
        return subject ?? throw new Exception("An error occured during removing subject");
    }
    public async Task<bool> SubjectDeleteConfirm(int? id)
    {
        var result = false;
        var subject = await _context.Subject.FindAsync(id);
        if (subject != null)
        {
            _context.Subject.Remove(subject);
        }
        var resultChecker = await _context.SaveChangesAsync();
        result = resultChecker > 0;
        return result;
    }
    public bool CheckSubjectExist(int? id)
    {
        var result = _context.Subject.Any(e => e.Id == id);
        return result;
    }
    public async Task<Subject> CreateSubject(Subject subject)
    {
        _context.Add(subject);
        await _context.SaveChangesAsync();
        return subject;
    }
    #endregion
    #region FieldOfStudiesMethods
    public async Task<List<FieldOfStudies>> IndexFieldOfStudy()
    {
        var model = await _context.FieldOfStudies.ToListAsync();
        return model;
    }
    public async Task<FieldOfStudies> DetailsFieldOfStudies(int? id)
    {
        var fieldOfStudies = await _context.FieldOfStudies
    .FirstOrDefaultAsync(m => m.Id == id);
        return fieldOfStudies ?? throw new Exception("An error occured");
    }
    public async Task<FieldOfStudies> CreateFieldOfStudies(FieldOfStudies fieldOfStudies)
    {
        _context.Add(fieldOfStudies);
        await _context.SaveChangesAsync();
        return fieldOfStudies;
    }
    public async Task<FieldOfStudies> EditFieldOfStudies(int? id)
    {
        var fieldOfStudies = await _context.FieldOfStudies.FindAsync(id);
        return fieldOfStudies ?? throw new Exception("An error occured");
    }
    public async Task<FieldOfStudies> EditFieldOfStudies(int id, FieldOfStudies fieldOfStudies)
    {
        _context.Update(fieldOfStudies);
        await _context.SaveChangesAsync();
        return fieldOfStudies;
    }
    public async Task<FieldOfStudies> DeleteFieldOfStudies(int? id)
    {
        var fieldOfStudies = await _context.FieldOfStudies
            .FirstOrDefaultAsync(m => m.Id == id);
        return fieldOfStudies ?? throw new Exception("An error occured during removing field of studies");
    }
    public async Task<bool> FieldOfStudiesDeleteConfirm(int? id)
    {
        var result = false;
        var fieldOfStudies = await _context.FieldOfStudies.FindAsync(id);
        if (fieldOfStudies != null)
        {
            _context.FieldOfStudies.Remove(fieldOfStudies);
        }
        var resultChecker = await _context.SaveChangesAsync();
        result = resultChecker > 0;
        return result;
    }
    public bool CheckFieldOfStudiesExist(int? id)
    {
        var result = _context.FieldOfStudies.Any(e => e.Id == id);
        return result;
    }
    #endregion
    #region BookMethods
    public async Task<List<Book>> IndexBook()
    {
        var result = await _context.Book.ToListAsync();
        return result;
    }
    public async Task<Book> DetailsBook(int? id)
    {
        var book = await _context.Book
    .FirstOrDefaultAsync(m => m.Id == id);
        return book ?? throw new Exception("An error occured");
    }
    public async Task<Book> CreateBook(Book book)
    {
        _context.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }
    public async Task<Book> EditBook(int? id)
    {
        var book = await _context.Book.FindAsync(id);
        return book ?? throw new Exception("An error occured");
    }
    public async Task<Book> EditBook(int id, Book book)
    {
        _context.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }
    public async Task<Book> DeleteBook(int? id)
    {
        var book = await _context.Book
            .FirstOrDefaultAsync(m => m.Id == id);
        return book ?? throw new Exception("An error occured during removing book");
    }
    public async Task<bool> DeleteBookConfirmed(int? id)
    {
        var result = false;
        var book = await _context.Book.FindAsync(id);
        if (book != null)
        {
            _context.Book.Remove(book);
        }
        var resultChecker = await _context.SaveChangesAsync();
        result = resultChecker > 0;
        return result;
    }
    public bool CheckBookExist(int? id)
    {
        var result = _context.Book.Any(e => e.Id == id);
        return result;
    }
    #endregion
    #region HallMethods
    public async Task<List<LectureHall>> IndexHall()
    {
        var result = await _context.LectureHall.ToListAsync();
        return result;
    }
    public async Task<LectureHall> DetailsHall(int? id)
    {
        var hall = await _context.LectureHall
    .FirstOrDefaultAsync(m => m.Id == id);
        return hall ?? throw new Exception("An error occured");
    }
    public async Task<LectureHall> CreateHall(LectureHall hall)
    {
        _context.Add(hall);
        await _context.SaveChangesAsync();
        return hall;
    }
    public async Task<LectureHall> EditHall(int? id)
    {
        var hall = await _context.LectureHall.FindAsync(id);
        return hall ?? throw new Exception("An error occured");
    }
    public async Task<LectureHall> EditHall(int id, LectureHall hall)
    {
        _context.Update(hall);
        await _context.SaveChangesAsync();
        return hall;
    }
    public async Task<LectureHall> DeleteHall(int? id)
    {
        var hall = await _context.LectureHall
            .FirstOrDefaultAsync(m => m.Id == id);
        return hall ?? throw new Exception("An error occured during removing hall");
    }
    public async Task<bool> DeleteHallConfirmed(int? id)
    {
        var result = false;
        var book = await _context.LectureHall.FindAsync(id);
        if (book != null)
        {
            _context.LectureHall.Remove(book);
        }
        var resultChecker = await _context.SaveChangesAsync();
        result = resultChecker > 0;
        return result;
    }
    public bool CheckHallExist(int? id)
    {
        var result = _context.LectureHall.Any(e => e.Id == id);
        return result;
    } 
    #endregion
}
    #endregion // Public Methods

