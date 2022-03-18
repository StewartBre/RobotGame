using System;
using SplashKitSDK;

public class Bullet
{
    public Circle c;
    
    public double X
    {
        get;
        private set;
    }

    public double Y
    {
        get;
        private set;
    }

    private Vector2D Velocity 
    {
        get; set;
    }

    public Circle CollisionCircle
    {
        get
        {
            return SplashKit.CircleAt(X, Y, 10);
        }
    }

    public Bullet(Window gameWindow, Player _Player)
    {

        X = _Player.X;
        Y = _Player.Y;

        const int Speed = 8;

            
        Point2D frompoint  = new Point2D()
        {
            X = X, Y = Y
        };

            
        Point2D mousepoint = SplashKit.MousePosition();
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(frompoint, mousepoint));
        Velocity = SplashKit.VectorMultiply(dir, Speed);
    }

     public void Draw()
    {
        SplashKit.ProcessEvents();
        SplashKit.FillCircle(Color.Orange , X, Y, 10);
        c = SplashKit.CircleAt(X, Y, 10);
            
    }

    public void Update()
    {
        X += Velocity.X;
        Y += Velocity.Y;
    }

    public bool IsOffScreen(Window screen)
    {
        if( (X < -15) || (X > screen.Width) || (Y < -15) || (Y > screen.Height))
        {
            return true;
        }
        else
        return false; 
    }

    public bool BulletHit(Robot other)
    {

        return SplashKit.CirclesIntersect(c, other.CollisionCircle);
                     
    }
    
}