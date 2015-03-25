using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Superfluous.Renderers;
using UIKit;

[assembly: ExportRenderer (typeof (BorderlessEntry), typeof (BorderlessEntryRenderer))]
public class BorderlessEntryRenderer : EntryRenderer
{
	protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
	{
		base.OnElementChanged (e);

		if (Control != null) {
			Control.BorderStyle = UITextBorderStyle.None;
		}
	}
}

