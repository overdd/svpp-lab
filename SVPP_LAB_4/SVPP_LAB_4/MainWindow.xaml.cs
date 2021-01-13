using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SVPP_LAB_4
{

    public partial class MainWindow : Window
    {
        readonly List<Horse> HorseList = new List<Horse>();
        bool isStarted = false;
        bool isPaused = false;
        public MainWindow()
        {
            InitializeComponent();
            HorseList.Add(new Horse("Spark", 1));
            HorseList.Add(new Horse("Fluffy", 2));
            HorseList.Add(new Horse("Cold heart", 3));
            HorseList.Add(new Horse("Unicorn", 4));
        }


        private void ButtonStartClick(object sender, RoutedEventArgs e)
        {
            if (!isStarted)
            {
                foreach (Horse horse in HorseList)
                {
                    BindingHorseControl(horse);
                    horse.horseControl.Start();
                    System.Threading.Thread.Sleep(50);
                }
                isStarted = true;
            }
            else
            { MessageBox.Show("The game's already started!"); }

        }
        private void ButtonPauseClick(object sender, RoutedEventArgs e)
        {
            if (!isPaused)
            {
                foreach (Horse horse in HorseList)
                {
                    horse.horseControl.Pause();
                }
                isPaused = true;
            }
            else
            {
                foreach (Horse horse in HorseList)
                {
                    horse.horseControl.UnPause();
                }
                isPaused = false;
            }
        }

        private void ButtonResetClick(object sender, RoutedEventArgs e)
        {
            isStarted = false;
            foreach (Horse horse in HorseList)
            {
                UnBindingHorseControl(horse);
            }
        }

        private void BindingHorseControl(Horse horse)
        {
            HippodromeField.Children.Add(horse.rectangle);
            horse.rectangle.MouseLeftButtonDown += HorseLeftMouseClick;
            horse.rectangle.MouseRightButtonDown += HorseRightMouseClick;
            horse.rectangle.DataContext = horse.horseControl;
            horse.rectangle.SetBinding(Canvas.TopProperty, new Binding("Y"));
            horse.rectangle.SetBinding(Canvas.LeftProperty, new Binding("X"));
        }
        private void UnBindingHorseControl(Horse horse)
        {
            HippodromeField.Children.Remove(horse.rectangle);
            horse.horseControl.X = horse.horseControl.StartPointX;
            horse.horseControl.Y = horse.horseControl.StartPointY;
            horse.horseControl.angle = 90;
        }

        private void HorseLeftMouseClick(object sender, RoutedEventArgs e)
        {
            foreach (Horse horse in HorseList)
            {
                if (horse.rectangle.GetHashCode().ToString() == sender.GetHashCode().ToString())
                {
                    HorseNameTextBlock.Text = horse.name;
                    HorseSpeedTextBlock.Text = (Math.Round((decimal)1 / horse.horseControl.Speed * 1400)).ToString();
                }
            }
        }

        private void HorseRightMouseClick(object sender, RoutedEventArgs e)
        {
            List<int> SpeedList = new List<int>();
            foreach (Horse horsy in HorseList)
            {
                SpeedList.Add(horsy.horseControl.Speed);
            }
            SpeedList.Sort();

            foreach (Horse horse in HorseList)
            {
                if (horse.rectangle.GetHashCode().ToString() == sender.GetHashCode().ToString())
                {
                    for (int i = 0; i < HorseList.Count; i++)
                    {
                        if (SpeedList[i] == horse.horseControl.Speed) {
                            HorsePositionTextBlock.Text = (i+1).ToString();
                        }
                    }
                }
            }
        }
    }
}
