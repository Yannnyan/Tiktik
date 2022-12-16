namespace TiktikHttpServer.Models;
using Google.Cloud.Firestore;

[FirestoreData]
public class Student
{
    [FirestoreProperty("id")]
    public int Id{get; set;}
    [FirestoreProperty("password")]
    public String? Password{get; set;}
    [FirestoreProperty("name")]
    public String? Name{get; set;}
    [FirestoreProperty("phone")]
    public String? Phone{get; set;}
    [FirestoreProperty("email")]
    public String? Email{get; set;}
    
    
    
    public Student(string phone, string name, string pass, string email, int id){
        this.Email = email;
        this.Id = id;
        this.Name = name;
        this.Password = pass;
        this.Phone = phone;
    }

    public Student(){
        this.Email = "";
        this.Id = -1;
        this.Name = "";
        this.Password = "";
        this.Phone = "";
    }
    
    public override string ToString()
    {
        return this.Email + ", " + this.Name + ", " + this.Id + ", " + this.Password + ", " + this.Phone;
    }
}






