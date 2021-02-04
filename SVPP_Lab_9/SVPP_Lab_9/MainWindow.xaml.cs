using System;
using System.Windows;
using System.Collections.ObjectModel;
using SVPP_Lab_9_BusinessLayer.Models;


namespace SVPP_Lab_9
{
    public partial class MainWindow : Window
    {
        readonly ObservableCollection<CartelViewModel> cartels;
        ICartelHandler cartelHandler;
        public MainWindow()
        {
            InitializeComponent();
            cartelHandler = new CartelHandler("TestDbConnection");
            cartels = cartelHandler.GetAll();
            cBoxCartel.DataContext = cartels;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var railway = new RailwayViewModel
            {
                FoundationDate = new DateTime(1990, 01, 01)
            };
            var dialog = new Dialogs.EditRailway(railway);
            var result = dialog.ShowDialog();
            if (result == true)
            {
                var cartel = (CartelViewModel)cBoxCartel.SelectedItem;
                cartel.Railways.Add(railway);
                cartelHandler.AddRailwayToCartel(cartel.CartelId, railway);
                dialog.Close();
            }
        }
    }
}
