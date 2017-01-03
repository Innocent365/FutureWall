using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FutureWall
{
    public static class Path
    {
        public static PathGeometry Bezier(Point startP, Point endP)
        {
            PathGeometry animationPath = new PathGeometry();
            PathFigure pFigure = new PathFigure();
            pFigure.StartPoint = new Point(startP.X, startP.Y);

            PolyBezierSegment pBezierSegment = new PolyBezierSegment();
            pBezierSegment.Points.Add(new Point(startP.X + 10, endP.Y - 5));
            pBezierSegment.Points.Add(new Point((startP.X + endP.X) / 2, (startP.Y + endP.Y) / 2));
            pBezierSegment.Points.Add(new Point(endP.X - 10, endP.Y + 5));

            pBezierSegment.Points.Add(new Point(endP.X, endP.Y));

            pFigure.Segments.Add(pBezierSegment);
            animationPath.Figures.Add(pFigure);

            animationPath.Freeze();

            return animationPath;
        }

        public static PathGeometry Straight(Point startP, Point endP)
        {
            PathGeometry animationPath = new PathGeometry();
            PathFigure pFigure = new PathFigure();
            pFigure.StartPoint = new Point(startP.X, startP.Y);

            PolyLineSegment pBezierSegment = new PolyLineSegment();
            
            pBezierSegment.Points.Add(new Point(endP.X - 10, endP.Y + 5));
            pBezierSegment.Points.Add(new Point(endP.X, endP.Y));

            pFigure.Segments.Add(pBezierSegment);
            animationPath.Figures.Add(pFigure);

            animationPath.Freeze();

            return animationPath;
        }
    }

    public static class Util
    {
        
        /**
        private void ApplyBezierAnimation(FrameworkElement img)
        {
            var left = _random.NextDouble() * CanvasSize.Width;
            var top = _random.NextDouble() * CanvasSize.Height;

            var matrixAnimation = new MatrixAnimationUsingPath
            {
                PathGeometry = Path.Bezier(new Location(left, top), (Location)img.DataContext),
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
         * */
    }
}
