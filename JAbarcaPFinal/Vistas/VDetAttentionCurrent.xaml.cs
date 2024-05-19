using System.IO;

namespace JAbarcaPFinal.Vistas;

public partial class VDetAttentionCurrent : ContentPage
{
	private System.IO.Stream _img;
    private double _lat;
    private double _long;
    private ImageSource _img2;

    public VDetAttentionCurrent(System.IO.Stream img,double lat, double longi )
	{
		InitializeComponent();
		_img = img;
        _lat = lat;
        _long = longi;
        imgFoto.Source = ImageSource.FromStream(() => _img);
        latlong.Text = _lat.ToString();
        lonlong.Text = _long.ToString();
    }
    public VDetAttentionCurrent()
    {
        latlong.Text = "";
        lonlong.Text = "";
        imgFoto.Source = "";
        InitializeComponent();
    }



    async void obtenerLatLong_Clicked(System.Object sender, System.EventArgs e)
    {
        await Navigation.PopAsync();
        await Shell.Current.GoToAsync($"//{nameof(VDetAttention)}");


    }


}
