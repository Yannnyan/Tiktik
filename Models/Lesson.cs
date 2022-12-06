namespace TiktikHttpServer.Models;
using Google.Cloud.Firestore;

public class Lesson
{
    public int Id{get;set;}
    public int TeacherId{get;set;}
    public int StudentId{get;set;}
    public DateTime Date{get;set;}
    public String? Comment{get;set;}

    public Lesson(int id, int TheacherId, int StudentId, DateTime Date, string Comment){
        this.Id = id;
        this.TeacherId = TheacherId;
        this.StudentId = StudentId;
        this.Date = Date;
        this.Comment = Comment;
    }

    public Lesson(){}
    
    public override string ToString()
    {
        return " " + TeacherId + " " + StudentId + " " + Date.ToString() + " " + Comment;
    }
}



