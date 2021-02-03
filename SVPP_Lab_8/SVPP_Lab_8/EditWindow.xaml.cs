using System.Windows;


namespace SVPP_Lab_8
{
    public partial class EditWindow : Window
    {
        readonly Railway railway;
        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(Railway railway) : this()
        {
            this.railway = railway;
            grid.DataContext = railway;
        }

        private void ButtonOkClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
