using TiktikHttpServer.Models;
using System.Collections;

namespace TiktikHttpServer.Services;


public class LogService
{
    public static ArrayList loggedUsers{get;}
    static LogService()
    {
        loggedUsers = new ArrayList();
    }
    public static void LogIn(Object? obj)
    {
        if (obj is null)
            return;
        if(obj.GetType() != typeof(Student) && obj.GetType() != typeof(Teacher))
        {
            return;
        }
        loggedUsers.Add(obj);
    }
    public static void Disconnect(Object? obj)
    {
        if(obj is null || loggedUsers.IndexOf(obj) == -1)
            return;
        loggedUsers.Remove(obj);
    }

}








