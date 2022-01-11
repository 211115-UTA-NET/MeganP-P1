using System;

namespace Client.UI.Dtos {
	public class Item {
		public int ProductId { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public decimal SalePrice { get; set; }  //Customer Price
		public decimal PurchasePrice { get; set; } //Owner Price

	}
}
