using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D2RModding_SpriteEdit
{
    public partial class HSVEditForm : Form
    {
        public event EventHandler OKClicked;
        public event EventHandler HueChanged;
        public event EventHandler SaturationChanged;
        public event EventHandler ValueChanged;
        public event EventHandler CancelClicked;

        public HSVEditForm(float pendingHue, float pendingSaturation, float pendingValue)
        {
            InitializeComponent();

            hueTrackBar.Value = (int)(pendingHue * 100.0f);
            saturationTrackBar.Value = (int)(pendingSaturation * 100.0f);
            brightnessTrackBar.Value = (int)(pendingValue * 100.0f);
        }

        public void onOKClicked(object sender, EventArgs e)
        {
            HSVOkClickEvent e1 = new HSVOkClickEvent();
            OKClicked.Invoke(this, e1);
            Close();
        }
        public void onCancelClicked(object sender, EventArgs e)
        {
            HSVCancelClickEvent e1 = new HSVCancelClickEvent();
            CancelClicked.Invoke(this, e1);
            Close();
        }
        public void onHueChanged(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            HSVHueChangeEvent e1 = new HSVHueChangeEvent(tb.Value / 100.0f);
            HueChanged.Invoke(this, e1);
        }
        public void onSaturationChanged(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            HSVSaturationChangeEvent e1 = new HSVSaturationChangeEvent(tb.Value / 100.0f);
            SaturationChanged.Invoke(this, e1);
        }
        public void onValueChanged(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            HSVValueChangeEvent e1 = new HSVValueChangeEvent(tb.Value / 100.0f);
            ValueChanged.Invoke(this, e1);
        }

        public class HSVOkClickEvent : EventArgs
        {

        }
        public class HSVCancelClickEvent : EventArgs
        {

        }
        public class HSVHueChangeEvent : EventArgs
        {
            public float currentValue;
            public HSVHueChangeEvent(float value)
            {
                currentValue = value;
            }
        }
        public class HSVSaturationChangeEvent : EventArgs
        {
            public float currentValue;
            public HSVSaturationChangeEvent(float value)
            {
                currentValue = value;
            }
        }
        public class HSVValueChangeEvent : EventArgs
        {
            public float currentValue;
            public HSVValueChangeEvent(float value)
            {
                currentValue = value;
            }
        }
    }
}
