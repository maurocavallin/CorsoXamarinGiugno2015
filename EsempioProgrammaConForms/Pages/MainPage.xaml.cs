using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EsempioProgrammaConForms
{
	public partial class MainPage : ContentPage
	{
		MainPageViewModel _viewModel= new MainPageViewModel();

		public MainPage ()
		{
			InitializeComponent ();
			Title = "App";
			  
			this.BindingContext = _viewModel;
			var cell = new DataTemplate (typeof(MainPageArticoloViewCell)); 
			ListaPrincipale.ItemTemplate = cell;
			ListaPrincipale.RowHeight = 80;

			ListaPrincipale.SetBinding<MainPageViewModel> (ListView.ItemsSourceProperty, 
				c => c.Lista, BindingMode.OneWay);

			ListaPrincipale.ItemTapped += async (object sender, ItemTappedEventArgs e) => {

				var articoloSelezionato = (Articolo)e.Item;
				Navigation.PushAsync(new DetaglioPage(articoloSelezionato));
			};

			Menu1.Icon  = "user.png";

			Ricerca.TextChanged += async (object sender, TextChangedEventArgs e) => {

				System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch ();
				sw.Start ();
				await _viewModel.LoadDataAsync(e.NewTextValue);
				sw.Stop ();

				this.info.Text = "Tempo : " + sw.Elapsed.TotalMilliseconds + " nro art: " + _viewModel.Lista.Count;
			};
			 
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			// testare se utente autenticato
			if (!App.SeUtenteAutenticato) {
				// Navigation.PushModalAsync (new LoginPage());
			}

			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch ();
			sw.Start ();


			await _viewModel.LoadDataAsync ("");
			sw.Stop ();

			this.info.Text = "Tempo : " + sw.Elapsed.TotalMilliseconds + " nro art: " + _viewModel.Lista.Count;
		}
	}
}

