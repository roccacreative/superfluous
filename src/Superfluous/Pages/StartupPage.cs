using System;
using Xamarin.Forms;
using Superfluous.Services;
using System.Threading.Tasks;

namespace Superfluous.Pages
{
	public class StartupPage : ContentPage
	{
		private readonly ISessionService _sessionService;

		public StartupPage ()
		{
			_sessionService = TinyIoC.TinyIoCContainer.Current.Resolve<ISessionService> ();

			BackgroundColor = Helpers.Color.Blue.ToFormsColor();

			var layout = new StackLayout { 
				Padding = 10,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			var label = new Label
			{
				Text = "Initializing Session",
				FontSize =  25,
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
				await Start ();
			});
		}

		private Task Start()
		{
			return Task.Run (async () => {
				await _sessionService.Init();

				Device.BeginInvokeOnMainThread(async ()=> {
					// replace route page once loaded
					var np = new NavigationPage (new RootPage ()) { Title = "Navigation Stack" };
					App.Current.MainPage = np;
				});
			});
		}
	}
}