namespace TiktikHttpServer.Models;
using Google.Cloud.Firestore;


[FirestoreData]
public class Lesson
{
    [FirestoreProperty("id")]
    public int Id{get;set;}

    [FirestoreProperty("teacherid")]
    public int TeacherId{get;set;}

    [FirestoreProperty("studentid")]
    public int StudentId{get;set;}
    [FirestoreProperty("date")]
    public DateTime Date{get;set;}
    [FirestoreProperty("comment")]
    public String? Comment
    {get;set;}

    private DateTime getDateFromVal(object val)
    {
        if(val is string)
        {
            return DateTime.Parse((string)val);
        }
        else if(val is DateTime)
        {
            return (DateTime)val;
        }
        else{ // default
            return DateTime.UnixEpoch;
        }

    }
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



