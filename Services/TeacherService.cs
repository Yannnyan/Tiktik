
using TiktikHttpServer.Models;

namespace TiktikHttpServer.Services;
using TiktikHttpServer.Database;
public class TeacherService
{
    static List<Teacher> Teachers {get;}
    static Dictionary<int, List<int>> TeacherToStudents;
    static int nextId;
    static TeacherService()
    {
        Teachers = CrudService.crud.GetAll(new Teacher()).Result.Cast<Teacher>().ToList();
        nextId = Teachers.Max(std => std.Id) + 1;

        TeacherToStudents = new Dictionary<int, List<int>>();
        updateTeacherToStudents();
    }
    private static void updateTeacherToStudents()
    {
       
        foreach(Teacher teacher in Teachers)
        {
            TeacherToStudents.Add(teacher.Id, new List<int>());
            List<LearnsWith> lst = CrudService.crud.getStudentsByTeacher(teacher.Id).Result.Cast<LearnsWith>().ToList();
            lst.ForEach(v => 
            {
                if(v.teacherid == teacher.Id) 
                    TeacherToStudents[teacher.Id].Add(v.studentid);
            });
        }
    }
    public static List<int> GetAllStudents(int TeacherId)
    {
        return TeacherToStudents[TeacherId];
    }
    public static void AddStudentToTeacher(int StudentId, int TeacherId)
    {
        TeacherToStudents[TeacherId].Add(StudentId);
    }
    public static void DeleteStudentFromTeacher(int StudentId, int TeacherId)
    {
        TeacherToStudents[TeacherId].Remove(StudentId);
    }

    public static List<Teacher> GetAll() => Teachers;

    public static Teacher? Get(int id) => Teachers.FirstOrDefault(p => p.Id == id);
    public static Teacher? Get(String email) => Teachers.FirstOrDefault(p => p.Email == email);
    public static void Add(Teacher Teacher)
    {
        Teacher.Id = nextId++;
        Teachers.Add(Teacher);
        CrudService.crud.add(Teacher);
    }

    public static void UpdateWorkTimes(int id, List<string?> start, List<string?> end)
    {
        Teacher? teacher = Get(id);
        start.ForEach(t => Console.WriteLine(t is null ? "null" : t));
        end.ForEach(t => Console.WriteLine(t is null ? "null" : t));
        List<string> startValues = teacher.StartTimes;
        List<string> endValues = teacher.EndTimes;
        for(int i=0; i< start.Count; i++){
            if(start[i] is not null)
                startValues[i] = start[i];
            if(end[i] is not null)
                endValues[i] = end[i];
        }
        teacher.StartTimes = startValues;
        teacher.EndTimes = endValues;
        // TODO: Add database integration

    }
    public static List<string> GetStartTime(int id) => Get(id).StartTimes;
    public static List<string> GetEndTime(int id) => Get(id).EndTimes;

    public static void Delete(int id)
    {
        var Teacher = Get(id);
        if(Teacher is null)
            return;

        Teachers.Remove(Teacher);
        CrudService.crud.Delete(Teacher);
    }

    public static void Update(Teacher Teacher)
    {
        var index = Teachers.FindIndex(std => std.Id == Teacher.Id);
        if(index == -1)
            return;

        Teachers[index] = Teacher;
    }

}





