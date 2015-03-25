using System;
using Superfluous.Data;
using System.IO;
using SQLite.Net;
using SQLite.Net.Async;

namespace Superfluous.Droid.Data
{
	public class SQLite_Android : ISQLite {
		public SQLite_Android () {}
		public SQLite.Net.Async.SQLiteAsyncConnection GetConnection ()
		{
			var sqliteFilename = "TodoSQLite.db3";
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);

			// Create the connection
			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();

			var connectionWithLock = new SQLiteConnectionWithLock (
				platform,
				new SQLiteConnectionString (path, true));

			var connection = new SQLiteAsyncConnection (() => connectionWithLock);

			// Return the database connection
			return connection;
		}
	}
}

