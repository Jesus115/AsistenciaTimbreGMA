using JAbarcaPFinal.Services;

namespace JAbarcaPFinal.Vistas;

public partial class VIUser : ContentPage
{
    private readonly AuthService _authService;

    public VIUser(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
	}

    void btnLogout_Clicked(System.Object sender, System.EventArgs e)
    {
        _authService.Logout();
        Shell.Current.GoToAsync($"//{nameof(VILogin)}");
    }
}
