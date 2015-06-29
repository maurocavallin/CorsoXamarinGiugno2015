using System;

using Xamarin.Forms;

namespace EsempioProgrammaConForms
{
	public class App : Application
	{

		static ArticoliDatabase database;

		public static ArticoliDatabase Database {
			get { 
				if (database == null) {
					database = new ArticoliDatabase ();
				}
				return database; 
			}
		}

		public static bool SeUtenteAutenticato = false;

		Page _mainPage = null;

		public App ()
		{
			_mainPage = new MasterDetailContainerPage();

			// The root page of your application
			MainPage = _mainPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

