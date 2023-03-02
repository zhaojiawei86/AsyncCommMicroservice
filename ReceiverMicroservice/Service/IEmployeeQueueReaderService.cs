using System;
using ReceiverMicroservice.Model;

namespace ReceiverMicroservice.Service
{
	public interface IEmployeeQueueReaderService
	{
		public Task<IEnumerable<Employee>> ReadQueue();
	}
}

