
using Google.Cloud.Firestore;

using TiktikHttpServer.Models;
using TiktikHttpServer.Database;




// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// // app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();



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

DB.add_new_student("0506541482", "sharon", "123456", "sharon.shab@gmail.com", 13);



