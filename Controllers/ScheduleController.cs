using TiktikHttpServer.Models;
using TiktikHttpServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace TiktikHttpServer.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase
{
    public ScheduleController(){}

    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        Teacher? teacher = TeacherService.Get(id);
        if(teacher is not null)
        {
            Dictionary<string, string[]> times = new Dictionary<string, string[]>();
            string[] startarr= new string[7], endarr = new string[7];
            for (int i=0; i< 7; i++)
            {
                startarr[i] = teacher.StartTimes[i];
                endarr[i] = teacher.EndTimes[i];
            }
            times.Add("StartTime", startarr);
            times.Add("EndTime", endarr);
            return Ok(times);
        }
        return NotFound();
    }
    
    [HttpPost("{id}")]
    public ActionResult Post(Schedule times, int id)
    {
        // if(!times.check_valid_schedule())
        //     return BadRequest();
        TeacherService.UpdateWorkTimes(id, times.Starts, times.Ends);
        return NoContent();
    }
    
}