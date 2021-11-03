using SplashKitSDK;
using System;
using System.Collections.Generic;

class GameScene
{
    public GameScene(Window window)
    {
        _GameWindow = window;
    }
    public void Start(double value)
    {
        _Enemys = new List<Enemy>();
        _King = new King(_GameWindow);
        _Bullet = new List<Bullet>();
        _EnemyBullet = new List<Bullet>();
        _LiveSaver = new List<LiveSaver>();
        Quit = false;
        _gameDifficulty = value;

    }

    private List<Bullet> _Bullet;
    private List<Bullet> _EnemyBullet;

    private double _gameDifficulty;
    private King _King;
    private Window _GameWindow;
    private List<Enemy> _Enemys;
    public bool Quit { get; set; }
    private int _Score = 0;
    private List<LiveSaver> _LiveSaver;
    public int Score
    {
        get { return _Score; }
    }

    public Bullet GenerateBullet(double x, double y)
    {
        SplashKit.LoadBitmap("sword", "sword.png");
        Bitmap bitmap = SplashKit.BitmapNamed("sword");
        //Create new bullet Object each time the Mouse is click
        Bullet bullet = new Bullet(_King.X, _King.Y, bitmap);
        bullet.Fire(x, y);
        return bullet;
    }
    public void HandleInput()
    {

        const int speed = 5;
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            //identify the location of the mouse
            double mouseX = Convert.ToDouble(SplashKit.MouseX());
            double mouseY = Convert.ToDouble(SplashKit.MouseY());
            //Set up the direction to reach to the target.
            _Bullet.Add(GenerateBullet(mouseX, mouseY));
        }

        if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            _King.Move(speed + 10, 0);
        }
        if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            _King.Move(-(speed + 10), 0);
        }
        //if the escapekey is typing the Quit will true.
        if (SplashKit.KeyDown(KeyCode.EscapeKey))
        {
            Quit = true;
        }
        _King.StayOnWindow(_GameWindow);
        foreach (Enemy Enemy in _Enemys)
        {
            Enemy.StayOnWindow(_GameWindow);
        }

    }
    public void Draw()
    {
        //Raw the King object
        _King.Draw();
        //Draw the Enemy object
        foreach (Enemy Enemy in _Enemys)
        {
            Enemy.Draw();
        }
        foreach (Bullet bullet in _Bullet)
        {
            bullet.Draw();
        }
        foreach (Bullet EnemyBullet in _EnemyBullet)
        {
            EnemyBullet.Draw();
        }
        foreach (LiveSaver liveSaver in _LiveSaver)
        {
            liveSaver.Draw();
        }

    }
    private void SocreCalculation()
    {
        //Score calculate base on the Timer ticks devided by 1000 to become a second.
        _GameWindow.DrawText("Score: " + _Score, Color.BrightGreen, "Hello", 60, _GameWindow.Width - 100, 5);
    }
    public void EnemyGenerateBullet()
    {
        SplashKit.LoadBitmap("bullet", "Gliese.png");
        Bitmap bitmap = SplashKit.BitmapNamed("bullet");
        int i = 0;
        foreach (Enemy Enemy in _Enemys)
        {
            Bullet bullet = new Bullet(Enemy.X, Enemy.Y, bitmap);
            bullet.Fire(_King.X, _King.Y);
            _EnemyBullet.Add(bullet);
            if (i == 3)
            {
                break;
            }

            i++;
        }
    }
    public void GenerateLiveSaver()
    {
        _LiveSaver.Add(new LiveSaver(_GameWindow));
    }
    public void RandomEnemy()
    {
         float randomNumber;
        //make sure there are only 4 airplain on the scene
        if (_Enemys.Count < 4)
        {
             randomNumber = SplashKit.Rnd();
            if (randomNumber < 0.33)
            {
                _Enemys.Add(new AirPlain(_GameWindow, _King));
            }
            else if (randomNumber < 0.66)
            {
                _Enemys.Add(new RedAirplain(_GameWindow, _King));
            }
            else
            {
                _Enemys.Add(new OrageAirplain(_GameWindow, _King));
            }

            if (randomNumber < 0.1)
            {
                GenerateLiveSaver();
            }
        }
        randomNumber = SplashKit.Rnd();
        if (randomNumber < 0.2)
        {
            EnemyGenerateBullet();
        }
    }
    public void Update()
    {


        if (SplashKit.Rnd() < _gameDifficulty)
        {
            RandomEnemy();
        }

        foreach (Enemy Enemy in _Enemys)
        {
            Enemy.Update();
        }
        foreach (Bullet bullet in _Bullet)
        {
            bullet.Update();
        }
        foreach (Bullet EnemyBullet in _EnemyBullet)
        {
            EnemyBullet.Update();
        }
        foreach (LiveSaver liveSaver in _LiveSaver)
        {
            liveSaver.Update();
        }
        CheckCollisions();
        //Update King Live
        CheckKingLive();
        //Update the Score
        SocreCalculation();
    }
    private void CheckKingLive()
    {
        SplashKit.LoadBitmap("Heart", "Heart.png");
        Bitmap heart = SplashKit.BitmapNamed("Heart");
        _GameWindow.DrawBitmap(heart, 0, 0);
        _GameWindow.DrawText(_King.Live.ToString(), Color.BrightGreen, "Hello", 60, heart.Width + 5, heart.Height / 2);
        //If the _King have no live, Should quick the game
        if (_King.Live <= 0)
        {
            _GameWindow.DrawText("You have Lost, Please try again !!", Color.Red, "Hello", 40, _GameWindow.Width / 2, _GameWindow.Height / 2);
            showButton("Play Again");
        }
    }
    public void showButton(string Title)
    {
        Color backColor = Color.White;
        bool buttonQuick = false;
        List<Button> buttons = new List<Button>();
        int Width = 100;
        int Height = 25;
        int X = ((_GameWindow.Width) / 2 - (Width / 2));
        int Y = (_GameWindow.Height) / 2 - (Height / 2);
        //fill the menu
        SplashKit.FillRectangle(Color.YellowGreen, X, Y, Width, Height * 2 + 10);
        Button playAgain = new Button(X, Y, Width, Height, Title);

        playAgain.OnClick += (btn) =>
        {
            buttonQuick = true;

        };

        buttons.Add(playAgain);
        Button quit = new Button(X, Y + Height + 5, Width, Height, "Quit");

        quit.OnClick += (btn) =>
        {
            Quit = true;
            buttonQuick = true;
        };

        buttons.Add(quit);

        while (!_GameWindow.CloseRequested && !buttonQuick)
        {
            SplashKit.ProcessEvents();

            foreach (Button btn in buttons) btn.HandleInput();

            _GameWindow.Clear(backColor);

            foreach (Button btn in buttons) btn.Draw();

            _GameWindow.Refresh(60);
        }
        if (!Quit)
        {
            GameDifficulty();
        }
    }
    private void GameDifficulty()
    {

        Color backColor = Color.BrightGreen;
        bool buttonQuick = false;
        List<Button> buttons = new List<Button>();
        int Width = 100;
        int Height = 25;
        int X = ((_GameWindow.Width) / 2 - (Width / 2));
        int Y = (_GameWindow.Height) / 2 - (Height / 2);
        //fill the menu
        SplashKit.FillRectangle(Color.YellowGreen, X, Y, Width, Height * 2 + 10);
        Button easy = new Button(X, Y, Width, Height, "Easy");

        easy.OnClick += (btn) =>
        {
            buttonQuick = true;
            Start(0.01);
        };

        buttons.Add(easy);
        Button medium = new Button(X, Y + Height + 5, Width, Height, "Medium");

        medium.OnClick += (btn) =>
        {
            buttonQuick = true;
            Start(0.03);
        };

        buttons.Add(medium);
        //choose level
        Button hard = new Button(X, Y + (Height * 2) + 10, Width, Height, "Hard");
        hard.OnClick += (btn) =>
       {

           buttonQuick = true;
           Start(0.06);
       };
        buttons.Add(hard);

        while (!_GameWindow.CloseRequested && !buttonQuick)
        {
            SplashKit.ProcessEvents();

            foreach (Button btn in buttons) btn.HandleInput();

            _GameWindow.Clear(backColor);

            foreach (Button btn in buttons) btn.Draw();

            _GameWindow.Refresh(60);
        }

    }
    private void CheckCollisions()
    {
        List<Enemy> EnemyToRemove = new List<Enemy>();
        List<Bullet> bulletToRemoves = new List<Bullet>();
        List<Bullet> bulletEnemyToRemoves = new List<Bullet>();
        List<LiveSaver> LiveSaverToRemoves = new List<LiveSaver>();
        //Check collisions between Enemy and Bullet

        foreach (Enemy Enemy in _Enemys)
        {
            foreach (Bullet bullet in _Bullet)
            {
                if (bullet.BulletBitmap.CircleCollision(bullet.X, bullet.Y, Enemy.CollisionCircleEnemy) == true)
                {
                    bulletToRemoves.Add(bullet);
                    EnemyToRemove.Add(Enemy);
                    SoundEffect music = new SoundEffect("Explosion", "Explosion.wav");
                    music.Play();
                    _Score++;
                }
            }
        }

        //Check collision between Enemy and King
        foreach (Enemy Enemy in _Enemys)
        {
            if (Enemy.IsOffScreen(_GameWindow, _King) == true)
            {
                EnemyToRemove.Add(Enemy);
            }
            if (_King.KingCollidedWithEnemy(Enemy))
            {
                SoundEffect music = new SoundEffect("Punches", "Punches.wav");
                music.Play();
                EnemyToRemove.Add(Enemy);
                _King.Live--;
            }
        }
        //Check the collision bettwn King and Bullet
        foreach (Bullet EnemyBullet in _EnemyBullet)
        {
            if (_King.KingCollidedWithBullet(EnemyBullet))
            {
                SoundEffect music = new SoundEffect("Punches", "Punches.wav");
                music.Play();
                bulletEnemyToRemoves.Add(EnemyBullet);
                _King.Live--;
            }
        }
        foreach (LiveSaver liveSaver in _LiveSaver)
        {
            if (_King.KingCollidedWithLiveSaver(liveSaver))
            {
                _King.Live++;
                LiveSaverToRemoves.Add(liveSaver);

            }
        }
        //Remove Enemy that collision with King and Bullet
        foreach (Enemy EnemyRMove in EnemyToRemove)
        {
            _Enemys.Remove(EnemyRMove);
        }
        //Remove bullets that collisions with Enemy
        foreach (Bullet bulletToRemove in bulletToRemoves)
        {
            _Bullet.Remove(bulletToRemove);
        }
        //Remove Enemybullets that collisions with King
        foreach (Bullet bulletToRemove in bulletEnemyToRemoves)
        {
            _EnemyBullet.Remove(bulletToRemove);
        }
        //Remove LiveSaver 
        foreach (LiveSaver liveSaverToRemove in LiveSaverToRemoves)
        {
            _LiveSaver.Remove(liveSaverToRemove);
        }
    }

}