namespace GameTest.Menu;

public partial class MainMenu : ContentPage
{
    public MainMenu()
    {
        InitializeComponent();
        TitleAnimation();
        WelcomeMessage.Text = $"Welcome {Preferences.Get("Username", "player")}";
    }
    private async void TitleAnimation()
    {
        var scaling = Title.Scale * 0.3;

        while (true)
        {
            await Title.ScaleTo(Title.Scale / scaling, 1000, Easing.CubicInOut);
            await Title.ScaleTo(Title.Scale * scaling, 1000, Easing.CubicInOut);
        }
    }

    private async void PlayButton_Clicked(object sender, EventArgs e)
    {
        await PlayButton.ScaleTo(0.8);
        await PlayButton.ScaleTo(1);
        for (int i = 0; i < 3; i++)
        {
            await PlayButton.RotateTo(15, 70, Easing.CubicInOut);
            await PlayButton.RotateTo(-15, 70, Easing.CubicInOut);
        }
        try
        {
            await Shell.Current.GoToAsync("///LevelPicker");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "ok");
        }
        PlayButton.Rotation = 0;
    }
}