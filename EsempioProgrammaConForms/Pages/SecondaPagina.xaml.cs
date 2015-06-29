using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using PCLStorage;
using System.IO;
using System.Threading;

namespace EsempioProgrammaConForms
{
	public partial class SecondaPagina : ContentPage
	{
		WebApiProxyService _proxy = new WebApiProxyService();

		public SecondaPagina ()
		{
			InitializeComponent ();

			Sincronizza.Clicked += async (object sender, EventArgs e) => { 
				await proceduraDiSincro(); // 
			};
		}

		private async Task proceduraDiSincro() // 
		{
			var listaArticoli = await _proxy.GetArticoliAsync ();
			  
			// App.Database.DeleteAllItems ();

			int indice = 0;
			int totale = listaArticoli.Count;
 
				foreach (var articolo in listaArticoli) {
					indice++;

					App.Database.SaveItem (articolo);

					try {
						await saveImageToDisk (articolo); // 
					} catch (Exception ex) {
						var a = ex;

					}

					this.Progress.Text = String.Format ("Art {0} di {1}",
						indice, totale);
				} 
			// var listaArt = App.Database.GetItems (null);


			// CATEGORIE
			var listaCategorie = await _proxy.GetCategorieAsync ();

			indice = 0;
			totale = listaCategorie.Count;

			App.Database.DeleteAllCategorie ();

			foreach (var categoria in listaCategorie) {
				indice++; 
				App.Database.SaveCategoria (categoria);  
				this.Progress.Text = String.Format ("Categoria {0} di {1}",
					indice, totale);
			}
		} 




		private async Task saveImageToDisk(Articolo art) // async
		{
			HttpClient c = new HttpClient();
			c.BaseAddress =  new Uri(art.UrlImg);
			c.Timeout = new TimeSpan(0,0, 3);

			Byte[] imgByteArray = await c.GetByteArrayAsync ("");

			CancellationToken a = new CancellationToken();

			IFolder rootFolder = FileSystem.Current.LocalStorage;
  
			IFolder folder = await rootFolder.CreateFolderAsync("Immagini",
				CreationCollisionOption.OpenIfExists);

			var listaFilePerVerifica = await folder.GetFilesAsync (new CancellationToken ());

			var listafile = await folder.GetFilesAsync ();
			
			IFile file = await folder.CreateFileAsync(art.ImageFileName ,
				CreationCollisionOption.ReplaceExisting);
  
			// http://www.mallibone.com/post/storing-files-from-the-portable-class-library-(pcl)

			using (var fileHandler = await file.OpenAsync (FileAccess.ReadAndWrite)) {
				fileHandler.WriteAsync(imgByteArray, 0, imgByteArray.Length); // await
			}   
		}
	}
}

