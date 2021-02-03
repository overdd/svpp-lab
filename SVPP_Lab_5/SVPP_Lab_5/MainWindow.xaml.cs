using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SVPP_Lab_5

{
    public partial class MainWindow : Window
    {
        private readonly List<Figure> figureColection = new List<Figure>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Point mousePosition = e.GetPosition(this);
                int border = int.Parse(Thck.Text.ToString());
                Color lineColor = Color.FromRgb(byte.Parse(rL.Text), byte.Parse(gL.Text), byte.Parse(bL.Text));
                Color backgroundColor = Color.FromRgb(byte.Parse(rB.Text), byte.Parse(gB.Text), byte.Parse(bB.Text));

                Figure figureObject = new Figure(mousePosition, border, lineColor, backgroundColor);

                figureColection.Add(figureObject);
                Canvas.Children.Add(figureObject.GetFigeure);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void MousePositionMouseMove(object sender, MouseEventArgs e)
        {
            MousePosition.Content = "Mouse position: " + e.GetPosition(this).ToString();
        }

        private void SaveFileMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random = new Random();
                string fileName = "file", extension = ".d";

                if (File.Exists(fileName + extension))
                {
                    fileName += random.Next(1, 777);
                }

                using (FileStream fileStream = File.Create(fileName + extension))
                {
                    fileStream.Close();

                    StreamWriter streamWriter = new StreamWriter(fileName + extension, true);
                    foreach (Figure figure in figureColection)
                    {
                        streamWriter.WriteLine(figure.ToString());
                    }

                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void OpenFileMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                Point mousePosition = new Point();
                Color lineColor, backgroundColor;

                string line = "";
                int border = 0;
                openFileDialog.Filter = "Text Files (.d)|*.d";

                if (openFileDialog.ShowDialog() == true)
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                    if (fileInfo.Extension.ToLower() == ".d")
                    {
                        FileName.Content = "File name: " + fileInfo.Name;
                        Canvas.Children.Clear();

                        using (StreamReader streamReader = fileInfo.OpenText())
                        {

                            while ((line = streamReader.ReadLine()) != null)
                            {
                                string[] split = line.Split(' ');
                                mousePosition.X = Convert.ToDouble(split[0]);
                                mousePosition.Y = Convert.ToDouble(split[1]);
                                border = Convert.ToInt32(split[14]);
                                lineColor = (Color)ColorConverter.ConvertFromString(split[12]);
                                backgroundColor = (Color)ColorConverter.ConvertFromString(split[13]);

                                Figure figOpen = new Figure(mousePosition, border, lineColor, backgroundColor);
                                Canvas.Children.Add(figOpen.GetFigeure);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: ", ex.Message);
            }
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
