using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace draw_pattern
{
    public partial class Form1 : Form
    {
        Graphics gr;
        Bitmap bmp;

        List<List<PointF>> grid = new List<List<PointF>>();
        List<PointF> row;
        PointF currentPoint;
        
        float distance = 50;

        List<GradientControl> gradients = new List<GradientControl>();

        Random rand = new Random();
        byte colorDispersion;

        struct PolygonType
        {
            public const byte RIGHT_TRIANGLE = 1;
            public const byte DOWN_TRIANGLE = 2;
            public const byte RIGHT_UP_LONG_TRIANGLE = 4;
            public const byte RIGHT_DOWN_LONG_TRIANGLE = 8;
            public const byte LEFT_UP_LONG_TRIANGLE = 16;
            public const byte LEFT_DOWN_LONG_TRIANGLE = 32;
            public const byte DOWN_RIGHT_LONG_TRIANGLE = 64;
            public const byte DOWN_LEFT_LONG_TRIANGLE = 128;
        }

        byte[][] pattern = new byte[][] {
            new byte[] {1|2|16|32,2,1|2,2|4|8,0,0},
            new byte[] {1|2,1|64|128,1|2,0,4|8|16|32|64|128,1},
            new byte[] {4|8|2,0,0,1|2|16|32,2,1|2},
            new byte[] {0,4|8|16|32|64|128,1,1|2,1|64|128,1|2}
        };

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            colorDispersion = (byte)dispersionTrackBar.Value;
        }

        private Color CalcPointColor(PointF point)
        {
            float summaryLenght = 0;

            for (int i = 0; i < gradients.Count; i++)
            {
                summaryLenght += (float)Math.Sqrt(Math.Pow(point.X - gradients[i].relativeX * pictureBox1.Width, 2) + Math.Pow(point.Y - gradients[i].relativeY * pictureBox1.Height, 2));
            }
            float a = 0, r = 0, g = 0, b = 0;
            for (int i = 0; i < gradients.Count; i++)
            {
                float proportion = (float)Math.Sqrt(Math.Pow(point.X - gradients[i].relativeX*pictureBox1.Width, 2) + Math.Pow(point.Y - gradients[i].relativeY*pictureBox1.Height, 2)) / summaryLenght/*gradients.Count*/;
                a += (1-proportion) * gradients[i].color.A;
                r += (1-proportion) * gradients[i].color.R;
                g += (1-proportion) * gradients[i].color.G;
                b += (1-proportion) * gradients[i].color.B;
            }
            return useNoiseCheckBox.Checked
                ? Color.FromArgb(
                    (byte)Clamp(a + rand.Next(-colorDispersion,colorDispersion), 0, 255), 
                    (byte)Clamp(r + rand.Next(-colorDispersion, colorDispersion), 0, 255), 
                    (byte)Clamp(g + rand.Next(-colorDispersion, colorDispersion), 0, 255), 
                    (byte)Clamp(b + rand.Next(-colorDispersion, colorDispersion), 0, 255)
                    ) 
                : Color.FromArgb(
                    (byte)Clamp(a, 0, 255), 
                    (byte)Clamp(r, 0, 255), 
                    (byte)Clamp(g, 0, 255), 
                    (byte)Clamp(b, 0, 255)
                    );
        }

        private void drawRightTriangle(PointF point0)
        {
            PointF point1 = new PointF(point0.X + distance, point0.Y);
            PointF point2 = new PointF(point0.X + distance / 2, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF middlePoint = new PointF((point0.X + point1.X + point2.X)/3, (point0.Y + point1.Y + point2.Y) / 3);            
            gr.FillPolygon(new SolidBrush(CalcPointColor(middlePoint)), new PointF[] {point0, point1, point2});
        }

        private void drawDownTriangle(PointF point0)
        {
            PointF point1 = new PointF(point0.X + distance / 2, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF point2 = new PointF(point0.X - distance / 2, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF middlePoint = new PointF((point0.X + point1.X + point2.X) / 3, (point0.Y + point1.Y + point2.Y) / 3);
            gr.FillPolygon(new SolidBrush(CalcPointColor(middlePoint)), new PointF[] { point0, point1, point2 });
        }

        private void drawRightUpLongTriangle(PointF point0)
        {
            PointF point1 = new PointF(point0.X + distance, point0.Y);
            PointF point2 = new PointF(point0.X + distance * 1.5f, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF middlePoint = new PointF((point0.X + point1.X + point2.X) / 3, (point0.Y + point1.Y + point2.Y) / 3);
            gr.FillPolygon(new SolidBrush(CalcPointColor(middlePoint)), new PointF[] { point0, point1, point2 });
        }

        private void drawRightDownLongTriangle(PointF point0)
        {
            PointF point1 = new PointF(point0.X + distance * 1.5f, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF point2 = new PointF(point0.X + distance * 0.5f, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF middlePoint = new PointF((point0.X + point1.X + point2.X) / 3, (point0.Y + point1.Y + point2.Y) / 3);
            gr.FillPolygon(new SolidBrush(CalcPointColor(middlePoint)), new PointF[] { point0, point1, point2 });
        }

        private void drawLeftUpLongTriangle(PointF point0)
        {
            PointF point1 = new PointF(point0.X - distance * 1.5f, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF point2 = new PointF(point0.X - distance, point0.Y);
            PointF middlePoint = new PointF((point0.X + point1.X + point2.X) / 3, (point0.Y + point1.Y + point2.Y) / 3);
            gr.FillPolygon(new SolidBrush(CalcPointColor(middlePoint)), new PointF[] { point0, point1, point2 });
        }

        private void drawLeftDownLongTriangle(PointF point0)
        {
            PointF point1 = new PointF(point0.X - distance * 0.5f, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF point2 = new PointF(point0.X - distance * 1.5f, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF middlePoint = new PointF((point0.X + point1.X + point2.X) / 3, (point0.Y + point1.Y + point2.Y) / 3);
            gr.FillPolygon(new SolidBrush(CalcPointColor(middlePoint)), new PointF[] { point0, point1, point2 });
        }

        private void drawDownRightLongTriangle(PointF point0)
        {
            PointF point1 = new PointF(point0.X + distance * 0.5f, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF point2 = new PointF(point0.X, point0.Y + 2 * (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF middlePoint = new PointF((point0.X + point1.X + point2.X) / 3, (point0.Y + point1.Y + point2.Y) / 3);
            gr.FillPolygon(new SolidBrush(CalcPointColor(middlePoint)), new PointF[] { point0, point1, point2 });
        }

        private void drawDownLeftLongTriangle(PointF point0)
        {
            PointF point1 = new PointF(point0.X - distance * 0.5f, point0.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF point2 = new PointF(point0.X, point0.Y + 2 * (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2)));
            PointF middlePoint = new PointF((point0.X + point1.X + point2.X) / 3, (point0.Y + point1.Y + point2.Y) / 3);
            gr.FillPolygon(new SolidBrush(CalcPointColor(middlePoint)), new PointF[] { point0, point1, point2 });
        }

        private void DrawPolygonByType(PointF point, byte type)
        {
            switch (type)
            {
                case PolygonType.RIGHT_TRIANGLE:
                    drawRightTriangle(point); break;
                case PolygonType.DOWN_TRIANGLE:
                    drawDownTriangle(point); break;
                case PolygonType.RIGHT_UP_LONG_TRIANGLE:
                    drawRightUpLongTriangle(point); break;
                case PolygonType.RIGHT_DOWN_LONG_TRIANGLE:
                    drawRightDownLongTriangle(point); break;
                case PolygonType.LEFT_UP_LONG_TRIANGLE:
                    drawLeftUpLongTriangle(point); break;
                case PolygonType.LEFT_DOWN_LONG_TRIANGLE:
                    drawLeftDownLongTriangle(point); break;
                case PolygonType.DOWN_RIGHT_LONG_TRIANGLE:
                    drawDownRightLongTriangle(point); break;
                case PolygonType.DOWN_LEFT_LONG_TRIANGLE:
                    drawDownLeftLongTriangle(point); break;
            }
        }

        private double Clamp(double value, double min, double max)
        {
            return value > max ? max : (value < min ? min : value);
        }

        private void DrawPolygonsByNumber(PointF point, byte type)
        {            
            for (int i = 0; i < 8; i++)
            {
                if ((type & (1 << i)) > 0)
                {
                    DrawPolygonByType(point, (byte)(1 << i));
                }
            }
        }

        private void GenPoints()
        {
            grid.Clear();
            currentPoint = new PointF(-distance,-distance);
            while (currentPoint.Y < pictureBox1.Height)
            {
                row = new List<PointF>();
                while (currentPoint.X < pictureBox1.Width + 1.5*distance)
                {
                    row.Add(currentPoint);
                    currentPoint= new PointF(currentPoint.X + distance,currentPoint.Y);
                }
                currentPoint = new PointF( 
                    grid.Count % 2 == 0 ? -distance+distance/2 : -distance
                    , currentPoint.Y + (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(distance / 2, 2))
                    );
                grid.Add(row);
            }
        }

        private void DrawPoints()
        {
            gr.Clear(Color.White);
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[i].Count; j++)
                {
                    DrawPolygonsByNumber(grid[i][j], (byte)(pattern[i%pattern.Length][j%pattern[i % pattern.Length].Length]));
                }
            }
        }

        private void render()
        {
            gr = Graphics.FromImage(bmp);
            GenPoints();
            DrawPoints();
            pictureBox1.Image = bmp;
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            render();
        }

        private void dispersionTrackBar_Scroll(object sender, EventArgs e)
        {
            colorDispersion = (byte)dispersionTrackBar.Value;
            render();
        }

        private void patternSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            distance = patternSizeTrackBar.Value;
            render();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "PNGImage|*.png";
            DialogResult dr = sd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                pictureBox1.Image.Save(sd.FileName);
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            
            render();
        }

        private void useNoiseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dispersionTrackBar.Enabled = useNoiseCheckBox.Checked;
            render();
        }

        private void addGradientButton_Click(object sender, EventArgs e)
        {
            GradientControl gr = new GradientControl();
            gradients.Add(gr);
            flowLayoutPanel1.Controls.Add(gr);
            gr.ColorChanged += GrEventHandler;
            gr.PositionChanged += GrEventHandler;
            gr.Remove += Gr_Remove;
            render();
        }

        private void Gr_Remove(object sender, EventArgs e)
        {
            gradients.Remove((GradientControl)sender);
            flowLayoutPanel1.Controls.Remove((GradientControl)sender);
            render();
        }

        private void GrEventHandler(object sender, EventArgs e)
        {
            render();
        }
    }
}
