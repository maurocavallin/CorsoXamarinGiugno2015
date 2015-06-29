using System;
using SQLite;

namespace EsempioProgrammaConForms
{
	public interface IDatabase {

		SQLiteConnection GetConnection();

	}
}

