using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using SerialPortTest;

namespace SerialPortTest.ViewModels
{
    public class SerialPortReceiveViewModel : INotifyPropertyChanged
    {
        private SerialPort _serialPort;

        private StringBuilder _receivedData;
        public string ReceivedData
        {
            get { return _receivedData.ToString(); }
            set
            {
                _receivedData.Clear(); // 清空_receivedData
                _receivedData.Append(value); // 设置_receivedData
                OnPropertyChanged(nameof(ReceivedData)); // 通知界面更新
            }
        }

        private bool _isHexDisplayEnabled; // 是否启用 HEX 显示
        public bool IsHexDisplayEnabled
        {
            get { return _isHexDisplayEnabled; }
            set
            {
                _isHexDisplayEnabled = value;
                OnPropertyChanged(nameof(IsHexDisplayEnabled));
            }
        }

        private bool _isTimeDisplayEnabled; // 是否附加时间显示
        public bool IsTimeDisplayEnabled
        {
            get { return _isTimeDisplayEnabled; }
            set
            {
                _isTimeDisplayEnabled = value;
                OnPropertyChanged(nameof(IsTimeDisplayEnabled));
            }
        }

        private bool isAutoLineEnabled;
        public bool IsAutoLineEnabled // 是否启用自动换行
        {
            get { return isAutoLineEnabled; }
            set
            {
                isAutoLineEnabled = value;
                OnPropertyChanged(nameof(IsAutoLineEnabled));
            }
        }

        public ICommand SaveDataCommand { get; } //保存读取数据
        public ICommand ClearDataCommand { get; } //清除数据

        

        public SerialPortReceiveViewModel()
        {
            _receivedData = new StringBuilder();
            IsHexDisplayEnabled = false; // 默认不启用
            IsTimeDisplayEnabled = false; // 默认不启用
            IsAutoLineEnabled = false; // 默认不启用

            SaveDataCommand = new RelayCommand(SaveData);
            ClearDataCommand = new RelayCommand(ClearData);
        }

        private void ClearData(object obj)
        {
            ReceivedData = string.Empty;
        }

        private void SaveData(object obj)
        {
            // 创建一个SaveFileDialog实例
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // 设置默认的文件扩展名和过滤器
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = "Text documents (.txt)|*.txt";

            // 显示SaveFileDialog，等待用户选择保存路径和文件名
            bool? result = saveFileDialog.ShowDialog();

            // 如果用户点击了保存按钮
            if (result == true)
            {
                try
                {
                    // 获取用户选择的保存路径和文件名
                    string filePath = saveFileDialog.FileName;

                    // 写入ReceivedData的内容到选定的文件
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(ReceivedData);
                    }

                    MessageBox.Show("数据已保存至：" + filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存数据时出错：" + ex.Message);
                }
            }
        }

        // 订阅串口对象的事件
        public void SubscribeToSerialPort(SerialPortSettingViewModel viewModel)
        {
            viewModel.SerialPortReceived += ViewModel_SerialPortReceived;
        }

        private void ViewModel_SerialPortReceived(SerialPort serialPort)
        {
            _serialPort = serialPort;

            // 订阅串口数据接收事件
            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int len = _serialPort.BytesToRead;
            byte[] buffer = new byte[len];
            _serialPort.Read(buffer, 0, len);

            if (IsHexDisplayEnabled)
            {
                // 将字节转换为十六进制格式的字符串并追加到 _receivedData 中
                StringBuilder hexString = new StringBuilder();
                foreach (byte b in buffer)
                {
                    hexString.AppendFormat("{0:X2} ", b);
                }
                _receivedData.Append(hexString.ToString());
                _receivedData.Append(" ");
            }
            else
            {
                // 将字节转换为字符串
                string data = Encoding.ASCII.GetString(buffer);
                _receivedData.Append(data);
                _receivedData.Append(" ");
            }

            if(IsTimeDisplayEnabled)
            {
                _receivedData.Append("[");
                _receivedData.Append(DateTime.Now.ToString("G"));
                _receivedData.Append("]");
                _receivedData.Append("  ");
            }

            if(IsAutoLineEnabled)
            {
                _receivedData.Append("\r\n");
            }

            // 触发属性更改通知，通知界面更新
            OnPropertyChanged(nameof(ReceivedData));
        }

        // 更新界面上的文本显示
        private void UpdateText(string text)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_receivedData != null)
                {
                    _receivedData.Append(text);
                }
            });
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
