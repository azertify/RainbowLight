using Blynclight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RainbowLight
{
    class Blynclight
    {
        private BlynclightController controller;

        private int deviceCount;

        private double val;
        private double sat;
        private double hue;

        private object valLock = new object();
        public Blynclight()
        {
            hue = 0.0;
            val = 0.0;
            sat = 1.0;

            controller = new BlynclightController();
            deviceCount = controller.InitBlyncDevices();

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(10);
                    decreaseBrightness();
                }
            });
            thread.Start();
        }

        public void changeColor()
        {
            Random rand = new Random();
            lock (valLock)
            {
                hue = rand.NextDouble() * 360;
                val = 0.6; // 0.6 is a nice brightness
                setColor();
            }
        }
        private void setColor()
        {
            int red = 0;
            int green = 0;
            int blue = 0;

            Color.HsvToRgb(hue, sat, val, out red, out green, out blue);

            controller.TurnOnRGBLights(deviceCount - 1, (byte)red, (byte)green, (byte)blue);
        }

        private void decreaseBrightness()
        {
            lock (valLock)
            {
                val -= 0.01;
                if (val < 0)
                {
                    val = 0;
                }
                
                setColor();
            }
        }
    }
}
