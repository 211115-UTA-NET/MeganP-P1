using System;
using Client.UI.Dtos;


namespace Client.UI.Logic {
	public class OrderLogic {
		public Store Store { get; set; }
		public Customer Customer { get; set; }
		public List<Item>? Items { get; set; }
		public Item Item { get; set; }
		public decimal Total { get; set; }
		/*<summary> property returning Store
		 * <params>
		 * Store - the store the order took place at
		 * Customer - the customer who made the purchase
		 * List<Items> - the list of items being purchased
		<return> Store
	    */
		public OrderLogic(Order order) {
			this.Store = order.Store;
			this.Customer = order.Customer;
			this.Items = order.Items;
			this.Total = this.TotalItUp();
		}

		
		/*<summary> method to calculate the total if the user is a customer
		<return> decimal
	    */
		public decimal TotalItUp() {
			decimal total = 0;
			for (int i = 0; i < Items.Count; i++) {
				total += (Items[i].Quantity * Items[i].SalePrice);	
			}
			return total;
		}
	}
}
