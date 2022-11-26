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
    public ActionResult<List<Lesson>> GetAll() => new List<Lesson>
        {
            new Lesson {Id = 1, TeacherId = 1, Date= DateTime.Now , StudentId = 1, Comment = "Meet me at new zealand."},
            new Lesson {Id = 2, TeacherId = 2, Date= DateTime.Now ,StudentId = 2, Comment = "Ariel, Rehov Hatziyonut."}
        };

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
        LessonService.Add(lesson);
        return CreatedAtAction("Created new Lesson ", new {lesson.Id}, lesson);
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
    [HttpDelete]
    public ActionResult Delete(int lessonId)
    {
        if (LessonService.GetById(lessonId) is null)
            return NotFound();
        LessonService.Delete(lessonId);
        return NoContent();
    }
}







