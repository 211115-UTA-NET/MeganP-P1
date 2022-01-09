using System.Data.SqlClient;

namespace Server.Api.Dtos {
    public class Customer : Person {
		public List<Item>? ShoppingCart { get; set; }
		public Store? Store { get; set; }

		public Customer() {
			
        }
	}
}
