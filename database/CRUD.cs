namespace TiktikHttpServer.Database;
using Google.Cloud.Firestore;
using TiktikHttpServer.Models;


public class CRUD{
    FirestoreDb db;
    public CRUD(){
        System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:/Users/Yan/Desktop/tiktikdb-bfa5d-70273e817eb9 (1).json");
        // System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:/Users/ברוכסון/OneDrive/שולחן העבודה/tiktikdb-bfa5d-70273e817eb9 (1).json");
        db = FirestoreDb.Create("tiktikdb-bfa5d");
        Console.WriteLine("Created Cloud Firestore client with project ID: tiktikdb-bfa5d");

    }

    public async Task add_student(Student s){

        DocumentReference docRef = db.Collection("Student").Document(s.Name);

        Dictionary<string, object> newuser = new Dictionary<string, object>
        {
            { "name", s.Name },
            { "email", s.Email },
            { "password", s.Password },
            { "phone", s.Phone },
            { "id", s.Id }
        };

        WriteResult writeResult = await docRef.SetAsync(newuser);
        Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Student collection.");

    }

    public async Task add_new_student(string phone, string name, string pass, string email, int id){

        DocumentReference docRef = db.Collection("Student").Document(name);

        Student new_student = new Student(phone, name, pass, email, id);

        Dictionary<string, object> newuser = new Dictionary<string, object>
        {
            { "name", new_student.Name },
            { "email", new_student.Email },
            { "password", new_student.Password },
            { "phone", new_student.Phone },
            { "id", new_student.Id }
        };
        WriteResult writeResult = await docRef.SetAsync(newuser);
        Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Student collection.");

    }


}

