using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongRunningTask.Messsages;
using Xamarin.Forms;

namespace LongRunningTask
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			MessagingCenter.Subscribe<Application, int>(Application.Current, "UpdateUICounter", (sender, arg) =>
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					this.labelCounter.Text = "Counter " + arg.ToString();
				});
			});
		}

		/// <summary>
		/// StartClicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void StartClicked(System.Object sender, System.EventArgs e)
		{
			var message = new StartLongRunningTaskMessage();
			MessagingCenter.Send(message, "StartLongRunningTimerMessage");

		}

		/// <summary>
		/// StopClicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void StopClicked(System.Object sender, System.EventArgs e)
		{
			var message = new StopLongRunningTaskMessage();
			MessagingCenter.Send(message, "StopLongRunningTimerMessage");
		}
	}
}
