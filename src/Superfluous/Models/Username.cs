using System;
using SQLite;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Superfluous.Models
{
	public class Username
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[ForeignKey(typeof(Session))] 
		public int SessionId {get;set;}

		public string Address {get;set;}

		public DateTime Created {get;set;}

		public Username()
		{
			Created = DateTime.Now;
		}
	}
}

