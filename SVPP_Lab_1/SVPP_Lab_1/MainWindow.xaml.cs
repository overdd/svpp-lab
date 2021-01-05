using System;
using System.Windows;
using System.Windows.Controls;


namespace SVPP_Lab_1
{
    public partial class MainWindow : Window
    {
        public string firstValue_string, secondValue_string, operand;
        public double firstValue_double, secondValue_double;
        public bool begin = true, first = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Number_Button_Click(object sender, RoutedEventArgs args)
        {
            Button currentButton = (Button)sender;
            if (!(DisplayField.Text.Contains(",") && (currentButton.Content.ToString() == ",")))
            {
                if (DisplayField.Text == "0" && !(currentButton.Content.ToString() == ","))
                {
                    DisplayField.Text = currentButton.Content.ToString();
                    Description.Text += currentButton.Content.ToString();
                }
                else
                {
                    DisplayField.Text += currentButton.Content.ToString();
                    Description.Text += currentButton.Content.ToString();
                }
            }
        }
        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            begin = true;
            first = true;
            operand = null;
            DisplayField.Text = "0";
            Description.Text = "";
        }
        private void MathAction_Button_Click(object sender, RoutedEventArgs e)
        {
            operand = ((Button)sender).Content.ToString();
            if (first)
            {
                firstValue_string = DisplayField.Text;
                first = false;
                DisplayField.Text = "";
            }
            else
                secondValue_string = DisplayField.Text;
            Description.Text += operand;
        }
        private void Calculations_Button_Click(object sender, RoutedEventArgs e)
        {
            Button currentButton = (Button)sender;
            if (!(currentButton.Content.ToString() == "="))
                operand = currentButton.Content.ToString();

            if (first)
            {
                firstValue_string = DisplayField.Text;
                first = false;
            }
            else
                secondValue_string = DisplayField.Text;
            Description.Text += "=";
            Description.Text += doTheMath();
        }
        private string doTheMath()
        {
            try
            {
                if (begin)
                {
                    firstValue_double = Convert.ToDouble(firstValue_string);
                    begin = false;
                }

                if (operand == "+" || operand == "-" || operand == "*" || operand == "/")
                {
                    secondValue_double = Convert.ToDouble(secondValue_string);
                }


                switch (operand)
                {
                    case "+":
                        firstValue_double += secondValue_double;
                        break;
                    case "-":
                        firstValue_double -= secondValue_double;
                        break;
                    case "*":
                        firstValue_double = firstValue_double * secondValue_double;
                        break;
                    case "/":
                        if (secondValue_double == 0)
                            throw new DivideByZeroException();
                        else
                            firstValue_double = firstValue_double / secondValue_double;
                        break;
                    case "√х":
                        firstValue_double = System.Math.Sqrt(firstValue_double);
                        break;
                    case "х²":
                        firstValue_double = System.Math.Pow(firstValue_double, 2);
                        break;
                    case "1/x":
                        if (firstValue_double == 0)
                            throw new DivideByZeroException();
                        else
                            firstValue_double = 1 / firstValue_double;
                        break;
                    case "sin":
                        if ((bool)Grad.IsChecked)
                            firstValue_double = (System.Math.Sin((firstValue_double * Math.PI / 180)));
                        else
                            firstValue_double = System.Math.Sin(firstValue_double);
                        break;
                    case "cos":
                        if ((bool)Grad.IsChecked)
                            firstValue_double = (System.Math.Cos((firstValue_double * Math.PI / 180)));
                        else
                            firstValue_double = System.Math.Cos(firstValue_double);
                        break;
                    case "tan":
                        if (tan_zero(firstValue_double))
                            throw new DivideByZeroException();
                        else
                        {
                            if ((bool)Grad.IsChecked)
                                firstValue_double = (System.Math.Tan((firstValue_double * Math.PI / 180)));
                            else
                                firstValue_double = System.Math.Tan(firstValue_double);
                        }
                        break;
                }
                DisplayField.Text = firstValue_double.ToString();
            }

            catch (DivideByZeroException)
            {
                DisplayField.Text = "Error!";
                first = true;
                begin = true;
                firstValue_double = 0;
                secondValue_double = 0;
                return "Cannot divide to zero";
            }
            catch (InvalidCastException)
            {
                DisplayField.Text = "Error!";
                Description.Text = "Wrong format";
                first = true;
                begin = true;
                firstValue_double = 0;
                secondValue_double = 0;
                return "Wrong format";
            }
            catch (Exception ex)
            {
                DisplayField.Text = "Error!";
                Description.Text = ex.Message;
                first = true;
                begin = true;
                firstValue_double = 0;
                secondValue_double = 0;
                return ex.Message;
            }
            return firstValue_double.ToString();
        }

        bool tan_zero(double Zif)
        {
            double part;

            if (!(bool)Grad.IsChecked)
                Zif = Zif * 180 / Math.PI;

            part = Zif / 90;
            if (part == Math.Truncate(part))
            {
                part = part / 2;

                if (part == Math.Truncate(part))
                    return false;
                else
                    return true;
            }

            else
                return false;
        }
    }
}
