using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Dtos {
    public class OrderHistory {
		[Required]
		public int OrderId { get; set; }
		[Required]
		public int StoreId { get; set; }
		[Required]
		public int CustomerId { get; set; }
		[Required]
		public decimal Total { get; set; }
		[Required]
		public DateTime dateTime { get; set; }
		[Required]
		public int PurchaseId { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		public string Name { get; set; }

		public OrderHistory() {

        }


	}
}
