using System.ComponentModel;

namespace SerialPortTest.Models
{
    public class SerialPortSettingModel : INotifyPropertyChanged
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public string Parity { get; set; }
        public string StopBits { get; set; }
        private bool _isPortOpen;
        public bool IsPortOpen 
        {
            get { return _isPortOpen; }
            set 
            { 
                _isPortOpen = value;
                OnPropertyChanged(nameof(IsPortOpen));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
