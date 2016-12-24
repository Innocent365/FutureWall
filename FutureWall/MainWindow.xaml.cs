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
            var example = new AnimationUsingPathExample();
            example.Width = 800;
            example.Height = 600;
            example.Background = new SolidColorBrush(Colors.Chocolate);

            Content= example;
        }
    }
}
