using System.Windows;
using System.Windows.Media;

namespace FutureWall
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
//            WindowState = WindowState.Maximized;
//            WindowStyle = WindowStyle.None;
//            AllowsTransparency = true;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            var margin = 20;

            var example = new AnimationPage();
            example.Width = sizeInfo.NewSize.Width - 2 * margin;
            example.Height = sizeInfo.NewSize.Height - 2 * margin;
            example.Background = new SolidColorBrush(Colors.Chocolate);

            Content= example;
        }
    }
}
