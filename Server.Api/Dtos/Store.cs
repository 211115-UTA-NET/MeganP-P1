using System;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Dtos {
	public class Store {
		[Required]
		public string Location { get; set; }
		[Required]
		public List<Product>? Inventory { get; set; }
		[Required]
		public int Id { get; set; }

		public Store() {

        }
	}
}
