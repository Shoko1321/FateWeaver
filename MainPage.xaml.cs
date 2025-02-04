namespace FateWeaver;

public partial class MainPage : ContentPage
{
	private int loadCharacterCounter = 0;
	public MainPage()
	{
		InitializeComponent();

		if (DeviceInfo.Platform == DevicePlatform.WinUI)
		{
			//Set initial windows size
			var window = Application.Current?.Windows?.FirstOrDefault();

			if (window != null)
			{
			window.Width = 400;
			window.Height = 550;
			}
		}
	}

	private async void OnNewCharacterClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new CharacterCreationPage());
	}
	private void OnLoadCharacterClicked(object sender, EventArgs e)
	{
		loadCharacterCounter++;

		if (loadCharacterCounter == 1)
			LoadBtn.Text = $"Clicked {loadCharacterCounter} time";
		else
			LoadBtn.Text = $"Clicked {loadCharacterCounter} times";

		SemanticScreenReader.Announce(LoadBtn.Text);
	}
}