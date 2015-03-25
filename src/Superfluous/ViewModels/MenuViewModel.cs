using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Superfluous.ViewModels
{
	public class MenuViewModel : BaseViewModel
	{
		public const string SavedEmailsPropertyName = "SavedEmails";
		private List<OptionItem> savedEmails;
		public List<OptionItem> SavedEmails
		{
			get { return savedEmails; }
			set { SetProperty(ref savedEmails, value, SavedEmailsPropertyName); }
		}

		public const string SelectedItemPropertyName = "SelectedItem";
		private OptionItem selectedItem;
		public OptionItem SelectedItem
		{
			get { return selectedItem; }
			set { SetProperty (ref selectedItem, value, SelectedItemPropertyName, () => ItemSelected ()); }
		}

		public const string SavedEmailsTemplatePropertyName = "SavedEmailsTemplate";
		private DataTemplate savedEmailsTemplate;
		public DataTemplate SavedEmailsTemplate
		{
			get { return savedEmailsTemplate; }
			set { SetProperty (ref savedEmailsTemplate, value, SavedEmailsTemplatePropertyName); }
		}

		public MenuViewModel ()
		{
			SavedEmails = new List<OptionItem> ();
		
			UpdateRememberedEmails();

			EmailService.UsernameForgotten += (username) =>  {
				SessionService.Current.Emails.Remove(username);

				Device.BeginInvokeOnMainThread(() => {
					UpdateRememberedEmails();
				});
			};

			EmailService.UsernameRemembered+= (username) => {
				SessionService.Current.Emails.Add(username);

				Device.BeginInvokeOnMainThread(() => {
					UpdateRememberedEmails();
				});
			};

			SavedEmailsTemplate = CreateTemplate ();
		}

		private void UpdateRememberedEmails()
		{
			var emails = new List<OptionItem> ();

			foreach (var email in SessionService.Current.Emails) {
				emails.Add (new OptionItem () { Title = email.Address });
			}

			SavedEmails = emails;
		}

		private DataTemplate CreateTemplate()
		{
			var cell = new DataTemplate(typeof(TextCell));
			cell.SetBinding(TextCell.TextProperty, "Title");
			cell.SetValue (TextCell.TextColorProperty, Color.White);
			cell.SetValue(VisualElement.BackgroundColorProperty, Color.Transparent);

			return cell;
		}

		private Task ItemSelected()
		{
			return Task.Run (() => {
				if (SelectedItem != null) {
					EmailService.SetEmail (SelectedItem.Title);
				}
			});
		}
	}
}

