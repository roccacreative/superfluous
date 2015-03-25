using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Superfluous.Services;
using Superfluous.ViewModels;

namespace Superfluous.Pages
{
	public class MenuPage : ContentPage
	{
		public ListView Menu { get; set; }

		public MenuPage (MenuViewModel viewModel)
		{
			viewModel.Navigation = Navigation;
			BindingContext = viewModel;

			Icon = "menu.png";
			Title = "menu";
				
			BackgroundColor = Color.FromHex("333333");

			var layout = new StackLayout { Spacing = 0, VerticalOptions = LayoutOptions.FillAndExpand };

			var label = new ContentView {
				Padding = new Thickness(10, 36, 0, 5),
				Content = new Xamarin.Forms.Label {
					TextColor = Color.FromHex("AAAAAA"),
					Text = "FAVOURITES", 
				}
			};

			Device.OnPlatform (
				iOS: () => ((Xamarin.Forms.Label)label.Content).Font = Font.SystemFontOfSize (NamedSize.Micro),
				Android: () => ((Xamarin.Forms.Label)label.Content).Font = Font.SystemFontOfSize (NamedSize.Medium)
			);

			layout.Children.Add(label);

			Menu = new ListView {
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Transparent,
			};

			Menu.SetBinding<MenuViewModel> (ListView.ItemsSourceProperty, m => m.SavedEmails);
			Menu.SetBinding<MenuViewModel> (ListView.SelectedItemProperty, m => m.SelectedItem);
			Menu.SetBinding<MenuViewModel> (ListView.ItemTemplateProperty, m => m.SavedEmailsTemplate);

			layout.Children.Add(Menu);

			Content = layout;
		}
	}
}

