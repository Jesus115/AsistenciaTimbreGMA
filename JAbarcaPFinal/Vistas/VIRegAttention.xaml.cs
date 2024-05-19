namespace JAbarcaPFinal.Vistas;
using System.Diagnostics;
using System.Net;
using System.Text;

public partial class VIRegAttention : ContentPage
{
    private const string _baseUrl = "http://192.168.1.4:8000/api/";
    private readonly HttpClient _httpClient = new HttpClient();
    public VIRegAttention()
	{
		InitializeComponent();
        _httpClient = new HttpClient();

    }
    private async Task<string> EnviarDataApi(string endpoint, System.Net.Http.HttpContent data)
    {
        string token = Preferences.Get("AuthToken", "Error");
        if (token == "Error")
        {
            await Shell.Current.DisplayAlert("Error", "Token no encontrado", "Cerrar");
            return "Token no encontrado";
        }
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PostAsync(_baseUrl + endpoint, data);
        if (response.IsSuccessStatusCode)
        {
            await Shell.Current.DisplayAlert("Exito", "Sus Datos Fueron Guardados Exitosamente.", "Cerrar");

            return await response.Content.ReadAsStringAsync();

        }
        else
        {
            // Manejar errores de la respuesta
            string errorMessage = await response.Content.ReadAsStringAsync();
            return $"Error en la respuesta del servidor: {errorMessage}";
        }
    }
    private async Task<string> ConvertImageToBase64(Stream imageStream)
    {
        using (var memoryStream = new MemoryStream())
        {
            await imageStream.CopyToAsync(memoryStream);
            byte[] imageBytes = memoryStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }

    private async void TomarFoto_Clicked(System.Object sender, System.EventArgs e)
    {
        string endpoint = "asistencia/crear";
        
        if (MediaPicker.Default.IsCaptureSupported) {
            //tomar pic or capture photo
            FileResult takePic = await MediaPicker.Default.CapturePhotoAsync();
			//cargar foto
			if (takePic != null) {
                var memoriaStream = await takePic.OpenReadAsync();
                //imgFoto.Source = ImageSource.FromStream(() => memoriaStream);
                //guardar imagen
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, takePic.FileName);
				using Stream sourceStream = await takePic.OpenReadAsync();
				using FileStream localFileStream = File.OpenWrite(localFilePath);
				await sourceStream.CopyToAsync(localFileStream);
                
                var location = await Geolocation.GetLocationAsync();
                if (location != null)
                {
                    double latitude = location.Latitude;
                    double longitude = location.Longitude;
                    string imageBase64 = await ConvertImageToBase64(memoriaStream);

                    var contentBytes = new StringContent($"{{\"tipo\": \"1\", \"longitud\": \"{longitude}\", \"latitud\": \"{latitude}\", \"foto\": \"{imageBase64}\"}}", Encoding.UTF8, "application/json");
                    string result = await EnviarDataApi(endpoint, contentBytes);

                    // envio ubicación obtenida
                    await Navigation.PushAsync(new VDetAttentionCurrent(memoriaStream, latitude, longitude));

                }
                else
                {
                    // Manejar caso en que no se pueda obtener la ubicación
                    await Shell.Current.DisplayAlert("Error", "Necesita Dar Permisos A La Ubicacion De Su Telefono", "Cerrar");

                }

            }
        }
		else {
			await Shell.Current.DisplayAlert("Error", "Tu Dispositivo No Es Compatible", "Cerrar");
        }

    }

}
