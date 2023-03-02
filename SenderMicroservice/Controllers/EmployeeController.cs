using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SenderMicroservice.Model;
using SenderMicroservice.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SenderMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IQueueService queueService;

        public EmployeeController(IQueueService _queueService)
        {
            queueService = _queueService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(Employee emp)
        {
            await queueService.SendMessageAsync<Employee>("employeequeue", emp);
            return Ok();
        }

 
    }
}

