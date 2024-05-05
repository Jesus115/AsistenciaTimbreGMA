using System.IO;

namespace JAbarcaPFinal.Vistas;

public partial class VDetAttentionCurrent : ContentPage
{
	private System.IO.Stream _img;
	private string _tipoRegistro;
	public VDetAttentionCurrent(System.IO.Stream img,string tipoRegistro)
	{
		InitializeComponent();
		_img = img;
		_tipoRegistro = tipoRegistro;
        imgFoto.Source = ImageSource.FromStream(() => _img);
    }

    
}
