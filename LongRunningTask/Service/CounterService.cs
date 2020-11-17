using System;
using System.Threading.Tasks;
using LongRunningTask.Interface;

namespace LongRunningTask.Service
{
	public class CounterService : IService
	{
		private int counter = 0;

		public CounterService()
		{
		}

		public async Task<int> Add()
		{
			await Task.Delay(1000);
			
			counter = counter + 1;
			return counter;
		}
	}
}
