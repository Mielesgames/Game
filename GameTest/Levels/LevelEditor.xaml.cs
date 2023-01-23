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
			if (HeightDifference > 0)
			{
                for (var i = 0; i < HeightDifference; i++)
                {
                    Map.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                }
            }
			else if (HeightDifference < 0)
			{
                for (var i = 0; i > HeightDifference; i--)
                {
                    Map.RowDefinitions.RemoveAt(Map.RowDefinitions.Count - 1);
                    Map.RowDefinitions.RemoveAt(Map.RowDefinitions.Count - 1);
                }
            }

            if (WidthDifference > 0)
            {
                for (var i = 0; i < WidthDifference; i++)
                {
                    Map.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }
            }
            else if (WidthDifference < 0)
            {
                for (var i = 0; i > WidthDifference; i--)
                {
                    Map.ColumnDefinitions.RemoveAt(Map.ColumnDefinitions.Count - 1);
					// Remove the last row and its contents from the Map
					Map.Clear();
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