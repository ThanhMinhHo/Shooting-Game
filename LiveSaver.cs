using SplashKitSDK;
using System;
using System.Collections.Generic;
public class LiveSaver
{
    public LiveSaver(Window gameWindow)
    {
        SplashKit.LoadBitmap("Heart", "Heart.png");
        _liveSaverBitmap = SplashKit.BitmapNamed("Heart");
        GenerateLiveSaver(gameWindow);
    }

    private Bitmap _liveSaverBitmap;
    public Bitmap LiveSaverBitmap
    {
        get { return _liveSaverBitmap; }
    }
    public Circle CollisionCircleKing
    {
        get { return SplashKit.CircleAt(X, Y, 20); }
    }
    public void GenerateLiveSaver(Window gwindow)
    {
        X = SplashKit.Rnd(gwindow.Width);

        const int SPEED = 1;
        //Get a Point fro the Robot
        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };
        Point2D toPt = new Point2D()
        {
            X = X,
            Y = Y + 50
        };
        //Calculate the direction to head.
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));

        //Set the speed and assign to the Velocity
        Velocity = SplashKit.VectorMultiply(dir, SPEED);

    }


    public Vector2D Velocity { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    public void Update()
    {
        X += Velocity.X;
        Y += Velocity.Y;
    }
    public void Draw()
    {

        _liveSaverBitmap.Draw(X, Y);

    }

}