using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using LongRunningTask.Messsages;
using LongRunningTask.Service;
using Xamarin.Forms;

namespace LongRunningTask.Droid.LongRunningTask
{
	[Service]
	public class DroidLongRunningTaskCounter : Android.App.Service
	{
		CancellationTokenSource _cts;

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			_cts = new CancellationTokenSource();

			Task.Run(() => {
				try
				{
					// INVOKE THE SHARED CODE
					var timerMainLoop = new LongrunningTimerTask(App.counterService);
					timerMainLoop.CounterAdd(_cts.Token).Wait();
				}
				catch (Android.OS.OperationCanceledException)
				{
				}
				finally
				{
					if (_cts.IsCancellationRequested)
					{
						var message = new CancelledMessage();
						Device.BeginInvokeOnMainThread(
							() => MessagingCenter.Send(message, "CancelledMessage")
						);
					}
				}

			}, _cts.Token);

			return StartCommandResult.Sticky;
		}

		public override void OnDestroy()
		{
			if (_cts != null)
			{
				_cts.Token.ThrowIfCancellationRequested();

				_cts.Cancel();
			}
			base.OnDestroy();
		}

		public DroidLongRunningTaskCounter()
		{
		}

	}
}
