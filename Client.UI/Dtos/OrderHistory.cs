using System;
using System.ComponentModel.DataAnnotations;

namespace Client.UI.Dtos {
	public class OrderHistory {
		
		public int OrderId { get; set; }
		
		public int StoreId { get; set; }
		
		public int CustomerId { get; set; }
		
		public decimal Total { get; set; }
		
		public DateTime dateTime { get; set; }
		
		public int PurchaseId { get; set; }
		
		public int Quantity { get; set; }
		
		public decimal Price { get; set; }
		
		public int ProductId { get; set; }
		
		public string Name { get; set; }

		public OrderHistory() {

		}


	}
}