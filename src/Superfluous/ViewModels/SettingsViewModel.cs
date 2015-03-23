using System;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using TinyIoC;
using Superfluous.Pages;
using Superfluous.Models;
using DisposableMail;

namespace Superfluous.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		public const string UsernamePropertyName = "Username";
		private string _username;
		public string Username
		{
			get { return _username; }
			set { SetProperty(ref _username, value, UsernamePropertyName); }
		}

		public const string SaveBackgroundColorPropertyName = "SaveBackgroundColor";
		private Color _saveBackgroundColor;
		public Color SaveBackgroundColor
		{
			get { return _saveBackgroundColor; }
			set { SetProperty(ref _saveBackgroundColor, value, SaveBackgroundColorPropertyName); }
		}

		public const string SaveTextPropertyName = "SaveText";
		private string _saveText;
		public string SaveText
		{
			get { return _saveText; }
			set { SetProperty(ref _saveText, value, SaveTextPropertyName); }
		}

		public SettingsViewModel ()
		{
			Username = GetEmail ();

			UpdateSaveButton ();

			EmailService.UsernameChanged+= (EmailUser obj) => {
				Username = GetEmail();
				UpdateSaveButton ();
			};
		}

		string GetEmail ()
		{
			string email = EmailService.Email;
			int atIndex = EmailService.Email.IndexOf ("@");
			string name = email.Substring (0, atIndex);

			//string domain = email.Substring (atIndex, email.Length - atIndex);

			return name;
		}

		void UpdateSaveButton()
		{
			if (SessionService.Current.Emails.Count (e => e.Address == Username) > 0) {
				SaveBackgroundColor = Helpers.Color.Gray.ToFormsColor ();
				SaveText = "Forget";
			} else {
				SaveBackgroundColor = Helpers.Color.Green.ToFormsColor ();
				SaveText = "Remember";
			}
		}

		private Command saveAddressCommand;
		public const string SaveAddressCommandPropertyName = "SaveAddressCommand";
		public Command SaveAddressCommand
		{
			get
			{
				return saveAddressCommand ?? (saveAddressCommand = new Command(async () => await SaveAddress()));
			}
		}

		private Task SaveAddress()
		{
			return Task.Run (async () => {
				if (SessionService.Current.Emails.Count (e => e.Address == Username) > 0) {
					var email = SessionService.Current.Emails.FirstOrDefault(e => e.Address == Username);
					await EmailService.Forget(email);
				}
				else {
					var username = new Username() {
						Address = Username,
						Created = DateTime.Now,
						SessionId = SessionService.Current.Id
					};
					await EmailService.Remember(username);
				}

				UpdateSaveButton();
			});
		}

		private Command changeAddressCommand;
		public const string ChangeAddressCommandPropertyName = "ChangeAddressCommand";
		public Command ChangeAddressCommand
		{
			get
			{
				return changeAddressCommand ?? (changeAddressCommand = new Command(async () => await ChangeAddress()));
			}
		}

		async Task ChangeAddress()
		{
			var page = TinyIoCContainer.Current.Resolve<UsernamePage> ();
			await Navigation.PushModalAsync(page);
		}
	}
}

