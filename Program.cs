
bool to_test_Database = true;
if(to_test_Database)
    testStuff.test();
else
    RunServer.Run(args);








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

