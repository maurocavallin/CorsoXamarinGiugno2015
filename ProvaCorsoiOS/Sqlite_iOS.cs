using System; 
using ProvaCorsoiOS;
using EsempioProgrammaConForms;
using System.IO;
using Xamarin.Forms;


	[assembly: Dependency (typeof (SQLite_iOS))]
	

namespace ProvaCorsoiOS
{
	public class SQLite_iOS : IDatabase {
		public SQLite_iOS () {}
		public SQLite.SQLiteConnection GetConnection ()
		{
			var sqliteFilename = "TodoSQLite.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			// Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
	
	}
}

