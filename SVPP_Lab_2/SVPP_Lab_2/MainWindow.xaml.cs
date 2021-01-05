using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;


namespace SVPP_Lab_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Run(object sender, MouseEventArgs e)
        {
            Random randomValue = new Random();
            NoButton.SetValue(Grid.RowProperty, randomValue.Next(0, 6));
            NoButton.SetValue(Grid.ColumnProperty, randomValue.Next(0, 6));
        }

        private void Button_MessageBox(object sender, EventArgs e)
        {
            MessageBox.Show("Okay!");
        }
    }
}
