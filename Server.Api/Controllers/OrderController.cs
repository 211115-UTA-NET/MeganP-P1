using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Server.Api.Dtos;
using Server.Api.Logic;


namespace Server.Api.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase {

        [HttpPost("items")]
        public IActionResult SaveItem([FromQuery, Required] int storeId, [FromQuery, Required] int customerId, [FromQuery, Required] decimal total, [FromQuery, Required] int productId, [FromQuery, Required] decimal salePrice, [FromQuery, Required] decimal purchasePrice, [FromQuery, Required] string name, [FromQuery, Required] int quantity) {
            Item item = new Item();
            item.Name = name;
            item.Quantity = quantity;
            item.SalePrice = salePrice;
            item.PurchasePrice = purchasePrice;
            item.ProductId = productId;
            StoreLogic storeLogic = new StoreLogic(storeId, customerId);
            try {
                storeLogic.MakePurchase(item); //updates inventory quantity
                CustomerLogic.SaveItems(item, storeId, customerId); //saves purchased item to DB

                return StatusCode(200);
            } catch (Exception ex) {
                return StatusCode(500);
            }

        }
        [HttpPost("order")]
        public IActionResult SaveOrder([FromQuery, Required] int storeId, [FromQuery, Required] int customerId, [FromQuery, Required] decimal total) {
            try {
                CustomerLogic.SaveOrder(storeId, customerId, total);
                return StatusCode(200);
            } catch (Exception ex) {
                Console.WriteLine("Save order broke");
                return StatusCode(500);
            }
        }

        [HttpGet("customer")]
        public ActionResult<List<OrderHistory>> GetCustomerOrders([FromQuery, Required] int customerId) {
            try {
                List<OrderHistory> orders = CustomerLogic.LoadOrderHistory(customerId);
                return orders;
            } catch (Exception ex) {
                Console.WriteLine("Error in server broke load customer orders");
                return StatusCode(500);
            }
            
        }

        [HttpGet("store")]
        public ActionResult<List<OrderHistory>> GetStoreOrders() {
            try {
                List<OrderHistory> orders = StoreLogic.LoadOrderHistory();
                Console.WriteLine("Im here");
                return orders;
            } catch (Exception ex) {
                Console.WriteLine("Error in server broke load customer orders");
                return StatusCode(500);
            }

        }
    }
}
