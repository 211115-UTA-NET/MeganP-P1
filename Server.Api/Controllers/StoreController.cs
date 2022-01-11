using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Server.Api.Dtos;

namespace Server.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase {

        private readonly IRepository _repository;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IRepository repository, ILogger<StoreController> logger) {
            this._repository = repository;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<Store> GetStore([FromQuery, Required] int storeId) {
            Store store = _repository.LoadStore(storeId);
            return store;
        }

        [HttpGet("liststores")]
        public ActionResult<List<Store>> GetStores() {
            List<Store> stores = _repository.GetStoreID();
            return stores;
        }
    }
}
