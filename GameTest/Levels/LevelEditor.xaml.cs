namespace GameTest.Levels;

public partial class LevelEditor : ContentPage
{
    public LevelEditor()
    {
        InitializeComponent();
    }
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (HasPickedSize)
        {
            var ClearLayout = await DisplayAlert("Clear layout?", "The previous layout is still loaded, do you want to keep using it or make a new one?", "Clear layout", "Keep layout");
            if (ClearLayout)
            {
                Map.ColumnDefinitions.Clear();
                Map.RowDefinitions.Clear();
                Map.Children.Clear();
                Pick_Size();
            }
        }
        else
        {
            Pick_Size();
        }
    }
    public bool HasUsedLevelEditor = false;
    public int MapHeight = 0;
    public int MapWidth = 0;
    public bool HasPickedSize = false;
    private async void Pick_Size()
    {
        var mapSize = await DisplayPromptAsync("Map-Size", "Choose a size for your map, don't forget to separate the numbers with a comma !", placeholder: "Height,Width");
        if (mapSize != null)
        {
            try
            {
                Map.Children.Clear();
                string[] numbers = mapSize.Split(',');
                int counter = 0;
                // checks the numbers you filled in (because of the "try, catch" it won't crash if you entered something other then a number) \\
                foreach (var number in numbers)
                {
                    counter++;
                    if (counter == 1)
                    {
                        MapHeight = Int32.Parse(number.Replace("[", "").Replace("]", "").Trim());
                    }
                    else if (counter == 2)
                    {
                        MapWidth = Int32.Parse(number.Replace("[", "").Replace("]", "").Trim());
                    }
                    else
                    {
                        await DisplayAlert("Error", "You have entered more then 2 numbers, only the first 2 will be used", "ok");
                        break;
                    }
                }
                for (int currentRow = 0; currentRow < MapHeight; currentRow++)
                {
                    Map.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Star) });
                    for (int currentColumn = 0; currentColumn < MapWidth; currentColumn++)
                    {
                        Map.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
                        Frame newFrame = new Frame { BackgroundColor = Colors.Red };
                        Map.Children.Add(newFrame);
                        Grid.SetRow(newFrame, currentRow);
                        Grid.SetColumn(newFrame, currentColumn);
                    }
                }
                HasPickedSize = true;
            }
            catch (Exception ex)
            {
                var wantsToContinue = await DisplayAlert("Error", ex.Message, "Ok", "Exit Level Editor");
                if (wantsToContinue)
                {
                    Pick_Size();
                }
                else
                {
                    if (HasPickedSize)
                    {
                        await DisplayAlert("Error", "an error occurred", "ok");
                    }
                    else
                    {
                        await Shell.Current.GoToAsync("///MainPage");
                    }
                }
            }
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }
}