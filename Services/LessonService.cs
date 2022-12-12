using TiktikHttpServer.Models;
namespace TiktikHttpServer.Services;



public class LessonService
{
    public static List<Lesson> Lessons {get;}
    static int nextId;
    static LessonService()
    {
        Lessons = CrudService.crud.GetAll(new Lesson()).Result.Cast<Lesson>().ToList();
        nextId = Lessons.Max(std => std.Id) + 1;
        // Lessons = new List<Lesson>
        // {
        //     new Lesson {Id = 1, TeacherId = 1, StudentId = 1, Comment = "Meet me at new zealand."},
        //     new Lesson {Id = 2, TeacherId = 2, StudentId = 2, Comment = "Ariel, Rehov Hatziyonut."}
        // };
    }
    public static List<Lesson> GetAll()
    {
        // Console.WriteLine(Lessons[0].Id);
        return Lessons;
    }
    public static List<Lesson> GetByStudent(int studentId)
    {
        List<Lesson> lessons = new List<Lesson>();
        foreach(Lesson lesson in Lessons)
        {
            if(lesson.StudentId == studentId)
            {
                lessons.Add(lesson);
            }
        }
        return lessons;
    }
    public static List<Lesson> GetByTeacher(int teacherId)
    {
        List<Lesson> lessons = new List<Lesson>();
        foreach(Lesson lesson in Lessons)
        {
            if (lesson.TeacherId == teacherId)
            {
                lessons.Add(lesson);
            }
        }
        return lessons;
    }
    public static Lesson? GetById(int lessonId) => Lessons.FirstOrDefault(l => l.Id == lessonId);
    public static void Add(Lesson lesson)
    {
        lesson.Id = nextId++;
        Lessons.Add(lesson);
        CrudService.crud.add(lesson);
    }
    public static void Delete(int lessonId)
    {
        var lesson = GetById(lessonId);
        if (lesson is null)
            return;
        Lessons.Remove(lesson);
    }

    public static void Update(Lesson lesson)
    {
        var index = Lessons.FindIndex(l => l.Id == lesson.Id);
        if (index == -1)
            return;
        Lessons[index] = lesson;
    }

}










