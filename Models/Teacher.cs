namespace TiktikHttpServer.Models;

public class Teacher
{
    public int Id{get; set;}
    public String Password
    {get; set;}
    public String Email{get; set;}
    public String Name{get; set;}
    public String Phone{get; set;}

    public Teacher()
    {
        Id = -1;
        Password = "";
        Email = "";
        Name = "";
        Phone = "";
    }

}

