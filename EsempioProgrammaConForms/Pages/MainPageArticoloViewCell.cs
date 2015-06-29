using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace EsempioProgrammaConForms
{
	public class MainPageArticoloViewCell : ViewCell
	{

		Image immagineArticolo = null;

		public MainPageArticoloViewCell ()
		{
			var label = new Label ();
			label.FontSize = 22;
			label.VerticalOptions = LayoutOptions.Center;
			label.HorizontalOptions = LayoutOptions.FillAndExpand;
			label.SetBinding (Label.TextProperty, Articolo.FIELD_NAME_art_DESC);

			immagineArticolo = new Image ();
			immagineArticolo.HorizontalOptions = LayoutOptions.End;
			// immagineArticolo.SetBinding (Image.SourceProperty, Articolo.FIELD_NAME_ImageFullFileName);
			immagineArticolo.SetBinding (Image.SourceProperty, Articolo.FIELD_NAME_UrlImg);
			immagineArticolo.HeightRequest = immagineArticolo.WidthRequest = 40;

			StackLayout contenitore = new StackLayout (){ 
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(10),
				Children = {label, immagineArticolo}
			};

		
			this.View = contenitore;

			var moreAction = new MenuItem { Text = "Sposta Img", Icon = "arrow.png" };
			moreAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			moreAction.Clicked += async (sender, e) => {
				var mi = ((MenuItem)sender);
				Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);

				await immagineArticolo.TranslateTo (-100, 0, 2000, Easing.CubicIn);
				// immagineArticolo.TranslationX = -100;
			};
			var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
			deleteAction.IsDestructive = true;
			deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			deleteAction.Clicked += async (sender, e) => {
				var mi = ((MenuItem)sender);
				Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
			};
			// add to the ViewCell's ContextActions property
			ContextActions.Add (moreAction);
			ContextActions.Add (deleteAction);

		}
	}
}



