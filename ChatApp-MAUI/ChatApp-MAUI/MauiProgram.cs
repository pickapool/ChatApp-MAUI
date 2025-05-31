using Microsoft.Extensions.Logging;
using ChatApp_MAUI.Shared.Services;
using ChatApp_MAUI.Services;
using MudBlazor.Services;
using FirebaseAdmin;
using Microsoft.Extensions.Options;
using System.Net;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using ChatApp_MAUI.Services.AuthenticationServices;

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

        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
        
        //Firebase
        FirebaseApp.Create(new AppOptions
        {
           Credential = GoogleCredential.FromFile("firebaseconfig.json"),
        });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
