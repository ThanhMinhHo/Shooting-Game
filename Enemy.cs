using SplashKitSDK;
using System;

abstract class Enemy
{
    public Enemy(Window gameWindow, King player)
    {
        GenerateNewEnemy(gameWindow, player);

    }
    public void GenerateNewEnemy(Window gwindow, King player)
    {
        MainColor = Color.RandomRGB(200);
       
        X = SplashKit.Rnd(gwindow.Width);
       
        const int SPEED = 1;
        //Get a Point fro the Enemy
        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };
        Point2D toPt = new Point2D()
        {
            X = player.X,
            Y = player.Y
        };
        //Calculate the direction to head.
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));

        //Set the speed and assign to the Velocity
        Velocity = SplashKit.VectorMultiply(dir, SPEED);

    }
     public void StayOnWindow(Window window)
    {
        const int GAP = 10;

        //Check on the left/ right bound of the screen
        if (X < GAP)
        {
            X = (window.Width - GAP) - 50;
        }
        if ((X + 10) > (window.Width - GAP))
        {
            X = GAP;
        }
        //check on the top and bottom bound of the window
        if (Y < GAP)
        {
            Y = GAP;
        }
        if (Y  > 40)
        {
            Y = 1;
        }

    }

    public bool IsOffScreen(Window screen, King player)
    {
        return (X < -Width || X > screen.Width || Y < -Height || Y > screen.Height);

    }
    public abstract void Draw();
    public Vector2D Velocity { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public Color MainColor { get; set; }
    public Circle CollisionCircleEnemy
    {
        get { return SplashKit.CircleAt(X, Y, 20); }
    }
    public int Width
    {
        get { return 50; }
    }
    public int Height
    {
        get { return 50; }
    }
    public void Update()
    {
        X += Velocity.X;
        Y += Velocity.Y;
    }

}
class AirPlain : Enemy
{
    public AirPlain(Window gameWindow, King player) : base(gameWindow, player)
    {
    }
    public override void Draw()
    {
        SplashKit.LoadBitmap("airplain", "airplain.png");
        Bitmap _bulletBitmap = SplashKit.BitmapNamed("airplain");
         _bulletBitmap.Draw(X, Y);

    }
}

class OrageAirplain : Enemy
{
    public OrageAirplain(Window gameWindow, King player) : base(gameWindow, player)
    {
    }
    public override void Draw()
    {
         SplashKit.LoadBitmap("orangeairplain", "orangeairplain.gif");
        Bitmap _bulletBitmap = SplashKit.BitmapNamed("orangeairplain");
         _bulletBitmap.Draw(X, Y);
    }
}
class RedAirplain : Enemy
{
    public RedAirplain(Window gameWindow, King player) : base(gameWindow, player)
    {
    }
    public override void Draw()
    {
        SplashKit.LoadBitmap("redairplain", "redairplain.png");
        Bitmap _bulletBitmap = SplashKit.BitmapNamed("redairplain");
         _bulletBitmap.Draw(X, Y);

    }
}
