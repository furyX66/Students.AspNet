using Microsoft.Extensions.Logging;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Students.Services;

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

    public bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst)
    {
        var result = false;

        // Find the student
        var student = _context.Student.Find(id);
        if (student != null)
        {
            // Update the student's properties
            student.Name = name;
            student.Age = age;
            student.Major = major;

            // Get the chosen subjects
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();

            // Remove the existing StudentSubject entities for the student
            var studentSubjects = _context.StudentSubject
                .Where(ss => ss.StudentId == id)
                .ToList();
            _context.StudentSubject.RemoveRange(studentSubjects);

            // Add new StudentSubject entities for the chosen subjects
            foreach (var subject in chosenSubjects)
            {
                var studentSubject = new StudentSubject
                {
                    Student = student,
                    Subject = subject
                };
                _context.StudentSubject.Add(studentSubject);
            }

            // Save changes to the database
            var resultInt = _context.SaveChanges();
            result = resultInt > 0;
        }

        return result;
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
                    student.StudentSubjects = _context.StudentSubject
                        .Where(x => x.StudentId == id)
                        .ToList();
                    student.AvailableSubjects = availableSubjects;
                    return student;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
            return null;
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
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught in DisplayStudent: " + ex);
        }

        return student;
    }
    public async Task<List<Student>> IndexStudents()
    {
        var model = await _context.Student.ToListAsync();
        return model;
    }
    public bool CheckStudentExist(int? id)
    {
        var result = _context.Student.Any(e => e.Id == id);
        return result;
    }
    public async Task<Student> StudentDetails(int? id)
    {
        var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
        var studentSubjects = _context.StudentSubject
            .Where(ss => ss.StudentId == id)
            .Include(ss => ss.Subject)
            .ToList();
        student.StudentSubjects = studentSubjects;
        if (studentSubjects != null)
        {
            return student;
        }
        else
        {
            return null;
        }
    }
    public Student CreateStudent()
    {
        var listOfSubjects = _context.Subject
                .ToList();
        var newStudent = new Student();
        newStudent.AvailableSubjects = listOfSubjects;
        return newStudent;
    }
    public async Task<bool> CreateStudent(int id, string name, int age, string major, int[] subjectIdDst)
    {
        var result = false;
        try
        {
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();
            var availableSubjects = _context.Subject
                .Where(s => !subjectIdDst.Contains(s.Id))
                .ToList();
            var student = new Student()
            {
                Id = id,
                Name = name,
                Age = age,
                Major = major,
                AvailableSubjects = availableSubjects
            };
            foreach (var chosenSubject in chosenSubjects)
            {
                student.AddSubject(chosenSubject);
            }
            _context.Add(student);
            var addResult = await _context.SaveChangesAsync();
            if(addResult == 0)
            {
                throw new Exception("An error occured during saving data");
            }
            result = addResult > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }
        return result;
    }
    public async Task<Subject> SubjectDetails (int? id)
    {
        var subject = await _context.Subject
    .FirstOrDefaultAsync(m => m.Id == id);
        return subject;
    }
    public async Task<Subject> EditSubject(int? id)
    {
        var subject = await _context.Subject.FindAsync(id);
        return subject ?? throw new Exception("An error occured");
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
    public async Task<Subject> EditSubject(int id, Subject subject)
    {
        _context.Update(subject);
        await _context.SaveChangesAsync();
        return subject;
    }
    public async Task<Subject> CreateSubject(Subject subject)
    {
        _context.Add(subject);
        await _context.SaveChangesAsync();
        return subject;
    }


}
    #endregion // Public Methods

