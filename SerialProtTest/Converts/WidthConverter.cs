using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SerialPortTest.Converts
{
    public class WidthConverter : IValueConverter
    {
        // 将 RichTextBox 的实际宽度转换为稍小的值
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                // 减去一个固定的值来使宽度稍小
                double reducedWidth = width - 20; // 这里可以根据需要调整
                return reducedWidth < 0 ? 0 : reducedWidth;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
