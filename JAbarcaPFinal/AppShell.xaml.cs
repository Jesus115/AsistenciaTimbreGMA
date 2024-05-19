using JAbarcaPFinal.Vistas;

namespace JAbarcaPFinal;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(VILogin), typeof(VILogin));
		Routing.RegisterRoute(nameof(VLoading), typeof(VLoading));
		Routing.RegisterRoute(nameof(VIUser), typeof(VIUser));
		Routing.RegisterRoute(nameof(VIRegAttention), typeof(VIRegAttention));
		Routing.RegisterRoute(nameof(VDetAttentionCurrent), typeof(VDetAttentionCurrent));

    }
}

