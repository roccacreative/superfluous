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
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
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

