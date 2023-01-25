using Microsoft.Maui.Controls;

namespace GameTest.Levels;

public partial class LevelLoader : ContentPage
{
	public LevelLoader(bool cutscene = false, string mapImage = "snas.png", int levelID = 0)
	{
		InitializeComponent();
        IsACutscene = cutscene;
        map.Source = "snas.png";
        if (IsACutscene)
        {
            Player.IsVisible = false;
            thumbstick.IsVisible = false;
            thumbstickBackground.IsVisible = false;
            ShootingEnabled = false;
            if (levelID == 0)
            {
                _ = Cutscene1();
            }
        }
        if (ShootingEnabled)
        {
            ShootButton.IsVisible = true;
        }
        else
        {
            ShootButton.IsVisible = false;
        }
	}
    private async Task Cutscene1()
    {
        await Typewrite("hello",1000);
    }

    private bool ShootingEnabled = true;
    private bool IsACutscene = false;
    private int walkCycleTimer = 0;
    private readonly int movementSpeed = 1; // the walkspeed \\
    private readonly int stepSpeed = 5; // the higher the number the slower the animation plays\\
    private string playerFacingDirection = "East";
    private double initialX, initialY;
    private async void OnThumbstickPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if (e.StatusType == GestureStatus.Started)
        {
            initialX = thumbstick.TranslationX;
            initialY = thumbstick.TranslationY;
        }
        else if (e.StatusType == GestureStatus.Running)
        {
            double x = e.TotalX, y = e.TotalY;
            await thumbstick.TranslateTo(x, y, 15, Easing.CubicOut);
            //thumbstick.TranslationX = initialX + e.TotalX; ;
            //thumbstick.TranslationY = initialY + e.TotalY;
            walkCycleTimer++;
            if (walkCycleTimer == stepSpeed)
            {
                Player.Source = "first.png";
            }
            else if (walkCycleTimer == stepSpeed * 2)
            {
                Player.Source = "second.png";
            }
            else if (walkCycleTimer == stepSpeed * 3)
            {
                Player.Source = "third.png";
            }
            else if (walkCycleTimer == stepSpeed * 4)
            {
                Player.Source = "fourth.png";
            }
            else if (walkCycleTimer == stepSpeed * 5)
            {
                Player.Source = "fifth.png";
            }
            else if (walkCycleTimer == stepSpeed * 6)
            {
                Player.Source = "last_frame.png";
                walkCycleTimer = 0;
            }
            // detects which way its being pulled \\
            await Task.Delay(100);
            if (x > 12.75 && y > 12.75)
            {
                playerFacingDirection = "South-East";
                Player.ScaleX = -1;
                map.TranslationX = map.TranslationX - movementSpeed;
                map.TranslationY = map.TranslationY - movementSpeed;
            }
            else if (x > 12.75 && y < -12.75)
            {
                playerFacingDirection = "North-East";
                Player.ScaleX = -1;
                map.TranslationX = map.TranslationX - movementSpeed;
                map.TranslationY = map.TranslationY + movementSpeed;
            }
            else if (x < -12.75 && y > 12.75)
            {
                playerFacingDirection = "South-West";
                Player.ScaleX = 1;
                map.TranslationX = map.TranslationX + movementSpeed;
                map.TranslationY = map.TranslationY - movementSpeed;
            }
            else if (x < -12.75 && y < -12.75)
            {
                playerFacingDirection = "North-West";
                Player.ScaleX = 1;
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
                Player.ScaleX = -1;
                map.TranslationX = map.TranslationX - movementSpeed;
            }
            else if (x < -20 && y > -12.75 && y < 12.75)
            {
                Player.ScaleX = 1;
                playerFacingDirection = "West";
                map.TranslationX = map.TranslationX + movementSpeed;
            }
            else
            {
                Player.Source = "idle.png";
                walkCycleTimer = 0;
            }
        }
        else if (e.StatusType == GestureStatus.Completed)
        {
            await Task.Delay(20);
            walkCycleTimer = 0;
            Player.Source = "idle.png";
            thumbstick.TranslationX = 0;
            thumbstick.TranslationY = 0;
        }
    }
    private Task Typewrite(string message, int delay = 60)
    {
        DialogueBox.Text = "";
        DialogueBox.IsVisible = true;
        for (int i = 0; i < message.Length; i++)
        {
            DialogueBox.Text = DialogueBox.Text + message[i].ToString();
            Thread.Sleep(delay);
        }
        return null;
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
    private async void Shoot(object sender, EventArgs e)
    {
        Player.Source = "shoot_first.png";
        Bullet(playerFacingDirection);
        await Task.Delay(50);
        Player.Source = "shoot_final.png";
        await Task.Delay(50);
        Player.Source = "hold_gun.png";
    }
}