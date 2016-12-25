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
            Loaded += OnLoad;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
//            var page = new VisualDemo();
//            Content = page;
//            return;

            var example = new AnimationPage();
            example.Width = 800;
            example.Height = 600;
            example.Background = new SolidColorBrush(Colors.Chocolate);

            Content= example;
        }
    }
}
