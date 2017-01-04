using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace FutureWall
{
    /// <summary>
    /// VisualDemo.xaml 的交互逻辑
    /// </summary>
    public partial class VisualDemo
    {
        public VisualDemo()
        {
            InitializeComponent();
        }

    }

    public static class AssistGeometry
    {
        public static List<Geometry> GetStoryGeometrys()
        {
            var result = new List<Geometry>();

            var demo = new VisualDemo();
            var groups = demo.MainPanel.Children.OfType<Canvas>();

            foreach (var canvase in groups)
            {
                
                var paths = canvase.Children.OfType<System.Windows.Shapes.Path>().ToList();                
                var strList = paths.Select(p => p.Data.ToString());
                var coll = PathFigureCollection.Parse(string.Join(" ", strList));

                var geo = new PathGeometry() { Figures = coll };
                //geometrys.ForEach(p=>((PathGeometry)geo).AddGeometry(p.RenderedGeometry));

                result.Add(geo);
            }

            return result;
        }
    }
}
