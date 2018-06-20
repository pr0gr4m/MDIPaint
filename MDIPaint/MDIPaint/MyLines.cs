using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MDIPaint
{
    public class MyLines
    {
        private Point[] point = new Point[2];
        private int thick;
        private Color color;

        public MyLines()
        {
            point[0] = new Point();
            point[1] = new Point();
            thick = 1;
        }

        public void SetPoint(Point start, Point finish, int thick, Color color)
        {
            point[0] = start;
            point[1] = finish;
            this.thick = thick;
            this.color = color;
        }

        public Point GetPoint1()
        {
            return point[0];
        }

        public Point GetPoint2()
        {
            return point[1];
        }

        public int GetThick()
        {
            return thick;
        }

        public Color GetColor()
        {
            return color;
        }
    }
}
