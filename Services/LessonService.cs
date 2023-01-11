using TiktikHttpServer.Models;
namespace TiktikHttpServer.Services;



public class LessonService
{
    public static List<Lesson> Lessons {get;}
    static int nextId;
    static LessonService()
    {
        Lessons = CrudService.crud.GetAll(new Lesson()).Result.Cast<Lesson>().ToList();
        if(Lessons.Count <= 0)
        {
            nextId = 1;
        }
        else
        {
            nextId = Lessons.Max(std => std.Id) + 1;
        }
        
    }
    public static List<Lesson> GetAll() => Lessons;
    
    public static List<Lesson> GetByStudent(int studentId)
    {
        if(Lessons.Count == 0)
            return new List<Lesson>();
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
        if(Lessons.Count == 0)
            return new List<Lesson>();
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
    public static int GetAmountLessonsByStudent(int StudentId)
    {
        if(StudentService.Get(StudentId) != null)
            return CrudService.crud.get_my_lessons_as_student(StudentId).Result.Count;
        return 0;
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
        CrudService.crud.Delete(lesson);
    }
    public static void DeleteLessonsOfStudentId(int StudentId)
    {
        // delete all lessons that the student is assigned to
        Lessons.ForEach((l) => 
        {
            if(l.StudentId == StudentId)
                CrudService.crud.Delete(l);
            });
        Lessons.RemoveAll((l) => l.StudentId == StudentId);
    }
    public static void Update(Lesson lesson)
    {
        var index = Lessons.FindIndex(l => l.Id == lesson.Id);
        if (index == -1)
            return;
        Lessons[index] = lesson;
    }

}










