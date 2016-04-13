using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BloodApp.UI.Uwp.Views
{
	public sealed partial class TabHeader : UserControl
	{
		public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register("Glyph", typeof(string), typeof(TabHeader), null);

		public string Glyph
		{
			get { return this.GetValue(TabHeader.GlyphProperty) as string; }
			set { this.SetValue(TabHeader.GlyphProperty, value); }
		}

		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TabHeader), null);

		public string Label
		{
			get { return this.GetValue(TabHeader.LabelProperty) as string; }
			set { this.SetValue(TabHeader.LabelProperty, value); }
		}


		public TabHeader()
		{
			this.InitializeComponent();
			this.DataContext = this;
		}
	}
}
