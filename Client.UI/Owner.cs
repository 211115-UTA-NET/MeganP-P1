using System;
using System.Data.SqlClient;

namespace Client.UI {
	public class Owner : Person {
		private List<Store>? stores = new List<Store>();

		/*<summary> constructor
		<return> creates new Owner
	    */
		public Owner (int ID, string firstName, string lastName, string password) : base (ID, firstName, lastName, password) {
			
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

	}
}
