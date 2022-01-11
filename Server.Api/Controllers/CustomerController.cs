using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Server.Api.Dtos;

namespace Server.Api.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase {

        private readonly IRepository _repository;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IRepository repository, ILogger<CustomerController> logger) {
            this._repository = repository;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<Customer> GetCustomer([FromQuery, Required] string FirstName, [FromQuery, Required] string LastName) {
            
            Customer customer;
            try {
                customer = _repository.LoadCustomer(FirstName, LastName);
            } catch (Exception ex) {
                Console.WriteLine("Server Error with CustomerController");
                return StatusCode(500);
            }

            return customer;
            
        }

        [HttpGet("doesexist")]
        public ActionResult<bool> DoesExist([FromQuery, Required] string FirstName, [FromQuery, Required] string LastName) {
            Customer customer;
            Console.WriteLine("1");
            try {
                Console.WriteLine("2");
                customer = _repository.LoadCustomer(FirstName, LastName);
                Console.WriteLine("3");
                if (customer != null) {
                    return true;
                } else {
                    return false;
                }
            } catch (Exception ex) {
                Console.WriteLine("Server Error with doesexist in CustomerController");
                return StatusCode(500);
            }
        } 

        [HttpPost]
        public IActionResult PostNewCustomer([FromQuery, Required] string FirstName, [FromQuery, Required] string LastName, [FromQuery, Required] string password, [FromQuery, Required] int storeId) {
            Console.WriteLine("in post");
            Store store = new Store();
            store.Id = storeId;

            Customer customer = new Customer();
            customer.FirstName = FirstName;
            customer.LastName = LastName;
            customer.Password = password;
            customer.Store = store;

            try {
                Console.WriteLine("in post 1");
                _repository.SaveNewPerson(FirstName, LastName, password, storeId);
                Console.WriteLine("in post 2");
                return StatusCode(200);
            } catch (Exception ex) {
                Console.WriteLine("Server error in Post new Customer");
                return StatusCode(500);
            }
        }
    }
}
