using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
	}
}
