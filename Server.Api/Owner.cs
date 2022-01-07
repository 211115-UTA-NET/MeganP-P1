using System;
using System.Data.SqlClient;

namespace Server.Api {
	public class Owner : Person, ISaveOrder {
		private List<Store>? stores = new List<Store>();

		/*<summary> constructor
		<return> creates new Owner
	    */
		public Owner (int ID, string firstName, string lastName, string password, decimal initialFunds) : base (ID, firstName, lastName, password, initialFunds) {
			
		}

		/*<summary> property returning Stores
		<return> List<Store>?
	    */
		public List<Store>? Stores {
			get { return this.stores; }
		}

		/*<summary> sets list of stores
		<return> void
	    */
		public void SetStores(List<Store> stores) {
			this.stores = stores;
        }

		/*<summary> saves order to the db
		<return> void
	    */
		public void SaveOrder(Order order) {
			string connectionString = File.ReadAllText("StringConnection.txt");
			using SqlConnection connection = new(connectionString);

			connection.Open();
			string insertOrder = $"INSERT INTO Orders(StoreID, PersonID, Total) VALUES ({order.GetStoreID}, {this.ID}, {order.Total});";
			using SqlCommand command = new(insertOrder, connection);
			using SqlDataReader reader = command.ExecuteReader();
			connection.Close();

			connection.Open();
			string getOrderID = "SELECT MAX(OrderID) FROM Orders;";
			using SqlCommand getOrderCommand = new SqlCommand(getOrderID, connection);
			using SqlDataReader getOrderReader = getOrderCommand.ExecuteReader();
			getOrderReader.Read();
			int OrderID = getOrderReader.GetInt32(0);
			connection.Close();

			connection.Open();
			string insertItems = $"INSERT INTO PurchasedItems(OrderID, ProductID, Quantity, Price) VALUES ({OrderID}, {order.Items[0].ProductID}, {order.Items[0].Quantity}, {order.Items[0].PurchasePrice});";
			using SqlCommand sqlCommand = new SqlCommand(insertItems, connection);
			using SqlDataReader sqlReader = sqlCommand.ExecuteReader();
			connection.Close();
			
		}

	}
}
