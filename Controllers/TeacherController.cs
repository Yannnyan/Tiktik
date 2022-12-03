using TiktikHttpServer.Models;
using TiktikHttpServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace TiktikHttpServer.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController : ControllerBase
{
    public TeacherController(){}

    [HttpGet]
    public ActionResult<List<Teacher>> GetAll() => TeacherService.GetAll();
    
    [HttpGet("{id}")]
    public ActionResult<Teacher> Get(int id)
    {
        var Teacher = TeacherService.Get(id);

        if(Teacher == null)
            return NotFound();

        return Teacher;
    }

    // POST action
    [HttpPost]
    public ActionResult Post(Teacher Teacher)
    {
        TeacherService.Add(Teacher);
        return CreatedAtAction("Created new Teacher, ", new {Teacher.Id}, Teacher);
    }
    [HttpPost("Register")]
    public ActionResult Register(Teacher teach)
    {
        if(teach.Email is null || teach.Name is null || teach.Password is null ||
        teach.Phone is null)
            return BadRequest();
        TeacherService.Add(teach);
        return NoContent();
    }
    // PUT action
    [HttpPut("{id}")]
    public ActionResult Put(Teacher Teacher, int id)
    {
        if(Teacher.Id != id)
        {
            return BadRequest();
        }
        if(TeacherService.Get(id) is null)
        {
            return NotFound();
        }
        TeacherService.Update(Teacher);
        return NoContent();

    }
    // DELETE action
    [HttpDelete]
    public ActionResult Delete(int id)
    {
        if(TeacherService.Get(id) is null)
        {
            return NotFound();
        }
        TeacherService.Delete(id);
        return NoContent();
    }
}
