using TiktikHttpServer.Models;
using TiktikHttpServer.Database;
using System.Collections;
namespace TiktikHttpServer.Services;

public class StudentService
{
    static List<Student> Students {get;}
    static Dictionary<int, int?> StudentToTeacher{get;}
    static int nextId;
    static int nextConnectId;

    static StudentService()
    {
        Students = CrudService.crud.GetAll(new Student()).Result.Cast<Student>().ToList();
        nextId = Students.Max(std => std.Id) + 1;
        List<LearnsWith> learnsWiths = CrudService.crud.GetAll(new LearnsWith()).Result.Cast<LearnsWith>().ToList();
        nextConnectId = learnsWiths.Max(l => l.id) + 1;
        StudentToTeacher = new Dictionary<int, int?>();
        update_StudentToTeacher();
    }
    private static void update_StudentToTeacher()
    {
        List<LearnsWith> lst = CrudService.crud.GetAll(new LearnsWith()).Result.Cast<LearnsWith>().ToList();
        foreach(LearnsWith map in lst)
        {
            StudentToTeacher.Add(map.studentid, map.teacherid);
        }
        
    }
    public static void AddTeacherToStudent(int StudentId, int TeacherId)
    {
        StudentToTeacher[StudentId] = TeacherId;

    }
    // TODO: remember to change this to another service
    public static void AddStudentToTeacherData(int studentId, int teacherId)
    {
        LearnsWith l = new LearnsWith();
        l.studentid = studentId;
        l.teacherid = teacherId;
        l.id = nextConnectId ++;
        CrudService.crud.add(l);
    }
    public static int? GetTeacherId(int StudentId)
    {
        try{
            return StudentToTeacher[StudentId];
        }
        catch(KeyNotFoundException)
        {
            return null;
        }
        
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
        CrudService.crud.Delete(student);

    }

    public static void Update(Student student)
    {
        var index = Students.FindIndex(std => std.Id == student.Id);
        if(index == -1)
            return;

        Students[index] = student;
    }

}


