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

        
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationState>();
        builder.Services.AddScoped<ILoginService, LoginService>();
        builder.Services.AddScoped<IRegistrationService, RegistrationService>();
        builder.Services.AddScoped<INavigationService, NavigationService>();

        //Firebase

        builder.Services.AddAuthorizationCore();
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyDA_IPMONTKeIr-7P8XvyMwQATA9Dpxb3A",
            AuthDomain = "maui-5e9d0.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
        }));


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

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
