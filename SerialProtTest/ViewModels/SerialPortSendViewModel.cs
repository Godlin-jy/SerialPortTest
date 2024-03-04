using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SerialPortTest.ViewModels
{
    public class SerialPortSendViewModel : INotifyPropertyChanged
    {
        private SerialPort _serialPort;

        private string _sendData;
        public string SendData
        { 
            get { return _sendData; }
            set 
            { 
                _sendData = value; 
                OnPropertyChanged(nameof(SendData));
            }
        }

        public ICommand SendDataCommand { get; } //发送数据命令
        public ICommand ClearSendCommand { get; } //清除发送框


        public SerialPortSendViewModel()
        {
            SendDataCommand = new RelayCommand(SendDataButton);
            ClearSendCommand = new RelayCommand(ClearSend);
        }

        //发送数据
        private void SendDataButton(object obj)
        {
            try
            {
                if(_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.WriteLine(SendData);
                }
                else
                {
                    MessageBox.Show("请打开串口！");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //清除数据
        private void ClearSend(object obj)
        {
            SendData = string.Empty;
        }

        // 订阅串口对象的事件
        public void SubscribeToSerialPort(SerialPortSettingViewModel viewModel)
        {
            viewModel.SerialPortReceived += ViewModel_SerialPortReceived;
        }

        private void ViewModel_SerialPortReceived(SerialPort serialPort)
        {
            _serialPort = serialPort;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
