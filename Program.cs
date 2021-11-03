using System;
using SplashKitSDK;

public class Program
{

    public static void Main()
    {
        //Create the Window and refresh it
        Window gameWindow = new Window("Game", 600, 600);
        
        
        GameScene robotDodge = new GameScene(gameWindow);
        robotDodge.showButton("Play");
        //While the window is not close and player is not quit
        while (!gameWindow.CloseRequested && !robotDodge.Quit)
        {
            gameWindow.Clear(Color.Green);
            SplashKit.ProcessEvents();
            robotDodge.Draw();
            robotDodge.HandleInput();
            robotDodge.Update();
            gameWindow.Refresh(60);
        }
        
        gameWindow.Close();
        gameWindow = null;
    }
}
