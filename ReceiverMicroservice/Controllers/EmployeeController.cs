using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReceiverMicroservice.Model;
using ReceiverMicroservice.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReceiverMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeQueueReaderService employeeQueueReaderService;

        public EmployeeController(IEmployeeQueueReaderService employeeQueueReaderService)
        {
this.employeeQueueReaderService = employeeQueueReaderService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await employeeQueueReaderService.ReadQueue();
        }
    }
}

