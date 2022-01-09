using System;


namespace Server.Api.Dtos {
	public class Order {
		public Store Store { get; set; }
		public Person Customer { get; set; }
		public List<Item>? Items { get; set; }
		public Item Item { get; set; }
		public decimal Total { get; set; }

		public Order() {

        }
	}
}
