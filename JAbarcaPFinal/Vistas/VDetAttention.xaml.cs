using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using Android.Content.Res;
using Android.Media.TV;
using AndroidX.ConstraintLayout.Core;
using GoogleGson;
using JAbarcaPFinal.Models;
using Newtonsoft.Json;
using static Android.Webkit.WebStorage;

namespace JAbarcaPFinal.Vistas;

public partial class VDetAttention : ContentPage
{
	private const string _baseUrl = "http://25.46.184.61:8000/api/";
	private readonly HttpClient _httpClient = new HttpClient();
   
    public VDetAttention()
	{
        _httpClient = new HttpClient();

        InitializeComponent();
		ObtenerDatos();

    }
	public async void ObtenerDatos() {
        string endpoint = "asistencia/listar";
        // Configurar el encabezado de autorización con el token
        string token = Preferences.Get("AuthToken", "Error");
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        // Convertir el contenido a bytes
        var contentBytes = new StringContent($"{{\"email\": \"jj@gmail.com\", \"password\": \"12345678\"}}", Encoding.UTF8, "application/json");
        // Realizar la solicitud POST
        var response = await _httpClient.PostAsync(_baseUrl + endpoint, contentBytes);
        // Verificar si la solicitud fue exitosa
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            Console.WriteLine(data);
            // Leer la respuesta
            return ;
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            // Manejar el error de la solicitud
            return ;
        }


    }
}
