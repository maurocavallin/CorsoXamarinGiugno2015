using System;
using SQLite;

namespace EsempioProgrammaConForms
{
	public class Categoria
	{
		public Categoria ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string cat_ID { get; set; }
		public string cat_CODICE { get; set; }
		public string cat_DESC { get; set; }

	}
}

