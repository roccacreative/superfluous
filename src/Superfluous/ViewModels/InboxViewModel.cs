using System;
using DisposableMail;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Superfluous.Pages;

namespace Superfluous.ViewModels
{
	public class InboxViewModel : BaseViewModel
	{
		public const string InboxPropertyName = "Inbox";
		private List<Email> _inbox;
		public List<Email> Inbox
		{
			get { return _inbox; }
			set { SetProperty(ref _inbox, value, InboxPropertyName); }
		}

		public const string SelectedItemPropertyName = "SelectedItem";
		private Email _selectedItem;
		public Email SelectedItem
		{
			get { return _selectedItem; }
			set { SetProperty(ref _selectedItem, value, SelectedItemPropertyName); }
		}

		public const string InboxTemplatePropertyName = "InboxTemplate";
		private DataTemplate _inboxTemplate;
		public DataTemplate InboxTemplate
		{
			get { return _inboxTemplate; }
			set { SetProperty(ref _inboxTemplate, value, InboxTemplatePropertyName); }
		}

		private Command _refreshCommand;
		public const string RefreshCommandPropertyName = "RefreshCommand";
		public Command RefreshCommand
		{
			get
			{
				return _refreshCommand ?? (_refreshCommand = new Command(async () => await RefreshInbox()));
			}
		}

		private Command _itemSelectedCommand;
		public const string ItemSelectedCommandPropertyName = "ItemSelectedCommand";
		public Command ItemSelectedCommand
		{
			get
			{
				return _itemSelectedCommand ?? (_itemSelectedCommand = new Command(async () => await ItemSelected()));
			}
		}

		private Task RefreshInbox()
		{
			return Task.Run (() => {
				UpdateEmails();
				IsBusy = false;
			});
		}

		private Task ItemSelected()
		{
			return Task.Run (() => {
				var selected = SelectedItem;

				var email = EmailService.GetEmail(selected.MailId);

				Device.BeginInvokeOnMainThread(async () => {
					var emailPage = TinyIoC.TinyIoCContainer.Current.Resolve<EmailPage>();
					(emailPage.BindingContext as EmailViewModel).Email = email;

					await Navigation.PushAsync(emailPage);	
				});
			});
		}

		public InboxViewModel ()
		{
			Inbox = EmailService.CheckEmail ();
			InboxTemplate = new DataTemplate (typeof(EmployeeCell));

			EmailService.UsernameChanged += (EmailUser user) => {
				Device.BeginInvokeOnMainThread(() => {
					UpdateEmails ();
				});
			};
		}

		void UpdateEmails ()
		{
			try {
				var emails = EmailService.CheckEmail ();

				if (emails.Count > 0) {
					Inbox = EmailService.Inbox;
				}
			} catch (Exception ex) {
				throw ex;
			}
		}
	}
}

