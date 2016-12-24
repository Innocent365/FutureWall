using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FutureWall
{
    public class AnimationUsingPathExample : Page
    {
        public AnimationUsingPathExample()
        {
            this.Width = 800;
            this.Height = 600;

            NameScope.SetNameScope(this, new NameScope());

            InitCanvas();
            InitCells();
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);        
            pathAnimationStoryboard.Begin(this);
        }

        public Storyboard pathAnimationStoryboard = new Storyboard();

        public Panel MainPanel;
        private void InitCanvas()
        {
            MainPanel = new Canvas
            {
                Width = this.Width,
                Height = this.Height,
                Background = new SolidColorBrush(Colors.BurlyWood)
            };
            this.Content = MainPanel;
        }

        private void InitCells()
        {
            var cellWidth = 10;
            var cellHeight = 20;
            var random = new Random();
            var maxLeft = Width - cellWidth;
            var maxTop = Height - cellHeight;

            var aimPoints = new List<Point>();
            for (var i = 0d; i < MainPanel.Width; i += cellWidth)
            {
                for (var j = 0d; j < MainPanel.Height; j += cellHeight)
                {
                    if (Math.Pow(i - maxTop/2, 2) + Math.Pow(j - maxTop/2, 2) <= Math.Pow(maxTop/2, 2))
                        aimPoints.Add(new Point(i, j));
                }
            }

            for (var i = 0; i < aimPoints.Count; i++)
            {
               Button aButton = new Button
                {
                    Width = cellWidth,
                    Height = cellHeight,
                    Content = i.ToString()
                };

                var left = random.NextDouble()*maxLeft;
                var top = random.NextDouble()*maxTop;

                MainPanel.Children.Add(aButton);

                MatrixAnimationUsingPath matrixAnimation = new MatrixAnimationUsingPath
                {
                    PathGeometry = SetPath(new Point(left, top), aimPoints[i]),
                    Duration = TimeSpan.FromSeconds(6),
                    //RepeatBehavior = RepeatBehavior.Forever
                };

                MatrixTransform buttonMatrixTransform = new MatrixTransform();
                aButton.RenderTransform = buttonMatrixTransform;

                var abttonName = "B" + i;

                this.RegisterName(abttonName, buttonMatrixTransform);

                Storyboard.SetTargetName(matrixAnimation, abttonName);
                Storyboard.SetTargetProperty(matrixAnimation, new
                    PropertyPath(MatrixTransform.MatrixProperty));

                pathAnimationStoryboard.Children.Add(matrixAnimation);
            }
        }

        private PathGeometry SetPath(Point startP, Point endP)
        {
            PathGeometry animationPath = new PathGeometry();
            PathFigure pFigure = new PathFigure();
            pFigure.StartPoint = new Point(startP.X, startP.Y);
            
            PolyBezierSegment pBezierSegment = new PolyBezierSegment();
            pBezierSegment.Points.Add(new Point(startP.X + 10, endP.Y - 5));
            pBezierSegment.Points.Add(new Point((startP.X + endP.X)/2, (startP.Y + endP.Y)/2));
            pBezierSegment.Points.Add(new Point(endP.X - 10, endP.Y + 5));
            
            pBezierSegment.Points.Add(new Point(endP.X, endP.Y));

            pFigure.Segments.Add(pBezierSegment);
            animationPath.Figures.Add(pFigure);

            animationPath.Freeze();
            
            return animationPath;
        }
    }
}
