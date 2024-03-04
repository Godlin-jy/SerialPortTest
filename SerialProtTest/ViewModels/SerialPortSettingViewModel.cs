using System.Collections.Immutable;
using System.ComponentModel;
using System.Globalization;
using System.IO.Ports;
using System.Windows;
using System.Windows.Input;
using SerialPortTest.Converts;
using SerialPortTest.Models;

namespace SerialPortTest.ViewModels
{
    public class SerialPortSettingViewModel : INotifyPropertyChanged
    {
        public event Action<SerialPort> SerialPortReceived;

        public SerialPort serialPort; // 声明一个SerialPort对象用于与串口通信
        private readonly StopBitsConverter StopBitsConverterInstance = new StopBitsConverter(); // 实例化StopBits转换器
        private readonly ParityConverter ParityConverterInstance = new ParityConverter(); // 实例化Parity转换器

        private SerialPortSettingModel _settings; // 声明Settings属性对应的私有字段
        public SerialPortSettingModel Settings // Settings属性，用于存储串口设置
        {
            get { return _settings; } // 获取Settings属性值
            set
            {
                _settings = value; // 设置Settings属性值
                OnPropertyChanged(nameof(Settings)); // 在属性值更改时触发属性更改通知
            }
        }

        //Combox在串口打开时就不能再选择了
        private bool _isEnableComboBox;
        public bool IsEnableComboBox
        {
            get { return _isEnableComboBox; }
            set
            {
                _isEnableComboBox = value;
                OnPropertyChanged(nameof(IsEnableComboBox));
            }
        }

        // 串口参数列表
        public List<string> PortNames { get; } // 可用串口列表
        public List<int> BaudRates { get; } // 波特率列表
        public List<int> DataBits { get; } // 数据位列表
        public List<string> Paritys { get; } // 校验位列表
        public List<string> StopBits { get; } // 停止位列表
        public ICommand OpenOrClosePortCommand { get; } // 打开或关闭串口的命令

        // 构造函数，初始化串口参数列表和Settings对象
        public SerialPortSettingViewModel()
        {
            PortNames = GetAvailablePortNames(); // 获取可用串口列表
            BaudRates = GetAvailableBaudRates(); // 获取可用波特率列表
            DataBits = GetAvailableDataBits(); // 获取可用数据位列表
            Paritys = GetAvailableParitys(); // 获取可用校验位列表
            StopBits = GetAvailableStopBits(); // 获取可用停止位列表
            IsEnableComboBox = true;
            // 初始化Settings对象，并设置默认值
            Settings = new SerialPortSettingModel
            {
                PortName = PortNames.FirstOrDefault(), // 默认选择第一个串口
                BaudRate = BaudRates[0], // 默认选择第一个波特率
                DataBits = DataBits[3], // 默认选择第四个数据位
                Parity = Paritys.FirstOrDefault(), // 默认选择第一个校验位
                StopBits = StopBits.FirstOrDefault(), // 默认选择第一个停止位
                IsPortOpen = false, // 默认串口关闭
            };

            // 初始化打开或关闭串口命令
            OpenOrClosePortCommand = new RelayCommand(OpenOrClosePort);
        }

        // 获取可用串口列表
        private List<string>? GetAvailablePortNames()
        {
            return new List<string>(SerialPort.GetPortNames());
        }

        // 获取可用波特率列表
        private List<int>? GetAvailableBaudRates()
        {
            return new List<int> { 9600, 19200, 38400, 57600, 115200 };
        }

        // 获取可用数据位列表
        private List<int>? GetAvailableDataBits()
        {
            return new List<int> { 5, 6, 7, 8 };
        }

        // 获取可用校验位列表
        private List<string>? GetAvailableParitys()
        {
            return new List<string> { "无校验", "奇校验", "偶校验" };
        }

        // 获取可用停止位列表
        private List<string>? GetAvailableStopBits()
        {
            return new List<string> { "1", "2", "1.5" };
        }

        // 打开或关闭串口
        private void OpenOrClosePort(object parameter)
        {
            try
            {
                if (!Settings.IsPortOpen) // 如果串口未打开
                {
                    serialPort = new SerialPort(); // 创建新的串口对象

                    // 设置串口参数
                    serialPort.PortName = Settings.PortName;
                    serialPort.BaudRate = Settings.BaudRate;
                    serialPort.DataBits = Settings.DataBits;
                    serialPort.StopBits = (StopBits)StopBitsConverterInstance.Convert(Settings.StopBits, typeof(StopBits), null, CultureInfo.InvariantCulture);
                    serialPort.Parity = (Parity)ParityConverterInstance.Convert(Settings.Parity, typeof(Parity), null, CultureInfo.InvariantCulture);
                    serialPort.Open(); // 打开串口

                    Settings.IsPortOpen = true; // 更新串口状态为打开
                    IsEnableComboBox = false; //ComboBox不能再选择了

                    // 触发事件，将串口对象传递给订阅者
                    SerialPortReceived?.Invoke(serialPort);
                }
                else // 如果串口已打开
                {
                    serialPort.Close(); // 关闭串口
                    Settings.IsPortOpen = false; // 更新串口状态为关闭
                    IsEnableComboBox = true; //ComboBox可以再选择了
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // 捕获并显示异常信息
            }
        }

        // 属性更改通知事件
        public event PropertyChangedEventHandler PropertyChanged;

        // 触发属性更改通知
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
