using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EsempioProgrammaConForms
{
	public partial class MenuPage : ContentPage
	{
		MenuPageViewModel _viewModel = null;

		public event Action<int> EventoVoceSelezionata;

		public MenuPage ()
		{
			InitializeComponent ();

			_viewModel = new MenuPageViewModel ();
			this.BindingContext = _viewModel;


		  
			var cell = new DataTemplate (typeof(MenuPageViewCell));
			// cell.SetBinding (SwitchCell.TextProperty, "TestoVoceDiMenu");
			// cell.SetBinding (SwitchCell.OnProperty, "Attivato");
			// cell.SetBinding (SwitchCell, "Immagine");
			ListaVociMenu.ItemTemplate = cell;
			ListaVociMenu.RowHeight = 80;

			ListaVociMenu.SetBinding<MenuPageViewModel> (ListView.ItemsSourceProperty, 
				c => c.Lista, BindingMode.OneWay);

			Title = "App";
			BackgroundColor = Color.Gray;

			ListaVociMenu.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				var elementoscelto = (MenuListItem)e.Item;

				ListaVociMenu.SelectedItem = null;
				if (EventoVoceSelezionata != null)
					EventoVoceSelezionata(elementoscelto.IdVoceDiMenu);
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			List<MenuListItem> listaElementiMenu = new List<MenuListItem> () {
				new MenuListItem() { IdVoceDiMenu = 1, TestoVoceDiMenu = "Lista Articoli", Immagine = "icon.png", Attivato = true },	
				new MenuListItem() { IdVoceDiMenu = 2, TestoVoceDiMenu = "Sincronizza", Immagine = "sync.png", Attivato = false },
				// new MenuListItem() { IdVoceDiMenu = 3, TestoVoceDiMenu = "Voce 3", Immagine = "icon.png", Attivato = false },
			};

			_viewModel.Lista = listaElementiMenu;
		}
	}

	public class MenuListItem
	{
		public int IdVoceDiMenu { get; set; }
		public string TestoVoceDiMenu { get; set;}
		public string Immagine { get; set; }
		public bool Attivato { get; set; }
	}

	public class MenuPageViewModel : INotifyPropertyChanged
	{
		public MenuPageViewModel ()
		{
			_lista = new List<MenuListItem> ();
		}

		private List<MenuListItem> _lista;
		public List<MenuListItem> Lista 
		{ 
			get
			{ 
				return _lista;
			}
			set
			{ 
				_lista = value;
				OnPropertyChanged ();
			} 
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

