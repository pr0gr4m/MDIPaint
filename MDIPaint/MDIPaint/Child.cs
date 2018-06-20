using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MDIPaint
{
    public partial class Child : Form
    {
        private Image image = null;
        public Color color;
        public bool draw, line, rect, circle;
        public int thick;
        public bool isFill;
        private Point start, finish;
        private Pen pen;
        private SolidBrush brush;
        private int nLine, nRect, nCircle;
        private MyLines[] myLines;
        private MyRect[] myRect;
        private MyCircle[] myCircle;

        public Child()
        {
            InitializeComponent();
        }

        public Child(Image image)
            : this()
        {
            this.image = image;
        }

        public Panel GetPanel()
        {
            return panel;
        }

        public void SetImage(Image image)
        {
            this.image = image;
        }

        public void SetupVar()
        {
            thick = 1;
            isFill = false;
            draw = line = rect = circle = false;
            start = new Point(0, 0);
            finish = new Point(0, 0);
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.Black);
            myLines = new MyLines[1024];
            myRect = new MyRect[64];
            myCircle = new MyCircle[64];
            nLine = nRect = nCircle = 0;
            color = Color.Black;
            for (int i = 0; i < 1024; i++)
                myLines[i] = new MyLines();
            for (int i = 0; i < 64; i++)
            {
                myRect[i] = new MyRect();
                myCircle[i] = new MyCircle();
            }
        }
        
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (image != null)
            {
                g.DrawImage(image, 0, 0);
            }

            for (int i = 0; i <= nLine; i++)
            {
                pen.Width = myLines[i].GetThick();
                pen.Color = myLines[i].GetColor();
                g.DrawLine(pen, myLines[i].GetPoint1(), myLines[i].GetPoint2());
            }

            for (int i = 0; i <= nRect; i++)
            {
                if (myRect[i].GetFill())
                {
                    brush.Color = myRect[i].GetColor();
                    g.FillRectangle(brush, myRect[i].GetRect());
                }
                else
                {
                    pen.Width = myRect[i].GetThick();
                    pen.Color = myRect[i].GetColor();
                    g.DrawRectangle(pen, myRect[i].GetRect());
                }
            }

            for (int i = 0; i <= nCircle; i++)
            {
                if (myCircle[i].GetFill())
                {
                    brush.Color = myCircle[i].GetColor();
                    g.FillEllipse(brush, myCircle[i].GetRectC());
                }
                else
                {
                    pen.Width = myCircle[i].GetThick();
                    pen.Color = myCircle[i].GetColor();
                    g.DrawEllipse(pen, myCircle[i].GetRectC());
                }
            }

            pen.Width = thick;
            pen.Color = color;
            brush.Color = color;
        }

        public void SavePanelImage(string path)
        {
            Bitmap bitmap = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(bitmap, new Rectangle(0, 0, panel.Width, panel.Height));

            if (image != null)
                image.Dispose();

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                System.Drawing.Imaging.ImageFormat fmt = System.Drawing.Imaging.ImageFormat.Jpeg;
                switch (Path.GetExtension(path))
                {
                    case ".jpg":
                    case ".jpeg":
                        fmt = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;

                    case ".png":
                        fmt = System.Drawing.Imaging.ImageFormat.Png;
                        break;

                    case ".bmp":
                        fmt = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;

                    case ".gif":
                        fmt = System.Drawing.Imaging.ImageFormat.Gif;
                        break;
                }
                bitmap.Save(path, fmt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                bitmap.Dispose();
            }
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            start.X = e.X;
            start.Y = e.Y;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if ((start.X == 0) && (start.Y == 0))
                return;

            finish.X = e.X;
            finish.Y = e.Y;

            if (draw)
            {
                myLines[nLine++].SetPoint(start, finish, thick, color);
                start.X = finish.X;
                start.Y = finish.Y;
            }

            if (line)
                myLines[nLine].SetPoint(start, finish, thick, color);

            if (rect)
                myRect[nRect].SetRect(start, finish, thick, color, isFill);

            if (circle)
                myCircle[nCircle].SetRectC(start, finish, thick, color, isFill);

            panel.Invalidate(true);
            panel.Update();
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (line)
                nLine++;
            if (rect)
                nRect++;
            if (circle)
                nCircle++;
            start.X = 0;
            start.Y = 0;
            finish.X = 0;
            finish.Y = 0;
        }
    }
}
