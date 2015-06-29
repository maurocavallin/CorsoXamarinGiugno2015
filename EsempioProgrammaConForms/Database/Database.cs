using System;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsempioProgrammaConForms
{
	public class ArticoliDatabase
	{
		static object locker = new object ();

		SQLiteConnection database;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public ArticoliDatabase()
		{
			var db = DependencyService.Get<IDatabase> ();

			database = db.GetConnection ();
			// create the tables
			database.CreateTable<Articolo>();
			database.CreateTable<Categoria> ();
		}

		public async Task<IEnumerable<Articolo>> GetItemsAsync (string filtro, string filtroCategoria)
		{
			var result = await Task.Run (() => {
				return GetItems (filtro, filtroCategoria);
			});

			return result;
		}

		public IEnumerable<Articolo> GetItems (string filtro, string filtroCategoriaCodice)
		{
			lock (locker) {

				var query = from i in database.Table<Articolo> () select i;

				if (!String.IsNullOrEmpty (filtro))
				{
					query = query.Where (x => x.art_DESC.Contains (filtro));
				}

				if (!String.IsNullOrEmpty (filtroCategoriaCodice))
				{
					query = query.Where (x => x.art_CATEGORIA_CODICE.Contains (filtroCategoriaCodice));
				}

				return query.ToList ();
			}
		}

		public int SaveItem (Articolo item) 
		{
			lock (locker) {
				if (item.Id != 0) {
					database.Update(item);
					return item.Id;
				} else {
					return database.Insert(item);
				}
			}
		}

		public IEnumerable<Articolo> GetItemsNotDone ()
		{
			lock (locker) {
				return database.Query<Articolo>("SELECT * FROM [Articolo]");
			}
		}

		public Articolo GetItem (int id) 
		{
			lock (locker) {
				return database.Table<Articolo>().FirstOrDefault(x => x.Id == id);
			}
		}



		public int DeleteItem(int id)
		{
			lock (locker) {
				return database.Delete<Articolo>(id);
			}
		}

		public void DeleteAllItems()
		{
			lock (locker) {
				database.Execute ("DELETE FROM Articolo");
			}
		}



		public async Task<IEnumerable<Categoria>> GetCategorieAsync (string filtro)
		{
			var result = await Task.Run (() => {
				return GetCategorie (filtro);
			});

			return result;
		}

		public IEnumerable<Categoria> GetCategorie (string filtro)
		{
			lock (locker) {
				if (String.IsNullOrEmpty (filtro)) {
					return (from i in database.Table<Categoria> ()
						select i).ToList ();
				} else {
					return (from i in database.Table<Categoria> ()
						select i).Where (x => x.cat_ID.Contains (filtro)).ToList ();
				}
			}
		}

		public int SaveCategoria (Categoria item) 
		{
			lock (locker) {
				if (item.Id != 0) {
					database.Update(item);
					return item.Id;
				} else {
					return database.Insert(item);
				}
			}
		}

		public void DeleteAllCategorie()
		{
			lock (locker) {
				database.Execute ("DELETE FROM Categoria");
			}
		}

	}
}

