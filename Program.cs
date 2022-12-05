
using Google.Cloud.Firestore;
using System.Collections;
using TiktikHttpServer.Models;
using TiktikHttpServer.Database;


/*
using System.Net.Http.Headers;

using HttpClient client = new();
// client.DefaultRequestHeaders.Accept.Clear();
// client.DefaultRequestHeaders.Accept.Add(
//     new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
// client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

await ProcessRepositoriesAsync(client);

static async Task ProcessRepositoriesAsync(HttpClient client)
{
    // var json = await client.GetStringAsync(
    //      "https://api.github.com/orgs/dotnet/repos");
    // var json = await client.GetStringAsync(
    //      "https://localhost:7191/Student");
    
    var json = await client.GetStringAsync(
         "https://firestore.googleapis.com/v1/projects/example-83225/databases/(default)/documents/Student");
     Console.Write(json);
}

*/

CRUD DB = new CRUD();

for(int i = 1; i<10; i++){
    await DB.add_new_student("050984356"+i.ToString(), "name" + i.ToString(), "1234", "sssss", -1);
}

for(int i = 1; i<10; i++){
    await DB.add_new_theacher("054984356"+i.ToString(), "name" + i.ToString(), "1234", "sssss", -1);
}
Timestamp date = new Timestamp();

int counter = 1;
for(int i = 1; i<10; i++){
    await DB.add_new_lesson(i,counter,i,date, "the lesson will start in petah tikva, please come with a mask");
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
