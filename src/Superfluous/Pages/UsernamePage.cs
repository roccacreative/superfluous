using System;
using System.Linq;
using Xamarin.Forms;
using Superfluous.ViewModels;
using TinyIoC;

namespace Superfluous.Pages
{
	public class UsernamePage : ContentPage
	{
		public UsernamePage (UsernameViewModel viewModel)
		{
			viewModel.Navigation = Navigation;

			BindingContext = viewModel;

			BackgroundColor = Helpers.Color.DarkBlue.ToFormsColor();

			var layout = new StackLayout { Padding = 10 };

			var label = new Label
			{
				Text = "Select new identity",
				FontSize = 20,
				FontAttributes = FontAttributes.None,
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
			};

			layout.Children.Add(label);

			var username = new Entry { Placeholder = "Username", HeightRequest = 50 };
			username.SetBinding(Entry.TextProperty, UsernameViewModel.UsernamePropertyName);
			layout.Children.Add(username);

			var button = new Button { Text = "Confirm", TextColor = Color.White, BackgroundColor = Helpers.Color.Green.ToFormsColor(), HeightRequest = 50 };
			button.SetBinding(Button.CommandProperty, UsernameViewModel.LoginCommandPropertyName);
			layout.Children.Add(button);

			var cancelButton = new Button { Text = "Cancel", TextColor = Color.Red, BackgroundColor = Color.White, HeightRequest = 40 };
			cancelButton.SetBinding(Button.CommandProperty, UsernameViewModel.CancelCommandPropertyName);
			layout.Children.Add(cancelButton);

			Content = new ScrollView { Content = layout };
		}
	}
}

