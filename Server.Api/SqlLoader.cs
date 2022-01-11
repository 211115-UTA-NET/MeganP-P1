using System.Data.SqlClient;
using Server.Api.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Server.Api {
    public class SqlLoader : IRepository{

        private string connectionString;
        private readonly ILogger<SqlLoader> _logger;


        public SqlLoader(string connectionString, ILogger<SqlLoader> _logger) { 
            this.connectionString = connectionString;
            this._logger = _logger;
        }

        /*<summary> loads products out of database
		<return> List<Product>
	    */
        public List<Product> LoadProducts(int ID) {
            List<Product> products = new List<Product>();
            
            using SqlConnection connection = new(connectionString);

            connection.Open();
            string insertOrder = $"SELECT Products.*, Inventories.Quantity FROM Products INNER JOIN Inventories ON Products.ProductID = Inventories.ProductID WHERE Inventories.StoreID = {ID}";
            using SqlCommand command = new(insertOrder, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                Product product = new Product();
                product.Id = reader.GetInt32(0);
                product.Name = reader.GetString(1);
                product.Quantity = reader.GetInt32(4);
                product.SalePrice = reader.GetDecimal(2);
                product.PurchasePrice = reader.GetDecimal(3);
                products.Add(product);
            }

            connection.Close();
            return products;
        }

        /*<summary> loads customer out of db
		<return> Customer
	    */
        public Customer LoadCustomer(string firstName, string lastName) {
            try {
                string username = firstName.ToLower() + lastName.ToLower();
                
                
                using SqlConnection connection = new(connectionString);
                
                connection.Open();
                
                string insertOrder = $"SELECT * FROM People WHERE Username = '{username}';";
                using SqlCommand command = new(insertOrder, connection);
                using SqlDataReader reader = command.ExecuteReader();
                
                reader.Read();
                Store store;
                try {
                    store = LoadStore(reader.GetInt32(6));
                    
                } catch (InvalidOperationException ioe) {
                    return null;
                }
                
                Customer user = new Customer();
                user.Id = reader.GetInt32(0);
                user.Store = store;
                user.FirstName = reader.GetString(1);
                user.LastName = reader.GetString(2);
                user.Password = reader.GetString(4);

                connection.Close();

                return user;
                
                
            } catch (System.Data.SqlClient.SqlException){
                Console.WriteLine("Broken In customer loader");
                return null;
            }
        }

        /*<summary> loads store out of db
		<return> Store
	    */
        public Store LoadStore(int ID) {
            List<Product> products = LoadProducts(ID);

            
            using SqlConnection connection = new(connectionString);

            connection.Open();
            string insertOrder = $"SELECT * FROM Stores WHERE StoreID = {ID};";
            using SqlCommand command = new(insertOrder, connection);
            using SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            Store store = new Store();
            store.Id = reader.GetInt32(0);
            store.Location = reader.GetString(2);
            store.Inventory = products;

            connection.Close();

            return store;
        }

        /*<summary> saves a new customer to the db
		<return> void
	    */
        public void SaveNewPerson(string firstName, string lastName, string password, int storeID) {
            Console.WriteLine("In Function");
            using SqlConnection connection = new(connectionString);
            Console.WriteLine("In Function1");
            connection.Open();
            string insertOrder = $"INSERT INTO People(FirstName, LastName, Username, Password, Role, StoreID) VALUES ('{firstName}', '{lastName}', '{firstName.ToLower() + lastName.ToLower()}', '{password}', 'Customer', {storeID});";
            using SqlCommand command = new(insertOrder, connection);
            using SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("Did well in database");
            connection.Close();
        }

        /*<summary> prints list of stores to choose which store to shop at
		<return> int
	    */
        public List<Store> GetStoreID() {
            List<Store> stores = new List<Store> ();
            
            using SqlConnection connection = new(connectionString);

            connection.Open();
            string insertOrder = $"SELECT * FROM Stores;";
            using SqlCommand command = new(insertOrder, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                Store store = new Store();
                store.Location = reader.GetString(2);
                store.Id = reader.GetInt32(0);

                stores.Add(store);  
            }

            connection.Close();

            return stores;
        }

        public void SaveItems(Item item, int storeId, int customerId) {

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

        /*<summary> saves the order to the DB
		 * <params>
		 * Order - the order to save to the DB
		<return> void
	    */
        public void SaveOrder(int storeId, int customerId, decimal total) {

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
        public List<OrderHistory> LoadOrderHistory(int customerId) {

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

        /*<summary> method makes a purchase and removes purchased items from inventory
		<return> bool
	    */
        public bool MakePurchase(Item item, int storeId) {

            Dictionary<int, int> inventory = new Dictionary<int, int>();
            try {
                
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string getInventory = $"SELECT * FROM Inventories WHERE StoreId = {storeId}";
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
                        string insertOrder = $"UPDATE Inventories SET Quantity = {remainingInventory} WHERE	ProductID = {ele.Key} AND StoreID = {storeId};";
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

        /*<summary> loads the store order history
		<return> void
	    */
        public List<OrderHistory> LoadOrderHistory() {

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
