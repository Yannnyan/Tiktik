namespace TiktikHttpServer.Database;

using System.Collections;
using Google.Cloud.Firestore;
using TiktikHttpServer.Models;

public class crud_fun{


    public static Object from_dictionary_to_Object(Dictionary<string, object> dic, string collection_name){
        if(collection_name.Equals("Student"))
            return from_dictionary_to_student(dic);
        if(collection_name.Equals("Teacher"))
            return from_dictionary_to_theacher(dic); 
        return from_dictionary_to_lesson(dic);
    }


    public static Student from_dictionary_to_student(Dictionary<string, object> dic){
        Student s = new Student();

        s.Email =(string) dic["email"];
        s.Id = (int)(long) dic["id"];
        s.Name = (string) dic["name"];
        s.Password = (string) dic["password"];
        s.Phone = (string) dic["phone"];

        return s;
    }

    public static Teacher from_dictionary_to_theacher(Dictionary<string, object> dic){
        Teacher t = new Teacher();

        t.Email =(string) dic["email"];
        t.Id = (int)(long) dic["id"];
        t.Name = (string) dic["name"];
        t.Password = (string) dic["password"];
        t.Phone = (string) dic["phone"];

        return t;
    }

    public static Lesson from_dictionary_to_lesson(Dictionary<string, object> dic){
        Lesson lesson = new Lesson();

        lesson.Comment = (string) dic["Comment"];
        lesson.Date = (Timestamp) dic["Date"];
        lesson.Id = (int)(long) dic["id"];
        lesson.StudentId = (int)(long) dic["StudentId"];
        lesson.TeacherId = (int)(long) dic["TeacherId"];
        return lesson;

    }
}
