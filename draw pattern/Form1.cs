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

        private PointF FindLineCross(PointF point11, PointF point12, PointF point21, PointF point22)
        {
            float d = (point22.Y - point21.Y) * (point12.X - point11.X) - (point22.X - point21.X) * (point12.Y - point11.Y);
            float a = (point22.X - point21.X) * (point11.Y - point21.Y) - (point22.Y - point21.Y) * (point11.X - point21.X);
            return new PointF(
                point11.X + a * (point12.X - point11.X) / d,
                point11.Y + a * (point12.Y - point11.Y) / d
                );
        }

        private PointF PerpendicularToLineCross(PointF point, PointF linePoint1, PointF linePoint2)
        {
            PointF dir = new PointF(linePoint2.X - linePoint1.X, linePoint2.Y - linePoint1.Y);
            PointF point2 = new PointF();
            if (dir.Y != 0)
            {
                float tmp = dir.X * point.X + dir.Y * point.Y;
                point2.X = point.X != 1 ? 1 : 2;
                point2.Y = (-dir.X * point2.X + tmp) / dir.Y;
            }
            else
            {
                point2.X = point.X;
                point2.Y = linePoint1.Y;
            }

            return FindLineCross(point,point2,linePoint1,linePoint2);
        }

        private float LineLenght(PointF linePoint1, PointF linePoint2)
        {
            return (float)Math.Sqrt(Math.Pow(linePoint1.X-linePoint2.X,2) + Math.Pow(linePoint1.Y - linePoint2.Y, 2));
        }

        private List<GradientControl> FindNearest3(PointF point)
        {
            List<GradientControl> tmp = new List<GradientControl>();
            for (int i = 0; i < gradients.Count; i++)
            {
                tmp.Add(gradients[i]);
            }
            List<GradientControl> list = new List<GradientControl>();
            for (int i = 0; i < 2; i++)
            {
                int mini = 0;
                float minlen = LineLenght(point, new PointF(tmp[0].relativeX*pictureBox1.Width,tmp[0].relativeY*pictureBox1.Height));
                for (int j = 0; j < tmp.Count; j++)
                {
                    if (minlen > LineLenght(point, new PointF(tmp[j].relativeX * pictureBox1.Width, tmp[j].relativeY * pictureBox1.Height)))
                    {
                        mini = j;
                        minlen = LineLenght(point, new PointF(tmp[j].relativeX * pictureBox1.Width, tmp[j].relativeY * pictureBox1.Height));
                    }
                }
                list.Add(tmp[mini]);
                tmp.RemoveAt(mini);
            }
            for (int i = 0; i < gradients.Count; i++)
            {
                if (list.FindIndex(item => item == gradients[i]) < 0)
                {
                    PointF l1p1 = new PointF(list[0].relativeX*pictureBox1.Width,list[0].relativeY*pictureBox1.Height);
                    PointF l1p2 = new PointF(list[1].relativeX * pictureBox1.Width, list[0].relativeY * pictureBox1.Height);
                    PointF pt = new PointF(gradients[i].relativeX*pictureBox1.Width,gradients[i].relativeY*pictureBox1.Height);
                    PointF crosspt = FindLineCross(l1p1, l1p2, point, pt);
                    if (LineLenght(point, pt) < LineLenght(crosspt, pt))
                    {
                        if (list.Count == 2)
                            list.Add(gradients[i]);
                        else
                        {
                            PointF ppp = new PointF(list[2].relativeX * pictureBox1.Width, list[2].relativeY * pictureBox1.Height);
                            if (LineLenght(ppp, point) > LineLenght(point, pt))
                                list[2] = gradients[i];
                        }

                    }
                }
            }
            return list;
        }

        private Color CalcPointColor(PointF point)
        {
            float a = 0, r = 0, g = 0, b = 0;

            if (gradients.Count < 3)
            {
                float summaryLenght = 0;

                for (int i = 0; i < gradients.Count; i++)
                {
                    summaryLenght += (float)Math.Sqrt(Math.Pow(point.X - gradients[i].relativeX * pictureBox1.Width, 2) + Math.Pow(point.Y - gradients[i].relativeY * pictureBox1.Height, 2));
                }
                for (int i = 0; i < gradients.Count; i++)
                {
                    float proportion = (float)Math.Sqrt(Math.Pow(point.X - gradients[i].relativeX * pictureBox1.Width, 2) + Math.Pow(point.Y - gradients[i].relativeY * pictureBox1.Height, 2)) / summaryLenght/*gradients.Count*/;
                    a += (1 - proportion) * gradients[i].color.A;
                    r += (1 - proportion) * gradients[i].color.R;
                    g += (1 - proportion) * gradients[i].color.G;
                    b += (1 - proportion) * gradients[i].color.B;
                }
            }
            else
            {
                List <GradientControl> nearestGradients = FindNearest3(point);
                if (nearestGradients.Count == 3)
                {
                    List<PointF> perpendiculars1 = new List<PointF>();
                    for (int i = 0; i < nearestGradients.Count; i++)
                    {
                        PointF p1 = new PointF(nearestGradients[i].relativeX * pictureBox1.Width, nearestGradients[i].relativeY * pictureBox1.Height);
                        PointF p2 = new PointF(nearestGradients[(i + 1) % nearestGradients.Count].relativeX * pictureBox1.Width, nearestGradients[(i + 1) % nearestGradients.Count].relativeY * pictureBox1.Height);
                        PointF p3 = new PointF(nearestGradients[(i + 2) % nearestGradients.Count].relativeX * pictureBox1.Width, nearestGradients[(i + 2) % nearestGradients.Count].relativeY * pictureBox1.Height);
                        perpendiculars1.Add(PerpendicularToLineCross(p1, p2, p3));
                    }
                    List<PointF> perpendiculars2 = new List<PointF>();
                    for (int i = 0; i < nearestGradients.Count; i++)
                    {
                        PointF p1 = new PointF(nearestGradients[i].relativeX * pictureBox1.Width, nearestGradients[i].relativeY * pictureBox1.Height);
                        perpendiculars2.Add(PerpendicularToLineCross(point, p1, perpendiculars1[i]));
                    }
                    List<float> percentages = new List<float>();
                    for (int i = 0; i < nearestGradients.Count; i++)
                    {
                        PointF p1 = new PointF(nearestGradients[i].relativeX * pictureBox1.Width, nearestGradients[i].relativeY * pictureBox1.Height);
                        percentages.Add(1 - LineLenght(p1, perpendiculars2[i]) / LineLenght(p1, perpendiculars1[i]));
                    }
                    for (int i = 0; i < nearestGradients.Count; i++)
                    {
                        a += percentages[i] * nearestGradients[i].color.A;
                        r += percentages[i] * nearestGradients[i].color.R;
                        g += percentages[i] * nearestGradients[i].color.G;
                        b += percentages[i] * nearestGradients[i].color.B;
                    }
                }
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
            gr.Clear(Color.Black);
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
