
using TiktikHttpServer.Models;

namespace TiktikHttpServer.Services;




public class UserService
{
    static UserService(){}

    public static bool isTeacher(User user)
    {
        Student? student = StudentService.GetAll().FirstOrDefault(st => st.Email == user.Email);
        Teacher? teacher = TeacherService.GetAll().FirstOrDefault(teach => teach.Email == user.Email);
        if (student is not null)
        {
            return false;
        }
        if (teacher is not null)
        {
            return true;
        }
        throw(new Exception("invalid argument"));
    }
}