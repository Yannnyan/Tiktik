
namespace TiktikHttpServer.Models;
public class Schedule
{
    public int Id{get;set;}
    public List<string?> Starts{get;set;}
    public List<string?> Ends{get;set;}
    public Schedule(){}
    public bool check_valid_schedule()
    {
        TimeOnly time;
        // returns true if each object is not null and is parseable to timeonly object
        bool valid_Starts = Starts.FindIndex(0,Starts.Count, (t) => t == null || !TimeOnly.TryParse(t, out time)) != -1;
        bool valid_Ends = Ends.FindIndex(0,Starts.Count, (t) => t == null || !TimeOnly.TryParse(t, out time)) != -1;
        return valid_Starts && valid_Ends;
    }


}







