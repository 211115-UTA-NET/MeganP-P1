using System;
using System.Data.SqlClient;

namespace Client.UI {
	public class Store : ILoadOrderHistory {
		private string location;
		private List<Product>? inventory;
		private int ID;

		/*<summary> constructor
		<return> new Store
	    */
		public Store (int ID, string location, List<Product> inventory) {
			this.location = location;
			this.inventory = inventory;
			this.ID = ID;
		}


		/*<summary> property returning ID
		<return> int
	    */
		public int Id {
			get { return this.ID; }
		}

		/*<summary> property returning Name
		<return> string
	    */
		public string Name {
			get { return this.location; }
		}

		/*<summary> property returning Inventory
		<return> List<Product>
	    */
		public List<Product> Inventory {
			get { return this.inventory; }
		}


		/*<summary> prints inventory options
		<return> void
	    */
		public void PrintInventory() {
			for (int i = 0; i < inventory.Count; i++) {
				Console.WriteLine(i + ". " + inventory[i].Quantity + "x " + inventory[i].Name);
			}
		}

		/*<summary> resupplies all products in inventory
		<return> void
	    */
		public void ResupplyStore() {
			//implement
		}

		/*<summary> Adds a product to the inventory
		<return> void
	    */
		public void AddProductToInventory(Product product) {
			this.inventory.Add(product);
		}

		public void LoadOrderHistory() {

        }



	}
}
