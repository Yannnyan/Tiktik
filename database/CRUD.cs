namespace TiktikHttpServer.Database;

using System.Collections;
using Google.Cloud.Firestore;
using TiktikHttpServer.Models;


public class CRUD : crud_inter{
    FirestoreDb db;
    public CRUD(){
        //System.Environment.SetEnvironmentVariable("C:/Users/ברוכסון/OneDrive/מסמכים/יהונתן/אוניברסיטה עבודות/הנדסת תוכנה/Tiktik/database\tiktikdb-bfa5d-70273e817eb9 (1).json");
         System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:/Users/ברוכסון/OneDrive/שולחן העבודה/tiktikdb-bfa5d-70273e817eb9 (1).json");
        db = FirestoreDb.Create("tiktikdb-bfa5d");
        Console.WriteLine("Created Cloud Firestore client with project ID: tiktikdb-bfa5d");

    }

    //checking if "collection_name" id already exists. returns true if exists else, return false
    public async Task<bool> id_exist(int id, string collection_name){

        if(id <= 0){
            Console.WriteLine("incorrect id ({0}) input (non-positive)", id);
            return false;
        }else if(collection_name.Equals("Student")  || collection_name.Equals("Teacher") || collection_name.Equals("Lessons")){
                CollectionReference collectionRef = db.Collection(collection_name);

                Query query = collectionRef.WhereEqualTo("id", id);
                //Google.Cloud.Firestore.QuerySnapshot -> contains zero or more documentsSnapshot representing the result of the query
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
                if(querySnapshot.Count > 0)
                    return true;
                return false;
        }else
            return false;
            
        
    }

    //checking if Teacher exists returns true if exists else, return false
    public async Task<bool> id_exist_t(int id){
        if(id <= 0){
            Console.WriteLine("incorrect id input (negative)");
            return false;
        }
        CollectionReference studentsRef = db.Collection("Teacher");
        Query query = studentsRef.WhereEqualTo("id", id);
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
        if(querySnapshot.Count > 0)
            return true;
        return false;
    }

    //checking if Student exists returns true if exists else, return false
     public async Task<bool> id_exist_s(int id){
        if(id <= 0){
            Console.WriteLine("incorrect id input (negative)");
            return false;
        }
        CollectionReference studentsRef = db.Collection("Student");
        Query query = studentsRef.WhereEqualTo("id", id);
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
        if(querySnapshot.Count > 0)
            return true;
        return false;
    }

    //checking if Lessons exists returns true if exists else, return false
     public async Task<bool> id_exist_l(int lessonId){
        if(lessonId <= 0){
            Console.WriteLine("incorrect id input (negative)");
            return false;
        }
        CollectionReference studentsRef = db.Collection("Lessons");
        Query query = studentsRef.WhereEqualTo("id", lessonId);
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
        if(querySnapshot.Count > 0)
            return true;
        return false;
     }

    //deleting all documents in the collection "collection_name" with limmit of "batchSize"
    public async Task DeleteCollection(string collection_name, int batchSize)
    {
        CollectionReference cf = db.Collection(collection_name);
        QuerySnapshot snapshot = await cf.Limit(batchSize).GetSnapshotAsync();
        IReadOnlyList<DocumentSnapshot> documents = snapshot.Documents;
        while (documents.Count > 0)
        {
            foreach (DocumentSnapshot document in documents)
            {
                Console.WriteLine("Deleting document {0}", document.Id);
                await document.Reference.DeleteAsync();
            }
            snapshot = await cf.Limit(batchSize).GetSnapshotAsync();
            documents = snapshot.Documents;
        }
        Console.WriteLine("Finished deleting all documents from the collection.");
    }

    //returns the next smallest free id from collection "collection_name"
    public async Task<int> free_id(string collection_name){
        int counter = 1;
        CollectionReference cf = db.Collection(collection_name);
        QuerySnapshot allsnaps = await cf.GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in allsnaps.Documents)
        {
            if(Int64.Parse(documentSnapshot.Id) == counter)
                counter++;
            else
                break;
            
        }
        return counter;

    }

    public async Task<bool> change_document_value(string collection_name, string key, Object value, int id){
        if(key.Equals("id")){
            Console.WriteLine("you are trying to change the user id");
            return false;
        }
        if(!id_exist(id, collection_name).Result || id <= 0){
            Console.WriteLine("{0} does not exist", collection_name);
            return false;
        }

        if(collection_name.Equals("Student")  || collection_name.Equals("Teacher") || collection_name.Equals("Lessons")){
            DocumentReference docRef = db.Collection(collection_name).Document(id.ToString());
            await docRef.UpdateAsync(key, value);
            Console.WriteLine("changed the {0} value at id =  {1}.", key, id);
            return true;
        }
        return false;
        
        
    }

    public async Task<bool> delete_Document_byid(int id, string collection_name){


        if(collection_name != "Student" &&  collection_name != "Teacher" && collection_name != "Lessons"){
            return false;
        }

        if(!id_exist(id, collection_name).Result){
            Console.WriteLine("{0} does not exist", collection_name);
            return false;
        }else{

            DocumentReference docRef = db.Collection(collection_name).Document(id.ToString());
            WriteResult writeResult = await docRef.DeleteAsync();
            Console.WriteLine(writeResult.UpdateTime);
            Console.WriteLine("deleted data from {0} collection.", collection_name);
            return true;
        }

    }

    //gets value from a document 
    //if the document doesnt exist returns -1
    public async Task<object> get_value_Document(string collection_name, int Did, string key){

        if(Did <= 0){
            Console.WriteLine("incorrect id ({0}) input (non-positive)", Did);
            return false;
        }else if(collection_name.Equals("Student")  || collection_name.Equals("Teacher") || collection_name.Equals("Lessons")){
                DocumentReference documentReference = db.Collection(collection_name).Document(Did.ToString());

                DocumentSnapshot docsnap = await documentReference.GetSnapshotAsync();

                if(docsnap.Exists){
                    Dictionary<string, object> dictionary = docsnap.ToDictionary();
                    return dictionary[key];
                }else{
                    Console.WriteLine("there is no Document with id = {0}", Did);
                    return -1;
                }
        }else
            return false;

        
    }


    //--------------------------------------------------------student

    //adds student object to the Student Document. on seccess -> true, on failure -> false
    //If id is initialized to -1 the student will be given a free id value
    public async Task<bool> add_student(Student s){

        if(s.Id == -1){
            int new_id = free_id("Student").Result;
            s.Id = new_id;
        }else if(s.Id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", s.Id);
            return false;
        }else if(id_exist(s.Id, "Student").Result){
            Console.WriteLine("student id = {0} already exist", s.Id);
            return false;
        }
        
        DocumentReference docRef = db.Collection("Student").Document(s.Id.ToString());

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

        return true;
        
    }

    public async Task<Student> get_student_byid(int id)
    {
        DocumentReference docRef = db.Collection("Student").Document(id.ToString());
        
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        Student s = new Student();

        if(snapshot.Exists){
            Dictionary<string, object> lessonDic = snapshot.ToDictionary();
            
            s.Email = (string) lessonDic["email"];
            s.Name= (string) lessonDic["name"];
            s.Password = (string) lessonDic["password"];
            s.Phone = (string) lessonDic["phone"];
            s.Id=(int)(long)lessonDic["id"];
            return s;


        }else{
            Console.WriteLine("there is no Student with id = {0}", id);
            return s;
        }
    }

    //creates and adds student object to the Student Document. on seccess -> true, on failure -> false
    //If id is initialized to -1 the student will be given a free id value
    public async Task<bool> add_new_student(string phone, string name, string pass, string email, int id){
        
        if(id == -1){
            int new_id = free_id("Student").Result;
            id = new_id;
        }else if(id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", id);
            return false;
        }else if(id_exist(id, "Student").Result){
            Console.WriteLine("student id = {0} already exist", id);
            return false;
        }
    
        DocumentReference docRef = db.Collection("Student").Document(id.ToString());

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

        return true;
        

        
    }

    //delete a student by identifier string id
    public async Task<bool> delete_student(int id){

        if(!id_exist(id, "Student").Result){
            Console.WriteLine("Student does not exist");
            return false;
        }else{

            DocumentReference docRef = db.Collection("Student").Document(id.ToString());
            WriteResult writeResult = await docRef.DeleteAsync();
            Console.WriteLine(writeResult.UpdateTime);
            Console.WriteLine("deleted data from Student collection.");
            return true;
        }

    }
    

    public async Task<bool> change_s_phone_byid(string phone, int id){
        if(!id_exist(id, "Student").Result){
            Console.WriteLine("Student does not exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection("Student").Document(id.ToString());
            await docRef.UpdateAsync("phone", phone);
            Console.WriteLine("changed the value.");

            return true;
        }
        

    }

     public async Task<bool> change_s_email_byid(string email, int id){

        if(!id_exist(id, "Student").Result){
            Console.WriteLine("Student does not exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection("Student").Document(id.ToString());

            await docRef.UpdateAsync("email", email);

            Console.WriteLine("changed the value.");


            return true;
        }

       
    }

    public async Task<bool> change_s_name_byid(string name, int id){

        if(!id_exist(id, "Student").Result){
            Console.WriteLine("Student does not exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection("Student").Document(id.ToString());
            await docRef.UpdateAsync("name", name);
            Console.WriteLine("changed the value.");
            return true;
        }

    }

    public async Task<bool> change_s_pass_byid(string pass, int id){

        if(!id_exist(id, "Student").Result){
            Console.WriteLine("Student does not exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection("Student").Document(id.ToString());
            await docRef.UpdateAsync("password", pass);
            Console.WriteLine("changed the value.");
            return true;
        }     

    }

    //----------------------------------------theacher

    //creates and adds Teacher object to the Teacher Document. on seccess -> true, on failure -> false
    //If id is initialized to -1 the Teacher will be given a free id value
    public async Task<bool> add_new_theacher(string phone, string name, string pass, string email, int id){

        if(id == -1){
            int new_id = free_id("Teacher").Result;
            id = new_id;
        }else if(id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", id);
            return false;
        }else if(id_exist(id, "Teacher").Result){
            Console.WriteLine("Teacher id = {0} already exist", id);
            return false;
        }
    
        DocumentReference docRef = db.Collection("Teacher").Document(id.ToString());

        Teacher new_teacher = new Teacher(phone, name, pass, email, id);

        Dictionary<string, object> newuser = new Dictionary<string, object>
        {
            { "name", new_teacher.Name },
            { "email", new_teacher.Email },
            { "password", new_teacher.Password },
            { "phone", new_teacher.Phone },
            { "id", new_teacher.Id }
        };


        WriteResult writeResult = await docRef.SetAsync(newuser);
        Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Teacher collection.");

        return true;
        
    }

    //creates and adds Teacher object to the Teacher Document. on seccess -> true, on failure -> false
    //If id is initialized to -1 the Teacher will be given a free id value
    public async Task<bool> add_new_theacher(Teacher t){

        if(t.Id == -1){
            int new_id = free_id("Teacher").Result;
            t.Id = new_id;
        }else if(t.Id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", t.Id);
            return false;
        }else if(id_exist(t.Id, "Teacher").Result){
            Console.WriteLine("Teacher id = {0} already exist", t.Id);
            return false;
        }
    
        DocumentReference docRef = db.Collection("Teacher").Document(t.Id.ToString());

        Dictionary<string, object> newuser = new Dictionary<string, object>
        {
            { "name", t.Name },
            { "email", t.Email },
            { "password", t.Password },
            { "phone", t.Phone },
            { "id", t.Id }
        };


        WriteResult writeResult = await docRef.SetAsync(newuser);
        Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Teacher collection.");

        return true;
        

        
    }

    public async Task<Teacher> get_theacher_byid(int id)
    {
        DocumentReference docRef = db.Collection("Teacher").Document(id.ToString());
        
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        Teacher t = new Teacher();

        if(snapshot.Exists){
            Dictionary<string, object> lessonDic = snapshot.ToDictionary();
            
            t.Email = (string) lessonDic["email"];
            t.Name= (string) lessonDic["name"];
            t.Password = (string) lessonDic["password"];
            t.Phone = (string) lessonDic["phone"];
            t.Id= (int)(long) lessonDic["id"];
            return t;


        }else{
            Console.WriteLine("there is no Teacher with id = {0}", id);
            return t;
        }
    }

    public async Task<bool> change_t_phone_byid(string phone, int id){
        if(!id_exist_t(id).Result){
            Console.WriteLine("teacher doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection("Teacher").Document(id.ToString());
            await docRef.UpdateAsync("phone", phone);
            Console.WriteLine("changed the value.");
            return true;
        }
        
    }
    public async Task<bool> change_t_name_byid(string name, int id){
        if(!id_exist_t(id).Result){
            Console.WriteLine("teacher doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection("Teacher").Document(id.ToString());
            await docRef.UpdateAsync("name", name);
            Console.WriteLine("changed the value.");
            return true;
        }
        
    }
    public async Task<bool> change_t_pass_byid(string pass, int id){
        if(!id_exist_t(id).Result){
            Console.WriteLine("teacher doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection("Teacher").Document(id.ToString());
            await docRef.UpdateAsync("password", pass);
            Console.WriteLine("changed the value.");
            return true;
        }
        
    }
    public async Task<bool> change_t_email_byid(string email, int id){
        if(!id_exist_t(id).Result){
            Console.WriteLine("teacher doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection("Teacher").Document(id.ToString());
            await docRef.UpdateAsync("email", email);
            Console.WriteLine("changed the value.");
            return true;
        }
        
    }

    

    //---------------------------------------lessons

    public async Task<bool> add_new_lesson(Lesson l)
    {
        if(l.Id == -1){
            int new_id = free_id("Lesson").Result;
            l.Id = new_id;
        }else if(l.Id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", l.Id);
            return false;
        }else if(id_exist(l.Id, "Lessons").Result){
            Console.WriteLine("Lesson id = {0} already exist", l.Id);
            return false;
        }
    
        DocumentReference docRef = db.Collection("Lessons").Document(l.Id.ToString());

        Dictionary<string, object> newlesson = new Dictionary<string, object>
        {
            { "id", l.Id },
            { "TeacherId", l.TeacherId },
            { "StudentId", l.StudentId },
            { "Date", l.Date },   
            { "Comment", l.Comment }
        };


        WriteResult writeResult = await docRef.SetAsync(newlesson);
        Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Lessons collection.");

        return true;
        

    }

    public async Task<bool> add_new_lesson(int id, int TheacherId, int StudentId, DateTime date, string comment){
        if(id == -1){
            int new_id = free_id("Lesson").Result;
            id = new_id;
        }else if(id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", id);
            return false;
        }else if(id_exist(id, "Lessons").Result){
            Console.WriteLine("Lesson id = {0} already exist", id);
            return false;
        }

        Lesson newLesson = new Lesson(id, TheacherId, StudentId, date, comment);
    
        DocumentReference docRef = db.Collection("Lessons").Document(id.ToString());

        Dictionary<string, object> newlessonDic = new Dictionary<string, object>
        {
            { "id", newLesson.Id },
            { "TeacherId", newLesson.TeacherId },
            { "StudentId", newLesson.StudentId },
            { "Date", newLesson.Date },   
            { "Comment", newLesson.Comment }
        };


        WriteResult writeResult = await docRef.SetAsync(newlessonDic);
        Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Lessons collection.");

        return true;
    }

    public async Task<bool> change_comment_byid(int id, string comment)
    {
        if(!id_exist(id, "Lessons").Result){
            Console.WriteLine("lesson doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection("Lessons").Document(id.ToString());
            await docRef.UpdateAsync("Comment", comment);
            Console.WriteLine("changed the comment.");
            return true;
        }
    }

    public async Task<bool> change_date_byid(int Lid, DateTime newDate)
    {
        if(!id_exist(Lid, "Lessons").Result){
            Console.WriteLine("lesson doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection("Lessons").Document(Lid.ToString());
            await docRef.UpdateAsync("Date", newDate);
            Console.WriteLine("changed the Date.");
            return true;
        }
    }

    public async Task<Lesson> get_lesson_byid(int Lid)
    {
        DocumentReference docRef = db.Collection("Lessons").Document(Lid.ToString());
        
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        Lesson lesson = new Lesson();

        if(snapshot.Exists){
            Dictionary<string, object> lessonDic = snapshot.ToDictionary();
            
            //lessonDic.TryGetValue("comment", out comment);
            lesson.Comment = (string) lessonDic["Comment"];
            lesson.Date = (DateTime) lessonDic["Date"];
            lesson.Id = (int)(long) lessonDic["id"];
            lesson.StudentId = (int)(long) lessonDic["StudentId"];
            lesson.TeacherId = (int)(long) lessonDic["TeacherId"];
            return lesson;


        }else{
            Console.WriteLine("there is no lesson with id = {0}", Lid);
            return lesson;
        }
    }

    public async Task<ArrayList> get_my_lessons_as_theacher(int Lid)
    {
        CollectionReference lessonsref = db.Collection("Lessons");
        
        Query q = lessonsref.WhereEqualTo("TeacherId", Lid);

        QuerySnapshot qs = await q.GetSnapshotAsync();

        ArrayList arry = new ArrayList();

        foreach (DocumentSnapshot documentSnapshot in qs.Documents){
            arry.Add(crud_fun.from_dictionary_to_lesson(documentSnapshot.ToDictionary()));
        }

        return arry;


        
       
    }

    public async Task<ArrayList> get_my_lessons_as_student(int Lid)
    {
        CollectionReference lessonsref = db.Collection("Lessons");
        
        Query q = lessonsref.WhereEqualTo("StudentId", Lid);

        QuerySnapshot qs = await q.GetSnapshotAsync();

        ArrayList arry = new ArrayList();

        foreach (DocumentSnapshot documentSnapshot in qs.Documents){
            arry.Add(crud_fun.from_dictionary_to_lesson(documentSnapshot.ToDictionary()));
        }

        return arry;
    }

    
}

