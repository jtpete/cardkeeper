using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace cardkeeper.Helpers
{
    static class Converter
    {


        public static double ConvertStringToDouble(string value)
        {
            double dec;
            if (double.TryParse(value as string, out dec))
                return Math.Round(dec, 2);
            else
                return 0;
        }
        public static ImageSource ByteToImage(byte[] imageData)
        {
            return ImageSource.FromStream(() => new MemoryStream(imageData));
        }
    }
    public class DataSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return true;

            return ((IList)value).Count == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
