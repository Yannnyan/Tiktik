namespace TiktikHttpServer.Models;


public class Student
{
    public int Id{get; set;}
    public String? Password{get; set;}
    public String? Name{get; set;}
    public String? Phone{get; set;}
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

}






