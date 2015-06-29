using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EsempioProgrammaConForms
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		WebApiProxyService _proxyService = new WebApiProxyService();

		public MainPageViewModel ()
		{  
			_lista = new List<Articolo> ();

		}

		public async Task LoadDataAsync()
		{
			var listaDummyNonUsata = new List<Articolo> (){ 
				new Articolo() { art_ID = "1", art_DESC = "desc" , art_IMG = "icon.png" },
				new Articolo() { art_ID = "2", art_DESC = "desc 2" , art_IMG = "icon.png" },
			};

			// var listaArt = await _proxyService.GetArticoliAsync ();

			var listaArt = await App.Database.GetItemsAsync (FiltroDescArticolo, FiltroCodiceCategoria);

			Lista = listaArt.ToList();
		}

		public string FiltroDescArticolo { get; set; }
		public string FiltroCodiceCategoria { get; set; }

		public async Task LoadCategorieAsync()
		{
			var listaCategorie = await App.Database.GetCategorieAsync (null);

			ListaCategorie = listaCategorie.ToList();
		}

		private List<Articolo> _lista;
		public List<Articolo> Lista 
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

		private List<Categoria> _listaCategorie;
		public List<Categoria> ListaCategorie
		{ 
			get
			{ 
				return _listaCategorie;
			}
			set
			{ 
				_listaCategorie = value;
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

