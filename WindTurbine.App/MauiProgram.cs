using Microsoft.Extensions.Logging;
using WindTurbine.App.Services; 
using MudBlazor.Services;       

namespace WindTurbine.App
{
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

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

           
            builder.Services.AddSingleton<HttpClient>(sp =>
            {
                
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                
                return new HttpClient(handler)
                {
                    BaseAddress = new Uri("http://localhost:5279")
                };
            });

            
            builder.Services.AddSingleton<IApiService, ApiService>();

           
            builder.Services.AddMudServices();

            return builder.Build();
        }
    }
}