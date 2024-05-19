using System.IO;
using System.Text.Json;
using JAbarcaPFinal.Models;
using Microsoft.Maui.Controls;

namespace JAbarcaPFinal.Vistas;

public partial class VDetAttentionCurrent : ContentPage
{
    public VDetAttentionCurrent()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        string imageBytesBase64 = Preferences.Get("ImageBytes", null);
        string latitudeString = Preferences.Get("Latitude", null);
        string longitudeString = Preferences.Get("Longitude", null);

        if (!string.IsNullOrEmpty(imageBytesBase64) && !string.IsNullOrEmpty(latitudeString) && !string.IsNullOrEmpty(longitudeString))
        {
            var imageBytes = Convert.FromBase64String(imageBytesBase64);
            var imageStream = new MemoryStream(imageBytes);

            // Mostrar la imagen 
            imgFoto.Source = ImageSource.FromStream(() => imageStream);
            latlong.Text = latitudeString;
            lonlong.Text = longitudeString;
           
        }
    }



    async void obtenerLatLong_Clicked(System.Object sender, System.EventArgs e)
    {
        //Limpiar data
        Preferences.Remove("ImageBytes");
        Preferences.Remove("Latitude");
        Preferences.Remove("Longitude");
        //continuar
        await Navigation.PopAsync();
        await Shell.Current.GoToAsync($"//{nameof(VDetAttention)}");
    }
}
