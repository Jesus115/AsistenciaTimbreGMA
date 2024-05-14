namespace JAbarcaPFinal.Vistas;
using System.Diagnostics;

public partial class VIRegAttention : ContentPage
{
	public VIRegAttention()
	{
		InitializeComponent();
	}

    private async void TomarFoto_Clicked(System.Object sender, System.EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported) {
            //take pic or capture photo
            FileResult takePic = await MediaPicker.Default.CapturePhotoAsync();
			//cargar foto
            //FileResult takePic = await MediaPicker.Default.PickPhotoAsync();
			if (takePic != null) {
                var memoriaStream = await takePic.OpenReadAsync();
                imgFoto.Source = ImageSource.FromStream(() => memoriaStream);
                //guardar imagen
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, takePic.FileName);
				using Stream sourceStream = await takePic.OpenReadAsync();
				using FileStream localFileStream = File.OpenWrite(localFilePath);
				await sourceStream.CopyToAsync(localFileStream);
                //await Shell.Current.DisplayAlert("OOPS", localFileStream.Name, "Cerrar");
                //await Shell.Current.GoToAsync($"//{nameof(VDetAttentionCurrent)}");si vale
                //await Shell.Current.GoToAsync($"//{nameof(VDetAttentionCurrent)}?imgFoto={memoriaStream}");
                var authToken = SecureStorage.GetAsync("AuthToken");
                Debug.WriteLine(authToken);

                await Navigation.PushAsync(new VDetAttentionCurrent(memoriaStream,"hola"));

            }
        }
		else {
			await Shell.Current.DisplayAlert("OOPS", "Tu dispositivo no es compatible", "Cerrar");
        }

    }

}
