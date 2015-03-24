using System;
using Xamarin.Forms;
using DisposableMail;
using Superfluous.Renderers;
using Superfluous.ViewModels;

namespace Superfluous.Pages
{
	public class EmailPage : ContentPage
	{
		public EmailPage (EmailViewModel viewModel)
		{
			viewModel.Navigation = Navigation;
			BindingContext = viewModel;

			var fromLabel = new Label {
				FontSize = 15
			};

			fromLabel.SetBinding<EmailViewModel> (Label.TextProperty, m => m.Email.MailFrom);
			
			var from = new StackLayout () {
				Children = { 
					new Label {
						Text = "From: ",
						FontSize = 15,
						FontAttributes = FontAttributes.Bold,
					},
					fromLabel
				},
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(10)
			};

			var subjectLabel = new Label {
				FontSize = 18,
				FontAttributes = FontAttributes.Bold,
			};
			subjectLabel.SetBinding<EmailViewModel> (Label.TextProperty, m => m.Email.MailSubject);

			var dateLabel = new Label {
				BackgroundColor = Color.White,
				FontSize = 15,
			};
			dateLabel.SetBinding<EmailViewModel> (Label.TextProperty, m => m.Email.MailDate);

			var subject = new StackLayout () {
				Children = {
					subjectLabel,

				},
				Padding = new Thickness(10, 0)
			};

			var htmlSource = new HtmlWebViewSource ();
			htmlSource.SetBinding<EmailViewModel> (HtmlWebViewSource.HtmlProperty, m => m.Email.MailBody);

			var webView = new CustomWebView () {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Source = htmlSource
			};

			var body = new StackLayout () {
				Children = {
					webView
				},
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(10, 0)
			};

			// need to pass the binding onto the html source
			webView.BindingContextChanged += (sender, args) => {
				htmlSource.BindingContext = webView.BindingContext;
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

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			this.Title = (BindingContext as EmailViewModel).Email.MailSubject;
		}
	}
}

