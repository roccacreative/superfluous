using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Superfluous.Models
{
	public class Session
	{
		[PrimaryKey, AutoIncrement]
		public int Id {get;set;}

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<Username> Emails {get;set;}

		public Session ()
		{
			Emails = new List<Username> ();
		}
	}
}