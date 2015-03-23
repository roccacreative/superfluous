using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Superfluous.ViewModels
{
	public class StartupViewModel : BaseViewModel
	{
		public StartupViewModel ()
		{
			
		}

		public Task Start()
		{
			return Task.Run (async () => {
				await SessionService.Init();

				Device.BeginInvokeOnMainThread(async ()=> {
					// replace route page once loaded
					var root = TinyIoC.TinyIoCContainer.Current.Resolve<RootPage>();
					var np = new NavigationPage (root) { Title = "Navigation Stack" };
					App.Current.MainPage = np;
				});
			});
		}
	}
}

