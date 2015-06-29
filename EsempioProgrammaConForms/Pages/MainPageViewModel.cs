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

		public async Task LoadDataAsync(string filtro)
		{
			var listaDummyNonUsata = new List<Articolo> (){ 
				new Articolo() { art_ID = "1", art_DESC = "desc" , art_IMG = "icon.png" },
				new Articolo() { art_ID = "2", art_DESC = "desc 2" , art_IMG = "icon.png" },
			};

			// var listaArt = await _proxyService.GetArticoliAsync ();

			var listaArt = await App.Database.GetItemsAsync (filtro);

			Lista = listaArt.ToList();
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

