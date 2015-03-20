using System;
using System.Linq;
using Xamarin.Forms;
using DisposableMail;
using Superfluous.Services;
using Superfluous.Pages;
using System.Threading.Tasks;

namespace Superfluous
{
	public class InboxPage : ContentPage
	{	
		private readonly ISessionService _sessionService;
		private readonly IEmailService _emailService;

		public static GuerrillaMail Mail = new GuerrillaMail();
		public static Mail FirstMail;

		private ListView _listView;

		public InboxPage (
			ISessionService sessionService,
			IEmailService emailService)
		{
			_emailService = emailService;
			_sessionService = sessionService;

			_emailService.UsernameChanged += (EmailUser user) => {
				Device.BeginInvokeOnMainThread(() => {
					var emails = _emailService.CheckEmail ();

					if(emails.Count > 0)
						_listView.ItemsSource = _emailService.Inbox.ToArray();
				});
			};

			Title = "Inbox";
			Icon = "mailbox.png";

			var inbox = _emailService.CheckEmail ();

			_listView = new ListView
			{
				RowHeight = 100
			};

			_listView.ItemsSource = inbox.ToArray ();
			_listView.IsPullToRefreshEnabled = true;
			_listView.ItemTemplate = new DataTemplate(typeof(EmployeeCell));
			_listView.ItemTapped += async (object sender, ItemTappedEventArgs e) => {
				var selected = e.Item as Mail;

				var email = _emailService.GetEmail(selected.MailID);

				await Navigation.PushAsync(new EmailPage(email));
			};
			_listView.Refreshing += (object sender, EventArgs e) => {
				var emails = _emailService.CheckEmail ();

				if(emails.Count > 0)
					_listView.ItemsSource = _emailService.Inbox.ToArray();
				
				_listView.IsRefreshing = false;
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
			subjectLabel.SetBinding<Mail>(Label.TextProperty, m => m.MailSubject);

			var fromLabel = new Label
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontSize = 12
			};

			fromLabel.SetBinding<Mail>(Label.TextProperty, m => m.MailFrom);

			var excerptLabel = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontSize = 10,
				TextColor = Color.Gray,
				LineBreakMode = LineBreakMode.WordWrap,
				HeightRequest = 45
			};
			excerptLabel.SetBinding<Mail> (Label.TextProperty, m => m.MailExcerpt);

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

