namespace GameTest.Levels;

public partial class LevelLoader : ContentPage
{
    public LevelLoader(bool cutscene = false, string mapImage = "snas.png", int levelID = 0)
    {
        InitializeComponent();
        IsACutscene = cutscene;
        if (IsACutscene)
        {
            Player.IsVisible = false;
            thumbstick.IsVisible = false;
            thumbstickBackground.IsVisible = false;
            ShootingEnabled = false;
            if (levelID == 0)
            {
                map.IsVisible = false;
                _ = Cutscene1();
            }
        }
        else
        {
            if (levelID == 0)
            {
                _ = Level1();
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
    // Variables \\
            // walking \\
    private int walkCycleTimer = 0;                         // timer
    private readonly int movementSpeed = 1;            // walkspeed
    private int stepSpeed = 5;                              // walkcycle delay
    private string playerFacingDirection = "West";     // facing
            // other\\
    private string MainCharacterName = "Amogus";
    private bool ShootingEnabled = true;
    private bool IsACutscene = false;
    private int MovementMode = 0; // 0 is map moves, 1 is player moves 2 is disabled \\

    // Cutscenes \\
    private async Task Cutscene1()
    {
        try
        {
            await Task.Delay(2000);
            _ = Typewrite("Hello", 150);
            Player.ScaleY = 3;
            Player.ScaleX = -3;
            Player.IsVisible = true;
            Player.Opacity = 0.1;
            await Player.FadeTo(1,700);
            await Task.Delay(1000);
            await Typewrite($"I am {MainCharacterName}...", 100);
            await Typewrite("People have known me as the bad guy for years...", 60);
            await Typewrite("Everyone kept making jokes about me...", 60);
            await Typewrite("Today I decided to improve my reputation by being the hero for once");
            await Typewrite("but...", 60, 1000, true);
            await Typewrite("I can't do that without your help.");
            await Typewrite("But first I have to know your name.");
            await Typewrite("What is your name?");
            var username = await DisplayPromptAsync("Username", "What is your username?");
            if (username == null)
            {
                username = "Undefined Username";
            }
            else if (username.ToLower() =="sus" || username.ToLower().Contains("sussy"))
            {
                await Typewrite($"{username}...?", 200, 1000);
                Player.Source = "hold_gun.png";
                await Typewrite("Change it...|||| NOW!");
                await Typewrite("username = await DisplayPromptAsync(\"CHANGE\", \"IT\");");
                username = await DisplayPromptAsync("CHANGE", "IT");
                if (username.ToLower().Contains("sus") || username.ToLower() == "no")
                {
                    await Typewrite("you...",200, waitTime:100);
                    await Typewrite("YOU...", 200);
                    Player.Source = "shoot_first.png";
                    await Task.Delay(10);
                    Player.Source = "shoot_final.png";
                    await GameOver("You should have listened");
                }
                Player.Source = "idle.png";
                await Typewrite("That's better...");
            }
            await Typewrite($"{username}|| huh..?", waitTime:1000);
            if (username == "Undefined Username")
            {
                await Typewrite("It's weird that you chose this name though...");
                await Typewrite("why would you wanna call yourself \"Undefined Username\"???");
                await Typewrite("Are you sure you want to use this username?");
                var accepted = await DisplayAlert("Confirm", "Are you sure you want to use this username?", "No, change it", "Yes");
                if (accepted)
                {
                    username = await DisplayPromptAsync("New username", "Choose your new username");
                    if (username == null)
                    {
                        username = "random person";
                    }
                    await Typewrite($"{username}");
                    await Typewrite("Good choice!");
                }
                else
                {
                    await Typewrite("Alright...");
                }
            }
            else
            {
                await Typewrite("That's a nice name");
            }
            await Typewrite($"Nice to meet you {username}!");
            await Typewrite("Lets start our journey!");
            for (int i = 0; i < 100; i++)
            {
                stepSpeed = 2;
                await Task.Delay(50);
                Player.TranslationX += 7;
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
            }
            DialogueFrame.IsVisible = false;
            Preferences.Set("Username", username);
            Preferences.Set("HasCompletedIntro", true);
            await Task.Delay(1000);
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("test", ex.Message, "Aaa ok");
        }
    }

    // Levels \\
    private async Task Level1()
    {
        MovementMode = 2;
        ShootButton.IsVisible = false;
        map.Source = "dotnet_bot.svg";
        await Task.Delay(2000);
        await Typewrite("Welcome to the first level!");
        await Typewrite("I will give you a short tutorial on how everything works");
        await Typewrite("The movement is currently disabled");
        await Typewrite("Let's switch to the first movement mode");
        DialogueFrame.IsVisible = false;
        await Task.Delay(500);
        MovementMode = 0;
        await Typewrite("This is the default movement mode, you can walk around freely");
        await Typewrite("This mode is the most common mode that's used in the game.");
        MovementMode = 2;
        await Typewrite("Let's switch to the next movement mode.");
        DialogueFrame.IsVisible = false;
        await Task.Delay(500);
        MovementMode = 1;
        await Typewrite("This is the next movement mode, the camera is now stuck in place.");
        await Typewrite("This movement mode is mostly used in smaller maps.", waitTime: 500);
        DialogueFrame.IsVisible = false;
        await Task.Delay(2000);
        await Typewrite("There is also a shoot button which obviously shoots bullets");
        await Typewrite("Give it a try!");
        ShootButton.IsVisible = true;
        await Task.Delay(200);
        DialogueFrame.IsVisible = false;
        await Task.Delay(5000);
        await Typewrite("Well done, I think you are ready for the real challenge!");
        return;
    }

    // Controls \\
    private async void OnThumbstickPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if (e.StatusType == GestureStatus.Started)
        {
            Console.WriteLine("Started Walking");
        }
        else if (e.StatusType == GestureStatus.Running)
        {
            double x = e.TotalX, y = e.TotalY;
            await thumbstick.TranslateTo(x, y, 15, Easing.CubicOut);
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
            if (MovementMode == 0)
            {
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
            else if (MovementMode == 1)
            {
                if (x > 12.75 && y > 12.75)
                {
                    playerFacingDirection = "South-East";
                    Player.ScaleX = -1;
                    Player.TranslationX += movementSpeed;
                    Player.TranslationY += movementSpeed;
                }
                else if (x > 12.75 && y < -12.75)
                {
                    playerFacingDirection = "North-East";
                    Player.ScaleX = -1;
                    Player.TranslationX += movementSpeed;
                    Player.TranslationY -= movementSpeed;
                }
                else if (x < -12.75 && y > 12.75)
                {
                    playerFacingDirection = "South-West";
                    Player.ScaleX = 1;
                    Player.TranslationX -= movementSpeed;
                    Player.TranslationY += movementSpeed;
                }
                else if (x < -12.75 && y < -12.75)
                {
                    playerFacingDirection = "North-West";
                    Player.ScaleX = 1;
                    Player.TranslationX -= movementSpeed;
                    Player.TranslationY -= movementSpeed;
                }
                else if (x > -12.75 && x < 12.75 && y > 20)
                {
                    playerFacingDirection = "South";
                    Player.TranslationY += movementSpeed;
                }
                else if (x > -12.75 && x < 12.75 && y < -20)
                {
                    playerFacingDirection = "North";
                    Player.TranslationY -= movementSpeed;
                }
                else if (x > 20 && y > -12.75 && y < 12.75)
                {
                    playerFacingDirection = "East";
                    Player.ScaleX = -1;
                    Player.TranslationX += movementSpeed;
                }
                else if (x < -20 && y > -12.75 && y < 12.75)
                {
                    Player.ScaleX = 1;
                    playerFacingDirection = "West";
                    Player.TranslationX -= movementSpeed;
                }
                else
                {
                    Player.Source = "idle.png";
                    walkCycleTimer = 0;
                }
            }
            else if (MovementMode == 2)
            {
                Console.Write("Movement is currently disabled");
            }
            else
            {
                await DisplayAlert("Error", "Non-Existing Movement mode selected", "ok");
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
    private async Task Bullet(string Direction)
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
        _ = Bullet(playerFacingDirection);
        await Task.Delay(50);
        Player.Source = "shoot_final.png";
        await Task.Delay(50);
        Player.Source = "hold_gun.png";
    }

    // Extra Features \\

    private async Task GameOver(string message = "")
    {
        Game_Over.IsEnabled = true;
        Game_Over.IsVisible = true;
        await Typewrite("GAME OVER", chosenLabel: Game_Over_text);
        if (message != "")
        {
            await Typewrite($"{message}");
        }
        await Task.Delay(2000);
        await Navigation.PopAsync();
    }
    private async Task Typewrite(string message = "...", int delay = 60, int waitTime = 700, bool waitBeforeContinue = true, Label chosenLabel = null)
    {
        if (chosenLabel == null)
        {
            chosenLabel = DialogueBox;
        }

        chosenLabel.Text = "";
        DialogueFrame.IsVisible = true;
        for (int i = 0; i < message.Length; i++)
        {
            if (message[i].ToString() == "|")
            {
                // the | can be used to add delay to a typewrite string \\
                await Task.Delay(delay);
            }
            else
            {
                chosenLabel.Text += message[i].ToString();
            }
            await Task.Delay(delay);
        }
        if (waitBeforeContinue)
        {
            await Task.Delay(waitTime);
        }
        return;
    }
}