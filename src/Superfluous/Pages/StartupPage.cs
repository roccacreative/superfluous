using System;
using Xamarin.Forms;
using Superfluous.Services;
using System.Threading.Tasks;
using Superfluous.ViewModels;

namespace Superfluous.Pages
{
	public class StartupPage : ContentPage
	{
		public StartupPage (StartupViewModel viewModel)
		{
			viewModel.Navigation = Navigation;
			BindingContext = viewModel;

			BackgroundColor = Helpers.Color.Blue.ToFormsColor();

			var layout = new StackLayout { 
				Padding = 10,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			var label = new Label
			{
				Text = "Initializing Session",
				FontSize =  20,
				TextColor = Color.White,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
			};

			layout.Children.Add(label);

			var progress = new ActivityIndicator () {
				Color = Color.White
			};
			progress.IsRunning = true;

			layout.Children.Add (progress);

			Content = new ScrollView { Content = layout };

			Task.Run (async () => {
				await viewModel.Start();
			});
		}
	}
}