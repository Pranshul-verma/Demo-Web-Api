using DummyApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DummyApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmployeeDetailController : ControllerBase
    {
        private readonly ILogger<EmployeeDetail> _logger;
        private static List<EmployeeDetail>? EmployeeDetailsList;
        public EmployeeDetailController(ILogger<EmployeeDetail> logger)
        {
            _logger = logger;
            if (EmployeeDetailsList == null)
            {
                EmployeeDetailsList = new List<EmployeeDetail> { new EmployeeDetail { EmpId = 1, EmpName = "Pragya rathi", EmpEmail = "Pragya.com", Salary = 10000000 }
                };
            }
        }
        
        [ActionName("GetEmployeeDetail")]
        [HttpGet(Name = "GetEmployeeDetail")]
        public IActionResult Get()
        {
            if (EmployeeDetailsList?.Count == 0)
            {
                return NotFound("No data found.");
            }

            // Return all data in the collection
            return Ok(EmployeeDetailsList);
        }

        [ActionName("SetEmployeeDetail")]
        [HttpPost(Name = "SetEmployeeDetail")]
        public IActionResult SetEmployeeDetail([FromBody] EmployeeDetail empDtl)
        {

            if (empDtl == null)
            {
                return BadRequest("Data cannot be empty.");
            }

            // Add new data to the collection
            EmployeeDetailsList?.Add(empDtl);
            return Ok(EmployeeDetailsList);
        }
    }
}
