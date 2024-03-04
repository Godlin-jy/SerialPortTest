using System.Globalization;
using System.IO.Ports;
using System.Windows.Data;

//这是一个将字符串表示的停止位转换为 StopBits 枚举类型的转换器。根据输入的字符串值，它将返回对应的 StopBits 枚举值

namespace SerialPortTest.Converts
{
    public class StopBitsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string stopBitsString = value as string;
                switch (stopBitsString)
                {
                    case "1":
                        return StopBits.One;
                    case "2":
                        return StopBits.Two;
                    case "1.5":
                        return StopBits.OnePointFive;
                    default:
                        return StopBits.None;
                }
            }
            return StopBits.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StopBitsConverter stopBitsConverter = new StopBitsConverter();

            if (value is StopBits)
            {
                StopBits stopBits = (StopBits)value;
                return stopBitsConverter.Convert(stopBits.ToString(), targetType, parameter, culture);
            }
            return "None";
        }
    }
}
