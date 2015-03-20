using System;
using Xamarin.Forms;

namespace Superfluous.Pages
{
	public class MailboxPage : TabbedPage
	{
		public MailboxPage()
		{
			try {
				var inbox = TinyIoC.TinyIoCContainer.Current.Resolve<InboxPage> ();

				this.Children.Add (inbox);
				this.Children.Add (new SendPage () { Title = "Send", Icon = "send.png" });
				this.Children.Add (new SettingsPage() { Title = "Settings", Icon = "settings.png" });

			} catch (Exception ex) {
					
			}
		}
	}
}

