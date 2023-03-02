using System;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;
using ReceiverMicroservice.Model;

namespace ReceiverMicroservice.Service
{
	public class EmployeeQueueReaderService : IEmployeeQueueReaderService
	{
        private readonly IConfiguration configuration;
        private readonly QueueClient queueClient;
        List<Employee> employees = new List<Employee>();

        public EmployeeQueueReaderService(IConfiguration _configuration)
		{
            configuration = _configuration;
            queueClient = new QueueClient(configuration.GetConnectionString("EmployeeQueue"), "EmployeeQueue");

        }

        public async Task<IEnumerable<Employee>> ReadQueue()
        {
            var handler = new MessageHandlerOptions(ExceptionReceivedHandler) {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(ProcessMessageHandler, handler);
            await queueClient.CloseAsync();
            return employees;
        }

        private async Task ProcessMessageHandler(Message arg1, CancellationToken arg2)
        {
            var data = Encoding.UTF8.GetString(arg1.Body);
            var result = JsonSerializer.Deserialize<Employee>(data);
            employees.Add(result);
            await queueClient.CompleteAsync(arg1.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            return Task.CompletedTask;
        }
    }
}

