using System;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;

namespace SenderMicroservice.Service
{
	public class QueueService : IQueueService
	{
        private readonly IConfiguration configuration;

        public QueueService(IConfiguration _configuration)
		{
            configuration = _configuration;
        }

        public async Task SendMessageAsync<T>(string queueName, T serviceBusMessage)
        {
            QueueClient queueClient = new QueueClient(configuration.GetConnectionString("EmployeeQueue"), queueName);
            string message = JsonSerializer.Serialize(serviceBusMessage);
            Message m = new Message(Encoding.UTF8.GetBytes(message));
            await queueClient.SendAsync(m);
        }
	}
}

