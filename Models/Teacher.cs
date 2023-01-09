namespace TiktikHttpServer.Models;
using Google.Cloud.Firestore;

[FirestoreData]
public class Teacher
{
    [FirestoreProperty("id")]
    public int Id{get; set;}
    [FirestoreProperty("password")]
    public String Password
    {get; set;}
    [FirestoreProperty("email")]
    public String Email{get; set;}
    [FirestoreProperty("name")]
    public String Name{get; set;}
    [FirestoreProperty("phone")]
    public String Phone{get; set;}

    public List<string> StartTimes{get;set;}
    public List<string> EndTimes{get;set;}

    private int DEFAULT_START = 7;
    private int DEFAULT_END = 22;

    public Teacher(string phone, string name, string pass, string email, int id){
        this.Email = email;
        this.Id = id;
        this.Name = name;
        this.Password = pass;
        this.Phone = phone;
        this.setDefaultTimes();
    }
    

    public Teacher()
    {
        Id = -1;
        Password = "";
        Email = "";
        Name = "";
        Phone = "";
        this.setDefaultTimes();
    }
    private void setDefaultTimes()
    {
        this.StartTimes = new List<string>();
        this.EndTimes = new List<string>();
        for(int i=0; i< 7; i++)
        {
            StartTimes.Add(new TimeOnly(DEFAULT_START,0).ToString());
            EndTimes.Add(new TimeOnly(DEFAULT_END, 0).ToString());
        }
    }
}

