using System;
using System.Linq;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System.Collections.Generic;
using System.IO;
using Superfluous.Models;
using System.Threading.Tasks;
using DisposableMail;
using SQLite.Net.Async;
using SQLite.Net;
using Xamarin.Forms;

namespace Superfluous.Data
{
	public interface ISQLite {
		SQLiteAsyncConnection GetConnection();
	}

	public class SuperfluousDatabase
	{
		#if NETFX_CORE
		private static readonly string Path = "Database.db"; //TODO: change this later
		#elif NCRUNCH
		private static readonly string Path = System.IO.Path.GetTempFileName();
		#else
		private static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database.db");
		#endif
		private bool initialized = false;
		private SQLiteAsyncConnection connection;

		private readonly Type [] tableTypes = new Type []
		{
			typeof(Session),
			typeof(Username)
		};

		public SuperfluousDatabase()
		{
			connection = GetConnection ();
		}

		/// <summary>
		/// Global way to grab a connection to the database, make sure to wrap in a using
		/// </summary>
		public SQLiteAsyncConnection GetConnection ()
		{	
			var test = TinyIoC.TinyIoCContainer.Current.Resolve<ISQLite> ();
			var connection = TinyIoC.TinyIoCContainer.Current.Resolve<ISQLite> ().GetConnection ();
			if (!initialized)
			{
				CreateDatabase(connection).Wait();
			}
			return connection;
		}

		private Task CreateDatabase (SQLiteAsyncConnection connection)
		{
			return Task.Run (async () => {
				try {
					//Create the tables
					var createTask = await connection.CreateTablesAsync (tableTypes);

					//Count number of assignments
					var countTask = connection.Table<Session> ().CountAsync ();
					countTask.Wait ();

					//Mark database created
					initialized = true;
				} catch (Exception ex) {

				}
			});
		}
	}
}

