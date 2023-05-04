using ArenaDeBatalga.GameLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace ArenaDeBatalha.GUI
{
    public partial class FormPrincipal : Form
    {
        

        DispatcherTimer gameLoopTimer { get; set; }//Timer para redenrizar imagem de fundo 
        DispatcherTimer enemySpawnTimer { get; set; } //Timer para redenrizar imagem do inimigo
        Bitmap screenBuffer { get; set; }//desenhar todos os objetos antes de jogar na tela(imagem em si)
        Graphics screenPainter { get; set; }//conjunto de ferramentas para desenhar a imagem
        Background background { get; set; } 
        Player player { get; set; }
        GameOver gameOver { get; set; }
        List<GameObject> gameObjects { get; set; }
        public Random Random { get; set; }
        bool canShoot;
        public FormPrincipal()
        {
            
            InitializeComponent();

            this.Random = new Random();
            this.ClientSize = Media.Background.Size; //tamanho do formulario recebe o tamanho da imagem
            this.screenBuffer = new Bitmap(Media.Background.Width, Media.Background.Height);
            this.screenPainter = Graphics.FromImage(this.screenBuffer);
            this.gameObjects = new List<GameObject>();
            this.background = new Background(this.screenBuffer.Size, this.screenPainter);
            this.player = new Player(this.screenBuffer.Size, this.screenPainter);
            this.gameOver = new GameOver(this.screenBuffer.Size, this.screenPainter);




            this.gameLoopTimer = new DispatcherTimer(DispatcherPriority.Render);
            this.gameLoopTimer.Interval = TimeSpan.FromMilliseconds(16.66666);//determinar o tempo de execuçao do lupi do jogo
            this.gameLoopTimer.Tick += GameLoop;

            this.enemySpawnTimer = new DispatcherTimer(DispatcherPriority.Render);
            this.enemySpawnTimer.Interval = TimeSpan.FromMilliseconds(1000);//determinar o tempo de execuçao do lupi do jogo
            this.enemySpawnTimer.Tick += SpawnEnemy;

            StartGame();
        }

        //objeto para disparar laço
  
        public void StartGame()
        {
            this.gameObjects.Clear();
            this.gameObjects.Add(background);
            this.gameObjects.Add(player);
            this.player.SetStartPosition();
            this.player.Active = true;
            this.gameLoopTimer.Start();
            this.enemySpawnTimer.Start();
            this.canShoot = true;
        }
        public void EndGame()
        {
            this.gameObjects.RemoveAll( x => x is GameObject);
            this.gameLoopTimer.Stop();
            this.enemySpawnTimer.Stop();
            this.gameObjects.Add(background);
            this.gameObjects.Add(gameOver);
            this.background.UpdateObjecct();
            this.gameOver.UpdateObjecct();
            this.Invalidate();
            
        }


        public void SpawnEnemy(object sender, EventArgs e)
        {
            Point enemyPosition = new Point(this.Random.Next(10,this.screenBuffer.Width - 74), - 62); //Posiçao de rederizaçao objeto
            Enemy enemy = new Enemy(this.screenBuffer.Size, this.screenPainter, enemyPosition);
            this.gameObjects.Add(enemy);
        }

        public void GameLoop(object sender, EventArgs e)
        {
            this.gameObjects.RemoveAll(x => !x.Active);

            this.ProcessControls();
            foreach (GameObject go in this.gameObjects)
            {
                go.UpdateObjecct();
                if (go.IsOutOfBounds())
                {
                    go.Destroy();
                }
                if(go is Enemy)
                {
                    if(go.IsCollidingWith(player))
                    {
                        player.Destroy();
                        player.PlaySound();
                        EndGame();
                        return;
                    }
                    foreach(GameObject bullet in this.gameObjects.Where(x => x is Bullet))
                    {
                        if(go.IsCollidingWith(bullet))
                        {
                            go.Destroy();
                            bullet.Destroy();
                        }
                    }
                }
            }
            // Debug.WriteLine(this.gameObjects.Count); verificar a quantidade de objetos em memoria
            this.Invalidate();
        }

        private void FormPrincipal_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawImage(this.screenBuffer, 0, 0);
        }
    
        private void ProcessControls()
        {
            if (Keyboard.IsKeyDown(Key.Left)) player.MoveLeft();
            if (Keyboard.IsKeyDown(Key.Right)) player.MoveRight();
            if (Keyboard.IsKeyDown(Key.Up)) player.MoveUp();
            if (Keyboard.IsKeyDown(Key.Down)) player.MoveDown();
            if (Keyboard.IsKeyDown(Key.Space) && this.canShoot)
            {
                this.gameObjects.Insert(1, player.Shoot());
                this.canShoot = false;

            }
            if (Keyboard.IsKeyUp(Key.Space)) this.canShoot = true;
        }

        private void FormPrincipal_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.R)
            {
                this.StartGame();
            }
        }
    }
}
