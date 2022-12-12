using Google.Cloud.Firestore;
using System.Collections;
using TiktikHttpServer.Models;
using TiktikHttpServer.Database;


public class testStuff
{
    public async static Task test()
    {
        
        
        CRUD DB = new CRUD();
        Student s = new Student("05096541278", "carlos", "123456", "carlos.hagever@gmail.com", -1);

        Teacher t = new Teacher("05448975612", "theacher", "badpass123", "teacher@gmail.com", -1);

        DateTime date = new DateTime();
        int counter = 1;
        Lesson l = new Lesson(-1, counter, 1, date, "this lesson comment");

        // for(int i = 1; i<10; i++){
        //     s.Id = -1;
        //     l.Id = -1;
        //     t.Id = -1;
        //     await DB.add(s);
        //     await DB.add(t);
        //     await DB.add(l);

        //     if(i % 3 == 0){
        //         counter++;
        //     }
        // }
    
        Console.WriteLine("free space in: Student: {0}, Lessons: {1}, Teacher: {2}", await DB.free_id(DB.Students_collection), await DB.free_id(DB.Lessons_collection), await DB.free_id(DB.Teachers_collection));

        Lesson lesson = await DB.get_lesson_byid(4);
        Console.WriteLine("lesson with id = 4: ");
        Console.WriteLine(lesson.Comment);
        Console.WriteLine(lesson.Date);
        Console.WriteLine(lesson.StudentId);
        Console.WriteLine(lesson.TeacherId);

        Student student = await DB.get_student_byid(2);
        Console.WriteLine("Student with id = 2 : ");
        Console.WriteLine(student.Id);
        Console.WriteLine(student.Email);
        Console.WriteLine(student.Name);
        Console.WriteLine(student.Phone);

        ArrayList a = await DB.get_my_lessons_as_theacher(1);
        Console.WriteLine("the teacher num of lessons is: {0}", a.Count);

        ArrayList arry = await DB.GetAll(new Student());
        Console.WriteLine("the number of Students in student collection is {0}" ,arry.Count);

       int result = await DB.free_id(DB.Teachers_collection);
       Console.WriteLine("free id in Student collection {0}", result);

    // await DB.DeleteCollection(DB.Lessons_collection,50);
    // await DB.DeleteCollection(DB.Students_collection, 50);
    // await DB.DeleteCollection(DB.Teachers_collection, 50);



    
    }

}



