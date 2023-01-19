using Microsoft.Maui.Controls;

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

            var movementSpeed = 1;
            // detects which way its being pulled \\
            if (x > 12.75 && y > 12.75)
            {
                Console.WriteLine("South-East");
                Player.ScaleX = 1;
                Player.TranslationX = Player.TranslationX + movementSpeed;
                Player.TranslationY = Player.TranslationY + movementSpeed;
            }
            else if (x > 12.75 && y < -12.75)
            {
                Console.WriteLine("North-East");
                Player.ScaleX = 1;
                Player.TranslationX = Player.TranslationX + movementSpeed;
                Player.TranslationY = Player.TranslationY - movementSpeed;
            }
            else if (x < -12.75 && y > 12.75)
            {
                Console.WriteLine("South-West");
                Player.ScaleX = -1;
                Player.TranslationX = Player.TranslationX - movementSpeed;
                Player.TranslationY = Player.TranslationY + movementSpeed;
            }
            else if (x < -12.75 && y < -12.75)
            {
                Console.WriteLine("North-West");
                Player.ScaleX = -1;
                Player.TranslationX = Player.TranslationX - movementSpeed;
                Player.TranslationY = Player.TranslationY - movementSpeed;
            }
            else if (x > -12.75 && x < 12.75 && y > 20)
            {
                Console.WriteLine("South");
                Player.TranslationY = Player.TranslationY + movementSpeed;
            }
            else if (x > -12.75 && x < 12.75 && y < -20)
            {
                Console.WriteLine("North");
                Player.TranslationY = Player.TranslationY - movementSpeed;
            }
            else if (x > 20 && y > -12.75 && y < 12.75)
            {
                Console.WriteLine("East");
                Player.ScaleX = 1;
                Player.TranslationX = Player.TranslationX + movementSpeed;
            }
            else if (x < -20 && y > -12.75 && y < 12.75)
            {
                Player.ScaleX = -1;
                Console.WriteLine("West");
                Player.TranslationX = Player.TranslationX - movementSpeed;
            }
        }
        else if (e.StatusType == GestureStatus.Completed)
        {
            thumbstick.TranslationX = 0;
            thumbstick.TranslationY = 0;
        }
    }

}