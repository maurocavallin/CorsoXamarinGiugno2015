using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EsempioProgrammaConForms
{
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();

			ButtonSalvaImpostazioni.Clicked += (object sender, EventArgs e) => {

				try
				{
				CustomSettings.ServerAddress = this.EntryIndirizzoServer.Text; 

					DisplayAlert("", "Impostazioni salvate" ,"OK");
				}
				catch(Exception ex)
				{
					DisplayAlert("", "Errore salvataggio impostazioni " + ex.Message ,"OK");
				}

			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			this.EntryIndirizzoServer.Text = CustomSettings.ServerAddress;
		}
	}
}

