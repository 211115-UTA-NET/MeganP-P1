using System;

namespace Server.Api {
	public class Person {
		private string? firstName;
		private string? lastName;
		private string? password;
		private readonly string? username;
		protected BankAccount? bankAccount;
		protected int ID;

		/*<summary> constructor
		<return> new person
	    */
		public Person (int ID, string firstName, string lastName,  string password, decimal initialFunds) {
			this.ID = ID;
			this.firstName = firstName;
			this.lastName = lastName;
			this.username = firstName.ToLower() + lastName.ToLower();
			this.password = password;
			bankAccount = new BankAccount(this, initialFunds);
		}

		/*<summary> property returning Balance
		<return> decimal
	    */
		public decimal Balance {
			get { return bankAccount.Balance; }
		}

		/*<summary> property returning FirstName
		<return> string
	    */
		public string FirstName {
			get {return this.firstName;}
		}

		/*<summary> property returning LastName
		<return> string
	    */
		public string LastName {
			get {return this.lastName;}
		}

		/*<summary> property returning ID
		<return> int
	    */
		public int Id {
			get { return this.ID; }
		}

		/*<summary> property returning Username
		<return> string
	    */
		public string Username {
			get {return this.username;}
        }

		/*<summary> property returning Password
		<return> string
	    */
		public string Password {
			get { return this.password; }
		}

		/*<summary> resets password
		<return> void
	    */
		public void ForgotPassword() {
			Console.WriteLine("Please Enter Your New Password:");
			string password = Console.ReadLine();
			this.password = password;
		}

	}
}
