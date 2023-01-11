using Google.Cloud.Firestore;
using System.Collections;
using TiktikHttpServer.Models;
using TiktikHttpServer.Database;


public class testStuff
{
    public async static Task test()
    {
        
        
        // CRUD DB = new CRUD();
        // Student st = new Student("0000000001", "yosi zagiru","111111", "yosizaguri@gmai.com", 1);
        // Student st2 = new Student("1231231231", "haim levi","161616", "haimLevi@gmail.com", 2);
        // Teacher t1 = new Teacher("1919191919", "idan philosoph", "123123", "idanPhilo@gmail.com", 1);
        // Teacher t2 = new Teacher("2020202020", "yhonatan baruchson", "098098", "yonatanbaruchson@gmail.com",2);
        // Lesson l1 = new Lesson(1, t1.Id, st2.Id, new DateTime(2022, 12, 16), "meet me at Ariel university");
        // Lesson l2 = new Lesson(2, t1.Id, st.Id, new DateTime(2022, 12, 16,0,0,0),"i love elephants");
        // // Student[] studets = {st, st2};
        // // Teacher[] teachers = {t1, t2};
        // Lesson[] lessons = {l1, l2};
        // // foreach(Student student in studets)
        // // {
        // //     DB.add(student);
        // // }
        // // foreach(Teacher teacher in teachers)
        // // {
        // //     DB.add(teacher);
        // // }
        // foreach(Lesson lesson in lessons)
        // {
        //     await DB.add(lesson);
        // }

        // Teacher t1 = new Teacher("1919191919", "idan philosoph", "123123", "idanPhilo@gmail.com", -1);

        // CRUD DB = new CRUD();
        // // await DB.add_teacher(t1);
        // List<string?> new_start_times = new List<string?>();
        // List<string?> new_end_times = new List<string?>();
        // new_start_times.Add(new TimeOnly(1, 0).ToString());
        // new_start_times.Add(new TimeOnly(2, 0).ToString());
        // new_start_times.Add(new TimeOnly(3, 0).ToString());
        // new_start_times.Add(new TimeOnly(4, 0).ToString());
        // new_start_times.Add(new TimeOnly(5, 0).ToString());
        // new_start_times.Add(new TimeOnly(6, 0).ToString());
        // new_start_times.Add(new TimeOnly(7, 0).ToString());

        // new_end_times.Add(new TimeOnly(1, 0).ToString());
        // new_end_times.Add(new TimeOnly(2, 0).ToString());
        // new_end_times.Add(new TimeOnly(3, 0).ToString());
        // new_end_times.Add(new TimeOnly(4, 0).ToString());
        // new_end_times.Add(new TimeOnly(5, 0).ToString());
        // new_end_times.Add(new TimeOnly(6, 0).ToString());
        // new_end_times.Add(new TimeOnly(7, 0).ToString());
        // Schedule schedule = new Schedule();
        // schedule.Starts = new_start_times;
        // schedule.Ends = new_end_times;
        // await DB.change_schedule_byid(4, schedule);
        // string? h = DB.get_teacher_byid(4).Result.ToString();
        // Console.WriteLine(h);  

        CRUD crud = new CRUD();
        await crud.add_teacher(new Teacher("0101010101", "Yoskale", "1234", "lklk@gmail.com", 1));

    }

}



