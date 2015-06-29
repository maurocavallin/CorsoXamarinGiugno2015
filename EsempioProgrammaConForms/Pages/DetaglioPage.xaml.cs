using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EsempioProgrammaConForms
{
	public partial class DetaglioPage : ContentPage
	{
		Articolo _articolo = null;

		public DetaglioPage (Articolo articolo)
		{
			_articolo = articolo;
			InitializeComponent ();
			Title = articolo.art_DESC;
		}
	}
}

