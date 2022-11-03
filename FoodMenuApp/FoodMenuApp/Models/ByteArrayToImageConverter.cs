using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace FoodMenuApp
{
    public class ByteArrayToImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource ImageSrc = null;
            if (value != null)
            {
                //var ByteArray = value as byte[];
                //var stream = new MemoryStream(ByteArray);
                //ImageSrc = ImageSource.FromStream(() => stream);
                ImageSrc = ImageSource.FromStream(new Func<Stream>(() =>
                {
                    MemoryStream mem = new MemoryStream((byte[])value);
                    return mem;
                }));
            }
            return ImageSrc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
