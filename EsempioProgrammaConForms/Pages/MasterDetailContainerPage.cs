﻿using System;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace EsempioProgrammaConForms
{
	public class MasterDetailContainerPage : MasterDetailPage
	{
		private NavigationPage _paginaPrincipale;
		private SecondaPagina _secondaPagina;

		public MasterDetailContainerPage ()
		{
			var paginaDiMenu =  new MenuPage ();

			Title = "App";
			Master = paginaDiMenu;

			_paginaPrincipale = new NavigationPage(new MainPage () ) { BarBackgroundColor = Color.Green };
			Detail = _paginaPrincipale;



			paginaDiMenu.EventoVoceSelezionata+= (int idVoceSelezionata) => {

				switch(idVoceSelezionata)
				{
				case 1:
					Detail = _paginaPrincipale;
					IsPresented = false;	
					break;
				case 2:
  
					if (_secondaPagina == null)
						_secondaPagina = new SecondaPagina();
					Detail = _secondaPagina;

					IsPresented = false;	
					break;
				}
			};
		}



		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			await  Task.Delay(1000);
 
			IsPresented = true;	 
		}
	}
}


