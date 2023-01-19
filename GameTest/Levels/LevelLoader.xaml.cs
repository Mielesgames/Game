namespace GameTest.Levels;

public partial class LevelLoader : ContentPage
{
	public LevelLoader()
	{
		InitializeComponent();
	}
    private void OnThumbstickPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if (e.StatusType == GestureStatus.Running)
        {
            double x = e.TotalX, y = e.TotalY;
            thumbstick.TranslationX = x;
            thumbstick.TranslationY = y;
            // detects which way its being pulled \\
            if (x > 12.75 && y > 12.75)
            {
                Console.WriteLine("South-East");
            }
            else if (x > 12.75 && y < -12.75)
            {
                Console.WriteLine("North-East");
            }
            else if (x < -12.75 && y > 12.75)
            {
                Console.WriteLine("South-West");
            }
            else if (x < -12.75 && y < -12.75)
            {
                Console.WriteLine("North-West");
            }
            else if (x > -12.75 && x < 12.75 && y > 0)
            {
                Console.WriteLine("South");
            }
            else if (x > -12.75 && x < 12.75 && y < 0)
            {
                Console.WriteLine("North");
            }
            else if (x > 0 && y > -12.75 && y < 12.75)
            {
                Console.WriteLine("East");
            }
            else if (x < 0 && y > -12.75 && y < 12.75)
            {
                Console.WriteLine("West");
            }
        }
        else if (e.StatusType == GestureStatus.Completed)
        {
            thumbstick.TranslationX = 0;
            thumbstick.TranslationY = 0;
        }
    }

}