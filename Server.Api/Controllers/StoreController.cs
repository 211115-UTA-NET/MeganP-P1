using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Server.Api.Dtos;
using Server.Api.Logic;

namespace Server.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase {
        [HttpGet]
        public ActionResult<Store> GetStore([FromQuery, Required] int storeId) {
            Store store = SqlLoader.LoadStore(storeId);
            return store;
        }

        [HttpGet("liststores")]
        public ActionResult<List<Store>> GetStores() {
            List<Store> stores = SqlLoader.GetStoreID();
            return stores;
        }
    }
}
