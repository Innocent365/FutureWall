using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

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

            _cellSize = new Size(Width*_cellScale.Width, Height*_cellScale.Height);
        }

        private readonly Size _minSize = new Size(720, 405);
        private readonly Size _cellScale = new Size(1 / 30d, 1 / 30d);

        private Size _cellSize;

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            _pathAnimationStoryboard.Stop(this);

            MainPanel.Width = sizeInfo.NewSize.Width;
            MainPanel.Height = sizeInfo.NewSize.Height;

            _cellSize = new Size(Width * _cellScale.Width, Height * _cellScale.Height);
            
            Run();
            _pathAnimationStoryboard.Begin(this);
            _pathAnimationStoryboard.RepeatBehavior = RepeatBehavior.Forever;
        }

        private readonly Storyboard _pathAnimationStoryboard = new Storyboard();

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
            var maxLeft = Width - 2 * _cellSize.Width;
            var maxTop = Height - 2 * _cellSize.Height;

            CanvasSize = new Size(maxLeft, maxTop);

            var aimPoints = Geometry.FigureContour(CanvasSize, _cellSize);

            var imagePath = "pack://application:,,,/Resources/{0}.jpeg";
            for (var i = 0; i < aimPoints.Length; i++)
            {
                var img = new ImageCell
                {
                    Width  = _cellSize.Width,
                    Height = _cellSize.Height,
                    Margin = new Thickness(0),
                    Source = new BitmapImage(new Uri(string.Format(imagePath, i), 
                        UriKind.RelativeOrAbsolute)),
                    Tag= i,
                    DataContext = aimPoints[i]
                };

                MainPanel.Children.Add(img);

                ApplyStraightAnimation(img);
                ApplyBezierAnimation(img);
            }
        }

        private void ApplyStraightAnimation(FrameworkElement img)
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

            _pathAnimationStoryboard.Children.Add(matrixAnimation);
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

            _pathAnimationStoryboard.Children.Add(matrixAnimation);
        }
    }
}
