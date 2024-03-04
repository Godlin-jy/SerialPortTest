using System;
using System.Globalization;
using System.Windows.Data;

//这是一个将布尔值转换为按钮文本的转换器。如果布尔值为 true，则返回 "关闭串口"；如果布尔值为 false，则返回 "打开串口"

namespace SerialPortTest.Converts
{
    public class IsOpenToButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isOpen)
            {
                return isOpen ? "关闭串口" : "打开串口"; // 如果值为 true，则返回 "关闭串口"，否则返回 "打开串口"
            }
            return "打开串口"; // 默认返回 "打开串口"
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
