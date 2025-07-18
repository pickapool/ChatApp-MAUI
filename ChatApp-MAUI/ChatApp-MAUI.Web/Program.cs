using ChatApp_MAUI.Web.Components;
using ChatApp_MAUI.Web.Services;
using MudBlazor.Services;
using Blazored.LocalStorage;
using ChatApp_MAUI.AuthenticationProvider;
using Microsoft.AspNetCore.Components.Authorization;
using ChatApp_MAUI.Services;
using Microsoft.AspNetCore.Components.Server;
using ChatApp_MAUI.Infrastructure.Services;
using ChatApp_MAUI.Infrastructure.Services.RegistrationServices;
using ChatApp_MAUI.Infrastructure.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Infrastructure.Services.NavigationServices;
using ChatApp_MAUI.Infrastructure.Services.FirebaseStorageServices;
using ChatApp_MAUI.Infrastructure.Services.UserServices;
using ChatApp_MAUI.Infrastructure.Services.FriendServices;
using ChatApp_MAUI.Infrastructure.CallBackServices.LoadFriendsCallback;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ConversationsCallback;
using ChatApp_MAUI.Infrastructure.Services.MessageServices;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ChatRoomCallback;
using ChatApp_MAUI.Infrastructure;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.FriendRequestCallback;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.Configure<CircuitOptions>(options =>
{
    options.DetailedErrors = true;
});
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
builder.Services.AddScoped<IFriendRequestCallback, FriendRequestCallback>();
builder.Services.AddScoped<AppStateService>();

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
