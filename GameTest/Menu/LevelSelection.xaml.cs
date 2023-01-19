namespace GameTest.Menu;

public partial class LevelSelection : ContentPage
{
	public LevelSelection()
	{
		InitializeComponent();
	}

    private async void Button_Pressed(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LevelLoader");
    }
}