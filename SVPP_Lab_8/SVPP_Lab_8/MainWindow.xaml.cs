using System.Windows;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SVPP_Lab_8
{
    public partial class MainWindow : Window
    {
        EntityContext context;
        public MainWindow()
        {
            context = new EntityContext("TestDbConnection");
            InitializeComponent();
            context.railways.Load();
            dataGrid.DataContext = context.railways.Local;
        }

        private void ButtonAddClick(object sender, RoutedEventArgs e)
        {
            var railway = new Railway();
            EditWindow ew = new EditWindow(railway);
            var result = ew.ShowDialog();
            if (result == true)
            {
                context.Zkhs.Add(railway);
                context.SaveChanges();
                ew.Close();
            }
        }

        private void ButtonEditClick(object sender, RoutedEventArgs e)
        {
            Railway railway = dataGrid.SelectedItem as Railway;
            EditWindow ew = new EditWindow(railway);
            var result = ew.ShowDialog();
            if (result == true)
            {
                context.SaveChanges();
                ew.Close();
            }
            else
            {
                context.Entry(railway).Reload();
                dataGrid.DataContext = null;
                dataGrid.DataContext = context.Zkhs.Local;
            }
        }
        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Deleting record...", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Railway railway = dataGrid.SelectedItem as Railway;
                context.Railways.Remove(railway);
                context.SaveChanges();
            }
        }
    }
}
