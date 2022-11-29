
using TiktikHttpServer.Database;
using Google.Cloud.Firestore;
using TiktikHttpServer.Models;



public class testStuff
{
    public async static void test()
    {
        
        CRUD DB = new CRUD();

        await DB.add_new_student("0506541482", "sharon", "123456", "sharon.shab@gmail.com", 13);



    }

}



