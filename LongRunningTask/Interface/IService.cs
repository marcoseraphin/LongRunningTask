using System;
using System.Threading.Tasks;

namespace LongRunningTask.Interface
{
	/// <summary>
	/// Sample Counter service
	/// </summary>
	public interface IService
	{
		Task<int> Add();
	}
}
