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
using ChatApp_MAUI.Shared;
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
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("Authetication:TokenUri")) });

        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddMudServices();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationState>();
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
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

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
