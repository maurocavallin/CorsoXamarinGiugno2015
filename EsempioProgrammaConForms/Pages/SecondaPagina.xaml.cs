using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using PCLStorage;
using System.IO;

namespace EsempioProgrammaConForms
{
	public partial class SecondaPagina : ContentPage
	{
		WebApiProxyService _proxy = new WebApiProxyService();

		public SecondaPagina ()
		{
			InitializeComponent ();

			Sincronizza.Clicked += async (object sender, EventArgs e) => {
				await proceduraDiSincro();
			};
		}

		private async Task proceduraDiSincro()
		{
			var listaArticoli = await _proxy.GetArticoliAsync ();

			// App.Database.DeleteAllItems ();

			int indice = 0;
			int totale = listaArticoli.Count;

			foreach (var articolo in listaArticoli) {
				indice++;

				App.Database.SaveItem (articolo);

				await saveImageToDisk (articolo);

				this.Progress.Text = String.Format ("Art {0} di {1}",
					indice, totale);
			}

			// var listaArt = App.Database.GetItems (null);
		}

		private async Task saveImageToDisk(Articolo art)
		{
			HttpClient c = new HttpClient();
			c.BaseAddress =  new Uri(art.UrlImg);

			Byte[] imgByteArray = await c.GetByteArrayAsync ("");

			IFolder rootFolder = FileSystem.Current.LocalStorage;

			IFolder folder = await rootFolder.CreateFolderAsync("Immagini",
				CreationCollisionOption.OpenIfExists);

			var listafile = await folder.GetFilesAsync ();
			
			IFile file = await folder.CreateFileAsync(art.ImageFileName ,
				CreationCollisionOption.ReplaceExisting);
  
			// http://www.mallibone.com/post/storing-files-from-the-portable-class-library-(pcl)

			using (var fileHandler = await file.OpenAsync (FileAccess.ReadAndWrite)) {
				await fileHandler.WriteAsync(imgByteArray, 0, imgByteArray.Length);
			}   
		}
	}
}

