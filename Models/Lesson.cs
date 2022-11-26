namespace TiktikHttpServer.Models;

public class Lesson
{
    public int Id{get;set;}
    public int TeacherId{get;set;}
    public int StudentId{get;set;}
    public DateTime Date{get;set;}
    public String? Comment{get;set;}
}



