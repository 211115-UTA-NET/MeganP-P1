using System;
using Client.UI.Logic;

namespace Client.UI.Dtos {
	public class Product {
		public int Id { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public decimal SalePrice { get; set; } //Customer price
		public decimal PurchasePrice { get; set; } //Owner Price

		public Product() {

        }
	}
}
