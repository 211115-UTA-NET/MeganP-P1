using System;

namespace Server.Api.Dtos {
	public class Person {
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Password { get; set; }
		public int Id { get; set; }

		public Person () {

        }

	}
}
