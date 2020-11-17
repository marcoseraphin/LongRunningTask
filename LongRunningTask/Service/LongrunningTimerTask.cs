using System;
using System.Threading;
using System.Threading.Tasks;
using LongRunningTask.Interface;
using Xamarin.Forms;

namespace LongRunningTask.Service
{
	public class LongrunningTimerTask
	{
		private IService addService = null;

		public LongrunningTimerTask(IService addService)
		{
			this.addService = addService;
		}

		public async Task CounterAdd(CancellationToken token)
		{
			await Task.Run(async () =>
			{

				do
				{
					token.ThrowIfCancellationRequested();

					int uiCounter = 0;

					if (this.addService != null)
					{
						uiCounter = await this.addService.Add();

						Device.BeginInvokeOnMainThread(() =>
						{
							MessagingCenter.Send<Application, int>(Application.Current, "UpdateUICounter", uiCounter);
						});	
					}
				} while (true);
			}, token);
		}
	}
}
