using TiktikHttpServer.Models;
using Microsoft.AspNetCore.Mvc;
using TiktikHttpServer.Services;

namespace TiktikHttpServer.Controllers;


[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    public MessageController()
    {
    }

    [HttpGet]
    public ActionResult<List<Object>> GetAll() => LogService.loggedUsers.Cast<Object>().ToList();


    // log user in
    [HttpPost("Login")]
    public ActionResult<Response> Login(User user)
    {
        Console.WriteLine("User has Logged in: " + user.ToString());
        Student? std;
        Teacher? teach;
        try{
            bool is_teacher = UserService.isTeacher(user);
            if(is_teacher)
            {
                teach = TeacherService.Get(user.Email);
                if (teach is null)
                    return NotFound("Cannot find email");
                else if(!teach.Password.Equals(user.Password))
                    return NotFound("Password incorrect");
                LogService.LogIn(teach);
                return new Response_Teacher(teach);
            }
            else{
                std = StudentService.Get(user.Email);
                if(std is null || std.Password is null)
                    return NotFound();
                if (!std.Password.Equals(user.Password))
                    return NotFound();
                LogService.LogIn(std);
                return new Response_Student(std);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.ToString());
            return NotFound();
        }
    }
    [HttpPost("Disconnect")]
    public ActionResult<Response> Disconnect(User user)
    {
        bool isteacher = UserService.isTeacher(user);
        if(isteacher)
            LogService.Disconnect(TeacherService.Get(user.Email));
        else
            LogService.Disconnect(StudentService.Get(user.Email));
        return NoContent();
    }
}








