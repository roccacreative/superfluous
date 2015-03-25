using System;
using Xamarin.Forms;
using Superfluous.Pages;
using System.Linq;
using Superfluous.ViewModels;

namespace Superfluous
{
	public class RootPage : MasterDetailPage
	{
		public RootPage (RootViewModel viewModel)
		{
			viewModel.Navigation = Navigation;
			BindingContext = viewModel;

			var page = TinyIoC.TinyIoCContainer.Current.Resolve<MenuPage> ();

			NavigationPage.SetHasNavigationBar(this, false);

			Master = page;

			this.SetBinding<RootViewModel> (MasterDetailPage.IsPresentedProperty, m => m.IsPresented, BindingMode.TwoWay);
				
			// no binding to set detail
			NavigateTo(new MailboxPage());
		}

		void NavigateTo(Page page)
		{
			#if WINDOWS_PHONE
			Detail = new ContentPage();//work around to clear current page.
			#endif
			Detail = new NavigationPage(page)
			{
				BackgroundColor = Helpers.Color.Blue.ToFormsColor(),
			};

			IsPresented = false;
		}
	}
}