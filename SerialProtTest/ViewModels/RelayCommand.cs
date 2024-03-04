using System.Windows.Input;

namespace SerialPortTest.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute; // 执行命令时调用的方法
        private readonly Func<object, bool> _canExecute; // 检查是否可以执行命令的方法

        /// <summary>
        /// 创建 RelayCommand 的新实例
        /// </summary>
        /// <param name="execute">执行命令时调用的方法</param>
        /// <param name="canExecute">检查是否可以执行命令的方法（可选）</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// ICommand 接口中的 CanExecuteChanged 事件
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 确定此命令是否可在其当前状态下执行
        /// </summary>
        /// <param name="parameter">命令使用的数据。如果不需要传递数据，则此对象可以设置为 null</param>
        /// <returns>如果可以执行此命令，则为 true；否则为 false</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// 执行此命令
        /// </summary>
        /// <param name="parameter">命令使用的数据。如果不需要传递数据，则此对象可以设置为 null</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
