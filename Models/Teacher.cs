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



    public Teacher(string phone, string name, string pass, string email, int id){
        this.Email = email;
        this.Id = id;
        this.Name = name;
        this.Password = pass;
        this.Phone = phone;
    }


    public Teacher()
    {
        Id = -1;
        Password = "";
        Email = "";
        Name = "";
        Phone = "";
    }

}

