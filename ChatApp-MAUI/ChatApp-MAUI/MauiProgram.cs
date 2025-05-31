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
            });

        // Add device-specific services used by the ChatApp_MAUI.Shared project
        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("Authetication:TokenUri")) });

        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddMudServices();
        builder.Services.AddHttpClient();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationState>();
        builder.Services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
        builder.Services.AddScoped<IRegistrationService, RegistrationService>();

        //Firebase

        builder.Services.AddAuthorizationCore();
        builder.Services.AddCascadingAuthenticationState();
#if WINDOWS || MACCATALYST

        FirebaseApp.Create(new AppOptions
        {
           Credential = GoogleCredential.FromFile("firebaseconfig.json"),
        });
#endif
#if ANDROID || IOS
        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyDA_IPMONTKeIr-7P8XvyMwQATA9Dpxb3A",
            AuthDomain = "maui-5e9d0.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
        }));
#endif
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
