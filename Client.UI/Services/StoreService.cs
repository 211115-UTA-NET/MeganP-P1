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
using Newtonsoft.Json;
using System.Web;

namespace Client.UI.Services {
    public  class StoreService {
    
        public async Task<bool> MakePurchase(Order order) {
            HttpClient httpClient = new();
            Uri server = new("https://localhost:7078");
            httpClient.BaseAddress = server;

            var step1 = JsonConvert.SerializeObject(order);
            var step2 = JsonConvert.DeserializeObject<IDictionary<string, string>>(step1);
            //var step3 = step2.Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value));
            //string step4 =  string.Join("&", step3);
            

            //Dictionary<string, string> query = new() { step4 };

            string requestUri = QueryHelpers.AddQueryString("/api/order", step2);
            HttpRequestMessage request = new(HttpMethod.Post, requestUri);
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            //string output;
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

        public void LoadOrderHistory() {

        }

    }
}
