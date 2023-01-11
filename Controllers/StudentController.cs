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
    [HttpGet("Teacher/{StudentId}")]
    public ActionResult<Teacher> GetTeacher(int StudentId)
    {
        if(StudentService.Get(StudentId) is null)
            return NotFound();
        int? teachId = StudentService.GetTeacherId(StudentId);
        if(teachId is null)
            return NotFound("No Teacher for this student");
        Teacher? teach = TeacherService.Get((int)teachId);
        if (teach is null)
            return NotFound("Teach id is registered but not found in data");
        return teach;

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
            return BadRequest("Email or name or password or Phone is null");
        else if(StudentService.Get(student.Email) is not null)
            return BadRequest("Email already exists");
        StudentService.Add(student);
        return NoContent();
    }
    [HttpPost("AddTeacher/{StudentId}/{TeacherId}")]
    public ActionResult AddTeacher(int StudentId, int TeacherId)
    {
        Student? std = StudentService.Get(StudentId);
        Teacher? teach =  TeacherService.Get(TeacherId);
        if(std is null || teach is null)
            return BadRequest("Cannot get the Student's Id, or the Teacher Id's from the data");
        StudentService.AddTeacherToStudent(StudentId, TeacherId);
        TeacherService.AddStudentToTeacher(StudentId, TeacherId);
        StudentService.AddStudentToTeacherData(StudentId, TeacherId);
        return Ok();
    }
    [HttpDelete("DeleteTeacher/{StudentId}")]
    public ActionResult DeleteTeacher(int StudentId)
    {
        Student? std = StudentService.Get(StudentId);
        if(std is null)
            return BadRequest("This student does not exist");
        int? teachId = StudentService.GetTeacherId(StudentId);
        if(teachId is null)
            return BadRequest("this student does not have a teacher");
        Teacher? teach = TeacherService.Get((int)teachId);
        if(teach is null)
            return NotFound("Teacher id is registered but not found.");
        TeacherService.DeleteStudentFromTeacher(StudentId, (int)teachId);
        StudentService.DeleteTeacherOfStudent(StudentId);
        return Ok();
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
