using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acutal_final_project
{
    public class Targets
    {
        private Texture2D _texture;
        private Rectangle _rect;
        private Vector2 _speed;


        public Targets(Texture2D texture, Rectangle rectangle, Vector2 speed)
        {
            _texture = texture;
            _rect = rectangle;
            _speed = speed;
        }

        public void Move(Rectangle window)
        {
            _rect.Offset(_speed);
            if (_rect.Right > window.Width || _rect.Left < 0)
                _speed.X *= -1;
            if (_rect.Bottom > window.Height || _rect.Top < 0)
                _speed.Y *= -1;
        }

        public Rectangle Bounds
        {
            get { return _rect; }
            set { _rect = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rect, Color.White);
        }
        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }


    }
}
