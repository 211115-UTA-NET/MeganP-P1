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


namespace Client.UI.Services {
    public  class LoadService {

        public async void StoreLoadServiceAsync() {

        }

        public async void ProductLoadServiceAsync() {

        }

        public async Task<Customer> CustomerLoadServiceAsync(string firstName, string lastName) {
            
            HttpClient httpClient = new();
            Uri server = new("https://localhost:7078");
            httpClient.BaseAddress = server;
            Dictionary<string, string> query = new() { ["firstName"] = firstName, ["lastName"] = lastName };

            string requestUri = QueryHelpers.AddQueryString("/api/customer", query);
            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            Customer? customer;
            Console.WriteLine("1");
            try {
                Console.WriteLine("2");
                response = await httpClient.SendAsync(request);
                Console.WriteLine("3");
                try {
                    Console.WriteLine("4");
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine("5");
                } catch (HttpRequestException hre) {
                    Console.WriteLine("Bad Response Code in CustomerLoadServiceAsync");
                }
                Console.WriteLine("6");
                Console.WriteLine(response.StatusCode);
                try {
                    Console.WriteLine("7");
                    customer = await response.Content.ReadFromJsonAsync<Customer>();
                    Console.WriteLine("8");
                    return customer;
                } catch (InvalidOperationException ioe) {
                    Console.WriteLine("Bad Conversion from server response in LoadCustomerAsync");
                    return null;
                }
            } catch (NullReferenceException nre) {
                Console.WriteLine("Unexpected server behavior in CustomerLoadServiceAsync");
                return null;
            }
 
            
        }

        public async void OwnerLoadServiceAsync() {

        }

    }
}
