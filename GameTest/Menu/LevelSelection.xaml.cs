namespace GameTest.Menu;

public partial class LevelSelection : ContentPage
{
    public LevelSelection()
    {
        InitializeComponent();
    }

    private async void Loader_Pressed(object sender, EventArgs e)
    {
        if (sender == Intro)
        {
            await Navigation.PushAsync(new Levels.LevelLoader(cutscene: true));
        }
        else
        {
            await Navigation.PushAsync(new Levels.LevelLoader());
        }
    }
    private async void Editor_Pressed(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LevelEditor");
    }
}