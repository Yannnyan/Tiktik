
using TiktikHttpServer.Models;

namespace TiktikHttpServer.Services;

public class UserService
{
    static UserService(){}

    public static bool isTeacher(User user)
    {
        Student? student = StudentService.GetAll().FirstOrDefault(st => st.Email == user.Email);
        Teacher? teacher = TeacherService.GetAll().FirstOrDefault(teach => teach.Email == user.Email);
        bool isteach = teacher is not null;
        if (student is null && teacher is null)
            throw(new Exception("invalid argument"));
        return isteach;
    }
    
}