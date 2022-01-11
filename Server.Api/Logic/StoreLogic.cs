using System;
using System.Data.SqlClient;
using Server.Api.Dtos;
using System.Collections.Generic;

namespace Server.Api.Logic {
	public class StoreLogic {
		
		public int Id { get; set; }
		public int CustomerId { get; set; }

		/*<summary> constructor
		<return> new Store
	    */	
		public StoreLogic (int storeId, int customerId) {
			this.Id = storeId;
			this.CustomerId = customerId;
		}
		 
		
		/*<summary> method makes a purchase and removes purchased items from inventory
		<return> bool
	    */ 
		public bool MakePurchase(Item item) {
			
			Dictionary<int, int> inventory = new Dictionary<int, int>();
			try {
				string connectionString = File.ReadAllText("StringConnection.txt");
				using SqlConnection connection = new SqlConnection(connectionString);
				connection.Open();
				string getInventory = $"SELECT * FROM Inventories WHERE StoreId = {this.Id}";
				using SqlCommand getProducts = new(getInventory, connection);
				using SqlDataReader productReader = getProducts.ExecuteReader();
				
				while (productReader.Read()) {
					inventory.Add(productReader.GetInt32(0), productReader.GetInt32(2));
				}
				connection.Close();
				
				foreach (KeyValuePair<int, int> ele in inventory) {
					if (item.ProductId == ele.Key) {
						int remainingInventory = ele.Value - item.Quantity;
						Console.WriteLine(remainingInventory + " " + ele.Value + " " + item.Quantity);
							
						connection.Open();
						string insertOrder = $"UPDATE Inventories SET Quantity = {remainingInventory} WHERE	ProductID = {ele.Key} AND StoreID = {this.Id};";
						using SqlCommand command = new(insertOrder, connection);
						using SqlDataReader reader = command.ExecuteReader();
						connection.Close();

						break;
					}
				}
				
				Console.WriteLine("Purchase Successful with the Store");
				return true;
			} catch (Exception ex) {
				Console.WriteLine("Problem in store logic make purchase");
				return false;
            }
			
		} 

		/*<summary> prints inventory options
		<return> void
	    */ /*
		public void PrintInventory() {
			for (int i = 0; i < inventory.Count; i++) {
				Console.WriteLine(i + ". " + inventory[i].Quantity + "x " + inventory[i].Name);
			}
		}
		  */
		/*<summary> resupplies all products in inventory
		<return> void
	    */ /*
		public void ResupplyStore() {
			//implement
		}  */

		/*<summary> Adds a product to the inventory
		<return> void
	    */ /*
		public void AddProductToInventory(Product product) {
			this.inventory.Add(product);
		} */

		/*<summary> resupplies a product and returns the cost of the restock
		<return> decimal
	    *//*
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
		}*/

		/*<summary> loads the store order history
		<return> void
	    */ 
		public static List<OrderHistory> LoadOrderHistory() {
			string connectionString = File.ReadAllText("StringConnection.txt");
			using SqlConnection connection = new(connectionString);

			connection.Open();
			string insertOrder = $"SELECT Orders.*, PurchasedItems.PurchaseID, PurchasedItems.Quantity, PurchasedItems.Price, Products.* FROM Orders INNER JOIN PurchasedItems ON Orders.OrderID = PurchasedItems.OrderID INNER JOIN Products ON PurchasedItems.ProductID = Products.ProductID;";
			using SqlCommand command = new(insertOrder, connection);
			using SqlDataReader reader = command.ExecuteReader();

			List<OrderHistory> orders = new List<OrderHistory>();

			while (reader.Read()) {
				OrderHistory order = new OrderHistory();
				order.OrderId = reader.GetInt32(0);
				order.StoreId = reader.GetInt32(1);
				order.CustomerId = reader.GetInt32(2);
				order.Total = reader.GetDecimal(3);
				order.dateTime = reader.GetDateTime(4);
				order.PurchaseId = reader.GetInt32(5);
				order.Quantity = reader.GetInt32(6);
				order.Price = reader.GetDecimal(7);
				order.ProductId = reader.GetInt32(8);
				order.Name = reader.GetString(9);

				orders.Add(order);
			}

			connection.Close();
			return orders;
		} 
	}
}
