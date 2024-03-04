using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

//这是一个将布尔值转换为画笔（Brush）的转换器，如果布尔值为 true，则返回绿色的画笔；如果布尔值为 false，则返回红色的画笔

namespace SerialPortTest.Converts
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool isOpen = (bool)value;
                return isOpen ? Brushes.Green : Brushes.Red; // 如果值为 true，则返回绿色画笔，否则返回红色画笔
            }
            return Brushes.Red; // 默认为红色画笔
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
