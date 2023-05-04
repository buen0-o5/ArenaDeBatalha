using System.Drawing;


namespace ArenaDeBatalga.GameLogic
{
   public class GameOver : GameObject
    {
        public GameOver(Size bouns, Graphics screenPainter) : base(bouns, screenPainter)
        {
            this.Left = this.Bounds.Width / 2 - this.Width / 2;
            this.Top = this.Bounds.Height / 2 - this.Height / 2;
            this.Speed = 0;
        }

        public override Bitmap GetSprite()
        {
            return Media.GameOver;
        }
    }
}
