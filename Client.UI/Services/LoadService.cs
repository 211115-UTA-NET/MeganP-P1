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

        public async Task<Store> StoreLoadServiceAsync(int storeId) {
            HttpClient httpClient = new();
            Uri server = new("https://meganpostlewaitprojectone.azurewebsites.net");
            httpClient.BaseAddress = server;
            Dictionary<string, string> query = new() { ["storeId"] = Convert.ToString(storeId) };
            string requestUri = QueryHelpers.AddQueryString("/api/store", query);
            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            Store? store;
            try {
                response = await httpClient.SendAsync(request);
                try {
                    response.EnsureSuccessStatusCode();
                } catch (HttpRequestException hre) {
                    Console.WriteLine("Bad Response Code in StoreLoadServiceAsync");
                }
                
                try {
                    //output = await response.Content.ReadAsStringAsync();
                    store = await response.Content.ReadFromJsonAsync<Store>();
                    return store;
                } catch (InvalidOperationException ioe) {
                    Console.WriteLine("Bad Conversion from server response in LoadCustomerAsync");
                    return null;
                }
            } catch (NullReferenceException nre) {
                Console.WriteLine("Unexpected server behavior in CustomerLoadServiceAsync");
                return null;
            }

        }

        public async Task<List<Product>> ProductLoadServiceAsync(int storeId) {
            HttpClient httpClient = new();
            Uri server = new("https://meganpostlewaitprojectone.azurewebsites.net");
            httpClient.BaseAddress = server;
            Dictionary<string, string> query = new() { ["storeId"] = Convert.ToString(storeId) };
            string requestUri = QueryHelpers.AddQueryString("/api/product", query);
            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            List<Product>? products;
            try {
                response = await httpClient.SendAsync(request);
                try {
                    response.EnsureSuccessStatusCode();
                } catch (HttpRequestException hre) {
                    Console.WriteLine("Bad Response Code in StoreLoadServiceAsync");
                }
               
                try {
                    //output = await response.Content.ReadAsStringAsync();
                    products = await response.Content.ReadFromJsonAsync<List<Product>>();
                    return products;
                } catch (InvalidOperationException ioe) {
                    Console.WriteLine("Bad Conversion from server response in LoadCustomerAsync");
                    return null;
                }
            } catch (NullReferenceException nre) {
                Console.WriteLine("Unexpected server behavior in CustomerLoadServiceAsync");
                return null;
            }
        }

        public async Task<Customer> CustomerLoadServiceAsync(string firstName, string lastName) {
            
            HttpClient httpClient = new();
            Uri server = new("https://meganpostlewaitprojectone.azurewebsites.net");
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

        public async Task<bool> NewOrReturning(string firstName, string lastName) {
            HttpClient httpClient = new();
            Uri server = new("https://meganpostlewaitprojectone.azurewebsites.net");
            Console.WriteLine("1");
            httpClient.BaseAddress = server;
            Dictionary<string, string> query = new() { ["FirstName"] = firstName, ["LastName"] = lastName };
            Console.WriteLine("2");
            string requestUri = QueryHelpers.AddQueryString("/api/customer/doesexist", query);
            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));
            Console.WriteLine("3");
            HttpResponseMessage response;
            bool doesExist;
            //string output;
            try {
                response = await httpClient.SendAsync(request);
                try {
                    response.EnsureSuccessStatusCode();
                } catch (HttpRequestException hre) {
                    Console.WriteLine("Bad Response Code in CustomerLoadServiceAsync");
                }
                
                try {
                    //output = await response.Content.ReadAsStringAsync();
                    doesExist = await response.Content.ReadFromJsonAsync<bool>();
                    return doesExist;
                } catch (InvalidOperationException ioe) {
                    Console.WriteLine("Bad Conversion from server response in LoadCustomerAsync");
                    return false;
                }
            } catch (NullReferenceException nre) {
                Console.WriteLine("Unexpected server behavior in CustomerLoadServiceAsync");
                return false;
            }
        }

        public async void CustomerLoadOrdersAsync(int customerId) {
            HttpClient httpClient = new();
            Uri server = new("https://meganpostlewaitprojectone.azurewebsites.net");
            httpClient.BaseAddress = server;
            Dictionary<string, string> query = new() { ["customerId"] = Convert.ToString(customerId) };
            string requestUri = QueryHelpers.AddQueryString("/api/order/customer", query);
            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            List<OrderHistory>? orders;
            try {
                response = await httpClient.SendAsync(request);
                try {
                    response.EnsureSuccessStatusCode();
                } catch (HttpRequestException hre) {
                    Console.WriteLine("Bad Response Code in StoreLoadServiceAsync");
                }

                try {
                    orders = await response.Content.ReadFromJsonAsync<List<OrderHistory>>();
                    Console.Write("Items \t" + "OrderID\t" + "StoreID\t" + "PersonID\t" + "Total\t\t" + "TimeStamp\t\t" + "PurchaseID\t" + "Quantity\t" + "Price\t\t" + "ProductID\t" + "Product Name\n");
                    for (int i = 0; i < orders.Count; i++) {
                        Console.WriteLine("Item " + i + "\t" + orders[i].OrderId + "\t" + orders[i].StoreId + "\t" + orders[i].CustomerId + "\t\t" + orders[i].Total + "\t" + orders[i].dateTime + "\t" + orders[i].PurchaseId + "\t\t" + orders[i].Quantity + "\t\t" + orders[i].Price + "\t" + orders[i].ProductId+ "\t\t" + orders[i].Name + "\t\t");
                    }
                } catch (InvalidOperationException ioe) {
                    Console.WriteLine("Bad Conversion from server response in LoadCustomerAsync");
                    
                }
            } catch (NullReferenceException nre) {
                Console.WriteLine("Unexpected server behavior in CustomerLoadServiceAsync");
                
            }
        }

        public async Task StoreLoadOrdersAsync() {
            Console.WriteLine("1");
            HttpClient httpClient = new();
            Uri server = new("https://meganpostlewaitprojectone.azurewebsites.net");
            httpClient.BaseAddress = server;
            Console.WriteLine("2");
            Dictionary<string, string> query = new() { ["Location"] = "*" };
            string requestUri = QueryHelpers.AddQueryString("/api/order/store", query);
            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));
            Console.WriteLine("3");
            HttpResponseMessage response;
            List<OrderHistory>? orders;
            
            try {
                Console.WriteLine("4");
                response = await httpClient.SendAsync(request);
                Console.WriteLine("4.5");
                try {
                    Console.WriteLine("5");
                    response.EnsureSuccessStatusCode();
                } catch (HttpRequestException hre) {
                    Console.WriteLine("Bad Response Code in StoreLoadServiceAsync");
                }

                try {
                    Console.WriteLine("6");
                    orders = await response.Content.ReadFromJsonAsync<List<OrderHistory>>();
                    Console.Write("Items \t" + "OrderID\t" + "StoreID\t" + "PersonID\t" + "Total\t\t" + "TimeStamp\t\t" + "PurchaseID\t" + "Quantity\t" + "Price\t\t" + "ProductID\t" + "Product Name\n");
                    for (int i = 0; i < orders.Count; i++) {
                        Console.WriteLine("Item " + i + "\t" + orders[i].OrderId + "\t" + orders[i].StoreId + "\t" + orders[i].CustomerId + "\t\t" + orders[i].Total + "\t" + orders[i].dateTime + "\t" + orders[i].PurchaseId + "\t\t" + orders[i].Quantity + "\t\t" + orders[i].Price + "\t" + orders[i].ProductId + "\t\t" + orders[i].Name + "\t\t");
                    }
                    Console.WriteLine("7");
                } catch (InvalidOperationException ioe) {
                    Console.WriteLine("Bad Conversion from server response in LoadCustomerAsync");

                }
            } catch (NullReferenceException nre) {
                Console.WriteLine("Unexpected server behavior in StoreOrdersLoadServiceAsync");

            }
            
        }

    }
}
