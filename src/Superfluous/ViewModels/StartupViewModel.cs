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

				Device.BeginInvokeOnMainThread(()=> {
					try {
						// replace route page once loaded
						var root = TinyIoC.TinyIoCContainer.Current.Resolve<RootPage>();
						var np =  root;

						App.Current.MainPage = np;	
					} catch (Exception ex) {
						
					}
				});
			});
		}
	}
}

