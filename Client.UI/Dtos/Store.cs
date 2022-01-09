using System;
using System.Data.SqlClient;
using Client.UI.Logic;

namespace Client.UI.Dtos {
	public class Store {
		public string Location { get; set; }
        public List<Product>? Inventory { get; set; }
        public int Id { get; set; }	

		public Store() {

        }
	
	}
}
