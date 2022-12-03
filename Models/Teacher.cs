namespace TiktikHttpServer.Models;

public class Teacher
{
    public int Id{get; set;}
    public String Password
    {get; set;}
    public String Email{get; set;}
    public String Name{get; set;}
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

