using System;

namespace Server.Api {
	public class Product {
		private int ID;
		private string name;
		private int quantity;
		private decimal salePrice; //Customer price
		private decimal purchasePrice; //Owner Price

		/*<summary> constructor
		<return> new Product
	    */
		public Product(int ID, string name, int quantity, decimal salePrice, decimal purchasePrice) {
			this.ID = ID;
			this.name = name;
			this.quantity = quantity;
			this.salePrice = salePrice;
			this.purchasePrice = purchasePrice;
		}

		/*<summary> property returning ID
		<return> int
	    */
		public int Id {
			get { return ID; }
		}

		/*<summary> property returning Name
		<return> string
	    */
		public string Name {
			get {return this.name;}
		}

		/*<summary> property returning Quanity
		<return> Qunatity
	    */
		public int Quantity {
			get {return this.quantity;}
			set {this.quantity = value;}
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
			get {return this.purchasePrice;}
		}

		/*<summary> adds product quantity
		<return> void
	    */
		public void RefillProduct (int numUnits) {
			this.quantity += numUnits;
		}

		/*<summary> reduces product quantity
		<return> void
	    */
		public void BuyProduct(int numUnits) {
			this.quantity -= numUnits;
		}
	}
}
