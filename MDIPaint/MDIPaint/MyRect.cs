using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MDIPaint
{
    public class MyRect
    {
        private Rectangle rect;
        private int thick;
        private bool isFill;
        private Color color;

        public MyRect()
        {
            rect = new Rectangle();
            thick = 1;
        }

        public void SetRect(Point start, Point finish, int thick, Color color, bool isFill = false)
        {
            rect.X = Math.Min(start.X, finish.X);
            rect.Y = Math.Min(start.Y, finish.Y);
            rect.Width = Math.Abs(start.X - finish.X);
            rect.Height = Math.Abs(start.Y - finish.Y);
            this.thick = thick;
            this.color = color;
            this.isFill = isFill;
        }

        public Rectangle GetRect()
        {
            return rect;
        }

        public int GetThick()
        {
            return thick;
        }

        public bool GetFill()
        {
            return isFill;
        }

        public Color GetColor()
        {
            return color;
        }
    }
}
