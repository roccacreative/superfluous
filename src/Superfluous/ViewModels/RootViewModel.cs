using System;
using Xamarin.Forms;

namespace Superfluous.ViewModels
{
	public class RootViewModel : BaseViewModel
	{
		public const string IsPresentedPropertyName = "IsPresented";
		private bool _isPresented;
		public bool IsPresented
		{
			get { return _isPresented; }
			set { SetProperty(ref _isPresented, value, IsPresentedPropertyName); }
		}

		public RootViewModel ()
		{
			EmailService.UsernameChanged += (obj) => {
				Device.BeginInvokeOnMainThread(() => {
					// make sure menu is hidden
					IsPresented = false;	
				});
			};
		}
	}
}

