using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Timer = System.Timers.Timer;

namespace FutureWall
{
    public class AnimationPage : Page
    {
        public AnimationPage()
        {                        
            NameScope.SetNameScope(this, new NameScope());
            InitCanvas();

            if (Width <= 0 || Height <= 0)
            {
                Width = _minSize.Width;
                Height = _minSize.Height;
            }

            _cellSize = new Size(15, 15); //Width*_cellScale.Width, Height*_cellScale.Height);
        }

        private readonly Size _minSize = new Size(720, 405);
        private readonly Size _cellScale = new Size(1 / 30d, 1 / 30d);

        private Size _cellSize;

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            _pathAnimationStoryboard.ToList().ForEach(p=>p.Stop());

            MainPanel.Width = sizeInfo.NewSize.Width;
            MainPanel.Height = sizeInfo.NewSize.Height;

            //_cellSize = new Size(Width * _cellScale.Width, Height * _cellScale.Height);
            _cellSize = new Size(15, 15);

            Run();

            var enn = _pathAnimationStoryboard.AsEnumerable().GetEnumerator();

            Storyboard current = new Storyboard();

            Timer timer = new Timer(3000);
            timer.Elapsed += (sender, args) =>
            {
                if (current != null) this.Dispatcher.Invoke(() => {
                    MainPanel.Children.Clear();
                });                
                if (enn.MoveNext() == false) enn.Reset();
                current = enn.Current;
                if (current != null)
                    this.Dispatcher.Invoke(() =>
                    {
                        var list = (List<ImageCell>) (current.GetValue(TagProperty));
                        list.ForEach(p => MainPanel.Children.Add(p));
                        current.Begin(this);
                    });
            };

            timer.Start();

        }

        private readonly List<Storyboard> _pathAnimationStoryboard = new List<Storyboard>();

        public Panel MainPanel;
        public Size CanvasSize;

        private void InitCanvas()
        {
            MainPanel = new Canvas
            {
                Width = Width,
                Height = Height,
                Background = new SolidColorBrush(Color.FromRgb(45, 44, 48))
            };
            Content = MainPanel;
            //MainPanel.RenderTransformOrigin = new Point(Width/2, Height/2);            
        }

        private readonly Random _random = new Random(3245426);

        private void Run()
        {
            var maxLeft = Width - 2*_cellSize.Width;
            var maxTop = Height - 2*_cellSize.Height;

            CanvasSize = new Size(maxLeft, maxTop);

            GenerateAnimation();
        }

        private void GenerateAnimation()
        {
            var aimPointsPage = Geometry.FigureContour(CanvasSize, _cellSize);

            var imagePath = "pack://application:,,,/Resources/{0}.jpeg";

            for (int index = 0; index < aimPointsPage.Count; index++)
            {
                var pointse = aimPointsPage[index];

                var storyboard = new Storyboard();

                List<ImageCell> list = new List<ImageCell>();
                for (var i = 0; i < pointse.Length; i++)
                {
                    var img = new ImageCell
                    {
                        Width = _cellSize.Width,
                        Height = _cellSize.Height,
                        Margin = new Thickness(0),
                        Source = new BitmapImage(new Uri(string.Format(imagePath, i),
                            UriKind.RelativeOrAbsolute)),
                        Tag = index.ToString() + i,
                        DataContext = pointse[i]
                    };

                    list.Add(img);

                    ApplyStraightAnimation(img, storyboard);
                    //ApplyBezierAnimation(img);
                }
                
                storyboard.SetValue(TagProperty, list);

                _pathAnimationStoryboard.Add(storyboard);

            }
        }

        private void ApplyStraightAnimation(FrameworkElement img, Storyboard storyboard)
        {
            var left = _random.NextDouble() * CanvasSize.Width;
            var top = _random.NextDouble() * CanvasSize.Height;

            var matrixAnimation = new MatrixAnimationUsingPath
            {
                PathGeometry = Path.Straight(new Point(left, top), (Point)img.DataContext),
                Duration = TimeSpan.FromSeconds(2),
				AccelerationRatio = 0.9
            };

            var buttonMatrixTransform = new MatrixTransform();
            img.RenderTransform = buttonMatrixTransform;

            var regName = "A" + img.Tag;
            RegisterName(regName, buttonMatrixTransform);

            Storyboard.SetTargetName(matrixAnimation, regName);
            Storyboard.SetTargetProperty(matrixAnimation, new PropertyPath(MatrixTransform.MatrixProperty));

            storyboard.Children.Add(matrixAnimation);
        }

        private void ApplyBezierAnimation(FrameworkElement img)
        {
            var left = _random.NextDouble() * CanvasSize.Width;
            var top = _random.NextDouble() * CanvasSize.Height;

            var matrixAnimation = new MatrixAnimationUsingPath
            {
                PathGeometry = Path.Bezier(new Point(left, top), (Point)img.DataContext),
                Duration = TimeSpan.FromSeconds(2),
                AccelerationRatio = 0.9
            };

            var buttonMatrixTransform = new MatrixTransform();
            img.RenderTransform = buttonMatrixTransform;

            var regName = "B" + img.Tag;
            RegisterName(regName, buttonMatrixTransform);

            Storyboard.SetTargetName(matrixAnimation, regName);
            Storyboard.SetTargetProperty(matrixAnimation, new PropertyPath(MatrixTransform.MatrixProperty));

            var storyboard = new Storyboard();
            storyboard.Children.Add(matrixAnimation);
            _pathAnimationStoryboard.Add(storyboard);
        }
    }
}
