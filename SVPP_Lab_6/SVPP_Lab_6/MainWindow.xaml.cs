using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;


namespace SVPP_Lab_6
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Integral _integral;
        private static int N;
        private static double a, b;

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                a = Convert.ToDouble(aBox.Text.ToString());
                b = Convert.ToDouble(bBox.Text.ToString());

                var dialog = new Dialog();
                dialog.ShowDialog();
                N = dialog.Value;

                _integral = new Integral(a, b, N);

                _integral.ProgressChanged += Progress;
                _integral.IntegralCompleted += Complite;

                Thread thread = new Thread(_integral.Formula)
                {
                    IsBackground = true,
                    Priority = ThreadPriority.Normal
                };
                thread.Start();
                Start.IsEnabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Complite(bool isCompleted, double result)
        {
            Action action = () =>
            {
                Start.IsEnabled = isCompleted;
                Result.Text = Convert.ToString(result);
            };
            Start.Dispatcher.BeginInvoke(action);

        }

        private void Progress(double progress)
        {
            Action action = () =>
            {
                ProgressBar.Value = progress;
                ProgressBar.Minimum = a;
                ProgressBar.Maximum = b;
            };
            ProgressBar.Dispatcher.BeginInvoke(action);
        }
    }
}
