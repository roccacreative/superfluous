using System;

using Xamarin.Forms;
using Superfluous.Pages;
using Superfluous.Data;
using Superfluous.Models;
using Superfluous.Services;
using System.Threading.Tasks;

namespace Superfluous
{
	public class App : Application
	{
		public App ()
		{
			var np = TinyIoC.TinyIoCContainer.Current.Resolve<StartupPage> ();

			// The root page of your application
			MainPage = np;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

