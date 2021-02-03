using System;
using System.Threading;

namespace SVPP_Lab_6
{
    class Integral
    {
        private readonly int N;
        private readonly double a = 0;
        private readonly double b = 0;
        private double y = 0;
        private double h = 0;
        private bool _isCompleted = false;
        public event Action<double> ProgressChanged;
        public event Action<bool, double> IntegralCompleted;

        public Integral(double a, double b, int N)
        {
            this.a = a;
            this.b = b;
            this.N = N;
        }
        public void Formula()
        {
            h = (b - a) / N;
            for (double i = a; i <= b; i += h)
            {
                y += Math.Cos(2 * i) * h;
                ProgressChanged(i + h);
                Thread.Sleep(100);
            }
            _isCompleted = true;
            IntegralCompleted(_isCompleted, y);
        }
    }
}

