using System;

namespace Server.Api {
	public class Item {
		private int productID;
		private string name;
		private int quantity;
		private decimal salePrice;  //Customer Price
		private decimal purchasePrice; //Owner Price

		public Item(Product product, int quantity) {
			this.name = product.Name;
			this.quantity = quantity;
			this.salePrice = product.SalePrice;
			this.purchasePrice = product.PurchasePrice;
			this.productID = product.Id;
		}

		/*<summary> property returning ProductID
		<return> int
	    */
		public int ProductID {
			get { return productID; }
		}

		/*<summary> property returning Name
		<return> string
	    */
		public string Name {
			get {return this.name;}
		}

		/*<summary> property returning quantity
		<return> int
	    */
		public int Quantity {
			get {return this.quantity;}
		}

		/*<summary> property returning SalePrice
		<return> decimal
	    */
		public decimal SalePrice {
			get {return this.salePrice;}
		}

		/*<summary> property returning PurchasePrice
		<return> decimal
	    */
		public decimal PurchasePrice {
			get { return this.purchasePrice; }
		}

	}
}
