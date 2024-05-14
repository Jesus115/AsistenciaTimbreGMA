using System.IO;

namespace JAbarcaPFinal.Vistas;

public partial class VDetAttentionCurrent : ContentPage
{
	private System.IO.Stream _img;
	private string _tipoRegistro;
    IGeolocation geolocation;

    public VDetAttentionCurrent(System.IO.Stream img,string tipoRegistro )
	{
		InitializeComponent();
		_img = img;
		_tipoRegistro = tipoRegistro;
        imgFoto.Source = ImageSource.FromStream(() => _img);
    }

    async void obtenerLatLong_Clicked(System.Object sender, System.EventArgs e)
    {
        var location = await Geolocation.GetLocationAsync();
        if (location != null)
        {
            double latitude = location.Latitude;
            double longitude = location.Longitude;
            latlong.Text = latitude.ToString();
            // Haz algo con la ubicación obtenida
        }
        else
        {
            latlong.Text = "error";
            // Manejar caso en que no se pueda obtener la ubicación
        }
    }


}
