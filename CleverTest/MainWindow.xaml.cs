using CleverTest.Source.Commands;
using CleverTest.Source.FFmpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CleverTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnectDeviceManager manager = new ConnectDeviceManager();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            manager.Close();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var ret = await manager.StartServer();
            if(ret == StartServerResult.NotConnect)
            {
                MessageBox.Show("Not Connect a device");
                return;
            }

            if(ret == StartServerResult.NotSupportSDK)
            {
                MessageBox.Show("Not Support Device");
                return;
            }

            manager.ConnectServer(bmp=> {
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    var img = BitmapToBitmapImage(bmp);
                    showImageView.Source = img;
                });
            });
        }

        private BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }

            return bitmapImage;
        }
    }
}
