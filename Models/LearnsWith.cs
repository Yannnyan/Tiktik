
namespace TiktikHttpServer.Models;

using Google.Cloud.Firestore;

[FirestoreData]
public class LearnsWith
{
    [FirestoreProperty]
    public int studentid{get;set;}
    [FirestoreProperty]
    public int teacherid{get;set;}

    public LearnsWith(){}
    public LearnsWith(int studentid)
    {
        this.studentid = studentid;
    }   



}






