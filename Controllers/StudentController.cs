using TiktikHttpServer.Models;
using TiktikHttpServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace TiktikHttpServer.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    public StudentController(){}

    [HttpGet]
    public ActionResult<List<Student>> GetAll() => StudentService.GetAll();
    
    [HttpGet("{id}")]
    public ActionResult<Student> Get(int id)
    {
        var student = StudentService.Get(id);

        if(student == null)
            return NotFound();

        return student;
    }

    // POST action

    // PUT action

    // DELETE action
}
