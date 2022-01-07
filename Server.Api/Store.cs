using System;
using System.Data.SqlClient;

namespace Server.Api {
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
			get {return this.location;}
		}

		/*<summary> property returning Inventory
		<return> List<Product>
	    */
		public List<Product> Inventory {
			get { return this.inventory; }
		}

		/*<summary> method makes a purchase and removes purchased items from inventory
		<return> bool
	    */
		public bool MakePurchase(Order order) {

			string connectionString = File.ReadAllText("StringConnection.txt");
			using SqlConnection connection = new SqlConnection(connectionString);

			for (int i = 0; i < order.Items.Count; i++) {
				for (int j = 0; j < inventory.Count; j++) {
					if (order.Items[i].Name == inventory[j].Name) {
						int remainingInventory = inventory[j].Quantity - order.Items[i].Quantity;
						inventory[i].Quantity-= order.Items[i].Quantity;

						connection.Open();
						string insertOrder = $"UPDATE Inventories SET Quantity = {remainingInventory} WHERE	ProductID = {inventory[j].Id} AND StoreID = {this.ID};";
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

		/*<summary> resupplies a product and returns the cost of the restock
		<return> decimal
	    */
		public decimal ResupplyProduct(string product, int quantity) {
			decimal total = 0;
			for (int i = 0; i < inventory.Count; i++) {
				if (inventory[i].Name == product) {
					total = quantity * inventory[i].PurchasePrice;
					inventory[i].RefillProduct(quantity);
					break;
				}
			}
			return total;
		}

		/*<summary> loads the store order history
		<return> void
	    */
		public void LoadOrderHistory() {
			string connectionString = File.ReadAllText("StringConnection.txt");
			using SqlConnection connection = new(connectionString);

			connection.Open();
			string insertOrder = $"SELECT Orders.*, PurchasedItems.PurchaseID, PurchasedItems.Quantity, PurchasedItems.Price, Products.* FROM Orders INNER JOIN PurchasedItems ON Orders.OrderID = PurchasedItems.OrderID INNER JOIN Products ON PurchasedItems.ProductID = Products.ProductID WHERE Orders.StoreID = {this.ID};";
			using SqlCommand command = new(insertOrder, connection);
			using SqlDataReader reader = command.ExecuteReader();

			Console.Write("Items \t"  + "OrderID\t" +  "StoreID\t" + "PersonID\t" + "Total\t\t" + "TimeStamp\t\t" + "PurchaseID\t" + "Quantity\t" + "Price\t\t" + "ProductID\t" + "Product Name\t\t" + "Sale Price\t" + "Purchase Price\n");
			int i = 0;
			while (reader.Read()) {
				Console.WriteLine("Item " + i + "\t" + reader.GetInt32(0) + "\t"+ reader.GetInt32(1) + "\t"+ reader.GetInt32(2) + "\t\t"+ reader.GetDecimal(3) + "\t"+ reader.GetDateTime(4) + "\t" + reader.GetInt32(5) + "\t\t" + reader.GetInt32(6) + "\t\t" + reader.GetDecimal(7) + "\t" + reader.GetInt32(8) + "\t\t" + reader.GetString(9) + "\t\t" + reader.GetDecimal(10) + "\t" + reader.GetDecimal(11));
				i++;
			}

			connection.Close();
		}
	}
}
