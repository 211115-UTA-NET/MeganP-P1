using System;
using System.Data.SqlClient;
using Client.UI.Dtos;

namespace Client.UI.Logic {
	public class StoreLogic : ILoadOrderHistory {
		public string Location { get; set; }
		public List<Product>? Inventory { get; set; }
		public int Id { get; set; }

		/*<summary> constructor
		<return> new Store
	    */
		public StoreLogic(Store store) {
			this.Location = store.Location;
			this.Inventory = store.Inventory;
			this.Id = store.Id;
		}


		/*<summary> prints inventory options
		<return> void
	    */ 
		public void PrintInventory() {
			for (int i = 0; i < Inventory.Count; i++) {
				Console.WriteLine(i + ". " + Inventory[i].Quantity + "x " + Inventory[i].Name);
			}
		}

		/*<summary> Adds a product to the inventory
		<return> void
	    */
		public void AddProductToInventory(Product product) {
			this.Inventory.Add(product);
		}

		public void LoadOrderHistory() {

        }



	}
}
