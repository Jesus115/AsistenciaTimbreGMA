using JAbarcaPFinal.Services;

namespace JAbarcaPFinal.Vistas;

public partial class VILogin : ContentPage
{
	private readonly AuthService _authService; 
	public VILogin(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;

	}
    private async void Ingresar_Clicked(System.Object sender, System.EventArgs e)
    {
		_authService.Login();
		await Shell.Current.GoToAsync($"//{nameof(VIRegAttention)}");
    }
}
