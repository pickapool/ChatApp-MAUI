using Microsoft.Extensions.Logging;
using ChatApp_MAUI.Services;
using MudBlazor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using ChatApp_MAUI.AuthenticationProvider;
using CommunityToolkit.Maui;
using ChatApp_MAUI.Infrastructure.Services;
using ChatApp_MAUI.Infrastructure.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Infrastructure.Services.RegistrationServices;
using ChatApp_MAUI.Infrastructure.Services.NavigationServices;
using ChatApp_MAUI.Infrastructure.Services.FirebaseStorageServices;
using ChatApp_MAUI.Infrastructure.Services.UserServices;
using ChatApp_MAUI.Infrastructure.Services.FriendServices;
using ChatApp_MAUI.Infrastructure.CallBackServices.LoadFriendsCallback;
using ChatApp_MAUI.Infrastructure.Services.MessageServices;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ChatRoomCallback;
using ChatApp_MAUI.Infrastructure;
using System.Reflection;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ConversationsCallback;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.FriendRequestCallback;
namespace ChatApp_MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            }).UseMauiCommunityToolkitCamera();

        // Add device-specific services used by the ChatApp_MAUI.Shared project

        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddMudServices();
        builder.Services.AddHttpClient();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddSingleton<LayoutNotifierService>();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationState>();
        builder.Services.AddScoped<ILoginService, LoginService>();
        builder.Services.AddScoped<IRegistrationService, RegistrationService>();
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

        builder.Services.AddAuthorizationCore();
        builder.Services.AddCascadingAuthenticationState();

#if WINDOWS || MACCATALYST
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
#else
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("ChatApp_MAUI.appsettings.mobile.json");
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
        builder.Configuration.AddConfiguration(config);

#endif
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPI:Url")) });

//Fix connection timeout in server
#if ANDROID
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        var httpClient = new HttpClient(handler);
#endif

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
