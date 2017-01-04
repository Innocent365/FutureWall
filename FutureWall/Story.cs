using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FutureWall
{
    public class Story : Storyboard
    {
        public Story(Geometry pathGeometry, Panel panel)
        {
            _pathGeometry = pathGeometry;

            //显示轨迹，路径
            //var elm = new System.Windows.Shapes.Path();
            //elm.Data = _pathGeometry;
            //elm.Stroke = new SolidColorBrush { Color = Colors.Aquamarine };
            //elm.StrokeThickness = 10;
            //panel.Children.Add(elm);

            _panel = panel;
            //_panel.RenderTransformOrigin = new Point(0.5,0.5);
        }

        private readonly Geometry _pathGeometry;
        private Point[] _contourPoints;
        private readonly Panel _panel;

        public void Begin(List<ImageCell> imageCells)
        {
            _contourPoints = GetContourPoints(new Size(_panel.Width, _panel.Height)).ToArray();
            Transform(imageCells);
            _panel.BeginStoryboard(this);
        }

        #region 初始化
        private List<Point> GetContourPoints(Size panelSize)
        {
            var points = new List<Point>();
            var singleSize = AnimationPage.CellSize;

            for (var j = 0d; j < panelSize.Height; j += singleSize.Height)
            {
                for (var i = 0d; i < panelSize.Width; i += singleSize.Width)
                {
                    var rect = new Rect(i, j, singleSize.Width, singleSize.Height);

                    if (_pathGeometry.FillContains(new RectangleGeometry(rect), 0.00001, ToleranceType.Relative))
                    points.Add(new Point(i, j));
                }
            }
            return points;
        }

        private void Transform(List<ImageCell> imageCells)
        {
            var random = new Random(3245426);
            var cellSize = AnimationPage.CellSize;

            var left = _panel.Width - 2 * cellSize.Width;
            var top = _panel.Height - 2 * cellSize.Height;
            

            var i = 0;
            for (; i < _contourPoints.Length; i++)
            {
                var img = imageCells[i];

                if (img.DataContext == null)
                    img.Location = new Point(random.NextDouble()*left, random.NextDouble()*top);
                else img.Location = (Point) img.DataContext;
                img.DataContext = _contourPoints[i];
                ApplyStraightAnimation(img);
            }

            
            for (var j = i; j < imageCells.Count; j++)
            {
                var img = imageCells[j];
                if (img.DataContext == null) continue;

                img.Location = (Point)img.DataContext;//
                img.DataContext = new Point(_panel.Width / 2, _panel.Height + 50);
                ApplyStraightAnimation(img);
            }
        }

        #endregion

        public void ApplyStraightAnimation(ImageCell img)
        {

            var matrixAnimation = new MatrixAnimationUsingPath
            {
                PathGeometry = Path.Straight(img.Location, (Point)img.DataContext),
                Duration = TimeSpan.FromSeconds(2),
                AccelerationRatio = 0.9,
            };

            var buttonMatrixTransform = new MatrixTransform();
            
            img.RenderTransform = buttonMatrixTransform;

            var regName = "A" + new Random().Next(600);
            while (_panel.FindName(regName) != null)
            {
                regName += "B";
            }
            _panel.RegisterName(regName, buttonMatrixTransform);

            Storyboard.SetTargetName(matrixAnimation, regName);
            Storyboard.SetTargetProperty(matrixAnimation, new PropertyPath(MatrixTransform.MatrixProperty));
            
            Children.Add(matrixAnimation);
        }
    }
}
