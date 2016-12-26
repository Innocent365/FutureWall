using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FutureWall
{
    public class ImageCell : Image
    {
        public Size Size{
            set
            {
                this.Width = value.Width;
                this.Height = value.Height;
            }}

        public ImageSource Source;

        //虚化边缘，等处理
        public void Blur()
        {
            
        }




    }


}
