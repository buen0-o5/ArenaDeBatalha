using System.Drawing;


namespace ArenaDeBatalga.GameLogic
{
    public class Bullet : GameObject
    {

        public Bullet(Size bouns, Graphics screenPainter, Point position) : base(bouns, screenPainter)
        {
            this.Speed = 20;
            this.Sound = Media.Missile;
            this.Left = position.X;
            this.Top = position.Y;
            this.PlaySound();

        }
        public override Bitmap GetSprite()
        {
            return Media.Bullet;
        }
        public override void UpdateObjecct()
        {
            this.MoveUp();
            base.UpdateObjecct();
        }

    }
}
