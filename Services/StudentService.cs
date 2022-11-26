using TiktikHttpServer.Models;

namespace TiktikHttpServer.Services;

public class StudentService
{
    static List<Student> Students {get;}
    static int nextId = 3;
    static StudentService()
    {
        Students = new List<Student>
        {
            new Student {Id = 1, Name = "Moshe cohen", Email = "mosheCohen@gmail.com", Password = "123456", Phone = "0001234567"}
            , new Student {Id = 2, Name = "Yossi zaguri", Email = "YossiZaguri@gmail.com", Password = "321654", Phone = "1231231234"}

        };
    }

    public static List<Student> GetAll() => Students;

    public static Student? Get(int id) => Students.FirstOrDefault(p => p.Id == id);

    public static void Add(Student student)
    {
        student.Id = nextId++;
        Students.Add(student);
    }

    public static void Delete(int id)
    {
        var student = Get(id);
        if(student is null)
            return;

        Students.Remove(student);
    }

    public static void Update(Student student)
    {
        var index = Students.FindIndex(std => std.Id == student.Id);
        if(index == -1)
            return;

        Students[index] = student;
    }

}


