using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Client.UI.Dtos;
using Client.UI.Logic;
using System.Collections.Generic;
using System.Web;

namespace Client.UI.Services {
    public class StoreService {

        public async Task<int> GetStoreId() {
            HttpClient httpClient = new();
            Uri server = new("https://localhost:7078");
            httpClient.BaseAddress = server;

            HttpRequestMessage request = new(HttpMethod.Get, "/api/store/liststores");
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try {
                response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                    List<Store>? stores = await response.Content.ReadFromJsonAsync<List<Store>>();
                    for (int i = 0; i < stores.Count; i++) {
                        Console.WriteLine(stores[i].Id + ". " + stores[i].Location);
                    }
                    bool input = Int32.TryParse(Console.ReadLine(), out int storeId);
                    if (input) {
                        return storeId;
                    } else {
                        Console.WriteLine("You entered faulty input.");
                        return 0;
                    }
                } else {
                    return 0;
                }
            } catch (NullReferenceException nre) {
                Console.WriteLine("Unexpected server behavior in CustomerLoadServiceAsync");
                return 0;
            }
        }

        public async Task<bool> SaveOrder(int storeId, int customerId, decimal total) {
            HttpClient httpClient = new();
            Uri server = new("https://localhost:7078");
            httpClient.BaseAddress = server;

            Dictionary<string, string> query = new() { ["storeId"] = Convert.ToString(storeId), ["customerId"] = Convert.ToString(customerId), ["total"] = Convert.ToString(total) };
            string requestUri = QueryHelpers.AddQueryString("/api/order/order", query);

            HttpRequestMessage request = new(HttpMethod.Post, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try {
                response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                    Console.WriteLine(response.Content);

                } else {
                    return false;
                }
            } catch (NullReferenceException nre) {
                Console.WriteLine("Unexpected server behavior in CustomerLoadServiceAsync");
                return false;
            }
            return true;
        }
    
        public async Task<bool> SaveItems(Order order, int storeId, int customerId) {
            HttpClient httpClient = new();
            Uri server = new("https://localhost:7078");
            httpClient.BaseAddress = server;
            Console.WriteLine("1");

            //StringContent items = new StringContent(JsonSerializer.Serialize(order.Items), Encoding.UTF8, MediaTypeNames.Application.Json);
            

            for (int i = 0; i < order.Items.Count; i++) {
                Dictionary<string, string> query = new() { ["storeId"] = Convert.ToString(storeId), ["customerId"] = Convert.ToString(customerId), ["total"] = Convert.ToString(order.Total), ["productId"] = Convert.ToString(order.Items[i].ProductId), ["salePrice"] = Convert.ToString(order.Items[i].SalePrice), ["purchasePrice"] = Convert.ToString(order.Items[i].PurchasePrice), ["name"] = Convert.ToString(order.Items[i].Name), ["quantity"] = Convert.ToString(order.Items[i].Quantity)};
                string requestUri = QueryHelpers.AddQueryString("/api/order/items", query);

                HttpRequestMessage request = new(HttpMethod.Post, requestUri);
                //request.Content = new StringContent(JsonSerializer.Serialize(items), Encoding.UTF8, MediaTypeNames.Application.Json);
                request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

                HttpResponseMessage response;
                //string output;
                try {
                    response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                        return false;
                        
                    }
                } catch (NullReferenceException nre) {
                    Console.WriteLine("Unexpected server behavior in CustomerLoadServiceAsync");
                    return false;
                }
            }
            return true;
        }


    }
}
