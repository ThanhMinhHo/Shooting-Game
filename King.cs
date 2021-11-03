using SplashKitSDK;
using System;
class King
{
    public King(Window gameWindow)
    {
        _Live = 5;
           //Assign the value  
        _GameWindow = gameWindow;

        SplashKit.LoadBitmap("King", "king.png");
        _KingBitmap = SplashKit.BitmapNamed("King");
        //set Quite to false when the King is created.
       
        _angle = 0;
        //the cental of the screen minus haft of the King to get to the central
        X = (gameWindow.Width) / 2 - (_KingBitmap.Width / 2);
        Y = (gameWindow.Height) - (_KingBitmap.Height / 2);
        //Create a new Bullet object call _bullet
    }
  
    
    private Window _GameWindow; 
    private Bitmap _KingBitmap;
    private int _Live;
    public int Live
    {
        get{return _Live;}
        set{_Live = value;}
    }

   
    private double _angle;
   
    public double X { get; private set; }
    public double Y { get; private set; }
    public int Width
    {
        get { return _KingBitmap.Width; }
    }

    
    public double Angle { get => _angle; set => _angle = value; }


    public bool KingCollidedWithEnemy(Enemy Enemy)
    {
        return _KingBitmap.CircleCollision(X, Y, Enemy.CollisionCircleEnemy);  
    }
    public bool KingCollidedWithBullet(Bullet bullet)
    {
        return _KingBitmap.CircleCollision(X, Y, bullet.CollisionCircleBullet);  
    }
     public bool KingCollidedWithLiveSaver(LiveSaver liveSaver)
    {
        return _KingBitmap.CircleCollision(X, Y, liveSaver.CollisionCircleKing);  
    }
    public void StayOnWindow(Window window)
    {
        const int GAP = 10;

        //Check on the left/ right bound of the screen
        if (X < GAP)
        {
            X = GAP;
        }
        if ((X + _KingBitmap.Width) > (window.Width - GAP))
        {
            X = (window.Width - GAP) - _KingBitmap.Width;
        }
        //check on the top and bottom bound of the window
        if (Y < GAP)
        {
            Y = GAP;
        }
        if ((Y + _KingBitmap.Height) > (window.Height - GAP))
        {
            Y = (window.Height - GAP) - _KingBitmap.Height;
        }

    }
    public void Rotate(double amount)
    {
        _angle = (_angle + amount) % 360;
    }
    public void Draw()
    {
        _KingBitmap.Draw(X, Y, SplashKit.OptionRotateBmp(_angle));
       
        
    }
   
    public void Move(double amountForward, double amountStrafe)
    {
        Vector2D movement = new Vector2D();
        Matrix2D rotation = SplashKit.RotationMatrix(_angle);

        movement.X += amountForward;
        movement.Y += amountStrafe;

        movement = SplashKit.MatrixMultiply(rotation, movement);

        X += movement.X;
        Y += movement.Y;
    }

}

