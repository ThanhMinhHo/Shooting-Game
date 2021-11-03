using SplashKitSDK;
using System;
public class Bullet
{
    private Bitmap _bulletBitmap;
    public Bitmap BulletBitmap
    {
        get { return _bulletBitmap; }
    }
    private double _x;
    public double X
    {
        get { return _x; }
    }
    private double _y;
    public double Y
    {
        get { return _y; }
    }
    private bool _active = false;
    private Vector2D Velocity { get; set; }
    public Bullet(double playerPositionX, double playerPositionY, Bitmap bitmap)
    {
      
        _bulletBitmap = bitmap;
        _x = playerPositionX;
        _y = playerPositionY;

    }
    public void Fire(double mouseX, double mouseY)
    {
        //Set the bullet to active mode
        _active = true;
        const int SPEED = 10;
        //Get a Point fro the Robot
        Point2D fromPt = new Point2D()
        {
            X = _x,
            Y = _y
        };
        Point2D toPt = new Point2D()
        {
            X = mouseX,
            Y = mouseY
        };
        //Calculate the direction to head.
        Vector2D dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        //Set the speed and assign to the Velocity
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
    }
    public Bullet()
    {
        _active = false;
    }

    public void Update()
    {
        _x += Velocity.X;
        _y += Velocity.Y;

        if ((_x > SplashKit.ScreenWidth() || _x < 0) || _y > SplashKit.ScreenHeight() || _y < 0)
        {
            _active = false;
        }
    }
    public Circle CollisionCircleBullet
    {
        get { return SplashKit.CircleAt(X, Y, 20); }
    }
    public void Draw()
    {
        if (_active)
        {
            _bulletBitmap.Draw(_x, _y);
        }
    }

}