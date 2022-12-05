using Google.Cloud.Firestore;
using System.Collections;
using TiktikHttpServer.Models;
using TiktikHttpServer.Database;


public class testStuff
{
    public async static Task test()
    {
        
        
        CRUD DB = new CRUD();

        for(int i = 1; i<10; i++){
            await DB.add_student(""+i.ToString(), "name" + i.ToString(), "1234", "gmail@gmail.com", -1);
        }

        // for(int i = 1; i<10; i++){
        //     await DB.add_theacher("054984356"+i.ToString(), "name" + i.ToString(), "1234", "gmail@gmail.com", -1);
        // }
        Timestamp date = new Timestamp();

        int counter = 1;
        for(int i = 1; i<10; i++){
            await DB.add_lesson(i,counter,i,date, "the lesson will start in petah tikva, please come with a mask");
            if(i % 3 == 0){
                counter++;
            }
        }
        Console.WriteLine("free space in: Student: {0}, Lessons: {1}, Teacher: {2}", await DB.free_id("Student"), await DB.free_id("Lessons"), await DB.free_id("Teacher"));




        for(int i = 1; i<=11; i++){
            await DB.change_document_value("Student", "email", "www@gmail.com", i);
        }

        Console.WriteLine("should not let me change the id of 3 to 2");
        await DB.change_document_value("Student", "id", 2, 3);

        Lesson l = await DB.get_lesson_byid(4);
        Console.WriteLine("lesson: ");
        Console.WriteLine(l.Comment);
        Console.WriteLine(l.Date);
        Console.WriteLine(l.StudentId);
        Console.WriteLine(l.TeacherId);

        ArrayList a = await DB.get_my_lessons_as_theacher(1);
        Console.WriteLine("the teacher num of lessons is: {0}", a.Count);

        ArrayList b = await DB.GetAll(new Student());
        Console.WriteLine("the number of Students is {0}" ,b.Count);
    }

}



