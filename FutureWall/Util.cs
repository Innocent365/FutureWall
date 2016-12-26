using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
            //pBezierSegment.Points.Add(new Point((startP.X + endP.X) / 2, (startP.Y + endP.Y) / 2));
            pBezierSegment.Points.Add(new Point(endP.X - 10, endP.Y + 5));

            pBezierSegment.Points.Add(new Point(endP.X, endP.Y));

            pFigure.Segments.Add(pBezierSegment);
            animationPath.Figures.Add(pFigure);

            animationPath.Freeze();

            return animationPath;
        }
    }

    public static class Geometry
    {
        static Geometry()
        {
            Func<double, double, bool> Circle = (x, y) => { return false; };
            Func<double, double, bool> Circle1 = (x, y) => { return false; };
            Func<double, double, bool> Circle2 = (x, y) => { return false; };
        }

        public static Point[] FigureContour(Size panelSize, Size singleSize)
        {
            var aimPoints = new List<Point>();
            var pathGeo = GetPathGeometry();

            for (var i = 0d; i < panelSize.Width; i += singleSize.Width)
            {
                for (var j = 0d; j < panelSize.Height; j += singleSize.Height)
                {
                    var rect = new Rect(i, j, singleSize.Width, singleSize.Height);

                    if (pathGeo.FillContains(new RectangleGeometry(rect), 0.00001, ToleranceType.Relative))
                    aimPoints.Add(new Point(i, j));
                }
            }            
            return aimPoints.ToArray();

        }

        private static PathGeometry GetPathGeometry()
        {
            var pathGeo = new PathGeometry();
            pathGeo.Figures = PathFigureCollection.Parse("M 80,80 C 170,320 220,360 380,320 C 310,80 190,60 80,80");

            return pathGeo;
        }

        
    }
}
