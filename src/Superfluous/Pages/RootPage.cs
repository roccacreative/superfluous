using System;
using Xamarin.Forms;
using Superfluous.Pages;
using System.Linq;

namespace Superfluous
{
	public class RootPage : MasterDetailPage
	{
		public RootPage ()
		{
			NavigationPage.SetHasNavigationBar(this, false);

			var optionsPage = new MenuPage { Icon = "menu.png", Title = "menu" };

			optionsPage.Menu.ItemSelected += (sender, e) => SetEmail(e.SelectedItem as OptionItem);

			Master = optionsPage;

			NavigateTo(new MailboxPage());

			//ShowLoginDialog();
		}

		void NavigateTo(Page page)
		{
			#if WINDOWS_PHONE
			Detail = new ContentPage();//work around to clear current page.
			#endif
			Detail = new NavigationPage(page)
			{
				Tint = Helpers.Color.Blue.ToFormsColor(),
			};


			IsPresented = false;
		}

		void SetEmail(OptionItem item)
		{
			
		}
	}
}

