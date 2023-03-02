using System;
namespace SenderMicroservice.Service
{
	public interface IQueueService
	{
        Task SendMessageAsync<T>(string queueName, T serviceBusMessage);
    }
}

