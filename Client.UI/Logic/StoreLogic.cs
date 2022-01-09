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

		public bool MakePurchase(Order order) {

			string connectionString = File.ReadAllText("StringConnection.txt");
			using SqlConnection connection = new SqlConnection(connectionString);

			for (int i = 0; i < order.Items.Count; i++) {
				for (int j = 0; j < Inventory.Count; j++) {
					if (order.Items[i].Name == Inventory[j].Name) {
						int remainingInventory = Inventory[j].Quantity - order.Items[i].Quantity;
						Inventory[i].Quantity -= order.Items[i].Quantity;

						connection.Open();
						string insertOrder = $"UPDATE Inventories SET Quantity = {remainingInventory} WHERE	ProductID = {Inventory[j].Id} AND StoreID = {this.Id};";
						using SqlCommand command = new(insertOrder, connection);
						using SqlDataReader reader = command.ExecuteReader();
						connection.Close();

						break;
					}
				}
			}
			Console.WriteLine("Purchase Successful with the Store");
			return true;
		}



		/*<summary> prints inventory options
		<return> void
	    */ /*
		public void PrintInventory() {
			for (int i = 0; i < inventory.Count; i++) {
				Console.WriteLine(i + ". " + inventory[i].Quantity + "x " + inventory[i].Name);
			}
		} */

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
			this.Inventory.Add(product);
		}

		public void LoadOrderHistory() {

        }



	}
}
