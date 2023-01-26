namespace GameTest.Menu;

public partial class LevelSelection : ContentPage
{
    public LevelSelection()
    {
        InitializeComponent();
        _ = CheckLevelCompletionState();
    }
    private bool HasCompletedIntro = Preferences.Get("HasCompletedIntro", false);
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _ = CheckLevelCompletionState();
    }
    private async Task CheckLevelCompletionState()
    {
        if (Preferences.Get("HasCompletedIntro", false))
        {
            level1.IsVisible = true;
            if (Preferences.Get("HasCompletedIntro", false) != HasCompletedIntro)
            {
                level1.Opacity = 0.1;
                for (int i = 0; i < 9; i++)
                {
                    level1.Opacity += 0.1;
                }
                level1.BackgroundColor = Colors.Gold;
                level1.BorderColor = Colors.Yellow;
                await DisplayAlert("Congratulations!", "You have unlocked your first level!", "ok");
                HasCompletedIntro = true;
            }
            level1.IsEnabled = true;
        }
    }
        private async void Loader_Pressed(object sender, EventArgs e)
    {
        if (sender == Intro)
        {
            await Navigation.PushAsync(new Levels.LevelLoader(cutscene: true));
        }
        else if (sender == level1)
        {
            await Navigation.PushAsync(new Levels.LevelLoader(levelID: 0));
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