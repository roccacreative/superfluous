using System;
using Xamarin.Forms;
using System.IO;
using Superfluous.Data;
using SQLite.Net.Async;
using SQLite.Net;

namespace Superfluous.iOS.Data
{	
	public class SQLite_iOS : ISQLite {
		public SQLite_iOS () {}
		public SQLite.Net.Async.SQLiteAsyncConnection GetConnection ()
		{
			var sqliteFilename = "TodoSQLite.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			// Create the connection
			var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();

			var connectionWithLock = new SQLiteConnectionWithLock (
				platform,
				new SQLiteConnectionString (path, true));

			var connection = new SQLiteAsyncConnection (() => connectionWithLock);
		
			// Return the database connection
			return connection;
		}
	}
}