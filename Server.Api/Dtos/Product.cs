using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Dtos {
	public class Product {
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public decimal SalePrice { get; set; } //Customer price
		[Required]
		public decimal PurchasePrice { get; set; } //Owner Price

		public Product() {

        }
	}
}
