namespace GameTest.Menu;

public partial class LevelSelection : ContentPage
{
	public LevelSelection()
	{
		InitializeComponent();
	}

    private async void Loader_Pressed(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LevelLoader");
    }
    private async void Editor_Pressed(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LevelEditor");
    }
}