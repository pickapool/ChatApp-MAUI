using ChatApp_MAUI.Web.Components;
using ChatApp_MAUI.Shared.Services;
using ChatApp_MAUI.Web.Services;
using MudBlazor.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using ChatApp_MAUI.Shared.Services.RegistrationServices;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using Firebase.Auth.Providers;
using Firebase.Auth;
using Blazored.LocalStorage;
using ChatApp_MAUI.AuthenticationProvider;
using Microsoft.AspNetCore.Components.Authorization;
using ChatApp_MAUI.Shared.Services.NavigationServices;
using ChatApp_MAUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddServerSideBlazor().AddCircuitOptions(e => e.DetailedErrors = true);
}
else
{
    builder.Services.AddServerSideBlazor(); // Register services without detailed errors in production
}
// Add device-specific services used by the ChatApp_MAUI.Shared project
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("Authetication:TokenUri")) });
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationState>();
builder.Services.AddScoped<INavigationService, NavigationService>();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddServerSideBlazor()
        .AddHubOptions(o => o.MaximumReceiveMessageSize = 100_000_000); // add this

//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("firebaseconfig.json"),
});
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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(ChatApp_MAUI.Shared._Imports).Assembly);

app.Run();
