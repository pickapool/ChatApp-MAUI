using Firebase.Auth.Providers;
using Firebase.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//firebase admin
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("firebaseconfig.json"),
});
//firebaseauth client
builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
{
    ApiKey = "AIzaSyDA_IPMONTKeIr-7P8XvyMwQATA9Dpxb3A",
    AuthDomain = "maui-5e9d0.firebaseapp.com",
    Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/openapi/v1.json", "WebAPI v1");
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
