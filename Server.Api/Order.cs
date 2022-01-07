using System;


namespace Server.Api {
	public class Order {
		private Store store;
		private Person customer;
		private List<Item>? items;
		private Item item;
		private decimal total;

		/*<summary> property returning Store
		 * <params>
		 * Store - the store the order took place at
		 * Customer - the customer who made the purchase
		 * List<Items> - the list of items being purchased
		<return> Store
	    */
		public Order(Store store, Customer customer, List<Item> items) {
			this.store = store;
			this.customer = customer;
			this.items = items;
			this.total = this.CTotal();
		}

		/*<summary> property returning Store
		 * <params>
		 * Store - the store the order took place at
		 *Owner - the owner who made the purchase
		 * Items - the item being purchased
		<return> Store
	    */
		public Order(Store store, Owner owner, Item item) {
			this.store = store;
			this.customer = owner;
			//this.item = item;
			//this.total = this.OTotal();
        }

		/*<summary> property returning Items
		<return> List<Item>
	     */
		public List<Item> Items {
			get {return this.items;}
		}

		/*<summary> property returning StoreID
		<return> int
	    */
		public int GetStoreID {
			get { return this.store.Id; }
		} 
		
		/*<summary> property returning PersonID
		<return> int
	    */
		public int GetPersonID {
            get { return this.customer.Id; }
        }

		/*<summary> property returning Total
		<return> decimal
	    */
		public decimal Total {
			get { return this.total; }
		}

		/*<summary> method to calculate the total if the user is a customer
		<return> decimal
	     */
		public decimal CTotal() {
			decimal total = 0;
			for (int i = 0; i < items.Count; i++) {
				total += (items[i].Quantity * items[i].SalePrice);	
			}
			return total;
		}  

		/*<summary> method to calculate the total if the user is an owner
		<return> decimal
	    */ 
		public decimal OTotal() {
			decimal total = 0;
			total += (item.Quantity * item.PurchasePrice);
			return total;
		}

		/*<summary> order to string
		<return> void
	    */ 
		public void ToString() {
			Console.WriteLine("Store Location: " + this.store.Name + "\nTotal: " + total + "\nDateTime: " + DateTime.Now);
			for (int i = 0; i < this.items.Count; i++) {
				Console.WriteLine(this.items[i].Name + " x" + this.items[i].Quantity);
			}
		}
	}
}
