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

    }

    protected   override void OnAppearing()
    {
        base.OnAppearing();
        ObtenerDatos();
    }
    public async void ObtenerDatos() {
        LimpiarGrid();

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
    private void LimpiarGrid()
    {
        grdAtenciones.Children.Clear();
    }
        private void AddAtentionToGrid(registroAsistencia reg)
    {
        // Create new row for the employee
        int newRowIndex = grdAtenciones.RowDefinitions.Count;
        grdAtenciones.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        var lbLatitud = new Label { Text = reg.latitud + " ", AnchorX = 10 };
        Grid.SetColumn(lbLatitud, 0); 

        var lbLongitud = new Label { Text = reg.longitud + " ", AnchorX = 10 };
        Grid.SetColumn(lbLongitud, 1); 

        var lbTipo = new Label { Text = reg.nombreCat + " ", AnchorX = 10 };
        Grid.SetColumn(lbTipo, 2); 

        var lbFecha = new Label { Text = reg.fecha.ToString() + " ", AnchorX = 10 };
        Grid.SetColumn(lbFecha, 3); 
        grdAtenciones.Add(lbLatitud, 0, newRowIndex);
        grdAtenciones.Add(lbLongitud, 1, newRowIndex);
        grdAtenciones.Add(lbTipo, 2, newRowIndex);
        grdAtenciones.Add(lbFecha, 3, newRowIndex);
    }
    /*private void AddAtentionToGrid(registroAsistencia reg)
    {
        // Create new row for the employee
        int newRowIndex = grdAtenciones.RowDefinitions.Count;
        grdAtenciones.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        var lbLatitud = new Label { Text = reg.latitud + " ", VerticalOptions = LayoutOptions.Center };
        var lbLongitud = new Label { Text = reg.longitud + " ", VerticalOptions = LayoutOptions.Center };
        var lbTipo = new Label { Text = reg.nombreCat + " ", VerticalOptions = LayoutOptions.Center };
        var lbFecha = new Label { Text = reg.fecha.ToString() + " ", VerticalOptions = LayoutOptions.Center };

        // Convert the image byte array to ImageSource
        var imageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(reg.foto)));

        // Create a Frame to hold the image
        var imageFrame = new Frame
        {
            CornerRadius = 50, // Adjust the corner radius as needed
            HeightRequest = 100, // Adjust the height as needed
            WidthRequest = 100, // Adjust the width as needed
            Padding = 0,
            HasShadow = false,
            Content = new Image
            {
                Source = imageSource,
                Aspect = Aspect.AspectFill,
                HeightRequest = 40,
                WidthRequest = 40
            }
        };

        // Add elements to the grid in the new row
        grdAtenciones.Add(lbLatitud, 0, newRowIndex);
        grdAtenciones.Add(lbLongitud, 1, newRowIndex);
        grdAtenciones.Add(lbTipo, 2, newRowIndex);
        grdAtenciones.Add(lbFecha, 3, newRowIndex);
        grdAtenciones.Add(imageFrame, 4, newRowIndex);
    }*/



    async void btnAgregarRegistro_Clicked(System.Object sender, System.EventArgs e)
    {
        //await Navigation.PushAsync(new VIRegAttention());
        await Shell.Current.GoToAsync($"//{nameof(VIRegAttention)}");


    }
}
