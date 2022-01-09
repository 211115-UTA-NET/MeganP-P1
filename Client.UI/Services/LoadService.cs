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
            Dictionary<string, string> query = new() { ["FirstName"] = firstName, ["LastName"] = lastName };

            string requestUri = QueryHelpers.AddQueryString("/api/customer", query);
            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            Customer? customer;
            //string output;
            try {
                response = await httpClient.SendAsync(request);
                try {
                    response.EnsureSuccessStatusCode();
                } catch (HttpRequestException hre) {
                    Console.WriteLine("Bad Response Code in CustomerLoadServiceAsync");
                }
                Console.WriteLine(response.StatusCode);
                try {
                    //output = await response.Content.ReadAsStringAsync();
                    customer = await response.Content.ReadFromJsonAsync<Customer>();
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
