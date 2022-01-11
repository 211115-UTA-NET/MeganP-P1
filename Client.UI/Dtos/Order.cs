using System;
using Client.UI.Logic;

namespace Client.UI.Dtos {
	public class Order {
		public int StoreId { get; set; }
		public int CustomerId { get; set; }
		public List<Item>? Items { get; set; }
		public Item Item { get; set; }
		public decimal Total { get; set; }
		public DateTime dateTime { get; set; }

		public Order() {

        }
	}
}
