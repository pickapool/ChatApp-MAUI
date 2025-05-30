using ChatApp_MAUI.Web.Components;
using ChatApp_MAUI.Shared.Services;
using ChatApp_MAUI.Web.Services;
using MudBlazor.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add device-specific services used by the ChatApp_MAUI.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddMudServices();

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("firebaseconfig.json"),
});

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
