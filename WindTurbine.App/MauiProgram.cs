using Microsoft.Extensions.Logging;
using WindTurbine.App.Services;

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

                string baseAddress = "http://localhost:5279";


                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) => true
                };

                return new HttpClient(handler)
                {
                    BaseAddress = new Uri(baseAddress)
                };
            });

          
            builder.Services.AddSingleton<IApiService, ApiService>();

            return builder.Build();
        }
    }
}
