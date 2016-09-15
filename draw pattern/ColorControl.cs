using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace draw_pattern
{
    public partial class GradientControl : UserControl
    {
        private float _x, _y;

        public float relativeX {
            get {
                return _x;
            }
            set {
                _x = value;
            }
        }

        public float relativeY
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public Color color {
            get {
                return pictureBox1.BackColor;
            }
            set {
                pictureBox1.BackColor = value;                
            }
        }
        public GradientControl()
        {
            InitializeComponent();
            this.color = Color.Black;
            relativeX = (float)trackBar1.Value / 255;
            relativeY = (float)(255 - trackBar2.Value) / 255;
            label1.Text = "0:1";
        }
        public GradientControl(Color color)
        {
            InitializeComponent();
            this.color = color;
            relativeX = (float)trackBar1.Value / 255;
            relativeY = (float)(255 - trackBar2.Value) / 255;
            label1.Text = "0:1";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            DialogResult dr = cd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.color = cd.Color;
                OnColorChanged();
            }
        }



        #region events
        public event EventHandler ColorChanged;
        private void OnColorChanged()
        {
            if (ColorChanged != null)
            {
                ColorChanged(this, new EventArgs());
            }
        }

        public event EventHandler PositionChanged;

        private void OnPositionChanged()
        {
            if (ColorChanged != null)
            {
                PositionChanged(this, new EventArgs());
            }
        }
        
        public event EventHandler Remove;

        private void OnRemove()
        {
            if (ColorChanged != null)
            {
                Remove(this, new EventArgs());
            }
        }
        #endregion


        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            relativeY = (float)(255 - trackBar2.Value) / 255;
            label1.Text = relativeX.ToString("#.00")+":"+relativeY.ToString("#.00");
            OnPositionChanged();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnRemove();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            relativeX = (float)trackBar1.Value / 255;
            label1.Text = relativeX.ToString("#.00") + ":" + relativeY.ToString("#.00");
            OnPositionChanged();
        }
    }
}
