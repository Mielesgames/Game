using Microsoft.Maui.Controls;

namespace GameTest.Levels;

public partial class LevelLoader : ContentPage
{
	public LevelLoader()
	{
		InitializeComponent();
	}
    private string playerFacingDirection = "East";
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
                playerFacingDirection = "South-East";
                Player.ScaleX = 1;
                map.TranslationX = map.TranslationX - movementSpeed;
                map.TranslationY = map.TranslationY - movementSpeed;
            }
            else if (x > 12.75 && y < -12.75)
            {
                playerFacingDirection = "North-East";
                Player.ScaleX = 1;
                map.TranslationX = map.TranslationX - movementSpeed;
                map.TranslationY = map.TranslationY + movementSpeed;
            }
            else if (x < -12.75 && y > 12.75)
            {
                playerFacingDirection = "South-West";
                Player.ScaleX = -1;
                map.TranslationX = map.TranslationX + movementSpeed;
                map.TranslationY = map.TranslationY - movementSpeed;
            }
            else if (x < -12.75 && y < -12.75)
            {
                playerFacingDirection = "North-West";
                Player.ScaleX = -1;
                map.TranslationX = map.TranslationX + movementSpeed;
                map.TranslationY = map.TranslationY + movementSpeed;
            }
            else if (x > -12.75 && x < 12.75 && y > 20)
            {
                playerFacingDirection = "South";
                map.TranslationY = map.TranslationY - movementSpeed;
            }
            else if (x > -12.75 && x < 12.75 && y < -20)
            {
                playerFacingDirection = "North";
                map.TranslationY = map.TranslationY + movementSpeed;
            }
            else if (x > 20 && y > -12.75 && y < 12.75)
            {
                playerFacingDirection = "East";
                Player.ScaleX = 1;
                map.TranslationX = map.TranslationX - movementSpeed;
            }
            else if (x < -20 && y > -12.75 && y < 12.75)
            {
                Player.ScaleX = -1;
                playerFacingDirection = "West";
                map.TranslationX = map.TranslationX + movementSpeed;
            }
        }
        else if (e.StatusType == GestureStatus.Completed)
        {
            thumbstick.TranslationX = 0;
            thumbstick.TranslationY = 0;
        }
    }
    private async void Bullet(string Direction)
    {
        Frame bullet = new Frame { BackgroundColor = Colors.Red };
        bullet.Parent = Player;
        bullet.HeightRequest = 10;
        bullet.WidthRequest = 10;
        bullet.ZIndex = 200;
        bullet.IsClippedToBounds = false;
        bullet.IsVisible = true;
        Bullets.Children.Add(bullet);
        bullet.TranslationX = Player.TranslationX;
        bullet.TranslationY = Player.TranslationY;
        
        for (int i = 0; i < 100; i++)
        {
            await Task.Delay(50);
            if (Direction.Contains("North"))
            {
                bullet.TranslationY -= 10;
            }
            if (Direction.Contains("South"))
            {
                bullet.TranslationY += 10;
            }
            if (Direction.Contains("East"))
            {
                bullet.TranslationX += 10;
            }
            if (Direction.Contains("West"))
            {
                bullet.TranslationX -= 10;
            }
        }
        Bullets.Children.Remove(bullet);
        
    }
    private void Shoot(object sender, EventArgs e)
    {
        Bullet(playerFacingDirection);
    }
}