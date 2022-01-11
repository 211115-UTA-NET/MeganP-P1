using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Server.Api.Dtos;
using Server.Api.Logic;

namespace Server.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {
        [HttpGet]
        public ActionResult<List<Product>> GetProducts([FromQuery, Required] int storeId) {
            List<Product> products = SqlLoader.LoadProducts(storeId);
            return products;
        }
    }
}
