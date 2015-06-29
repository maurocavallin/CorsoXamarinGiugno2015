using System;
using Xamarin.Forms;

namespace EsempioProgrammaConForms
{
	public class MenuPageViewCell : ViewCell
	{
		public MenuPageViewCell ()
		{
			 
			var image = new Image {
				HorizontalOptions = LayoutOptions.Start
			};
			image.SetBinding (Image.SourceProperty, new Binding ("Immagine"));
			image.WidthRequest = image.HeightRequest = 40;
			image.HorizontalOptions = LayoutOptions.End;

			Label descrizioneLabel = new Label ();
			descrizioneLabel.VerticalOptions = LayoutOptions.Center;
			descrizioneLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
			descrizioneLabel.SetBinding (Label.TextProperty, new Binding ("TestoVoceDiMenu"));

			var viewLayout = new StackLayout () {
				Padding = new Thickness(10),
				// BackgroundColor = Color.Aqua,
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Fill,
				Children = { descrizioneLabel, image }
			};

			View = viewLayout; 
		}
	}
}

