
using TiktikHttpServer.Models;

namespace TiktikHttpServer.Services;

public class TeacherService
{
    static List<Teacher> Teachers {get;}
    static int nextId = 3;
    static TeacherService()
    {
        Teachers = new List<Teacher>
        {
            new Teacher {Id = 1, Name = "Moshe cohen", Email = "mosheCohen@gmail.com", Password = "123456", Phone = "0001234567"}
            , new Teacher {Id = 2, Name = "Yossi zaguri", Email = "YossiZaguri@gmail.com", Password = "321654", Phone = "1231231234"}

        };
    }

    public static List<Teacher> GetAll() => Teachers;

    public static Teacher? Get(int id) => Teachers.FirstOrDefault(p => p.Id == id);

    public static void Add(Teacher Teacher)
    {
        Teacher.Id = nextId++;
        Teachers.Add(Teacher);
    }

    public static void Delete(int id)
    {
        var Teacher = Get(id);
        if(Teacher is null)
            return;

        Teachers.Remove(Teacher);
    }

    public static void Update(Teacher Teacher)
    {
        var index = Teachers.FindIndex(std => std.Id == Teacher.Id);
        if(index == -1)
            return;

        Teachers[index] = Teacher;
    }

}





