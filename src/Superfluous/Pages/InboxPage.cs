using System;
using System.Linq;
using Xamarin.Forms;
using DisposableMail;
using Superfluous.Services;
using Superfluous.Pages;
using System.Threading.Tasks;
using Superfluous.ViewModels;

namespace Superfluous
{
	public class InboxPage : ContentPage
	{	
		public static GuerrillaMail Mail = new GuerrillaMail();
		public static Email FirstMail;

		private ListView _listView;

		public InboxPage (InboxViewModel viewModel)
		{
			viewModel.Navigation = Navigation;

			BindingContext = viewModel;

			Title = "Inbox";
			Icon = "mailbox.png";

			_listView = new ListView
			{
				RowHeight = 100
			};
					
			_listView.IsPullToRefreshEnabled = true;
			_listView.SetBinding<InboxViewModel> (ListView.ItemTemplateProperty, m => m.InboxTemplate);
			_listView.SetBinding<InboxViewModel> (ListView.RefreshCommandProperty, m => m.RefreshCommand);
			_listView.SetBinding<InboxViewModel> (ListView.SelectedItemProperty, m => m.SelectedItem);
			_listView.SetBinding<InboxViewModel> (ListView.IsRefreshingProperty, m => m.IsBusy);
			_listView.SetBinding<InboxViewModel> (ListView.ItemsSourceProperty, m => m.Inbox);

			// no binding available
			_listView.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				viewModel.ItemSelectedCommand.Execute(null);
			};

			Content = new StackLayout
			{
				Children = { _listView }
			};

			//Device.StartTimer (new TimeSpan (0, 0, 30), UpdateInbox);
		}
	}


	class EmployeeCell : ViewCell
	{
		public EmployeeCell()
		{
			//var image = new Image
			//{
			//	HorizontalOptions = LayoutOptions.Start
			//};
			//image.SetBinding(Image.SourceProperty, new Binding("ImageUri"));
			//image.WidthRequest = image.HeightRequest = 40;

			var nameLayout = CreateNameLayout();

			var viewLayout = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				Children = { nameLayout }
			};
			View = viewLayout;
		}

		static StackLayout CreateNameLayout()
		{
			var subjectLabel = new Label
			{
				HorizontalOptions= LayoutOptions.FillAndExpand,
				FontSize = 15,
				FontAttributes = FontAttributes.Bold
			};
			subjectLabel.SetBinding<Email>(Label.TextProperty, m => m.MailSubject);

			var fromLabel = new Label
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontSize = 12
			};

			fromLabel.SetBinding<Email>(Label.TextProperty, m => m.MailFrom);

			var excerptLabel = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontSize = 10,
				TextColor = Color.Gray,
				LineBreakMode = LineBreakMode.WordWrap,
				HeightRequest = 45
			};
			excerptLabel.SetBinding<Email> (Label.TextProperty, m => m.MailExcerpt);

			var nameLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Orientation = StackOrientation.Vertical,
				Children = { subjectLabel, fromLabel, excerptLabel },
				Padding = new Thickness(30, 10, 0, 0)
			};
			return nameLayout;
		}
	}
}

