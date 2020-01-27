using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POSTGRES_NET_Core_Example.Database;
using POSTGRES_NET_Core_Example.Models;

namespace POSTGRES_NET_Core_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        [HttpPost]
        public async Task<IActionResult> Craate(Customer payload)
        {
            var result = await _customerRepository.CreateAsync(payload.FirstName, payload.LastName);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _customerRepository.GetAsync(id);
            return Ok(result);
        }
    }
}
