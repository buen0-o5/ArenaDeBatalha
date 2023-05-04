using System.Drawing;
using System.IO;
using System.Media;


namespace ArenaDeBatalga.GameLogic
{
    public abstract class GameObject
    {
        #region Game Object Propreties
        public Bitmap Sprite { get; set; }//dimensao da figura estatica (nome da imagem que repesenta objeto do jogo )
        public bool Active { get; set; } //Propriedade para verificar se o objeto esta ativo
        public int Speed { get; set; } //Velocidade com que o objeto se locomove pela tela
        public int Left { get; set; }//localizaço do objeto para a margem esquerda
        public int Top { get; set; }//distancia do objeto para o topo
        public int Width { get { return this.Sprite.Width; } }//largura do objeto: e obtida a partir da largura da imagem
        public int Height { get { return this.Sprite.Height; } }//altura do objeto: e obtida a partir da altura da imagem
        public Size Bounds { get; set; }//Limite da tela
        public Rectangle Rectangle { get; set; }//contem a localizaçao do retangulo que esta na tela(detecçao de colisao)
        public Stream Sound { get; set; }//som do objto
        public Graphics Screen { get; set; }//referencia para tela de pintura(representa a tela)

        private SoundPlayer soundPlayer { get; set; }//reprodutor de som de Sound
        #endregion

        #region Game Object Methods
        public abstract Bitmap GetSprite();

        public GameObject(Size bounds, Graphics screen)
        {
            this.Bounds = bounds;
            this.Screen = screen;
            this.Active = true;
            this.soundPlayer = new SoundPlayer();
            this.Sprite = this.GetSprite();
            this.Rectangle = new Rectangle(this.Left, this.Top, this.Width, this.Height);
        }

        public virtual void UpdateObjecct() //lupi principal para atualizao do objeto
        {
            this.Rectangle = new Rectangle(this.Left, this.Top, this.Width, this.Height);
            this.Screen.DrawImage(this.Sprite, this.Rectangle);
        }

        public virtual void MoveLeft()//mover para esquerda
        {
            if (this.Left > 0)
                this.Left -= this.Speed;
        }
        public virtual void MoveRight()//mover para direita
        {
            if (this.Left < this.Bounds.Width - this.Width)
                this.Left += this.Speed;
        }

        public virtual void MoveDown()//mover para baixo
        {
            this.Top += this.Speed;
        }

        public virtual void MoveUp()//mover para cima
        {
            this.Top -= this.Speed;
        }

        public bool IsOutOfBounds() //verificar se objeto esta fora dos limites da tela
        {
            return
                (this.Top > this.Bounds.Height + this.Height) ||
                (this.Top < -this.Height) ||
                (this.Left > this.Bounds.Width + this.Width) ||
                (this.Left < -this.Width);
        }

        public bool IsCollidingWith(GameObject gameObject) //verificar se o objeto esta colidindo com outro objeto
        {
            if (this.Rectangle.IntersectsWith(gameObject.Rectangle))
            {
                this.PlaySound();//caso houver colisao ira tocar o som
                return true;
            }
            else return false;
        }
        public void PlaySound()//tocar o som
        {
            soundPlayer.Stream = this.Sound;
            soundPlayer.Play();
        }
        public void Destroy()//remove o objeto do jogo
        {
            this.Active = false;
        }

        #endregion
    }
}
