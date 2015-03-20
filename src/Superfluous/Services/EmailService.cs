using System;
using DisposableMail;
using System.Collections.Generic;
using Superfluous.Data;

namespace Superfluous.Services
{
	public interface IEmailService 
	{
		event Action<EmailUser> UsernameChanged;

		string Email {get;}
		string Alias {get;}

		List<Mail> Inbox { get; set; }

		List<Mail> CheckEmail ();
		Email GetEmail (int id);
		EmailUser SetEmail (string email);
	}

	public class EmailService : IEmailService
	{
		public event Action<EmailUser> UsernameChanged;

		public List<Mail> Inbox { get; set; }

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
			Inbox = new List<Mail> ();
				
			_mailbox = new GuerrillaMail ();
			_mailbox.GetEmailAddress ();
		}

		public List<Mail> CheckEmail()
		{
			var emails = _mailbox.CheckEmail ().MailList;
			Inbox.AddRange (emails);
			return emails;
		}

		public Email GetEmail(int id)
		{
			return _mailbox.FetchEmail (id);
		}

		public EmailUser SetEmail (string email)
		{
			var user = _mailbox.SetEmailUser (email);

			Inbox = new List<Mail> (); // reset inbox when changing username

			if (UsernameChanged != null) {
				UsernameChanged (user);
			}

			return user;
		}
	}
}

