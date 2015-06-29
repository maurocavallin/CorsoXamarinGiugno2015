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
		}

		public async Task<IEnumerable<Articolo>> GetItemsAsync (string filtro)
		{
			var result = await Task.Run (() => {
				return GetItems (filtro);
			});

			return result;
		}

		public IEnumerable<Articolo> GetItems (string filtro)
		{
			lock (locker) {
				if (String.IsNullOrEmpty (filtro)) {
					return (from i in database.Table<Articolo> ()
					        select i).ToList ();
				} else {
					return (from i in database.Table<Articolo> ()
					       select i).Where (x => x.art_DESC.Contains (filtro)).ToList ();
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
	}
}

