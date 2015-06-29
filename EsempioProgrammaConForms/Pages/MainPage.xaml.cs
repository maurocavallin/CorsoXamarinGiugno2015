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

			// this.BackgroundColor = Color.Yellow;
			  
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

				_viewModel.FiltroDescArticolo = e.NewTextValue;
				await _viewModel.LoadDataAsync();
				sw.Stop ();

				this.info.Text = "Tempo : " + sw.Elapsed.TotalMilliseconds + " nro art: " + _viewModel.Lista.Count;
			};

			PickerCategoria.SelectedIndexChanged += async (object sender, EventArgs e) => {

				string codiceCategoriaSelezionata = null;
				if (PickerCategoria.SelectedIndex >= 0)
				{
					Categoria catSelezionata = _viewModel.ListaCategorie[PickerCategoria.SelectedIndex]; 
					codiceCategoriaSelezionata = catSelezionata.cat_CODICE;
				}

				_viewModel.FiltroCodiceCategoria = codiceCategoriaSelezionata;
				await _viewModel.LoadDataAsync ();
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


			await _viewModel.LoadCategorieAsync ();
		
			foreach (var categoria in _viewModel.ListaCategorie)
			{ 
				PickerCategoria.Items.Add(categoria.cat_DESC);
			}

			this.PickerCategoria.SelectedIndex = -1;
 
			_viewModel.FiltroDescArticolo = "";
			_viewModel.FiltroCodiceCategoria = "";
			await _viewModel.LoadDataAsync ();

			sw.Stop ();

			this.info.Text = "Tempo : " + sw.Elapsed.TotalMilliseconds + " nro art: " + _viewModel.Lista.Count;
		}
	}
}

