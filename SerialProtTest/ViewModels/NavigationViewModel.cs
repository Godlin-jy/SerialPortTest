using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SerialPortTest.ViewModels
{
    public class NavigationViewModel
    {
        public ICommand OpenBilibiliCommand { get; } // 用于打开 Bilibili 主页的命令
        public ICommand OpenBlogCommand { get; } // 用于打开博客园主页的命令
        public ICommand OpenGithubCommand { get; } // 用于打开 Github 主页的命令

        /// <summary>
        /// 创建 NavigationViewModel 的新实例
        /// </summary>
        public NavigationViewModel()
        {
            // 初始化命令
            OpenBilibiliCommand = new RelayCommand(OpenBilibili);
            OpenBlogCommand = new RelayCommand(OpenBlog);
            OpenGithubCommand = new RelayCommand(OpenGithub);
        }

        /// <summary>
        /// 打开 Bilibili 主页的方法
        /// </summary>
        /// <param name="parameter">命令参数</param>
        private void OpenBilibili(object parameter)
        {
            OpenUrl("https://space.bilibili.com/3546379911694420");
        }

        /// <summary>
        /// 打开博客园主页的方法
        /// </summary>
        /// <param name="parameter">命令参数</param>
        private void OpenBlog(object parameter)
        {
            OpenUrl("https://www.cnblogs.com/LargeLin");
        }

        /// <summary>
        /// 打开 Github 主页的方法
        /// </summary>
        /// <param name="parameter">命令参数</param>
        private void OpenGithub(object parameter)
        {
            OpenUrl("https://github.com/Godlin-jy");
        }

        /// <summary>
        /// 使用默认浏览器打开指定 URL 的方法
        /// </summary>
        /// <param name="url">要打开的 URL</param>
        private void OpenUrl(string url)
        {
            try
            {
                // 使用默认浏览器打开指定 URL
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                // 捕获并显示异常信息
                MessageBox.Show("无法打开网页：" + ex.Message);
            }
        }
    }
}
