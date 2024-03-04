using SerialPortTest.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SerialPortTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            MainViewModel viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void receiveBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            receiveBox.ScrollToEnd();
        }
    }
}