using System;
using System.Linq;
using Xamarin.Forms;
using Superfluous.ViewModels;
using TinyIoC;

namespace Superfluous.Pages
{
	public class UsernamePage : ContentPage
	{
		public UsernamePage ()
		{
			var viewModel = TinyIoCContainer.Current.Resolve<LoginViewModel> ();
			viewModel.Navigation = Navigation;

			BindingContext = viewModel;

			BackgroundColor = Helpers.Color.DarkBlue.ToFormsColor();

			var layout = new StackLayout { Padding = 10 };

			var label = new Label
			{
				Text = "Select domain and username",
				FontSize = 20,
				FontAttributes = FontAttributes.None,
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
			};

			layout.Children.Add(label);

			var username = new Entry { Placeholder = "Username", HeightRequest = 50 };
			username.SetBinding(Entry.TextProperty, LoginViewModel.UsernamePropertyName);
			layout.Children.Add(username);

			var button = new Button { Text = "Confirm", TextColor = Color.Purple, BackgroundColor = Color.White, HeightRequest = 50 };
			button.SetBinding(Button.CommandProperty, LoginViewModel.LoginCommandPropertyName);
			layout.Children.Add(button);

			var cancelButton = new Button { Text = "Cancel", TextColor = Color.Red, BackgroundColor = Color.White, HeightRequest = 40 };
			cancelButton.SetBinding(Button.CommandProperty, LoginViewModel.LoginCommandPropertyName);
			layout.Children.Add(cancelButton);

			Content = new ScrollView { Content = layout };
		}
	}
}

