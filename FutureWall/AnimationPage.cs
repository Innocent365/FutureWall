using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FutureWall
{
    public class AnimationPage : Page
    {
        public AnimationPage()
        {
            NameScope.SetNameScope(this, new NameScope());
            if (Width <= 0 || Height <= 0)
            {
                Width = _minSize.Width;
                Height = _minSize.Height;
            }

            //初始化画板
            MainPanel = new Canvas
            {
                Width = Width,
                Height = Height,
                Background = new SolidColorBrush(Color.FromRgb(45, 44, 48))
            };
            Content = MainPanel;

            //初始化cell池
            var imagePath = "pack://application:,,,/Resources/{0}.jpeg";
            var array = Enumerable.Range(0, 599);

            _cellPool = array.Select(p => new ImageCell
            {
                Width = CellSize.Width,
                Height = CellSize.Height,
                Margin = new Thickness(0),                
                Source = new BitmapImage(new Uri(string.Format(imagePath, p),
                    UriKind.RelativeOrAbsolute)),
            }).ToList();
        }

        private List<ImageCell> _cellPool;

        public Panel MainPanel;
        private readonly Size _minSize = new Size(720, 405);
        public static readonly Size CellSize = new Size(15, 15);

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            MainPanel.Children.Clear();

            var width = MainPanel.Width = sizeInfo.NewSize.Width;
            var height = MainPanel.Height = sizeInfo.NewSize.Height;

            if (double.IsInfinity(width) || double.IsInfinity(height)) return;
            
            _cellPool.ForEach(p =>
            {
                p.Location = new Point(width/2, height + 50);
                MainPanel.Children.Add(p);
            });

            var index = 0;
            var stories = ShapeStr.Select(p => new Story(p, MainPanel)).ToList();
            
            var story = stories[index];

            EventHandler onCompleted = (sender, args) =>
            {                
                index++;
                Console.WriteLine("Here we go, " + index);
                if (index >= stories.Count)
                    index = 0;
                story = stories[index];
                story.RenderPage(_cellPool);
            };

            stories.ForEach(p => p.Completed += onCompleted);

            story.RenderPage(_cellPool);
        }

        public static readonly string[] ShapeStr =
            {
                "M 80,80 C 170,320 220,360 380,320 C 310,80 190,60 80,80",
                "M 180,90 C 300,530 350,560 600,530 C 410,20 210,70 180,90",
                "M 180,90 L 280,250 L 280,500 Q 330,540 380,500 L 380,250 L 460,90 Q 425,60 390,90 L 330,240 L 240,90 Q 210,70 180,90 ",                
            };
    }
}
