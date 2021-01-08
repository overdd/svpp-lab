using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SVPP_Lab_3._1
{
    public partial class MainWindow : Window
    {
        ObservableCollection<string> results;
        Values values;
        double _x, _S, _Y = 0.0;
        int _k, n = 0;

        Func<double, int, double> Sx = (double x, int k) => Math.Pow(x, k) * Math.Pow(Math.Sin(Math.PI / 4), k);
        Func<double, double> Yx = (double x) => x * Math.Sin(Math.PI / 4) / (1 - 2 * x * Math.Cos(Math.PI / 4) + x * x);

        public MainWindow()
        {
            InitializeComponent();
            values = new Values();
            this.DataContext = values;
            results = new ObservableCollection<string>();
            lResult.DataContext = results;
        }

        private void ButtonCalc_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                if (values.StepH == 0)
                {
                    throw new ArgumentException($"Step = {values.StepH}");
                }
                else if (values.NQuantity == 0)
                {
                    throw new ArgumentException($"N = {values.NQuantity}");
                }

                results.Clear();

                for (_x = values.StartX; _x <= values.StopX; _x += values.StepH)
                {
                    for (_k = 1; _k <= values.NQuantity; _k++)
                    {
                        _S += Sx(_x, _k);
                    }
                    _Y = Yx(_x);

                    results.Add($"Result №{++n}: S(x)={_S}  Y(x) = {_Y}");
                }
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show($"Error! {exc.Message}");
            }
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            textbox.SelectionStart = 0;
            textbox.SelectionLength = textbox.Text.Length;
        }

        private void TextBoxN_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());
        }
    }

    public class Values : IDataErrorInfo
    {
        public double StartX { get; set; }
        public double StopX { get; set; }
        public double StepH { get; set; }
        public int NQuantity { get; set; }

        bool isStartXChecked;
        bool isStopXChecked;
        bool isNQuantityChecked;

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string error = String.Empty;

                switch (columnName)
                {
                    case "StopX":
                        if (StopX <= StartX && isStartXChecked)
                            error = "Stop X value should be greater than Start X value";
                        isStartXChecked = true;
                        break;
                    case "StepH":
                        if (StepH > (StopX - StartX) && isStopXChecked)
                            error = "Step can't be less than segment Stop-Start";
                        else if (StepH == 0 && isStopXChecked)
                            error = "Step should be greater than 0";
                        isStopXChecked = true;
                        break;
                    case "NQuantity":
                        if (NQuantity <= 5 && isNQuantityChecked)
                            error = "N should be greater than 5";
                        isNQuantityChecked = true;
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean isDouble(double value)
        {
            Regex re = new Regex(@"\d*.\d*");
            return re.IsMatch(value.ToString());
        }
    }
}
