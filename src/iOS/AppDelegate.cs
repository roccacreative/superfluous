using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using TinyIoC;
using Superfluous.Services;
using Superfluous.Data;
using Superfluous.iOS.Data;

namespace Superfluous.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			InitializeServices ();

			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		private void InitializeServices()
		{
			var container = TinyIoCContainer.Current;

			container.Register<IEmailService, EmailService> ().AsSingleton();
			container.Register<ISessionService, SessionService> ().AsSingleton();

			container.Register<ISQLite, SQLite_iOS> ();
		}
	}
}

