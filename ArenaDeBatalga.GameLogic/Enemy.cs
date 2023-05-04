using System.Drawing;


namespace ArenaDeBatalga.GameLogic
{
    public class Enemy : GameObject
    {
        public Enemy(Size bouns, Graphics screenPainter, Point position): base(bouns, screenPainter)
        {
            this.Left = position.X;
            this.Top = position.Y;
            this.Speed = 5;
            this.Sound = Media.exploshion_short;
        }

        public override Bitmap GetSprite()
        {
            return Media.Enemy;
        }
        public override void UpdateObjecct()
        {
            this.MoveDown();
            base.UpdateObjecct();
        }
    }
}
