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
		private static ISessionService _sessionService;
		public static ISessionService SessionService {
			get { return _sessionService;}
		}

		private static IEmailService _emailService;
		public static IEmailService EmailService {
			get { return _emailService;}
		}

		public App ()
		{
			_sessionService = TinyIoC.TinyIoCContainer.Current.Resolve<ISessionService> ();
			_emailService = TinyIoC.TinyIoCContainer.Current.Resolve<IEmailService> ();

			var np = new StartupPage ();

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

