using JAbarcaPFinal.Services;

namespace JAbarcaPFinal.Vistas;

public partial class VLoading : ContentPage
{
	private readonly AuthService _authService;

	public VLoading(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
	}
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		if (await _authService.IsAuthenticateAsync())
		{
            // Usuario logeado
            //redireccion a inicio
            await Shell.Current.GoToAsync($"//{nameof(VIRegAttention)}");

        }
        else {
			//Usuario no logeado
			//redirige a login 
			await Shell.Current.GoToAsync($"//{nameof(VILogin)}");

        }
    }
}
