using System.Windows;
using System.Collections.ObjectModel;

namespace SVPP_Lab_7
{
    public partial class MainWindow : Window
    {
        readonly ObservableCollection<Railway> Railways;
        public MainWindow()
        {
            Railways = new ObservableCollection<Railway>();
            InitializeComponent();
            lBox.DataContext = Railways;
        }
        void FillData()
        {
            Railways.Clear();
            foreach (var item in Railway.GetAllRailways())
            {
                Railways.Add(item);
            }
        }
        private void ButtonFillClick(object sender, RoutedEventArgs e)
        {
            FillData();
        }
        private void ButtonAddClick(object sender, RoutedEventArgs e)
        {
            var railway = new Railway()
            {
                Name = "British Royal Railways",
                TrainQuantity = 50,
                RailwayLength = 7500,
                StaffNumber = 120
            };
            railway.Insert();
            FillData();
        }

        private void ButtonEditClick(object sender, RoutedEventArgs e)
        {
            var railway = (Railway)lBox.SelectedItem;
            railway.Name = "British renamed railways";
            railway.Update();
            FillData();
        }
        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            var id = ((Railway)lBox.SelectedItem).Id; Railway.Delete(id);
            FillData();
        }
    }
}
