using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Robot Dodge", 700, 700);
        RobotDodge robotDodge = new RobotDodge(gameWindow);
        
        while(!gameWindow.CloseRequested && robotDodge.Quit != true) 
        {
            SplashKit.ProcessEvents();
            robotDodge.HandleInput();
            robotDodge.Draw();
            robotDodge.Update();
        }

        gameWindow.Close();
        gameWindow = null;
    }
}
