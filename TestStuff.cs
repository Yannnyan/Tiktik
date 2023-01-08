using Google.Cloud.Firestore;
using System.Collections;
using TiktikHttpServer.Models;
using TiktikHttpServer.Database;


public class testStuff
{
    public async static Task test()
    {
        
        
        CRUD DB = new CRUD();
        Student st = new Student("0000000001", "yosi zagiru","111111", "yosizaguri@gmai.com", 1);
        Student st2 = new Student("1231231231", "haim levi","161616", "haimLevi@gmail.com", 2);
        Student st3 = new Student("121212121", "bob marly","allgoooddd", "music@gmail.com", -1);
        Teacher t1 = new Teacher("1919191919", "idan philosoph", "123123", "idanPhilo@gmail.com", 1);
        Teacher t2 = new Teacher("2020202020", "yhonatan baruchson", "098098", "yonatanbaruchson@gmail.com",2);
        Lesson l1 = new Lesson(1, t1.Id, st2.Id, new DateTime(2022, 12, 16), "meet me at Ariel university");
        Lesson l2 = new Lesson(2, t1.Id, st.Id, new DateTime(2022, 12, 16,0,0,0),"i love elephants");
        Dictionary<string, DateTime> D= new Dictionary<string, DateTime>();
        
    

        // Student[] studets = {st, st2};
        // Teacher[] teachers = {t1, t2};
        //Lesson[] lessons = {l1, l2};
        // foreach(Student student in studets)
        // {
        //     DB.add(student);
        // }
        // foreach(Teacher teacher in teachers)
        // {
        //     DB.add(teacher);
        // }
        // foreach(Lesson lesson in lessons)
        // {
        //     await DB.add(lesson);
        // }

        //await DB.add(st3);
        DateTime t = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,12,11,0,0);
        DateTime[] start ={t, t, t, t, t, t, t};
        DateTime[] finish ={t, t, t, t, t, t, t};
        Schedule s = new Schedule(start, finish, -1);

        await DB.add_Schedule(s);

        ArrayList arr = await DB.GetAll(s);
        Schedule sc = (Schedule) arr[1];
        Console.WriteLine(sc.StartD[0]);

    
    }

}



