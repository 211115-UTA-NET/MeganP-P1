using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Server.Api.Dtos;

namespace Server.Api.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase {
        
        [HttpGet]
        public ActionResult<Customer> GetRole([FromQuery, Required] string FirstName, [FromQuery, Required] string LastName) {
            Console.WriteLine("I'm hereeeee");
            Customer role;
            try {
                role = SqlLoader.LoadCustomer(FirstName, LastName);
            } catch (Exception ex) {
                Console.WriteLine("Server Error with CustomerController");
                return StatusCode(500);
            }

            return role;
            
        }
    }
}
