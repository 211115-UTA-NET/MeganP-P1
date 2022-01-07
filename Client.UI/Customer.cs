using System.Data.SqlClient;

namespace Client.UI {
    public class Customer : Person {
		private List<Item>? shoppingCart;
		private Store? store;

		/*<summary> constructor
		<params> 
		int - id number
		Store - the store the customer shops at
		string - first name
		string - last name
		string - password
		decimal - initialFunds
		<return> a new BankAccount
		*/
		public Customer(int ID, Store store, string firstName, string lastName, string password) : base (ID, firstName, lastName, password) {
			this.store = store;
			shoppingCart = new List<Item>();
		}

		/*<summary> property returning Store
		<return> Store
	    */
		public Store Store {
			get { return this.store; }
		}

		/*<summary> property returning int
		<return> int
	    */
		public int GetStoreID {
			get { return store.Id; }
        }

		/*<summary> property returning Store
		 * <params>
		 * Product - the product to add to cart
		 * numItems - the number of product wanted to be purchased
		<return> int
	    */
		public int AddToCart(Product product, int numItems) {
			if (numItems > 50) {
				Console.WriteLine("Cannot Add more than 50 of any given item.");
				return 0;
			} else {
				if (product.Quantity - numItems >= 0) {
					Item item = new Item(product, numItems);
					this.shoppingCart.Add(item);
					Console.WriteLine("Added to Cart");
					return numItems;
				} else {
					Console.WriteLine("Only " + Convert.ToString(product.Quantity) + " " + product.Name + "s" + " left.\nNo items have been added to your cart.");
					return 0;
				}
			}
		}

		/*<summary> prints the cart info
		<return> void
	    */
		public void PrintCart() {
			Console.WriteLine("Shopping Cart:");
			for (int i = 0; i < shoppingCart.Count; i++) {
				Console.WriteLine(i + ". " + shoppingCart[i].Quantity + "x " + shoppingCart[i].SalePrice + " - "+ shoppingCart[i].Name);
            }
        }

		/*<summary> makes an order and saves the order and updates the store inventory
		<return> bool
	    */ /*
		public bool MakePurchase() {
			Order order = new Order(this.store, this, this.shoppingCart);
			order.ToString();
			Console.WriteLine("1. Confirm Order");
			Console.WriteLine("2. Cancel Order");
			string? answer = Console.ReadLine();
			if (answer == "1") {
				bool success = this.bankAccount.MakeTransaction(order);
				if (success == true) {
					this.store.MakePurchase(order);
					this.SaveOrder(order);
					this.shoppingCart.Clear();
					return true;
				} else {
					return false;
				}
			} else {
				Console.WriteLine("Either you cancelled your order or you did not input a (1) to confirm order. Your shopping cart was saved, but the order was cancelled.");
				return false;
			}
		} */


		/*<summary> saves the order to the DB
		 * <params>
		 * Order - the order to save to the DB
		<return> void
	    */
		public void SaveOrder(Order order) {
			string connectionString = File.ReadAllText("StringConnection.txt");
			using SqlConnection connection = new(connectionString);
			
			connection.Open();
			string insertOrder = $"INSERT INTO Orders(StoreID, PersonID, Total) VALUES ({this.store.Id}, {this.ID}, {order.Total});";
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
			for (int i = 0; i < shoppingCart.Count; i++) {
				connection.Open();
				string insertItems = $"INSERT INTO PurchasedItems(OrderID, ProductID, Quantity, Price) VALUES ({OrderID}, {shoppingCart[i].ProductID}, {shoppingCart[i].Quantity}, {shoppingCart[i].SalePrice});";
				using SqlCommand sqlCommand = new SqlCommand(insertItems, connection);	
				using SqlDataReader sqlReader = sqlCommand.ExecuteReader();
				connection.Close();
			}
        }

		/*<summary> retrieves the customer order history
		<return> void
	    */
		public void LoadOrderHistory() {
			string connectionString = File.ReadAllText("StringConnection.txt");
			using SqlConnection connection = new(connectionString);

			connection.Open();
			string insertOrder = $"SELECT Orders.*, PurchasedItems.PurchaseID, PurchasedItems.Quantity, PurchasedItems.Price, Products.ProductID, Products.Name FROM Orders INNER JOIN PurchasedItems ON Orders.OrderID = PurchasedItems.OrderID INNER JOIN Products ON PurchasedItems.ProductID = Products.ProductID WHERE Orders.PersonID = {this.ID};";
			using SqlCommand command = new(insertOrder, connection);
			using SqlDataReader reader = command.ExecuteReader();

			Console.Write("Items \t" + "OrderID\t" + "StoreID\t" + "PersonID\t" + "Total\t\t" + "TimeStamp\t\t" + "PurchaseID\t" + "Quantity\t" + "Price\t\t" + "ProductID\t" + "Product Name\n");
			int i = 0;
			while (reader.Read()) {
				Console.WriteLine("Item " + i + "\t" + reader.GetInt32(0) + "\t" + reader.GetInt32(1) + "\t" + reader.GetInt32(2) + "\t\t" + reader.GetDecimal(3) + "\t" + reader.GetDateTime(4) + "\t" + reader.GetInt32(5) + "\t\t" + reader.GetInt32(6) + "\t\t" + reader.GetDecimal(7) + "\t" + reader.GetInt32(8) + "\t\t" + reader.GetString(9) + "\t\t");
				i++;
			}

			connection.Close();
		}
	}
}
