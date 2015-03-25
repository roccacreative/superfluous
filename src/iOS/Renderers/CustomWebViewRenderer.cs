using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Xamarin.Forms;
using Superfluous.Renderers;
using CoreGraphics;

[assembly: ExportRenderer (typeof (CustomWebView), typeof (CustomWebViewRenderer))]
public class CustomWebViewRenderer : WebViewRenderer
{
	protected override void OnElementChanged(VisualElementChangedEventArgs e)
	{
		base.OnElementChanged (e);
		if (e.OldElement == null) {   // perform initial setup
			// lets get a reference to the native UIWebView
			var webView = this;
			// do whatever you want to the UIWebView here!
			Frame = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
			webView.ScalesPageToFit = true;
		}
	}
}

