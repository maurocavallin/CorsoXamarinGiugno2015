﻿using System;
using System.IO;
using Xamarin.Forms; 
using EsempioProgrammaConForms;
using EsempioProgrammaConForms.Droid;
using PCLStorage;



[assembly: Dependency (typeof (SQLite_Android))]

/// <summary>
/// https://github.com/xamarin/xamarin-forms-samples/blob/master/Todo/PCL/Todo.Android/SQLite_Android.cs
/// </summary>
namespace EsempioProgrammaConForms.Droid
{
	public class SQLite_Android : IDatabase
	{
		public SQLite_Android ()
		{
		}

		#region ISQLite implementation
		public SQLite.SQLiteConnection GetConnection ()
		{
			var sqliteFilename = "TodoSQLite.db3";
			var  documentsPath =  System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder

			// string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);

			// This is where we copy in the prepopulated database
			Console.WriteLine (path);

			/*
			 * nel caso dovessi usare un database pre-popolato
			if (!File.Exists(path))
			{
				var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.TodoSQLite);  // RESOURCE NAME ###
				// create a write stream
				FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
				// write to the stream
				ReadWriteStream(s, writeStream);
			}
			*/

			var conn = new SQLite.SQLiteConnection(path);

			// Return the database connection 
			return conn;
		}
		#endregion

		/// <summary>
		/// helper method to get the database out of /raw/ and into the user filesystem
		/// </summary>
		void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}
	}
}
