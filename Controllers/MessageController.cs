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

    // [HttpPost]
    // public ActionResult PostRegisterStudent(Student student)
    // {
    //     StudentService.Add(student);
    //     return CreatedAtAction("Created obj Student, ", new {student.Id}, student);
    // }
    [HttpGet]
    public ActionResult<List<Object>> GetAll() => LogService.loggedUsers.Cast<Object>().ToList();

    [HttpPost]
    public ActionResult<Response> Post(User user)
    {
        Console.WriteLine("Email: " + user.Email);
        Console.WriteLine("Password: " + user.Password);
        Student? std;
        Teacher? teach;
        try{
            bool is_teacher = UserService.isTeacher(user);
            if(is_teacher)
            {
                teach = TeacherService.Get(user.Email);
                if (teach is null)
                    return NotFound();
                LogService.LogIn(teach);
                return new Response_Teacher(teach);
            }
            else{
                std = StudentService.Get(user.Email);
                if (std is null)
                    return NotFound();
                LogService.LogIn(std);
                return new Response_Student(std);
            }
        }
        catch(Exception)
        {
            return NotFound();
        }
        return NoContent();

    }
    // [HttpPost]
    // public ActionResult PostLogin(Teacher teacher)
    // {
    //     LogService.LogIn(teacher);
    //     return CreatedAtAction("Created new object, ", new{teacher.Id}, teacher);
    // }
}








