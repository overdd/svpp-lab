using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SVPP_LAB_4
{
    public partial class HorseControl : UserControl
    {
        static bool singleTon = false;
        public int X
        {
            get { return (int)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(int), typeof(HorseControl), new PropertyMetadata(0));

        public int Y
        {
            get { return (int)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(int), typeof(HorseControl), new PropertyMetadata(0));


        readonly DispatcherTimer timer = new DispatcherTimer();
        public int StartPointX = 350;
        public int StartPointY = 350;
        public int angle = 90;

        public int Speed;
        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof(int), typeof(HorseControl), new PropertyMetadata(0));


        float DEG2RAD = (float)(3.14159 / 180);
        double degInRad;

        public void Start()
        {
            X = StartPointX;
            Y = StartPointY;
            Pause();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, Speed);
            timer.Start();
        }
        public void Pause()
        {
            timer.Tick -= new EventHandler(Timer_Tick);
        }

        public void UnPause()
        {
            timer.Tick += new EventHandler(Timer_Tick);
        }

        public void Restart()
        {
            Pause();
            X = StartPointX;
            Y = StartPointY;
            Start();
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            degInRad = angle * DEG2RAD;
            X = 330 + (int)(Math.Cos(degInRad) * 200);
            Y = 190 + (int)(Math.Sin(degInRad) * 200);
            angle++;
        }

        public HorseControl()
        {
            if (!singleTon)
                DefaultStyleKeyProperty.OverrideMetadata(typeof(HorseControl), new FrameworkPropertyMetadata(typeof(HorseControl)));
            singleTon = true;
        }

    }

}
