using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace SVPP_Lab_9.Utils
{
    class ImageSourceConverter : IValueConverter
    {
        readonly string imageDirectory = Directory.GetCurrentDirectory();
        string ImageDirectory
        {
            get
            {
                return Path.Combine(imageDirectory, "images");
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? Path.Combine(ImageDirectory, (string)value) : value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
