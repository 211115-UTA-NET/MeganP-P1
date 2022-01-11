using System;
using System.ComponentModel.DataAnnotations;


namespace Server.Api.Dtos {
	public class Order {
		[Required]
		public int StoreId { get; set; }
		[Required]
		public int CustomerId { get; set; }
		[Required]
		public List<Item>? Items { get; set; }
		public Item Item { get; set; }
		[Required]
		public decimal Total { get; set; }
		[Required]
		public DateTime dateTime { get; set; }

		public Order() {

        }
	}
}
