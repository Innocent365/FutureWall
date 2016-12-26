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
            MinWidth = 720;
            MinHeight = 405;
            NameScope.SetNameScope(this, new NameScope());            

            InitCanvas();
            Run();
        }

//        protected override void OnRender(DrawingContext drawingContext)
//        {
//            base.OnRender(drawingContext);
////        }
//
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            
//            if(this.IsLoaded)
            _pathAnimationStoryboard.Begin(this);
        }

        private readonly Storyboard _pathAnimationStoryboard = new Storyboard();

        public Panel MainPanel;
        public Size CanvasSize;
        public readonly Size CellSize = new Size(20,25);

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

            var maxLeft = double.IsNaN(Width) ? MinWidth : Width - 2 * CellSize.Width;
            var maxTop = double.IsNaN(Height) ? MinHeight : Height - 2 * CellSize.Height;

            CanvasSize = new Size(maxLeft, maxTop);
        }

        private readonly Random _random = new Random(3245426);

        private void Run()
        {
            var aimPoints = Geometry.FigureContour(CanvasSize, CellSize, MainPanel);

            var imagePath = "pack://application:,,,/Resources/{0}.jpeg";
            for (var i = 0; i < aimPoints.Length; i++)
            {
                var img = new ImageCell
                {
                    Width  = CellSize.Width *4,
                    Height = CellSize.Height　*4,
                    Source = new BitmapImage(new Uri(string.Format(imagePath, i), 
                        UriKind.RelativeOrAbsolute)),
                    Tag= i,
                    DataContext = aimPoints[i]
                };

                MainPanel.Children.Add(img);
                ApplyAnimation(img);
            }
        }

        private void ApplyAnimation(FrameworkElement img)
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

            var abttonName = "B" + img.Tag;
            RegisterName(abttonName, buttonMatrixTransform);

            Storyboard.SetTargetName(matrixAnimation, abttonName);
            Storyboard.SetTargetProperty(matrixAnimation, new PropertyPath(MatrixTransform.MatrixProperty));

            _pathAnimationStoryboard.Children.Add(matrixAnimation);
        }

        
    }
}
