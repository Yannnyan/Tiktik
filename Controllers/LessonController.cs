using TiktikHttpServer.Models;
using TiktikHttpServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace TiktikHttpServer.Controllers;

[ApiController]
[Route("[controller]")]
public class LessonController : ControllerBase
{
    public LessonController(){}

    [HttpGet]
    public ActionResult<List<Lesson>> GetAll() => LessonService.GetAll();

    [HttpGet("{isTeacher}/{Id}")]
    public ActionResult<List<Lesson>> Get(bool isTeacher, int id) 
    {
        if (isTeacher)
        {
            if(TeacherService.Get(id) is null)
                return NotFound();
            return LessonService.GetByTeacher(id);
        }
        else
        {
            if (StudentService.Get(id) is null)
                return NotFound();
            return LessonService.GetByStudent(id);

        }
    }   

    // post
    [HttpPost]
    public ActionResult Post(Lesson lesson)
    {
        Console.WriteLine(lesson.ToString());
        LessonService.Add(lesson);
        return NoContent();
    }

    // put
    [HttpPut("{id}")]
    public ActionResult Put(Lesson lesson, int lessonId)
    {
        if(lesson.Id != lessonId)
            return BadRequest();
        if (LessonService.GetById(lessonId) is null)
            return NotFound();
        LessonService.Update(lesson);
        return NoContent();
    }
    // delete
    [HttpDelete("{lessonId}")]
    public ActionResult Delete(int lessonId)
    {
        if (LessonService.GetById(lessonId) is null)
            return NotFound();
        LessonService.Delete(lessonId);
        return Ok();
    }
}







