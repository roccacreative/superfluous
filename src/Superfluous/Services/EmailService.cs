using System;
using System.Linq;
using DisposableMail;
using System.Collections.Generic;
using Superfluous.Data;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;
using System.Threading.Tasks;
using Superfluous.Models;

namespace Superfluous.Services
{
	public interface IEmailService 
	{
		event Action<EmailUser> UsernameChanged;
		event Action<Username> UsernameForgotten;
		event Action<Username> UsernameRemembered;
		event Action<bool> EmailSent;

		string Email {get;}
		string Alias {get;}

		List<Email> Inbox { get; set; }

		List<Email> CheckEmail ();
		Email GetEmail (int id);
		EmailUser SetEmail (string email);
		bool SendEmail (string to, string subject, string body);

		Task Remember(Username username);
		Task Forget(Username username);
	}

	public class EmailService : IEmailService
	{
		public event Action<EmailUser> UsernameChanged;
		public event Action<Username> UsernameForgotten;
		public event Action<Username> UsernameRemembered;
		public event Action<bool> EmailSent;

		public List<Email> Inbox { get; set; }

		public string Email {
			get {
				return _mailbox.Email;
			}
		}

		public string Alias {
			get {
				return _mailbox.Alias;
			}
		}

		private readonly GuerrillaMail _mailbox;

		public EmailService ()
		{
			Inbox = new List<Email> ();
				
			_mailbox = new GuerrillaMail ();
			_mailbox.GetEmailAddress ();
		}

		public List<Email> CheckEmail()
		{
			var emails = _mailbox.CheckEmail ().MailList;

			if (emails.Count > 0)
				Inbox = emails;
			
			return emails;
		}

		public Email GetEmail(int id)
		{
			return _mailbox.FetchEmail (id);
		}

		public EmailUser SetEmail (string email)
		{
			var user = _mailbox.SetEmailUser (email);

			Inbox = new List<Email> (); // reset inbox when changing username

			if (UsernameChanged != null) {
				UsernameChanged (user);
			}

			return user;
		}

		public bool SendEmail (string to, string subject, string body)
		{
			var sent = _mailbox.SendEmail (to, subject, body);

			if (EmailSent != null) {
				EmailSent (sent);
			}

			return sent;
		}

		public Task Remember (Username username)
		{
			return Task.Run (async () => {
				await new SuperfluousDatabase ().GetConnection ()
					.InsertAsync (username);

				if (UsernameRemembered != null) {
					UsernameRemembered (username);
				}
			});
		}

		public Task Forget (Username username)
		{
			return Task.Run (async () => {
				await new SuperfluousDatabase().GetConnection ()
					.DeleteAsync (username);

				if(UsernameForgotten != null) {
					UsernameForgotten(username);
				}
			});
		}
	}
}

