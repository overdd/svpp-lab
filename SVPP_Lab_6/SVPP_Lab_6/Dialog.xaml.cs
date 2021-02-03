using System;
using System.Windows;
using System.Windows.Controls;

namespace SVPP_Lab_6
{

    public partial class Dialog : Window
    {
        private int _Value;

        public int ValueCheck
        {
            get { return _Value; }
            set
            {
                if (value <= 0) { throw new ArgumentException("Error! "); }
                _Value = value;
            }
        }
        public int Value
        {
            get { return Convert.ToInt32(nForm.Text.ToString()); }
        }
        private void ValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }
        public Dialog()
        {
            InitializeComponent();
        }
        private void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
