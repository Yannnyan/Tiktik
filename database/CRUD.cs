namespace TiktikHttpServer.Database;

using System.Collections;
using System.Reflection;
using Google.Cloud.Firestore;
using TiktikHttpServer.Models;


public class CRUD : crud_inter{
    FirestoreDb db;
    public static string Students_collection = "Student";
public static string Teachers_collection = "Teacher";
public static string Lessons_collection = "Lessons";
public static string LearnsWith_collection = "LearnsWith";

    public CRUD(){
        //System.Environment.SetEnvironmentVariable("C:/Users/ברוכסון/OneDrive/מסמכים/יהונתן/אוניברסיטה עבודות/הנדסת תוכנה/Tiktik/database\tiktikdb-bfa5d-70273e817eb9 (1).json");
         System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "tiktikdb-bfa5d-70273e817eb9 (1).json");
        db = FirestoreDb.Create("tiktikdb-bfa5d");
        Console.WriteLine("Created Cloud Firestore client with project ID: tiktikdb-bfa5d");

    }

    public async Task<bool> add(Object T){

        if(T is Student){
            return await add_student((Student)T);
        }else if(T is Teacher){
            return await add_theacher((Teacher)T);
        }else if(T is Lesson){
            return await  add_lesson((Lesson)T);
        }
        else if(T is LearnsWith)
        {
            return await add_LearnsWith((LearnsWith) T);
        }
        return false;
    }

    public async Task<ArrayList> GetAll(Object T){ 
        string collection_name;

        if(T is Student){
            collection_name = Students_collection;
        }else if(T is Teacher){
            collection_name = Teachers_collection;
        }else if(T is Lesson){
            collection_name = Lessons_collection;
        }
        else
        {
            collection_name = LearnsWith_collection;
        }


        CollectionReference lessonsref = db.Collection(collection_name);
        

        QuerySnapshot ds = await lessonsref.GetSnapshotAsync();

        ArrayList arry = new ArrayList();
        foreach(DocumentSnapshot doc in ds.Documents)
        {
            if(T is Student)
                arry.Add(doc.ConvertTo<Student>());
            else if(T is Teacher)
                arry.Add(doc.ConvertTo<Teacher>());
            else if(T is Lesson)
                arry.Add(crud_fun.from_dictionary_to_Object(doc.ToDictionary(), collection_name));
            else if(T is LearnsWith)
                arry.Add(doc.ConvertTo<LearnsWith>());
        }
        return arry;

        // foreach (DocumentSnapshot documentSnapshot in ds.Documents){
        //     arry.Add(crud_fun.from_dictionary_to_Object(documentSnapshot.ToDictionary(), collection_name));
        // }

        // return arry;
    }

    //checking if "collection_name" id already exists. returns true if exists else, return false
    public async Task<bool> id_exist(int id, string collection_name){

        if(id <= 0){
            Console.WriteLine("incorrect id ({0}) input (non-positive)", id);
            return false;
        }else if(collection_name.Equals(Students_collection)  || collection_name.Equals(Teachers_collection) || collection_name.Equals(Lessons_collection)){
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
        
        CollectionReference cf = db.Collection(collection_name);
        //geting a snap shot with no query
        QuerySnapshot allsnaps = await cf.GetSnapshotAsync();
        int[] idarry = new int[allsnaps.Count];
        int counter = 0;
        foreach (DocumentSnapshot documentSnapshot in allsnaps.Documents)
        {
            idarry[counter] = Int32.Parse(documentSnapshot.Id);
            
            counter++;
        }
        crud_fun.sort(idarry);

        counter = 1;
        foreach(int i in idarry){
            if(i == counter){
                
                counter++;
            }else{
                Console.WriteLine("free id = {0}", counter);
                return counter;
            }

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

        if(collection_name.Equals(Students_collection)  || collection_name.Equals(Teachers_collection) || collection_name.Equals(Lessons_collection)){
            DocumentReference docRef = db.Collection(collection_name).Document(id.ToString());
            await docRef.UpdateAsync(key, value);
            Console.WriteLine("changed the {0} value at id =  {1}.", key, id);
            return true;
        }
        return false;
        
        
    }

    public async Task<bool> Delete(Object T){
        string collection_name = "";
        int object_id = 0;
        if(T is Student){
            collection_name = Students_collection;
            // Get the PropertyInfo object representing MyProperty.
            object_id =((Student)T).Id;
            
        }else if(T is Teacher){
            collection_name = Teachers_collection;
            object_id =((Teacher)T).Id;
        }else if (T is Lesson)
        {
            collection_name = Lessons_collection;
            object_id = object_id =((Lesson)T).Id;
        }
        else if(T is LearnsWith)
        {
            collection_name = LearnsWith_collection;
            return delete_LearnsWith((LearnsWith) T) != null;
        }
        if(T is Student || T is Teacher || T is Lesson)
            return await delete_Document_byid(object_id, collection_name);
        return false;
         
    }
        
    public async Task<bool> delete_Document_byid(int id, string collection_name){


        if(collection_name != Students_collection &&  collection_name != Teachers_collection && collection_name != Lessons_collection){
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
        }else if(collection_name.Equals(Students_collection)  || collection_name.Equals(Teachers_collection) || collection_name.Equals(Lessons_collection)){
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
            int new_id = free_id(Students_collection).Result;
            s.Id = new_id;
        }else if(s.Id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", s.Id);
            return false;
        }else if(id_exist(s.Id, Students_collection).Result){
            Console.WriteLine("student id = {0} already exist", s.Id);
            return false;
        }
        
        DocumentReference docRef = db.Collection(Students_collection).Document(s.Id.ToString());

        Dictionary<string, object> newuser = new Dictionary<string, object>
        {
            { "name", s.Name },
            { "email", s.Email },
            { "password", s.Password },
            { "phone", s.Phone },
            { "id", s.Id }
        }; 

        WriteResult writeResult = await docRef.SetAsync(newuser);
        //Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Student collection.");

        return true;
        
    }

    public async Task<Student> get_student_byid(int id)
    {
        DocumentReference docRef = db.Collection(Students_collection).Document(id.ToString());
        
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
    public async Task<bool> add_student(string phone, string name, string pass, string email, int id){
        
        if(id == -1){
            id = free_id(Students_collection).Result;
            Console.WriteLine("THE ID IS: {0}", id);
        }else if(id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", id);
            return false;
        }else if(id_exist(id, Students_collection).Result){
            Console.WriteLine("student id = {0} already exist", id);
            return false;
        }
    
        DocumentReference docRef = db.Collection(Students_collection).Document(id.ToString());

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
        //Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Student collection.");

        return true;
        

        
    }

    //delete a student by identifier string id
    public async Task<bool> delete_student(int id){

        if(!id_exist(id, Students_collection).Result){
            Console.WriteLine("Student does not exist");
            return false;
        }else{

            DocumentReference docRef = db.Collection(Students_collection).Document(id.ToString());
            WriteResult writeResult = await docRef.DeleteAsync();
            Console.WriteLine(writeResult.UpdateTime);
            Console.WriteLine("deleted data from Student collection.");
            return true;
        }

    }
    

    public async Task<bool> change_s_phone_byid(string phone, int id){
        if(!id_exist(id, Students_collection).Result){
            Console.WriteLine("Student does not exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection(Students_collection).Document(id.ToString());
            await docRef.UpdateAsync("phone", phone);
            Console.WriteLine("changed the value.");

            return true;
        }
        

    }

     public async Task<bool> change_s_email_byid(string email, int id){

        if(!id_exist(id, Students_collection).Result){
            Console.WriteLine("Student does not exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection(Students_collection).Document(id.ToString());

            await docRef.UpdateAsync("email", email);

            Console.WriteLine("changed the value.");


            return true;
        }

       
    }

    public async Task<bool> change_s_name_byid(string name, int id){

        if(!id_exist(id, Students_collection).Result){
            Console.WriteLine("Student does not exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection(Students_collection).Document(id.ToString());
            await docRef.UpdateAsync("name", name);
            Console.WriteLine("changed the value.");
            return true;
        }

    }

    public async Task<bool> change_s_pass_byid(string pass, int id){

        if(!id_exist(id, Students_collection).Result){
            Console.WriteLine("Student does not exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection(Students_collection).Document(id.ToString());
            await docRef.UpdateAsync("password", pass);
            Console.WriteLine("changed the value.");
            return true;
        }     

    }

    //----------------------------------------theacher

    //creates and adds Teacher object to the Teacher Document. on seccess -> true, on failure -> false
    //If id is initialized to -1 the Teacher will be given a free id value
    public async Task<bool> add_theacher(string phone, string name, string pass, string email, int id){

        if(id == -1){
            int new_id = free_id(Teachers_collection).Result;
            id = new_id;
        }else if(id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", id);
            return false;
        }else if(id_exist(id, Teachers_collection).Result){
            Console.WriteLine("Teacher id = {0} already exist", id);
            return false;
        }
    
        DocumentReference docRef = db.Collection(Teachers_collection).Document(id.ToString());

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
        //Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Teacher collection.");

        return true;
        
    }

    //creates and adds Teacher object to the Teacher Document. on seccess -> true, on failure -> false
    //If id is initialized to -1 the Teacher will be given a free id value
    public async Task<bool> add_theacher(Teacher t){

        if(t.Id == -1){
            int new_id = free_id(Teachers_collection).Result;
            t.Id = new_id;
        }else if(t.Id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", t.Id);
            return false;
        }else if(id_exist(t.Id, Teachers_collection).Result){
            Console.WriteLine("Teacher id = {0} already exist", t.Id);
            return false;
        }
    
        DocumentReference docRef = db.Collection(Teachers_collection).Document(t.Id.ToString());

        Dictionary<string, object> newuser = new Dictionary<string, object>
        {
            { "name", t.Name },
            { "email", t.Email },
            { "password", t.Password },
            { "phone", t.Phone },
            { "id", t.Id }
        };


        WriteResult writeResult = await docRef.SetAsync(newuser);
        //Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Teacher collection.");

        return true;
        

        
    }

    public async Task<Teacher> get_theacher_byid(int id)
    {
        DocumentReference docRef = db.Collection(Teachers_collection).Document(id.ToString());
        
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
        if(!id_exist(id, Teachers_collection).Result){
            Console.WriteLine("teacher doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection(Teachers_collection).Document(id.ToString());
            await docRef.UpdateAsync("phone", phone);
            Console.WriteLine("changed the value.");
            return true;
        }
        
    }
    public async Task<bool> change_t_name_byid(string name, int id){
        if(!id_exist(id, Teachers_collection).Result){
            Console.WriteLine("teacher doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection(Teachers_collection).Document(id.ToString());
            await docRef.UpdateAsync("name", name);
            Console.WriteLine("changed the value.");
            return true;
        }
        
    }
    public async Task<bool> change_t_pass_byid(string pass, int id){
        if(!id_exist(id, Teachers_collection).Result){
            Console.WriteLine("teacher doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection(Teachers_collection).Document(id.ToString());
            await docRef.UpdateAsync("password", pass);
            Console.WriteLine("changed the value.");
            return true;
        }
        
    }
    public async Task<bool> change_t_email_byid(string email, int id){
        if(!id_exist(id, Teachers_collection).Result){
            Console.WriteLine("teacher doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection(Teachers_collection).Document(id.ToString());
            await docRef.UpdateAsync("email", email);
            Console.WriteLine("changed the value.");
            return true;
        }
        
    }

    

    //---------------------------------------lessons

    public async Task<bool> add_lesson(Lesson l)
    {
        if(l.Id == -1){
            int new_id = free_id(Lessons_collection).Result;
            l.Id = new_id;
        }else if(l.Id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", l.Id);
            return false;
        }else if(id_exist(l.Id, Lessons_collection).Result){
            Console.WriteLine("Lesson id = {0} already exist", l.Id);
            return false;
        }
    
        DocumentReference docRef = db.Collection(Lessons_collection).Document(l.Id.ToString());

        Dictionary<string, object> newlesson = new Dictionary<string, object>
        {
            { "id", l.Id },
            { "teacherid", l.TeacherId },
            { "studentid", l.StudentId },
            { "date", l.Date },   
            { "comment", l.Comment }
        };


        WriteResult writeResult = await docRef.SetAsync(newlesson);
        Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Lessons collection.");

        return true;
        

    }

    public async Task<bool> add_lesson(int id, int TheacherId, int StudentId, DateTime date, string comment){
        if(id == -1){
            int new_id = free_id(Lessons_collection).Result;
            id = new_id;
        }else if(id <= 0){
            Console.WriteLine("incorrect id = {0} input (non-positive)", id);
            return false;
        }else if(id_exist(id, Lessons_collection).Result){
            Console.WriteLine("Lesson id = {0} already exist", id);
            return false;
        }

        Lesson newLesson = new Lesson(id, TheacherId, StudentId, date, comment);
    
        DocumentReference docRef = db.Collection(Lessons_collection).Document(id.ToString());

        Dictionary<string, object> newlessonDic = new Dictionary<string, object>
        {
            { "id", newLesson.Id },
            { "teacherid", newLesson.TeacherId },
            { "studentid", newLesson.StudentId },
            { "date", newLesson.Date },   
            { "comment", newLesson.Comment }
        };


        WriteResult writeResult = await docRef.SetAsync(newlessonDic);
        Console.WriteLine(writeResult.UpdateTime);
        Console.WriteLine("Added data to the Lessons collection.");

        return true;
    }

    public async Task<bool> change_comment_byid(int id, string comment)
    {
        if(!id_exist(id, Lessons_collection).Result){
            Console.WriteLine("lesson doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection(Lessons_collection).Document(id.ToString());
            await docRef.UpdateAsync("Comment", comment);
            Console.WriteLine("changed the comment.");
            return true;
        }
    }

    public async Task<bool> change_date_byid(int Lid, DateTime newDate)
    {
        if(!id_exist(Lid, Lessons_collection).Result){
            Console.WriteLine("lesson doesnt exist");
            return false;
        }else{
            DocumentReference docRef = db.Collection(Lessons_collection).Document(Lid.ToString());
            await docRef.UpdateAsync("Date", newDate);
            Console.WriteLine("changed the Date.");
            return true;
        }
    }

    public async Task<Lesson> get_lesson_byid(int Lid)
    {
        DocumentReference docRef = db.Collection(Lessons_collection).Document(Lid.ToString());
        
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        Lesson lesson = new Lesson();

        if(snapshot.Exists){
            Dictionary<string, object> lessonDic = snapshot.ToDictionary();
            
            //lessonDic.TryGetValue("comment", out comment);
            lesson.Comment = (string) lessonDic["comment"];
            lesson.Date = (DateTime) lessonDic["date"];
            lesson.Id = (int)(long) lessonDic["id"];
            lesson.StudentId = (int)(long) lessonDic["studentid"];
            lesson.TeacherId = (int)(long) lessonDic["teacherid"];
            return lesson;


        }else{
            Console.WriteLine("there is no lesson with id = {0}", Lid);
            return lesson;
        }
    }

    public async Task<ArrayList> get_my_lessons_as_theacher(int Lid)
    {
        CollectionReference lessonsref = db.Collection(Lessons_collection);
        
        Query q = lessonsref.WhereEqualTo("teacherid", Lid);

        QuerySnapshot qs = await q.GetSnapshotAsync();

        ArrayList arry = new ArrayList();

        foreach (DocumentSnapshot documentSnapshot in qs.Documents){
            arry.Add(crud_fun.from_dictionary_to_lesson(documentSnapshot.ToDictionary()));
        }

        return arry;


        
       
    }

    public async Task<ArrayList> get_my_lessons_as_student(int Lid)
    {
        CollectionReference lessonsref = db.Collection(Lessons_collection);
        
        Query q = lessonsref.WhereEqualTo("studentid", Lid);

        QuerySnapshot qs = await q.GetSnapshotAsync();

        ArrayList arry = new ArrayList();

        foreach (DocumentSnapshot documentSnapshot in qs.Documents){
            arry.Add(crud_fun.from_dictionary_to_lesson(documentSnapshot.ToDictionary()));
        }

        return arry;
    }
        
    public void Update(object T, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> add_LearnsWith(LearnsWith learnsWith)
    {
        CollectionReference learnsWithCollection = db.Collection(LearnsWith_collection);
        return await learnsWithCollection.AddAsync(learnsWith) != null;
    }
    public async Task<bool> delete_LearnsWith(LearnsWith learnsWith)
    {
        DocumentReference dc = db.Collection(LearnsWith_collection).Document("studentid/" + learnsWith.studentid);
        return await dc.DeleteAsync() != null;
    }
    /*
        This function gets the teacher that teaches this student
    */
    public async Task<LearnsWith> getLearnsWith(int StudentId)
    {
        Query collection = db.Collection(LearnsWith_collection).WhereEqualTo("studentid", StudentId);
        QuerySnapshot snap = await collection.GetSnapshotAsync(); 
        if(snap.Documents.Count > 0)       
            return snap.Documents[0].ConvertTo<LearnsWith>();
        return new LearnsWith(StudentId);
    }
    /**
    This function returns all the students that learn with the teacher by his id
    */
    public async Task<ArrayList> getStudentsByTeacher(int teacherId)
    {
        Query collection = db.Collection(LearnsWith_collection).WhereEqualTo("teacherid", teacherId);
        QuerySnapshot snap = await collection.GetSnapshotAsync();
        ArrayList lst = new ArrayList();
        foreach(DocumentSnapshot doc in snap.Documents)
        {
            lst.Add(doc.ConvertTo<LearnsWith>());
        }
        return lst;
    }

    
}

