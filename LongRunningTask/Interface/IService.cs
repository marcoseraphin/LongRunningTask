using System;
using System.Threading.Tasks;

namespace LongRunningTask.Interface
{
	public interface IService
	{
		Task<int> Add();
	}
}
