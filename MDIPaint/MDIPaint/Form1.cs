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
    public partial class Parent : Form
    {
        private Child child = null;
        private Button[] button = new Button[5];
        private Button btnNoFill, btnSolidFill;
        private Panel panelWidth = null;
        private Panel panelFill = null;
        private bool isWidth = false;
        private bool isFill = false;
        private int thick = 1;

        public Parent()
        {
            InitializeComponent();

            panelWidth = new Panel();
            panelWidth.Size = new Size(27 * 5, 27);
            panelWidth.Location = new Point(26, 183);
            for (int i = 1; i <= 5; i++)
            {
                Button btn = new Button();
                btn.Text = i.ToString();
                btn.Size = new Size(25, 25);
                btn.Location = new Point(1 + 27 * (i - 1), 1);
                btn.Click += (sender, e) => 
                {
                    for (int j = 0; j < 5; j++)
                        button[j].Enabled = true;
                    btn.Enabled = false;
                    if (child != null)
                        child.thick = Int32.Parse(btn.Text);
                    this.thick = Int32.Parse(btn.Text);
                };
                button[i - 1] = btn;
                panelWidth.Controls.Add(btn);
            }
            button[0].Enabled = false;

            panelFill = new Panel();
            panelFill.Location = new Point(26, 205);
            panelFill.Size = new Size(145, 27);

            btnNoFill = new Button();
            btnNoFill.Text = "채우기 없음";
            btnNoFill.Size = new Size(80, 25);
            btnNoFill.Location = new Point(1, 1);
            btnNoFill.Click += (sender, e) =>
            {
                btnNoFill.Enabled = false;
                btnSolidFill.Enabled = true;
                if (child != null)
                    child.isFill = false;
            };
            btnNoFill.Enabled = false;

            btnSolidFill = new Button();
            btnSolidFill.Text = "단색";
            btnSolidFill.Size = new Size(50, 25);
            btnSolidFill.Location = new Point(90, 1);
            btnSolidFill.Click += (sender, e) =>
            {
                btnSolidFill.Enabled = false;
                btnNoFill.Enabled = true;
                if (child != null)
                    child.isFill = true;
            };

            panelFill.Controls.Add(btnNoFill);
            panelFill.Controls.Add(btnSolidFill);
        }

        private void Parent_Load(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Image files (*.jpg; *.jpeg) | *.jpg; *.jpeg; |Bitmap Image files (*.bmp; *gif) | *.bmp; *gif; |Lossless Image file (*.png) | *.png;";
            saveFileDialog.Filter = "Image files (*.jpg; *.jpeg) | *.jpg; *.jpeg; |Bitmap Image files (*.bmp; *gif) | *.bmp; *gif; |Lossless Image file (*.png) | *.png;";
        }

        private void 새로만들기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            child = new Child();
            child.MdiParent = this;
            child.WindowState = FormWindowState.Maximized;
            child.Show();
            저장SToolStripMenuItem.Enabled = true;
            이미지지우기ToolStripMenuItem.Enabled = true;
            child.SetupVar();
        }

        private void 열기OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(openFileDialog.FileName);
                child = new Child(image);
                child.MdiParent = this;

                child.Text = Path.GetFileName(openFileDialog.FileName);
                child.Size = new Size(image.Width + 16, image.Height + 39);

                child.Show();
                저장SToolStripMenuItem.Enabled = true;
                이미지지우기ToolStripMenuItem.Enabled = true;
                child.SetupVar();

                child.GetPanel().Invalidate(true);
                child.GetPanel().Update();
            }
        }

        private void 저장SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                child = (Child)(this.ActiveMdiChild);
                child.SavePanelImage(saveFileDialog.FileName);
                child.Text = Path.GetFileName(saveFileDialog.FileName);
            }
        }

        private void 끝내기XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 이미지지우기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            child = (Child)(this.ActiveMdiChild);
            child.SetImage(null);
            child.SetupVar();
            child.GetPanel().Invalidate(true);
            child.GetPanel().Update();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                child.color = colorDialog.Color;
                btnColor.BackColor = colorDialog.Color;
            }
        }

        private void btnPen_Click(object sender, EventArgs e)
        {
            child.draw = true;
            child.line = false;
            child.rect = false;
            child.circle = false;
            child.thick = 1;
        }

        private void btnBrush_Click(object sender, EventArgs e)
        {
            child.draw = true;
            child.line = false;
            child.rect = false;
            child.circle = false;
            child.thick = 5;
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            child.draw = false;
            child.line = true;
            child.rect = false;
            child.circle = false;
            child.thick = this.thick;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            child.draw = false;
            child.line = false;
            child.rect = true;
            child.circle = false;
            child.thick = this.thick;
        }

        private void btnOval_Click(object sender, EventArgs e)
        {
            child.draw = false;
            child.line = false;
            child.rect = false;
            child.circle = true;
            child.thick = this.thick;
        }

        private void btnWidth_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(panelFill);
            isFill = false;
            if (!isWidth)
            {
                isWidth = true;
                this.Controls.Add(panelWidth);
            }
            else
            {
                isWidth = false;
                this.Controls.Remove(panelWidth);
            }
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(panelWidth);
            isWidth = false;
            if (!isFill)
            {
                isFill = true;
                this.Controls.Add(panelFill);
            }
            else
            {
                isFill = false;
                this.Controls.Remove(panelFill);
            }
        }
    }
}
