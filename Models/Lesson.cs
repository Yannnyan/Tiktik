namespace TiktikHttpServer.Models;
using Google.Cloud.Firestore;

[FirestoreData]
public class Lesson
{
    [FirestoreProperty("id")]
    public int Id{get;set;}
    [FirestoreProperty]
    public int TeacherId{get;set;}
    [FirestoreProperty]
    public int StudentId{get;set;}
    [FirestoreProperty]
    public DateTime Date{get;set;}
    [FirestoreProperty]
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



