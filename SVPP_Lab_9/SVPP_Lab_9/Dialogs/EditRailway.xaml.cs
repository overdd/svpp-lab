using System;
using System.Windows;
using SVPP_Lab_9_BusinessLayer.Models;

namespace SVPP_Lab_9.Dialogs
{

    public partial class EditRailway : Window
    {
        public EditRailway(RailwayViewModel railway)
        {
            InitializeComponent();
            railway.FullName = "Railway " + new Random().Next();
        }


        private void ButtonClick(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
