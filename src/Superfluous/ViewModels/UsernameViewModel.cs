using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using DisposableMail;
using Superfluous.Services;

namespace Superfluous.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private readonly ISessionService _sessionService;
		private readonly IEmailService _emailService;

		private INavigation navigation;

		public static string[] Domains = {
			"guerrillamail.biz",
			"guerrillamail.com",
			"guerrillamail.de",
			"guerrillamail.net",
			"guerrillamail.org",
			"guerrillamailblock.com",
		};

		public LoginViewModel(
			ISessionService sessionService,
			IEmailService emailService)
		{
			_sessionService = sessionService;
			_emailService = emailService;
		}
		public const string UsernamePropertyName = "Username";
		private string username = string.Empty;
		public string Username
		{
			get { return username; }
			set { SetProperty(ref username, value, UsernamePropertyName); }
		}

		public const string PasswordPropertyName = "Password";
		private string password = string.Empty;
		public string Password
		{
			get { return password; }
			set { SetProperty(ref password, value, PasswordPropertyName); }
		}

		public const string EmailPropertyName = "Email";
		private string email = string.Empty;
		public string Email
		{
			get { return email; }
			set { SetProperty(ref email, value, EmailPropertyName); }
		}

		public const string DomainIndexPropertyName = "Domain";
		private int domain;
		public int DomainIndex
		{
			get { return domain; }
			set { SetProperty(ref domain, value, DomainIndexPropertyName); }
		}

		private Command loginCommand;
		public const string LoginCommandPropertyName = "LoginCommand";
		public Command LoginCommand
		{
			get
			{
				return loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginCommand()));
			}
		}

		private Command cancelCommand;
		public const string CancelCommandPropertyName = "CancelCommand";
		public Command CancelCommand
		{
			get
			{
				return cancelCommand ?? (cancelCommand = new Command(async () => await ExecuteCancelCommand()));
			}
		}

		protected async Task ExecuteLoginCommand()
		{
			try {
				_emailService.SetEmail (username + "@");
				await Navigation.PopModalAsync();
			} catch (Exception ex) {
				
			}
		}

		protected async Task ExecuteCancelCommand()
		{
			await Navigation.PopModalAsync();
		}
	}
}

