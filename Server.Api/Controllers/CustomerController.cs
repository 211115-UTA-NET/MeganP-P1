using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Server.Api.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase {
        
        [HttpGet]
        public ActionResult<Customer> GetRole([FromQuery, Required] string firstName, [FromQuery, Required] string lastName) {
            Console.WriteLine("I'm hereeeee");
            Customer role;
            try {
                role = SqlLoader.LoadCustomer(firstName, lastName);
            } catch (Exception ex) {
                Console.WriteLine("Server Error with CustomerController");
                return StatusCode(500);
            }

            return role;
            
        }
    }
}
