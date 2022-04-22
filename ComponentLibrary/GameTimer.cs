using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComponentLibrary
{
    public partial class GameTimer: UserControl
    {
        public GameTimer()
        {
            InitializeComponent();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void ResetLabel()
        {
            display.Text = "00:00:00";
        }

        public String ReturnTime()
        {
            return display.Text;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan time = new TimeSpan();
            if (TimeSpan.TryParse(display.Text, out time))
            {
                int hours = time.Hours;
                int minutes = time.Minutes;
                int seconds = time.Seconds;

                seconds++;
                if (seconds == 60)
                {
                    seconds = 0;
                    minutes++;
                    if (minutes == 60)
                    {
                        minutes = 0;
                        hours++;
                    }
                }

                if (hours < 10) display.Text = $"0{hours}:";
                else display.Text = $"{hours}:";

                if (minutes < 10) display.Text += $"0{minutes}:";
                else display.Text += $"{minutes}:";

                if (seconds < 10) display.Text += $"0{seconds}";
                else display.Text += $"{seconds}";
            }
        }
    }
}
