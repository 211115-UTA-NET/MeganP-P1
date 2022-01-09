using System;
using System.Data.SqlClient;

namespace Server.Api.Dtos {
	public class Owner : Person {
		public List<Store>? Stores { get; set; } = new List<Store>();

		public Owner() {

        }

	} 
}

