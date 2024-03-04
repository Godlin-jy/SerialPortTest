using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//这是一个将字符串表示的校验位转换为 Parity 枚举类型的转换器。根据输入的字符串值，它将返回对应的 Parity 枚举值

namespace SerialPortTest.Converts
{
    public class ParityConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string stopBitsString = value as string;
                switch (stopBitsString)
                {
                    case "无":
                        return Parity.None;
                    case "偶检验":
                        return Parity.Even;
                    case "奇校验":
                        return Parity.Odd;
                    default:
                        return Parity.None;
                }
            }
            return Parity.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ParityConverter parityConverter = new ParityConverter();

            if (value is Parity)
            {
                Parity parity = (Parity)value;
                return parityConverter.Convert(parity.ToString(), targetType, parameter, culture);
            }
            return "None";
        }
    }
}
