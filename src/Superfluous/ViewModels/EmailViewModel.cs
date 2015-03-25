using System;
using Superfluous.Services;
using DisposableMail;

namespace Superfluous.ViewModels
{
	public class EmailViewModel : BaseViewModel
	{
		public const string EmailPropertyName = "Email";
		private Email _email;
		public Email Email
		{
			get { return _email; }
			set { SetProperty(ref _email, value, EmailPropertyName); }
		}

		public EmailViewModel ()
		{
			
		}
	}
}

