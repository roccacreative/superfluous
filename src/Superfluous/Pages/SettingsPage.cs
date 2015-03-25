using System;
using Xamarin.Forms;
using Superfluous.Services;
using TinyIoC;
using Superfluous.ViewModels;

namespace Superfluous.Pages
{
	public class SettingsPage : ContentPage
	{
		private Label _nameLabel;
		private Label _domainLabel;

		public SettingsPage (SettingsViewModel viewModel)
		{
			viewModel.Navigation = Navigation;
			BindingContext = viewModel;

			Device.OnPlatform (
				() => {
					BackgroundColor = Helpers.Color.Purple.ToFormsColor ();
				},
				() => {
				});

			Icon = "settings.png";
			Title = "Settings";

			var layout = new StackLayout { Padding = 10 };

			_nameLabel = new Label {
				FontSize = 20,
				TextColor = Color.White,
				XAlign = TextAlignment.Center
			};
			_nameLabel.SetBinding<SettingsViewModel> (Label.TextProperty, m => m.Username);
				
			_domainLabel = new Label {
				Text = "@guerrillamail.com\r\n@guerrillamailblock.com",
				FontSize = 20,
				TextColor = Color.White,
				XAlign = TextAlignment.Center,
			};

			var details = new StackLayout {
				Children = {
					_nameLabel,
					_domainLabel
				},
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
				
			layout.Children.Add (details);

			var button = new Button { Text = "Change Address" };
			button.SetBinding<SettingsViewModel> (Button.CommandProperty, m => m.ChangeAddressCommand);

			Device.OnPlatform (
				() => {
					button.BackgroundColor = Color.White;
					button.TextColor = Helpers.Color.Purple.ToFormsColor ();
				},
				() => {
					button.BackgroundColor = Helpers.Color.Blue.ToFormsColor ();
					button.TextColor = Color.White;
				});

			var saveButton = new Button { TextColor = Color.White };
			saveButton.SetBinding<SettingsViewModel> (Button.BackgroundColorProperty, m => m.SaveBackgroundColor);
			saveButton.SetBinding<SettingsViewModel> (Button.TextProperty, m => m.SaveText);
			saveButton.SetBinding<SettingsViewModel> (Button.CommandProperty, m => m.SaveAddressCommand);

			layout.Children.Add (button);
			layout.Children.Add (saveButton);

			Content = new ScrollView { Content = layout };
		}
	}
}

