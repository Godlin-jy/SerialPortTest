using System.ComponentModel;

namespace SerialPortTest.ViewModels
{
    public class MainViewModel
    {
        // 用于存储与时间相关的视图模型
        private TimeViewModel _timeViewModel;
        public TimeViewModel TimeViewModel
        {
            get { return _timeViewModel; }
            set 
            { 
                _timeViewModel = value; 
                OnPropertyChanged(nameof(TimeViewModel)); // 通知属性更改
            }
        }

        // 用于存储与导航相关的视图模型
        private NavigationViewModel _navigationViewModel;
        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set
            {
                _navigationViewModel = value;
                OnPropertyChanged(nameof(NavigationViewModel));
            }
        }

        // 用于存储与串口配置相关的视图模型
        private SerialPortSettingViewModel _serialPortSettingViewModel;
        public SerialPortSettingViewModel SerialPortSettingViewModel
        {
            get { return _serialPortSettingViewModel; }
            set
            {
                _serialPortSettingViewModel = value;
                OnPropertyChanged(nameof(SerialPortSettingViewModel));
            }
        }

        //用于处理数据接收的视图模型
        private SerialPortReceiveViewModel _serialPortReceiveViewModel;
        public SerialPortReceiveViewModel SerialPortReceiveViewModel
        {
            get { return _serialPortReceiveViewModel; }
            set
            {
                _serialPortReceiveViewModel = value;
                OnPropertyChanged(nameof(SerialPortReceiveViewModel));
            }
        }

        //用于处理数据发送的视图模型
        private SerialPortSendViewModel _serialPortSendViewModel;
        public SerialPortSendViewModel SerialPortSendViewModel
        {
            get { return _serialPortSendViewModel; }
            set
            {
                _serialPortSendViewModel = value;
                OnPropertyChanged(nameof(SerialPortSendViewModel));
            }
        }

        // 构造函数
        public MainViewModel()
        {
            // 初始化 TimeViewModel 和 NavigationViewModel 的实例
            TimeViewModel = new TimeViewModel();
            NavigationViewModel = new NavigationViewModel();
            SerialPortSettingViewModel = new SerialPortSettingViewModel();
            SerialPortReceiveViewModel = new SerialPortReceiveViewModel();
            SerialPortReceiveViewModel.SubscribeToSerialPort(SerialPortSettingViewModel);
            SerialPortSendViewModel = new SerialPortSendViewModel();
            SerialPortSendViewModel.SubscribeToSerialPort(SerialPortSettingViewModel);
        }

        // INotifyPropertyChanged 接口的 PropertyChanged 事件
        public event PropertyChangedEventHandler PropertyChanged;

        // 用于引发 PropertyChanged 事件的方法
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
