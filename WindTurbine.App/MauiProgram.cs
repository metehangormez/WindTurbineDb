using Microsoft.Extensions.Logging;
using WindTurbine.App.Services;
using MudBlazor.Services; // <-- BU ÇOK ÖNEMLİ

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

            // API Bağlantısı (Port numaranız 5279 idi, değişirse güncelleyin)
            builder.Services.AddSingleton<HttpClient>(sp =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };
                return new HttpClient(handler) { BaseAddress = new Uri("http://localhost:5279") };
            });

            builder.Services.AddSingleton<IApiService, ApiService>();

            // --- MUD BLAZOR SERVİSİ (BU EKSİKTİ) ---
            builder.Services.AddMudServices();

            return builder.Build();
        }
    }
}