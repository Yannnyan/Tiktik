namespace TiktikHttpServer.Database;

using System.Collections;
using Google.Cloud.Firestore;
using TiktikHttpServer.Models;

public class crud_fun{


    public static Object from_dictionary_to_Object(Dictionary<string, object> dic, string collection_name){
        if(collection_name.Equals(CRUD.Students_collection))
            return from_dictionary_to_student(dic);
        if(collection_name.Equals(CRUD.Teachers_collection))
            return from_dictionary_to_theacher(dic); 
        if(collection_name.Equals(CRUD.Lessons_collection))
            return from_dictionary_to_lesson(dic);
        else
            return from_dictionary_to_Schedule(dic);
        
            
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
        lesson.Comment = (string) dic["comment"];
        lesson.Date = DateTime.Parse((string)dic["date"]);
        lesson.Id = (int)(long) dic["id"];
        lesson.StudentId = (int)(long) dic["studentid"];
        lesson.TeacherId = (int)(long) dic["teacherid"];
        return lesson;
    }

    public static Schedule from_dictionary_to_Schedule(Dictionary<string, object> dic){
        Lesson lesson = new Lesson();

        Schedule schedule = new Schedule();

        schedule.Id = (int)(long) dic["id"];

        DateTime[] start = new DateTime[7];
        List<Object> list = (List<Object>) dic["start"]; 
        int i = 0;
        foreach(string t in list){
            start[i] = DateTime.Parse((string)t);
            i++;
        }

        schedule.StartD = start;

        DateTime[] finish = new DateTime[7];
        list = (List<Object>) dic["finish"];
        i = 0;
        foreach(string t in list){
            finish[i] = DateTime.Parse((string)t);
            i++;
        }

        schedule.FinishD = finish;
        return schedule;
    }

    public static void sort(int[] arr){
        int n = arr.Length;
        for (int i = 1; i < n; ++i) {
            int key = arr[i];
            int j = i - 1;
 
            // Move elements of arr[0..i-1],
            // that are greater than key,
            // to one position ahead of
            // their current position
            while (j >= 0 && arr[j] > key) {
                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
        }
    }
    
}
