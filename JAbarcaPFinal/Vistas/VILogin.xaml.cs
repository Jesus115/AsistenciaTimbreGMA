using System.Collections.ObjectModel;
using System.Text;
using JAbarcaPFinal.Models;
using JAbarcaPFinal.Services;
using Newtonsoft.Json;

namespace JAbarcaPFinal.Vistas;

public partial class VILogin : ContentPage
{
	private readonly AuthService _authService;
    private const string url = "http://25.46.184.61:8000/api/";
    private readonly HttpClient client = new HttpClient();
    public VILogin(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;

	}
    private async void Ingresar_Clicked(System.Object sender, System.EventArgs e)
    {
        string endpoint = "login";

        string email = txtUsuario.Text;
        string clave = txtClave.Text;
        //string valor = Preferences.Get("AuthToken", "Error");
        var content = new StringContent($"{{\"email\": \"{email}\", \"password\": \"{clave}\"}}", Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url + endpoint, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseApi = JsonConvert.DeserializeObject<ApiLogin>(responseContent);
        Preferences.Set("AuthToken", responseApi.Token);
        string valor = Preferences.Get("AuthToken", "Error");
        if (responseApi.StatusCode == 200)
        {
            _authService.Login(valor);
            await Shell.Current.GoToAsync($"//{nameof(VIRegAttention)}");

        }
        else {
            _ = DisplayAlert("Alerta", "Error en usuario o Contarsena", "Ok");
        }
        //_authService.Login("aqui token");
    }
}
