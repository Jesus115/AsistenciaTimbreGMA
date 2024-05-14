using JAbarcaPFinal.Services;
using JAbarcaPFinal.Vistas;
using Microsoft.Extensions.Logging;

namespace JAbarcaPFinal;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddTransient<AuthService>();
        builder.Services.AddTransient<VLoading>();
        builder.Services.AddTransient<VILogin>();
        builder.Services.AddTransient<VIUser>();
        return builder.Build();
	}
}

