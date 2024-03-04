using System.ComponentModel;
using System.Windows.Threading;

namespace SerialPortTest.ViewModels
{
    public class TimeViewModel : INotifyPropertyChanged
    {
        private string _currentTime;
        public string CurrentTime
        {
            get { return _currentTime; }
            set 
            { 
                _currentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        }

        public TimeViewModel()
        {
            // 创建定时器，并设置定时器间隔为1秒
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick; // 将 Tick 事件与 Timer_Tick 方法关联
            timer.Start(); // 启动定时器
        }

        // 定时器 Tick 事件处理方法
        private void Timer_Tick(object? sender, EventArgs e)
        {
            // 更新界面上显示的当前时间
            CurrentTime = DateTime.Now.ToString("G");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
