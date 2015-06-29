using System;
using SQLite;
using PCLStorage;

namespace EsempioProgrammaConForms
{
	public class Articolo
	{
		public static string FIELD_NAME_art_DESC = "art_DESC";
		public static string FIELD_NAME_art_IMG = "art_IMG";
		public static string FIELD_NAME_UrlImg = "UrlImg";
		public static string FIELD_NAME_ImageFullFileName = "ImageFullFileName";

		public Articolo ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }


		public String art_ID { get; set; }
		public String art_DESC { get; set; }
		public String art_IMG { get; set; }

		public String UrlImg 
		{
			get
			{
				return "http://10.123.26.1" + art_IMG;
			}
		}

		public String ImageFileName
		{
			get
			{
				return art_ID.Replace (".", "").Replace("/","") + ".png";
			}
		}

		public String ImageFullFileName
		{
			get
			{ 
				// "file://" +
				var filename =  FileSystem.Current.LocalStorage.Path + "/" + "Immagini" + "/" + ImageFileName;

				return filename;
			}
		}
	}
}

