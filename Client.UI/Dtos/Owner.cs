using System;
using System.Data.SqlClient;
using Client.UI.Logic;

namespace Client.UI.Dtos {
	public class Owner : Person {
		public List<Store>? Stores { get; set; } = new List<Store>();

		public Owner() {

        }
		
	}
}
