using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MDIPaint
{
    public class MyCircle
    {
        private Rectangle rectC;
        private int thick;
        private bool isFill;
        private Color color;

        public MyCircle()
        {
            rectC = new Rectangle();
            thick = 1;
        }

        public void SetRectC(Point start, Point finish, int thick, Color color, bool isFill = false)
        {
            rectC.X = Math.Min(start.X, finish.X);
            rectC.Y = Math.Min(start.Y, finish.Y);
            rectC.Width = Math.Abs(start.X - finish.X);
            rectC.Height = Math.Abs(start.Y - finish.Y);
            this.thick = thick;
            this.color = color;
            this.isFill = isFill;
        }

        public Rectangle GetRectC()
        {
            return rectC;
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
