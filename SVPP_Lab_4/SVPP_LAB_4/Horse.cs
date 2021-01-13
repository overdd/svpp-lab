using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SVPP_LAB_4
{
    public class Horse
    {
        public string name;
        public HorseControl horseControl = new HorseControl();
        public Rectangle rectangle;
        private static readonly Random random = new Random();

        public Horse(string name, int track)
        {
            this.name = name;
            this.horseControl.Speed = random.Next(40, 80);
            this.rectangle = new Rectangle()
            {
                Width = 50,
                Height = 50,
                Fill = new ImageBrush(new BitmapImage(new Uri("Images/horse.png", UriKind.Relative)))
            };
        }
    }
}
