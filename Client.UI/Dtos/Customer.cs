using System.Data.SqlClient;
using Client.UI.Logic;

namespace Client.UI.Dtos {
    public class Customer : Person {
		public List<Item>? ShoppingCart { get; set; }
		public Store? Store { get; set; }

		public Customer() {

        }

	}
}
