using System;
using Xamarin.Forms;
using DisposableMail;
using Superfluous.Renderers;

namespace Superfluous.Pages
{
	public class EmailPage : ContentPage
	{
		public EmailPage (Email email)
		{
			this.Title = email.MailSubject;

			var from = new StackLayout () {
				Children = { 
					new Label {
						Text = "From: ",
						BackgroundColor = Color.White,
						FontSize = 15,
						FontAttributes = FontAttributes.Bold,
					},
					new Label {
						Text = email.MailFrom,
						BackgroundColor = Color.White,
						FontSize = 15
					}
				},
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(10)
			};

			var subject = new StackLayout () {
				Children = {
					new Label {
						Text = email.MailSubject,
						BackgroundColor = Color.White,
						FontSize = 18,
						FontAttributes = FontAttributes.Bold
					},
					new Label {
						Text = email.MailDate,
						BackgroundColor = Color.White,
						FontSize = 15,
					}
				},
				Padding = new Thickness(10, 0)
			};

			var htmlSource = new HtmlWebViewSource ();
			htmlSource.Html = email.MailBody;

			var body = new StackLayout () {
				Children = {
					new CustomWebView () {
						VerticalOptions = LayoutOptions.FillAndExpand,
						HeightRequest = 300,
						WidthRequest = 320,
						Source = htmlSource
					}
				},
				Padding = new Thickness(10, 0)
			};

			Content = new ScrollView() {
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					Spacing = 10,
					Children = { from, subject, body }
				}
			};
		}
	}
}

