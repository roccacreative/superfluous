using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TinyIoC;
using Superfluous.Services;
using Superfluous.Data;
using Superfluous.Droid.Data;

namespace Superfluous.Droid
{
	[Activity (Label = "Superfluous", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			InitializeServices ();

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
		}

		private void InitializeServices()
		{
			var container = TinyIoCContainer.Current;

			container.Register<IEmailService, EmailService> ().AsSingleton();
			container.Register<ISessionService, SessionService> ().AsSingleton();

			container.Register<ISQLite, SQLite_Android> ();
		}
	}
}

