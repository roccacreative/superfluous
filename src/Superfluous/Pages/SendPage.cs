using System;
using System.Linq;
using Xamarin.Forms;
using Superfluous.ViewModels;
using Superfluous.Renderers;

namespace Superfluous.Pages
{
	public class SendPage : ContentPage
	{
		public SendPage (SendViewModel viewModel)
		{
			viewModel.Navigation = Navigation;
			BindingContext = viewModel;

			Title = "Send";
			Icon = "send.png";

			MessagingCenter.Subscribe<SendViewModel, bool>(this, "EmailSent", (arg1, sent) => {
				Device.BeginInvokeOnMainThread(() => {
					if(sent) {
						DisplayAlert("Success", "Email sent succesfully", "Ok");
					}
					else {
						DisplayAlert("Failed", "Email failed to send", "Ok");
					}
				});
			}, viewModel);

			var toEntry = new BorderlessEntry {
				Placeholder = string.Empty,
				Keyboard = Keyboard.Default,
				BackgroundColor = Color.Transparent,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 30
			};
			toEntry.SetBinding<SendViewModel> (BorderlessEntry.TextProperty, m => m.To, BindingMode.TwoWay);

			var subjectEntry = new BorderlessEntry {
				Placeholder = string.Empty,
				Keyboard = Keyboard.Default,
				BackgroundColor = Color.Transparent,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 30
			};
			subjectEntry.SetBinding<SendViewModel> (BorderlessEntry.TextProperty, m => m.Subject, BindingMode.TwoWay);

			var bodyEditor = new Editor {
				Keyboard = Keyboard.Default,
				HeightRequest = 200
			};
			bodyEditor.SetBinding<SendViewModel> (Editor.TextProperty, m => m.Body, BindingMode.TwoWay);

			var button = new Button () {
				Text = "Send",
				BackgroundColor = Helpers.Color.Blue.ToFormsColor(),
				TextColor = Color.White
			};
			button.SetBinding<SendViewModel> (Button.CommandProperty, m => m.SendEmailCommand);

			Content = new StackLayout {
				Children = {
					new TableView {
						Intent = TableIntent.Form,
						Root = new TableRoot (string.Empty) {
							new TableSection ("New Message") {
								new ViewCell {
									View = new StackLayout {
										Children = {
											new Label {
												Text = "To: ",
												FontSize = 15,
												HeightRequest = 30
											},
											toEntry
										},
										Orientation = StackOrientation.Horizontal,
										Padding = new Thickness(10)
									}
								},
								new ViewCell {
									View = new StackLayout {
										Children = {
											new Label {
												Text = "Subject: ",
												FontSize = 15,
												HeightRequest = 30
											},
											subjectEntry
										},
										Orientation = StackOrientation.Horizontal,
										Padding = new Thickness(10)
									}
								},
								new ViewCell {
									View = new StackLayout {
										Children = {
											bodyEditor
										},
										HeightRequest = 200,
										Padding = new Thickness(10)
									},
									Height = 220
								}
							}
						},
						HasUnevenRows = true,
					},
					new StackLayout {
						Children = {
							button
						},
						Padding = new Thickness(10)
					}
				}
			};
		}
	}
}