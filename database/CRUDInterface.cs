namespace TiktikHttpServer.Database;
using TiktikHttpServer.Models;

public interface CRUDInterface
{
    // if T is a type of student, then add to the students table
    public void Create(Object T);
    // example: 
    // if T is instanceof Student, then
    // return all students, make it generic for Teacher and Lesson
    // to remove redundancy of code
    public Object[] GetAll(Object T);
    // update the object with the id-- id,
    // to be T
    // if T is student, update the student with that id to be T
    public void Update(Object T, int id);
    // delete the object T from the database
    public void Delete(Object T);
}








