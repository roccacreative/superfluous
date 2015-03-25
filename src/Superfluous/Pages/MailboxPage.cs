using System;
using Xamarin.Forms;

namespace Superfluous.Pages
{
	public class MailboxPage : TabbedPage
	{
		public MailboxPage()
		{
			var inbox = TinyIoC.TinyIoCContainer.Current.Resolve<InboxPage> ();
			var settings = TinyIoC.TinyIoCContainer.Current.Resolve<SettingsPage> ();
			var send = TinyIoC.TinyIoCContainer.Current.Resolve<SendPage> ();

			this.Children.Add (inbox);
			this.Children.Add (send);
			this.Children.Add (settings);
		}
	}
}

