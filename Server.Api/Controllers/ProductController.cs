using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Server.Api.Dtos;

namespace Server.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {

        private readonly IRepository _repository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IRepository repository, ILogger<ProductController> logger) {
            this._repository = repository;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts([FromQuery, Required] int storeId) {
            List<Product> products = _repository.LoadProducts(storeId);
            return products;
        }
    }
}
