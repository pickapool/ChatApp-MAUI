using Firebase.Auth.Providers;
using Firebase.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI.SignalRHub;
using Google.Cloud.Firestore.V1;
using Google.Cloud.Firestore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:5110")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = "https://securetoken.google.com/maui-5e9d0";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://securetoken.google.com/maui-5e9d0",
        ValidateAudience = true,
        ValidAudience = "maui-5e9d0",
        ValidateLifetime = true
    };
});
builder.Services.AddAuthorization();

//firebase admin
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("firebaseconfig.json")
});
//Fire store
builder.Services.AddSingleton(provider =>
{
    var credential = GoogleCredential.FromFile("firebaseconfig.json");
    var builderFirestore = new FirestoreClientBuilder
    {
        Credential = credential
    };
    var db = FirestoreDb.Create("maui-5e9d0", builderFirestore.Build());
    return db;
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
app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/openapi/v1.json", "WebAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapHub<NotificationHub>("/NotificationHub");

app.Run();
