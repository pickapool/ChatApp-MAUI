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
using ChatApp_MAUI.Shared.Services.FirebaseStorageServices;
using ChatApp_MAUI.Shared.Components;
using ChatApp_MAUI.Shared.Services.CameraServices;
using ChatApp_MAUI.Shared.Services.UserServices;
using ChatApp_MAUI.Shared.Services.INotificationServices;
using ChatApp_MAUI.Shared.Services.FriendServices;
using Services.CallBackServices.LoadFriendsCallback;
using ChatApp_MAUI.Shared.Services.CallBackServices.ConversationsCallback;
using ChatApp_MAUI.Shared.Services.MessageServices;
using ChatApp_MAUI.Shared.Services.CallBackServices.ChatRoomCallback;

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
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPI:Url")) });
builder.Services.AddHttpClient();
builder.Services.AddSingleton<LayoutNotifierService>();
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationState>();
builder.Services.AddScoped<INavigationService, NavigationService>();
builder.Services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<ICallBackService, CallBackService>();
builder.Services.AddScoped<IConversationCallback, ConversationCallback>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IChatRoomCallbackService, ChatRoomCallbackService>();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddServerSideBlazor()
        .AddHubOptions(o => o.MaximumReceiveMessageSize = 100_000_000); // add this


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
