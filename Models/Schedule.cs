namespace TiktikHttpServer.Models;

using Google.Cloud.Firestore;

[FirestoreData]
public class Schedule{

    [FirestoreProperty("start")]
    public DateTime[] StartD {get; set;}

    [FirestoreProperty("finish")]
    public DateTime[] FinishD{get; set;}

    [FirestoreProperty("id")]
    public int Id{get; set;}

    public Schedule(DateTime[] s, DateTime[] f, int id){
        this.StartD = s;
        this.FinishD = f;
        this.Id = id;
    }

    public Schedule(){
        this.StartD = new DateTime[7];
        this.FinishD = new DateTime[7];
        this.Id = -1;
    }
    
    
}