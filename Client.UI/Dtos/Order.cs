using System;
using Client.UI.Logic;

namespace Client.UI.Dtos {
	public class Order {
		public Store Store { get; set; }
		public Customer Customer { get; set; }
		public List<Item>? Items { get; set; }
		public Item Item { get; set; }
		public decimal Total { get; set; }

		public Order() {

        }
	}
}
