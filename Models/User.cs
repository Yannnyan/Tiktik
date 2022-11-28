
namespace TiktikHttpServer.Models;

public class User
{
    public String Password{get; set;}
    public String Email{get; set;}
    public User()
    {
        Password = "";
        Email = "";
    }
}