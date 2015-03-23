using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Superfluous.Pages;

namespace Superfluous.ViewModels
{
	public class SendViewModel : BaseViewModel
	{
		public const string FromPropertyName = "From";
		public string From
		{
			get { return EmailService.Email; }
		}

		public const string ToPropertyName = "To";
		private string _to;
		public string To
		{
			get { return _to; }
			set { SetProperty(ref _to, value, ToPropertyName); }
		}

		public const string SubjectPropertyName = "Subject";
		private string _subject;
		public string Subject
		{
			get { return _subject; }
			set { SetProperty(ref _subject, value, SubjectPropertyName); }
		}

		public const string BodyPropertyName = "Body";
		private string _body;
		public string Body
		{
			get { return _body; }
			set { SetProperty(ref _body, value, BodyPropertyName); }
		}

		private Command _sendEmailCommand;
		public const string SendEmailCommandPropertyName = "SendEmailCommand";
		public Command SendEmailCommand
		{
			get
			{
				return _sendEmailCommand ?? (_sendEmailCommand = new Command(() => SendEmail()));
			}
		}

		private Task SendEmail()
		{
			return Task.Run (() => {
				var sent = EmailService.SendEmail (To, Subject, Body);
				MessagingCenter.Send<SendViewModel, bool>(this, "EmailSent", sent);

				if(sent) {
					Device.BeginInvokeOnMainThread(()=> {
						To = string.Empty;
						Subject = string.Empty;
						Body = "\r\rSent from my iPhone";
					});
				}
			});
		}

		public SendViewModel ()
		{
			Body = "\r\rSent from my iPhone";
		}
	}
}

