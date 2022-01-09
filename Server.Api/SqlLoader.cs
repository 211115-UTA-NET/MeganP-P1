using System.Data.SqlClient;
using Server.Api.Dtos;

namespace Server.Api {
    public static class SqlLoader {

        /*<summary> loads products out of database
		<return> List<Product>
	    */
        public static List<Product> LoadProducts(int ID) {
            List<Product> products = new List<Product>();
            string connectionString = File.ReadAllText("StringConnection.txt");
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
        public static Customer LoadCustomer(string firstName, string lastName) {
            try {
                string username = firstName.ToLower() + lastName.ToLower();

                string connectionString = File.ReadAllText("StringConnection.txt");
                using SqlConnection connection = new(connectionString);

                connection.Open();
               
                string insertOrder = $"SELECT * FROM People WHERE Username = '{username}';";
                using SqlCommand command = new(insertOrder, connection);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Store store;
                try {
                    store = LoadStore(reader.GetInt32(7));
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
                return null;
            }
        }

        /*<summary> loads owner out of db
		<return> Owner
	    */
        public static Owner LoadOwner(string firstName, string lastName) {
            try {
                string username = firstName.ToLower() + lastName.ToLower();

                string connectionString = File.ReadAllText("StringConnection.txt");
                using SqlConnection connection = new(connectionString);

                connection.Open();
                string insertOrder = $"SELECT * FROM People WHERE Username = '{username}';";
                using SqlCommand command = new(insertOrder, connection);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                Owner user = new Owner();
                user.Id = reader.GetInt32(0);
                user.FirstName = reader.GetString(1);
                user.LastName = reader.GetString(2);
                user.Password = reader.GetString(4);
                connection.Close();
                return user;
                
            } catch (System.Data.SqlClient.SqlException) {
                return null;
            }
        }

        /*<summary> loads store out of db
		<return> Store
	    */
        public static Store LoadStore(int ID) {
            List<Product> products = LoadProducts(ID);

            string connectionString = File.ReadAllText("StringConnection.txt");
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

        /*<summary> loads owner list of stores out of db
		<return> List<Store>
	    */
        public static List<Store> LoadOwnerStores(string username) {
            List<Store> stores = new List<Store>();

            string connectionString = File.ReadAllText("StringConnection.txt");
            using SqlConnection connection = new(connectionString);

            connection.Open();
            string insertOrder = $"SELECT Stores.StoreID FROM Stores INNER JOIN People ON Stores.PersonID = People.PersonID WHERE People.Username = '{username}';";
            using SqlCommand command = new(insertOrder, connection);
            using SqlDataReader reader = command.ExecuteReader();
            
            while(reader.Read()) {
                Store store = LoadStore(reader.GetInt32(0));
                stores.Add(store);
            }


            connection.Close();

            return stores;
        }

        /*<summary> saves a new customer to the db
		<return> void
	    */
        public static void SaveNewPerson(string firstName, string lastName, string password, decimal balance, int storeID) {
            string connectionString = File.ReadAllText("StringConnection.txt");
            using SqlConnection connection = new(connectionString);

            connection.Open();
            string insertOrder = $"INSERT INTO People(FirstName, LastName, Username, Password, Role, Balance, StoreID) VALUES ('{firstName}', '{lastName}', '{firstName.ToLower() + lastName.ToLower()}', '{password}', 'Customer', {balance}, {storeID});";
            using SqlCommand command = new(insertOrder, connection);
            using SqlDataReader reader = command.ExecuteReader();

            connection.Close();
        }

        /*<summary> prints list of stores to choose which store to shop at
		<return> int
	    */
        public static int GetStoreID() {
 
            string connectionString = File.ReadAllText("StringConnection.txt");
            using SqlConnection connection = new(connectionString);

            connection.Open();
            string insertOrder = $"SELECT * FROM Stores;";
            using SqlCommand command = new(insertOrder, connection);
            using SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Enter the Number for the Location You're Shopping at.");

            while (reader.Read()) {
                Console.WriteLine(reader.GetInt32(0) + ". " + reader.GetString(2));
                
            }

            connection.Close();

            return Convert.ToInt32(Console.ReadLine());
        }

        public static string GetRole(string user) {
            string connectionString = File.ReadAllText("StringConnection.txt");
            using SqlConnection connection = new(connectionString);

            connection.Open();

            string insertOrder = $"SELECT * FROM People WHERE Username = '{user}';";
            using SqlCommand command = new(insertOrder, connection);
            using SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            string role = reader.GetString(5);

            connection.Close();
            return role;
        }
    }
}
