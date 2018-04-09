using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace 切
{
    public partial class Form1 : Form
    {
        private Point mouseDownPoint;
        private bool isSelected;
        private bool opened=false;
        private bool isChangingSize = false;
        private Bitmap open_img;
        private Bitmap picBox_img;
        private Bitmap cursor_img;
        private int cut_size = 512;
        private double zoom_rate=1;
        private string last_open_dir = "";
        private string save_cut_img_dir = "";
        public Form1()
        {
            InitializeComponent();
            this.picBox.MouseWheel += picBox_MouseWheel;
            cursor_img = new Bitmap(@"C:\Users\Morris\Documents\Visual Studio 2013\Projects\切\切\cursor.png");

        }
        public void SetCursor(Bitmap cursor, int tSize)
        {
            
            Bitmap myNewCursor = new Bitmap(tSize, tSize);
            Graphics g = Graphics.FromImage(myNewCursor);
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.DrawImage(cursor, 0, 0, tSize, tSize);
            this.picBox.Cursor = new Cursor(myNewCursor.GetHicon());
            g.Dispose();
            myNewCursor.Dispose();
        }
        public Bitmap im_resize(Bitmap img, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(result);
            g.Clear(Color.White);
            g.DrawImage(img, 0, 0, width, height);
            g.Dispose();
            return result;
        }
        public void reDrawPicBoxImg(int width=-1,int height=-1)
        {
            width = width == -1 ? Convert.ToInt32(Math.Ceiling(open_img.Width*zoom_rate) ): width;
            height = height == -1 ? Convert.ToInt32(Math.Ceiling(open_img.Height * zoom_rate)) : height;
            picBox.Height = height;
            picBox.Width = width;
            picBox.Image.Dispose();
            picBox.Image = im_resize(picBox_img, width, height);
        }
        void picBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!opened) return;
            if (isChangingSize )
            {
                if (cut_size % 256 != 0) cut_size = cut_size / 256 * 256;
                int tmpt = e.Delta > 0 ? cut_size + 256 : cut_size - 256;
                if (tmpt < 256 || tmpt > 2048) return;
                cut_size = tmpt;
                this.cutSizeTextBox.Text = "cut_size" + cut_size;
                SetCursor(cursor_img, Convert.ToInt32(Math.Ceiling(cut_size * zoom_rate)));
                return;
            }
            int Height=0;
            int Width=0;
            double tmp;
            tmp= e.Delta > 0 ? zoom_rate +0.1 : zoom_rate -0.1;
            if (tmp > 2 || tmp < 0.05) return;
            else { zoom_rate = tmp;  }
            Height = Convert.ToInt32(Math.Ceiling(open_img.Height * zoom_rate));
            Width = Convert.ToInt32(Math.Ceiling(open_img.Width * zoom_rate));
            if (Height < 200){ return;}
            SetCursor(cursor_img, Convert.ToInt32(Math.Ceiling(cut_size * zoom_rate)));
            reDrawPicBoxImg(Width, Height);
            zoomRateTextBox.Text = "img_size:"+open_img.Width+"*"+open_img.Height+"   zoomrate:" + zoom_rate+ "  picBox:"+Width+"*"+Height;
        }
        
        private void openFileToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (opened)
            {
                Image tmp_img = Image.FromHbitmap(picBox_img.GetHbitmap());
                string name = save_cut_img_dir+"/" + fileNameTextBox.Text+".jpg";
                tmp_img.Save(name, System.Drawing.Imaging.ImageFormat.Jpeg);
                tmp_img.Dispose();
                picBox_img.Dispose();
                open_img.Dispose();
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = last_open_dir==""?System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location):last_open_dir;
            openFileDialog.Filter = "png文件|*.png;*.jpg";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            string fName = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fName = openFileDialog.FileName;
                last_open_dir = System.IO.Path.GetDirectoryName(fName);
                save_cut_img_dir = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "/" + System.IO.Path.GetFileName(fName);
                if (!System.IO.Directory.Exists(save_cut_img_dir))
                {
                    System.IO.Directory.CreateDirectory(save_cut_img_dir);
                }
                open_img = new Bitmap(fName);
                picBox.Top = 0;
                picBox.Left = 0;
                this.picBox_img = new Bitmap(open_img);
                this.picBox.Image = im_resize(picBox_img,picBox_img.Width,picBox_img.Height);
                SetCursor(cursor_img, cut_size);
                this.picBox.Width = open_img.Width;
                this.picBox.Height = open_img.Height;
                zoom_rate = 1;
                zoomRateTextBox.Text =  "img_size:"+open_img.Width+"*"+open_img.Height+"   zoomrate:" + zoom_rate+ "  picBox:"+Width+"*"+Height;
                opened = true;
                fileNameTextBox.Text =  System.IO.Path.GetFileName(fName);
            }
        }

        Point get_Boundary(int x, int y)
        {
            int top = y - cut_size / 2;
            int left = x - cut_size / 2;
            if(top>0 && left>0 && left+cut_size<open_img.Width &&top + cut_size < open_img.Height)
            {
                return new Point(left, top);
            }
            return new Point(-1,-1);
        }
        private void picBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!opened) return;
            if (e.Button == MouseButtons.Left)
            {
                Point mouseLoc=this.PointToClient(Control.MousePosition);
                int tmp_x = mouseLoc.X - picBox.Left;
                int tmp_y = mouseLoc.Y - picBox.Top;
                int X = Convert.ToInt32(Math.Ceiling((tmp_x) / zoom_rate));
                int Y = Convert.ToInt32(Math.Ceiling((tmp_y) / zoom_rate));
                Point left_top = get_Boundary(X, Y);
                if (left_top.X != -1)
                {
                    Bitmap cut_img = new Bitmap(cut_size, cut_size);
                    Graphics g = Graphics.FromImage(cut_img);
                    g.DrawImage(open_img,0,0,new Rectangle(left_top.X,left_top.Y,cut_size,cut_size),GraphicsUnit.Pixel);

                    Graphics g2 = Graphics.FromImage(picBox_img);
                    g2.DrawLines(new Pen(Color.Gold, 4), new Point[] { new Point(left_top.X,left_top.Y), new Point(left_top.X, left_top.Y + cut_size), new Point(left_top.X + cut_size, left_top.Y + cut_size), new Point(left_top.X + cut_size, left_top.Y),new Point(left_top.X , left_top.Y ) });
                    reDrawPicBoxImg();

                    Image saveImage = Image.FromHbitmap(cut_img.GetHbitmap());
                    string name = DateTime.Now.ToString("hh：mm：ss");
                    name = save_cut_img_dir+"/" + name+ "x.jpg";
                    saveImage.Save(name, System.Drawing.Imaging.ImageFormat.Jpeg);
                    saveImage.Dispose();
                    cut_img.Dispose();
                    g.Dispose();
                    return;
                }
                string tmp = "";
                tmp += X + " " +Y;
                MessageBox.Show(tmp);
            }
        }
        private void PicBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                isChangingSize = true;
            }
        }

        private void PicBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected)
            {
                this.picBox.Left = this.picBox.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.picBox.Top = this.picBox.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }
        }

        private void PicBox_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;
            if (e.Button == MouseButtons.Right)
            {
                isChangingSize = false;
            }
        }

        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, picBox.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
        }

        private void cutSizeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!opened)
            {
                MessageBox.Show("please open an image first");
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    cut_size =Convert.ToInt32(cutSizeTextBox.Text);
                    cutSizeTextBox.Text = "cut_size:" + cut_size;
                }
                catch
                {
                    MessageBox.Show("please input pure number");
                    this.cutSizeTextBox.Text = "cut_size:512(default)";
                    this.SetCursor(cursor_img,  Convert.ToInt32(Math.Ceiling(512 * zoom_rate)));
                }
                if(cut_size>picBox_img.Height || cut_size > picBox_img.Width ||cut_size<27)
                {
                    MessageBox.Show("cutsize is too big or too small, it should less than " + (picBox_img.Height < picBox_img.Width ? picBox_img.Height : picBox_img.Width)+"and bigger than 27");
                    this.cutSizeTextBox.Text = "cut_size:512(default)";
                    cut_size = 512;
                }
                this.SetCursor(cursor_img,  Convert.ToInt32(Math.Ceiling(cut_size * zoom_rate)));

            }
        }


    }
}
