using System.Collections.ObjectModel;
using System.IO;
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
	private const string _baseUrl = "http://192.168.1.4:8000/api/";
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
            var tempObject = JsonConvert.DeserializeObject<dynamic>(data);
            List<registroAsistencia> registro = tempObject.data.ToObject<List<registroAsistencia>>();
            foreach (var reg in registro)
            {
                AddAtentionToGrid(reg);
            }
            // Leer la respuesta
            //return;
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            // Manejar el error de la solicitud
            return ;
        }


    }
    private void AddAtentionToGrid(registroAsistencia reg)
    {
        // Create new row for the employee
        int newRowIndex = grdAtenciones.RowDefinitions.Count;
        grdAtenciones.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        var lbLatitud = new Label { Text = reg.latitud + " ", AnchorX = 10 };
        Grid.SetColumn(lbLatitud, 0); // Set column index to 0

        var lbLongitud = new Label { Text = reg.longitud + " ", AnchorX = 10 };
        Grid.SetColumn(lbLongitud, 1); // Set column index to 0

        var lbTipo = new Label { Text = reg.nombreCat + " ", AnchorX = 10 };
        Grid.SetColumn(lbTipo, 2); // Set column index to 1

        var lbFecha = new Label { Text = reg.fecha.ToString() + " ", AnchorX = 10 };
        Grid.SetColumn(lbFecha, 3); // Set column index to 1
        // Add labels to the grid in the new row
        grdAtenciones.Add(lbLatitud, 0, newRowIndex);
        grdAtenciones.Add(lbLongitud, 1, newRowIndex);
        grdAtenciones.Add(lbTipo, 2, newRowIndex);
        grdAtenciones.Add(lbFecha, 3, newRowIndex);
    }

    async void btnAgregarRegistro_Clicked(System.Object sender, System.EventArgs e)
    {
        //await Navigation.PushAsync(new VIRegAttention());
        await Shell.Current.GoToAsync($"//{nameof(VIRegAttention)}");


    }
}
