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
    [HttpPost]
    public ActionResult Post(Student student)
    {
        StudentService.Add(student);
        return CreatedAtAction("Created new student, ", new {student.Id}, student);
    }
    [HttpPost("Register")]
    public ActionResult Register(Student student)
    {
        if(student.Email is null || student.Name is null || student.Password is null||
                                            student.Phone is null)
            return BadRequest();
        StudentService.Add(student);
        return NoContent();
    }
    // PUT action
    [HttpPut("{id}")]
    public ActionResult Put(Student student, int id)
    {
        if(student.Id != id)
        {
            return BadRequest();
        }
        if(StudentService.Get(id) is null)
        {
            return NotFound();
        }
        StudentService.Update(student);
        return NoContent();

    }
    // DELETE action
    [HttpDelete]
    public ActionResult Delete(int id)
    {
        if(StudentService.Get(id) is null)
        {
            return NotFound();
        }
        StudentService.Delete(id);
        return NoContent();
    }
}
