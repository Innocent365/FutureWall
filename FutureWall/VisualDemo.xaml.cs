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
                Geometry geo;
                
                var paths = canvase.Children.OfType<System.Windows.Shapes.Path>().ToList();
                if (paths.Count == 1)
                {
                    //geo = paths[0].RenderedGeometry;
                    geo = paths[0].Data;
                    result.Add(geo);
                    continue;
                }
                var geometrys = paths.ToList();
                var strList = geometrys.Select(p => p.Data.ToString()).ToArray();

                StringBuilder sb = new StringBuilder();
                foreach (var s in strList)
                {
                    sb.Append(s);
                    sb.Append(" ");
                }

                var coll = PathFigureCollection.Parse(sb.ToString());

                geo = new PathGeometry() { Figures = coll };
                
                //geometrys.ForEach(p=>((PathGeometry)geo).AddGeometry(p.RenderedGeometry));

                result.Add(geo);
            }

            return result;
        }
    }
}
