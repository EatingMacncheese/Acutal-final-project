using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Actual_final_project
{
    public class Targets
    {
        private Texture2D texture;
        private Rectangle rect;
        private Vector2 speed;

        public Targets(Texture2D texture, Rectangle rect, Vector2 speed)
        {
            this.texture = texture;
            this.rect = rect;
            this.speed = speed;
        }

        public void Move(Rectangle window, Rectangle panel)
        {
            rect.Offset(speed);

            if (rect.Right > panel.Left || rect.Left < 0)
                speed.X *= -1;

            if (rect.Bottom > window.Height || rect.Top < 0)
                speed.Y *= -1;
        }

        public Rectangle Bounds => rect;
        public Texture2D Texture => texture;
    }
}