using System;
using System.Data.SqlClient;

namespace Server.Api {
	public class BankAccount {
		private decimal balance;
		private Person accountOwner;


		/*<summary> constructor
		<params> 
		Person - the owner of the bank account
		Decimal - initial bank balance
		<return> a new BankAccount
	    */
		public BankAccount(Person accountOwner, decimal initialFunds) {
			this.accountOwner = accountOwner;
			if (initialFunds > 0) {
				this.balance = initialFunds;
			} else {
				while (true) {
					Console.WriteLine("Must have positive funds.\nPlease Enter You Positive Non-Zero initial balance");
					decimal.TryParse(Console.ReadLine(), out initialFunds);
					if (initialFunds > 0) {
						this.balance = initialFunds;
						break;
					}
				}
			}
		}

		/*<summary> property to return balance
		<return> decimal
	    */
		public decimal Balance {
			get { return this.balance; }
		}

		/*<summary> method to make a transaction
		<params> 
		Order - an order to access total of transaction
		<return> bool
	    */
		public bool MakeTransaction (Order order) {
			if (this.balance - order.Total < 0) {
				Console.WriteLine("Card Denied: Insufficent Funds");
				return false;
			} else {
				this.balance -= order.Total;

				string connectionString = File.ReadAllText("StringConnection.txt");
				using SqlConnection connection = new(connectionString);

				connection.Open();
				string insertOrder = $"UPDATE People SET Balance = {this.balance} WHERE PersonID = {order.GetPersonID}";
				using SqlCommand command = new(insertOrder, connection);
				using SqlDataReader reader = command.ExecuteReader();
				connection.Close();

				return true;
			}
		}
	}
}
