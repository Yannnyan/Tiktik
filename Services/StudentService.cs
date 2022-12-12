using TiktikHttpServer.Models;
using TiktikHttpServer.Database;
using System.Collections;
namespace TiktikHttpServer.Services;

public class StudentService
{
    static List<Student> Students {get;}
    static Dictionary<int, int?> StudentToTeacher{get;}
    static int nextId = 3;

    static StudentService()
    {
        Students = CrudService.crud.GetAll(new Student()).Result.Cast<Student>().ToList();
        Add(new Student("0001234567", "Moshe cohen", "123456", "mosheCohen@gmail.com", 10));

        // Students = new List<Student>
        // {
        //     new Student {Id = 1, Name = "Moshe cohen", Email = "mosheCohen@gmail.com", Password = "123456", Phone = "0001234567"}
        //     , new Student {Id = 2, Name = "Yossi zaguri", Email = "YossiZaguri@gmail.com", Password = "321654", Phone = "1231231234"}
        // };
        StudentToTeacher = new Dictionary<int, int?>();
        StudentToTeacher.Add(1, 2);
        StudentToTeacher.Add(2, 1);
    }
    public static void AddTeacherToStudent(int StudentId, int TeacherId)
    {
        StudentToTeacher[StudentId] = TeacherId;
    }
    public static int? GetTeacherId(int StudentId)
    {
        return StudentToTeacher[StudentId];
    }
    public static List<Student> GetAll() => Students;

    public static Student? Get(int id) => Students.FirstOrDefault(p => p.Id == id);
    public static Student? Get(String email) => Students.FirstOrDefault(p => p.Email == email);
    public static void Add(Student student)
    {
        student.Id = nextId++;
        Students.Add(student);
        // StudentToTeacher.Add(student.Id, null);
        CrudService.crud.add(student);

    }

    public static void Delete(int id)
    {
        var student = Get(id);
        if(student is null)
            return;

        Students.Remove(student);
        // TODO: Add Database integration here

    }

    public static void Update(Student student)
    {
        var index = Students.FindIndex(std => std.Id == student.Id);
        if(index == -1)
            return;

        Students[index] = student;
    }

}


