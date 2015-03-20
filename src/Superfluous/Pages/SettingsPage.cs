using System;
using Xamarin.Forms;
using Superfluous.Services;
using TinyIoC;

namespace Superfluous.Pages
{
	public class SettingsPage : ContentPage
	{
		private readonly IEmailService _emailService;

		private Label _nameLabel;
		private Label _domainLabel;

		public SettingsPage ()
		{
			_emailService = TinyIoCContainer.Current.Resolve<IEmailService> ();

			_emailService.UsernameChanged+= async (DisposableMail.EmailUser obj) => {
				_nameLabel.Text = GetEmail();
			};

			BackgroundColor = Helpers.Color.Purple.ToFormsColor();

			var layout = new StackLayout { Padding = 10 };

			_nameLabel = new Label {
				Text = GetEmail (),
				FontSize = 20,
				TextColor = Color.White,
				XAlign = TextAlignment.Center
			};

			_domainLabel = new Label {
				Text = "@guerrillamail.com\r\n@guerrillamailblock.com",
				FontSize = 20,
				TextColor = Color.White,
				XAlign = TextAlignment.Center
			};

			var details = new StackLayout
			{
				Children = {
					_nameLabel,
					_domainLabel
				},
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
				
			layout.Children.Add(details);

			var button = new Button { Text = "Change Address", TextColor = Color.Purple, BackgroundColor = Color.White };
			button.Clicked += (sender, e) => {
				ShowLoginDialog();
			};

			var saveButton = new Button {
				Text = "Save Address",
				TextColor = Color.White,
				BackgroundColor = Color.Green
			};

			saveButton.Clicked+= (sender, e) => {
				SaveEmailAddress();
			};

			layout.Children.Add(button);
			layout.Children.Add(saveButton);

			Content = new ScrollView { Content = layout };
		}

		string GetEmail ()
		{
			string email = _emailService.Email;
			int atIndex = _emailService.Email.IndexOf ("@");
			string name = email.Substring (0, atIndex);

			//string domain = email.Substring (atIndex, email.Length - atIndex);

			return name;
		}

		async void ShowLoginDialog()
		{
			var page = new UsernamePage();
			await Navigation.PushModalAsync(page);
		}

		void SaveEmailAddress()
		{
			
		}
	}
}

