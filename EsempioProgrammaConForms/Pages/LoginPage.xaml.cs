using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EsempioProgrammaConForms
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();

			// Invia.Image = "icon.png";

			var buttonStyle = new Style (typeof(Button)) {
				Setters = {
					new Setter { Property = Button.BackgroundColorProperty, Value = Color.Green },
					new Setter { Property = Button.BorderColorProperty, Value = Color.Blue },
					new Setter { Property = Button.BorderRadiusProperty, Value = 10 },
					new Setter { Property = Button.HeightRequestProperty, Value = 42 }
				}
			};

			this.Invia.Style = buttonStyle;


			// if (App.Current.Resources == null) {
			// 	App.Current.Resources = new ResourceDictionary ();
			//}
			// App.Current.Resources.Add (buttonStyle);

			Invia.Clicked += Invia_Clicked;;
		}

		void Invia_Clicked (object sender, EventArgs e)
		{
			if (EntryPassword.Text == "1") {
				App.SeUtenteAutenticato = true;
				Navigation.PopModalAsync ();
			} else {
				DisplayAlert ("", "Password non valida", "OK");
			}
		}


	}
}

