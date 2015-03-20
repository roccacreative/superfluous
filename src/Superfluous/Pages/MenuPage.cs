using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Superfluous.Services;

namespace Superfluous.Pages
{
	public class MenuPage : ContentPage
	{
		private readonly ISessionService _sessionService;

		readonly List<OptionItem> OptionItems = new List<OptionItem>();

		public ListView Menu { get; set; }

		public MenuPage ()
		{
			_sessionService = TinyIoC.TinyIoCContainer.Current.Resolve<ISessionService> ();

			foreach (var email in _sessionService.Current.Emails) {
				OptionItems.Add (new OptionItem () { Title = email.Address });
			}

			BackgroundColor = Color.FromHex("333333");

			var layout = new StackLayout { Spacing = 0, VerticalOptions = LayoutOptions.FillAndExpand };

			var label = new ContentView {
				Padding = new Thickness(10, 36, 0, 5),
				Content = new Xamarin.Forms.Label {
					TextColor = Color.FromHex("AAAAAA"),
					Text = "MENU", 
				}
			};

			Device.OnPlatform (
				iOS: () => ((Xamarin.Forms.Label)label.Content).Font = Font.SystemFontOfSize (NamedSize.Micro),
				Android: () => ((Xamarin.Forms.Label)label.Content).Font = Font.SystemFontOfSize (NamedSize.Medium)
			);

			layout.Children.Add(label);

			Menu = new ListView {
				ItemsSource = OptionItems,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Transparent,
			};

			var cell = new DataTemplate(typeof(ImageCell));
			cell.SetBinding(TextCell.TextProperty, "Title");
			cell.SetBinding(TextCell.DetailProperty, "Count");
			cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
			cell.SetValue(VisualElement.BackgroundColorProperty, Color.Transparent);

			Menu.ItemTemplate = cell;

			layout.Children.Add(Menu);

			Content = layout;
		}
	}
}

