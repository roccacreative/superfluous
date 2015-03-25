using System;
using System.Linq;
using Superfluous.Models;
using System.Threading.Tasks;
using Superfluous.Data;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;
using Xamarin.Forms;

namespace Superfluous.Services
{
	public interface ISessionService
	{
		Session Current {get;set;}

		Task<Session> GetDefaultAsync();
		Task<int> SaveAsync(Session session);
		Task<Session> Init();
	}

	public class SessionService : ISessionService
	{
		public Session Current { get; set; }

		public Task<Session> Init()
		{
			return Task.Run (async () => {
				var session = await GetDefaultAsync ();
				if (session == null) {
					session = new Session ();
					await SaveAsync (session);
				}
				Current = session;
				return session;
			});
		}

		public async Task<Session> GetDefaultAsync()
		{
			try {
				var sessions = await new SuperfluousDatabase()
					.GetConnection ()
					.GetAllWithChildrenAsync<Session> ();

				return sessions.FirstOrDefault();	
			} catch (Exception ex) {
				throw;
			}
		}

		public Task<int> SaveAsync (Session item) 
		{
			return Task.Run (async () => {
				if (item.Id != 0) {
					await new SuperfluousDatabase().GetConnection ()
						.UpdateAsync (item);
					return item.Id;
				} else {
					return await new SuperfluousDatabase().GetConnection ()
						.InsertAsync (item);
				}
			});
		}

		public async Task<int> ResetAsync()
		{
			var item = await GetDefaultAsync ();
			return await new SuperfluousDatabase().GetConnection ()
					.DeleteAsync (item);
		}
	}
}

