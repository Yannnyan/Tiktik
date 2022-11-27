using TiktikHttpServer.Services;

namespace TiktikHttpServer.Models;

public class Response
{
    public bool approved{get;set;}
    public bool isTeacher{get;set;}
    public Response(){}

}
public class Response_Teacher : Response
{
    public List<Student>? students{get;set;}
    public List<Lesson>? lessons{get;set;}
    public List<Lesson>? lesson_not_approved{get;set;}
    public Response_Teacher(Teacher teacher)
    {
        this.approved = true;
        this.isTeacher = true;
        this.lessons = null;
        this.students = null;
        this.lesson_not_approved = null;
    }
    
}
public class Response_Student : Response
{
    public Teacher? teacher{get;set;}
    public List<Lesson>? lessons{get;set;}
    public Response_Student(Student student)
    {
        this.approved = true;
        this.isTeacher = false;
        this.teacher = null;
        this.lessons = null;
    }
   
}




