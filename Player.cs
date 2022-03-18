using SplashKitSDK;
using System;
using System.Collections.Generic;

public class Player
{
    private Bitmap _PlayerBitmap;
    public double X { get; private set; }
    public double Y { get; private set; }
    public bool Quit {get; private set;}
    public int Score;
    public int Lives = 5;

    public int Width
    {
        get
        {
            return _PlayerBitmap.Width;
        }
    }

    public int Height
    {
        get { return _PlayerBitmap.Height; }
    }

    public Player(Window gameWindow)
    {
        _PlayerBitmap = new Bitmap("Player", "Player.png");
        
        X = (gameWindow.Width - Width) / 2;
        Y = (gameWindow.Height - Height) / 2;

    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_PlayerBitmap, X, Y);   
    } 
       
    public void HandleInput()
    {

        int speed = 5;
    
        SplashKit.ProcessEvents();

        if (SplashKit.KeyDown(KeyCode.EscapeKey) || Lives == 0)
        {
            Quit = true;
        }

        if(SplashKit.KeyDown(KeyCode.UpKey))
        {
            Y -= speed;
        }

        if(SplashKit.KeyDown(KeyCode.DownKey))
        {
            Y += speed;
        }

        if(SplashKit.KeyDown(KeyCode.RightKey))
        {
            X += speed;
        }

        if(SplashKit.KeyDown(KeyCode.LeftKey))
        {
            X -= speed;
        }

    }

    public void StayOnWindow(Window limit)
    {
        const int GAP = 10;
        
        if( X < GAP)
        {
            X = GAP;
        }
        
        if( X + Width > limit.Width - GAP)
        {
            X = limit.Width - GAP - Width;
        }

        if( Y < GAP)
        {
            Y = GAP;
        }

        if( Y + Height > limit.Height - GAP)
        {
            Y = limit.Height - GAP - Height;
        }
    }

    public bool CollidedWith(Robot other)
    {
        return _PlayerBitmap.CircleCollision(X,Y, other.CollisionCircle);
    }
}



    
