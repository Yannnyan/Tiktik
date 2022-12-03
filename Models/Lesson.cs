namespace TiktikHttpServer.Models;
using Google.Cloud.Firestore;

public class Lesson
{
    public int Id{get;set;}
    public int TeacherId{get;set;}
    public int StudentId{get;set;}
    public Timestamp Date{get;set;}
    public String? Comment{get;set;}

    public Lesson(int id, int TheacherId, int StudentId, Timestamp t, string Comment){
        this.Id = id;
        this.TeacherId = TheacherId;
        this.StudentId = StudentId;
        this.Date = t;
        this.Comment = Comment;
    }

    public Lesson(){
        this.Id = -1;
        this.TeacherId = -1;
        this.StudentId = -1;
        this.Date = new Timestamp();
        this.Comment = "";
    }
}



