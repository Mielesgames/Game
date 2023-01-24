namespace GameTest.Levels;

public partial class LevelEditor : ContentPage
{
	public LevelEditor()
	{
		InitializeComponent();
	}
	public int MapHeight = 0;
	public int MapWidth = 0;
	public int OldHeight = 0;
	public int OldWidth = 0;
    public string OccupiedSpots = "";
    private async void Button_Clicked(object sender, EventArgs e)
    {
		try
		{
			OldHeight = MapHeight;
			OldWidth = MapWidth;
            MapHeight = Int32.Parse(HeightEntry.Text);
			MapWidth = Int32.Parse(WidthEntry.Text);
			if (MapHeight <= 0 || MapWidth <= 0)
			{
				MapHeight = OldHeight;
				MapWidth = OldWidth;
				await DisplayAlert("Something went wrong", "Please enter a number higher then 0", "ok");
				return;
			}
			var HeightDifference = MapHeight - OldHeight;
			var WidthDifference = MapWidth - OldWidth;
            // adds row \\
			if (HeightDifference > 0)
			{
                for (var i = 0; i < HeightDifference; i++)
                {
                    Map.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    for (int column = 0; column != MapHeight; column++)
                    {
                        // Check if there is already a frame on the location \\
                        var row = Map.RowDefinitions.Count - 1;
                        if (!OccupiedSpots.Contains($"({row},{column})"))
                        {
                            // Places the Frame if there isn't already one \\
                            Frame newFrame = new Frame { BackgroundColor = Colors.Red };
                            newFrame.CornerRadius = 0;
                            Map.Children.Add(newFrame);
                            Grid.SetRow(newFrame, row);
                            Grid.SetColumn(newFrame, column);
                            OccupiedSpots += $"({row},{column})";
                        }
                    }
                }
            }
            // removes frame and grid row \\
            else if (HeightDifference < 0)
            {
                var number = 0;
                for (var i = 0; i > HeightDifference; i--)
                {
                    Map.RowDefinitions.RemoveAt(Map.RowDefinitions.Count - 1);
                    var lastRow = Map.RowDefinitions.Count - 1;
                    for (int c = 0; c != MapWidth; c++)
                    {
                        for (int j = Map.Children.Count - 1; j >= 0; j--)
                        {
                            var frame = (Frame)Map.Children[j];
                            if (Grid.GetRow(frame) == lastRow)
                            {
                                frame.Parent = null;
                                Map.Children.Remove(frame);
                                OccupiedSpots = OccupiedSpots.Replace($"({lastRow - 1},{number})", "");
                                number++;
                            }
                        }
                    }
                }
            }
            // adds column and frame \\
            if (WidthDifference > 0)
            {
                for (var i = 0; i < WidthDifference; i++)
                {
                    Map.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    for (int c = 0; c != MapHeight; c++)
                    {
                        var column = Map.ColumnDefinitions.Count - 1;
                        if (!OccupiedSpots.Contains($"({c},{column})"))
                        {
                            Frame newFrame = new Frame { BackgroundColor = Colors.Red };
                            Map.Children.Add(newFrame);
                            Grid.SetColumn(newFrame, column);
                            Grid.SetRow(newFrame, c);
                            OccupiedSpots += $"({c},{column})";
                        }
                    }
                }
            }
            // removes column and frame \\
            else if (WidthDifference < 0)
            {
                for (var i = 0; i > WidthDifference; i--)
                {
                    Map.ColumnDefinitions.RemoveAt(Map.ColumnDefinitions.Count - 1);
                    var lastColumn = Map.ColumnDefinitions.Count - 1;
                    var number = 0;
                    for (int c = 0; c != MapHeight; c++)
                    {
                        for (int j = Map.Children.Count - 1; j >= 0; j--)
                        {
                            var frame = (Frame)Map.Children[j];
                            if (Grid.GetColumn(frame) == lastColumn)
                            {
                                string position = $"({number},{lastColumn - 1})";
                                frame.Parent = null;
                                Map.Children.Remove(frame);
                                OccupiedSpots = OccupiedSpots.Replace(position, "");
                                number++;
                            }
                        }
                    }
                }
            }
        }
		catch(Exception ex)
		{
            MapHeight = OldHeight;
            MapWidth = OldWidth;
            var answer = await DisplayAlert("Something went wrong", "Are you sure you entered numbers?", "yes", "no");
			if (answer)
			{
				var copyToClipboard = await DisplayAlert("Error", $"Please send this error message to @Mielesgames on twitter: {ex.Message}","Copy to clipboard", "Close");
				if (copyToClipboard)
				{
					await Clipboard.Default.SetTextAsync(ex.Message);
				}
			}
		}
    }
}