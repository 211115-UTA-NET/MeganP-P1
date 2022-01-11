using System.Data.SqlClient;
using Server.Api.Dtos;

namespace Server.Api.Logic {
    public static class CustomerLogic{
		
		
		/*<summary> saves the order to the DB
		 * <params>
		 * Order - the order to save to the DB
		<return> void
	    */
		public static void SaveItems(Item item, int storeId, int customerId) {
			string connectionString = File.ReadAllText("StringConnection.txt");
			using SqlConnection connection = new(connectionString);

			connection.Open();
			string getOrderID = "SELECT MAX(OrderID) FROM Orders;";
			using SqlCommand getOrderCommand = new SqlCommand(getOrderID, connection);
			using SqlDataReader getOrderReader = getOrderCommand.ExecuteReader();
			getOrderReader.Read();
			int orderID = getOrderReader.GetInt32(0);
			connection.Close();
			connection.Open();
			string insertItems = $"INSERT INTO PurchasedItems(OrderID, ProductID, Quantity, Price) VALUES ({orderID}, {item.ProductId}, {item.Quantity}, {item.SalePrice});";
			using SqlCommand sqlCommand = new SqlCommand(insertItems, connection);
			using SqlDataReader sqlReader = sqlCommand.ExecuteReader();
			connection.Close();
			
		}

		public static void SaveOrder(int storeId, int customerId, decimal total) {
			string connectionString = File.ReadAllText("StringConnection.txt");
			using SqlConnection connection = new(connectionString);

			connection.Open();
			string insertOrder = $"INSERT INTO Orders(StoreID, PersonID, Total) VALUES ({storeId}, {customerId}, {total});";
			using SqlCommand command = new(insertOrder, connection);
			using SqlDataReader reader = command.ExecuteReader();
			connection.Close();
		}

		/*<summary> retrieves the customer order history
		<return> void
	    */ 
		public static List<OrderHistory> LoadOrderHistory(int customerId) {
			string connectionString = File.ReadAllText("StringConnection.txt");
			using SqlConnection connection = new(connectionString);

			List<OrderHistory> orders = new List<OrderHistory>();

			connection.Open();
			string insertOrder = $"SELECT Orders.*, PurchasedItems.PurchaseID, PurchasedItems.Quantity, PurchasedItems.Price, Products.ProductID, Products.Name FROM Orders INNER JOIN PurchasedItems ON Orders.OrderID = PurchasedItems.OrderID INNER JOIN Products ON PurchasedItems.ProductID = Products.ProductID WHERE Orders.PersonID = {customerId};";
			using SqlCommand command = new(insertOrder, connection);
			using SqlDataReader reader = command.ExecuteReader();

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
