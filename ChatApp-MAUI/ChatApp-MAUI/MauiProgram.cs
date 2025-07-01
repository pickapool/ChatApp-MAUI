using Microsoft.Extensions.Logging;
using ChatApp_MAUI.Shared.Services;
using ChatApp_MAUI.Services;
using MudBlazor.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using ChatApp_MAUI.AuthenticationProvider;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Shared.Services.RegistrationServices;
using Firebase.Auth;
using Firebase.Auth.Providers;
using CommunityToolkit.Maui;
using ChatApp_MAUI.Shared.Services.NavigationServices;
using ChatApp_MAUI.Components;
using System.Reflection;
using ChatApp_MAUI.Shared.Services.FirebaseStorageServices;
using ChatApp_MAUI.Shared.Services.CameraServices;
using ChatApp_MAUI.Shared.Services.UserServices;
using ChatApp_MAUI.Shared.Services.INotificationServices;
using ChatApp_MAUI.Shared.Services.FriendServices;
using ChatApp_MAUI.Shared.Services.CallBackServices;
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
