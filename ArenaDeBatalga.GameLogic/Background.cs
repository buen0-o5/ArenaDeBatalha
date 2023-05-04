using System.Drawing;


namespace ArenaDeBatalga.GameLogic
{
   public class Background : GameObject
    {
        //Tela de fundo do jogo
        public Background(Size bounds, Graphics screen ) : base(bounds, screen)
        {
            this.Left = 0;
            this.Top= 0;
            this.Speed = 0;
        }

        public override Bitmap GetSprite()
        {
            return Media.Background;
        }
    }
}
