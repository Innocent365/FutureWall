using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FutureWall {
	public partial class ImageCell {
		public ImageCell() {
			InitializeComponent();
		}

		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
			"Source", typeof (ImageSource), typeof (ImageCell), new PropertyMetadata(default(ImageSource)));

		public ImageSource Source
		{
			get { return (ImageSource) GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }		
        }


	    public Point Location { get; set; }
	        //get
	        //{
	        //    return new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
	        //}
	        //set
	        //{
	        //    Canvas.SetLeft(this, value.X);
	        //    Canvas.SetTop(this, value.Y);
	        //}
	    
	}
}
