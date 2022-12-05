using TiktikHttpServer.Models;
using System.Collections;
using Google.Cloud.Firestore;
/*

Methods marked with async in C# must return one of the following:

1. Task -> whene you dont want tou return information from the Task
2. Task<T> -> whene you want to return information from a task
3. ValueTask
4. ValueTask<T>
5. void 

In ordet to acces the Task result use "Result " property like this: Task.Result

*/
public interface crud_inter{


    //-----------------------------------------students: add, change, get, delete

    //creates and adds Student object to the Student Document. on seccess -> true, on failure -> false
    //If id is initialized to -1 the Student will be given a free id value
    //s.id should be -1 or s.id > 0
    public Task<bool> add_student(Student s);

    //creates and adds Student object to the Student Document. on seccess -> true, on failure -> false
    //If id is initialized to -1 the Student will be given a free id value
    //id should be -1 or id > 0
    public Task<bool> add_new_student(string phone, string name, string pass, string email, int id);

    //delete a student by identifier string id
    //return true on seccess and false on fail or the document doesent exist
    public Task<bool> delete_student(int id);
    
    public Task<bool> change_s_phone_byid(string phone, int id);

    public Task<bool> change_s_email_byid(string email, int id);

    public Task<bool> change_s_name_byid(string name, int id);

    public Task<bool> change_s_pass_byid(string pass, int id);

    //-----------------------------------theacher: add, change, get, delete

    //creates and adds Teacher object to the Teacher Document. on seccess -> true, on failure -> false
    //If id is initialized to -1 the Teacher will be given a free id value
    //id should be -1 or id > 0
    public Task<bool> add_new_theacher(string phone, string name, string pass, string email, int id);

    //creates and adds Teacher object to the Teacher Document. on seccess -> true, on failure -> false
    //If id is initialized to -1 the Teacher will be given a free id value
    //t.id should be -1 or t.id > 0
    public Task<bool> add_new_theacher(Teacher t);

    public Task<bool> change_t_phone_byid(string phone, int id);

    public Task<bool> change_t_email_byid(string email, int id);

    public Task<bool> change_t_name_byid(string name, int id);

    public Task<bool> change_t_pass_byid(string pass, int id);
    

    //------------------------------------------lessons

    //adds new lesson with id = l.id
    //if id = -1 lesson will get smallest free id
    public Task<bool> add_new_lesson(Lesson l);

    public Task<bool> add_new_lesson(int id, int TheacherId, int StudentId, Timestamp date, string comment);

    public Task<bool> change_comment_byid(int id, String comment);

    public Task<bool> change_date_byid(int id, Timestamp newDate);

    public Task<Lesson> get_lesson_byid(int LessonId);

    public Task<ArrayList> get_my_lessons_as_theacher(int Lid);

    public Task<ArrayList> get_my_lessons_as_student(int Lid);
       
    //-------------------------------extra or general

    public Task<bool> id_exist_s(int studentId);

    public Task<bool> id_exist_t(int teacherId);

    public Task<bool> id_exist_l(int lessonId);

    //this can also be used but be aware, wrong collection_name could lead to exeption
    public Task<bool> id_exist(int id, string collection_name);

    //this can also be used but be aware, wrong object could be selected for value
    public Task<bool> change_document_value(string collection_name, string key, Object value, int id);

    public Task<int> free_id(String collection_name);

    //deleting all documents from collection: collection_name
    public Task DeleteCollection(string collection_name, int batchSize);

    //deliting Document from collection_name specified by the document id
    public Task<bool> delete_Document_byid(int id, string collection_name);

    public Task<object> get_value_Document(string collection_name, int Did, string key);
    



    

    





}