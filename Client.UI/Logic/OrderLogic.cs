using System;
using Client.UI.Dtos;


namespace Client.UI.Logic {
	public static class OrderLogic {
		
		/*<summary> method to calculate the total if the user is a customer
		<return> decimal
	    */
		public static decimal TotalItUp(Order order) {
			decimal total = 0;
			for (int i = 0; i < order.Items.Count; i++) {
				total += (order.Items[i].Quantity * order.Items[i].SalePrice);	
			}
			return total;
		}

		public static void ToString(Order order, Customer customer) {
			Console.WriteLine("Store Location: " + customer.Store.Location + "\nTotal: " + order.Total + "\nDateTime: " + DateTime.Now);
			for (int i = 0; i < order.Items.Count; i++) {
				Console.WriteLine(order.Items[i].Name + " x" + order.Items[i].Quantity);
			}
		}
	}
}
