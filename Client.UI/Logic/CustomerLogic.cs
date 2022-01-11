using System.Data.SqlClient;
using Client.UI.Dtos;
using Client.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;
using Client.UI.Logic;
using System.Collections.Generic;
using System.Web;

namespace Client.UI.Logic {
    public class CustomerLogic {
		 
		 public List<Item> shoppingCart { get; set; }  = new List<Item>();

		/*<summary> method checks for correct password
 * <params> string - password being tested
<return> bool
*/
		public bool ValidatePassword(string expected, string actual) {
			if (expected == actual) {
				return true;
			} else {
				return false;
			}
		}

		public async Task<bool> PostNewCustomer(string firstName, string lastName, string password, int storeId) {
			HttpClient httpClient = new();
			Uri server = new("https://localhost:7078");
			httpClient.BaseAddress = server;

			Dictionary<string, string> query = new() { ["FirstName"] = firstName, ["LastName"] = lastName, ["password"] = password, ["storeId"] = Convert.ToString(storeId) };
			string requestUri = QueryHelpers.AddQueryString("/api/customer", query);

			HttpRequestMessage request = new(HttpMethod.Post, requestUri);
			request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

			HttpResponseMessage response;
			try {
				response = await httpClient.SendAsync(request);
				response.EnsureSuccessStatusCode();

				if (response.StatusCode == System.Net.HttpStatusCode.OK) {
					return true;
				} else {
					return false;
				}
			} catch (NullReferenceException nre) {
				Console.WriteLine("Unexpected server behavior in CustomerLoadServiceAsync");
				return false;
			}
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
					Item item = new Item();
					item.ProductId = product.Id;
					item.Quantity = numItems;
					item.Name = product.Name;
					item.SalePrice = product.SalePrice;
					item.PurchasePrice = product.PurchasePrice;
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
	    */ 
		public async Task<bool> MakePurchase(int storeId, Customer customer) {
			Order order = new Order();
			order.Items = this.shoppingCart;
			order.StoreId = storeId;
			order.CustomerId = customer.Id;
			order.Total = OrderLogic.TotalItUp(order);
			OrderLogic.ToString(order, customer);
			Console.WriteLine("1. Confirm Order");
			Console.WriteLine("2. Cancel Order");
			string? answer = Console.ReadLine();
			if (answer == "1") {
				StoreService storeService = new StoreService();
				await storeService.SaveOrder(storeId, customer.Id, order.Total);
				await storeService.SaveItems(order, storeId, customer.Id);
				this.shoppingCart.Clear();
				return true;
				
			} else {
				Console.WriteLine("Either you cancelled your order or you did not input a (1) to confirm order. Your shopping cart was saved, but the order was cancelled.");
				return false;
			}
		} 
	}
}
