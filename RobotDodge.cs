using SplashKitSDK;
using System.Collections.Generic;
using System;
using System.IO;

public class RobotDodge
{
    private Player _Player;
    private Window _GameWindow;
    private List<Robot> _Robots = new List<Robot>();
    private List<Robot> _removedRobots = new List<Robot>();
    private List<Bullet> _Bullets = new List<Bullet>();
    private List<Bullet> _removedBullets = new List<Bullet>();
    private Timer gameTimer;
    private Bitmap HeartBitmap = new Bitmap("Heart", "redheart.png");
   
    public bool Quit
    {
        get
        {
            return _Player.Quit;
        }
    }

    public RobotDodge( Window gameWindow )
    {
        _GameWindow = gameWindow;
        _Player = new Player(gameWindow);
        gameTimer = new Timer("Game Timer");
        gameTimer.Start();
    }

    public void HandleInput()
    {
        _Player.HandleInput();
        _Player.StayOnWindow(_GameWindow);
    }
    
    public void Draw()
    {
        _GameWindow.Clear(Color.White);
        foreach(Robot eachRobot in _Robots)
        {
            eachRobot.Draw();
        }
        _Player.Draw();
        foreach (Bullet eachBullet in _Bullets)
        {
            eachBullet.Draw();
        }
        gameHUD();
        if (_Player.Lives <= 0)
        {
            _GameWindow.Clear(Color.Black);
        }
        _GameWindow.Refresh(60);
    }

    public void Update()
    {
        CheckCollisions();
        _Player.Score = Convert.ToInt32(gameTimer.Ticks / 1000);
        SplashKit.ProcessEvents();
        foreach(Robot eachRobot in _Robots)
        {
            eachRobot.Update();
        }
        if (SplashKit.Rnd()<0.01)
        {
            Robot eachRobot = RandomRobot();
            _Robots.Add(eachRobot);           
        }   
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            _Bullets.Add(AddBullet());
        }
        foreach (Bullet eachBullet in _Bullets)
        {
            eachBullet.Update();
        }
    }

    public Robot RandomRobot()
    {
        double randomNum = SplashKit.Rnd(1, 900);

        if ((randomNum > 300 & randomNum < 600))
        {
            return new Boxy(_GameWindow, _Player);
        }
        else if (randomNum < 300)
        {
            return new Roundy(_GameWindow, _Player);
        }
        else
        {
            return new OwnRobot(_GameWindow, _Player);
        }
        
    }
    
    private void CheckCollisions()
    {
        foreach(Robot eachRobot in _Robots)
        {
            if (_Player.CollidedWith(eachRobot) & _Player.Lives > 0)
            {
                _Player.Lives = _Player.Lives - 1;
            }
            if (_Player.CollidedWith(eachRobot) || eachRobot.IsOffscreen(_GameWindow))
            {
                _removedRobots.Add(eachRobot);
            }
            foreach (Bullet eachBullet in _Bullets)
            {
                if (eachBullet.BulletHit(eachRobot))
                {
                    _removedBullets.Add(eachBullet);
                    _removedRobots.Add(eachRobot);
                }
                if (eachBullet.IsOffScreen(_GameWindow))
                {
                    _removedBullets.Add(eachBullet);
                }
            }       
        }
        foreach(Robot eachRobot in _removedRobots)
        {
            _Robots.Remove(eachRobot);
        }
        foreach (Bullet eachBullet in _removedBullets)
        {
            _Bullets.Remove(eachBullet);
        }
    }

    public void gameHUD()
    {
        DrawHearts(_Player.Lives);
        SplashKit.DrawText("Score: " + _Player.Score, Color.Black, 0, 40);
    }

    public void DrawHearts(int numberOfHearts)
    {
        int heartX = 0;
        for (int i = 0; i < numberOfHearts; i ++ )
        {
            if (heartX < 300)
            {
                SplashKit.DrawBitmap(HeartBitmap, heartX, 0);
                heartX = heartX + 40;
            }
        }
    }

    public Bullet AddBullet()
    {
        Bullet _RandomBullet = new Bullet(_GameWindow, _Player);
        return _RandomBullet;
    }
}

