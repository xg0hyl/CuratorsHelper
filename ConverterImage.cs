using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CuratorsHelper
{
    public class ConverterImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is byte[] bytes) || bytes.Length == 0)
            {
                // Если путь к фотографии не существует, возвращаем ImageSource заглушки
                var imageUri = new Uri("pack://application:,,,/CuratorsHelper;component/images/nonePhoto.png");
                return new BitmapImage(imageUri);
            }
            else
            {
                var tempPath = Path.GetTempFileName();
                File.WriteAllBytes(tempPath, bytes);
                return new BitmapImage(new Uri(tempPath));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

