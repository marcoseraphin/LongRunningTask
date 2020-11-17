using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using LongRunningTask.iOS.LongRunningTask;
using LongRunningTask.Messsages;
using UIKit;
using Xamarin.Forms;

namespace LongRunningTask.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		iOSLongRunningTaskCounter iOSLongRunningTaskCounterTask = null;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new App());

			this.WireUpLongRunningTimerTask();

			return base.FinishedLaunching(app, options);
		}

		/// <summary>
		/// WireUpLongRunningTimerTask
		/// </summary>
		void WireUpLongRunningTimerTask()
		{
			MessagingCenter.Subscribe<StartLongRunningTaskMessage>(this, "StartLongRunningTimerMessage", async message =>
			{
				if (iOSLongRunningTaskCounterTask == null)
				{
					iOSLongRunningTaskCounterTask = new iOSLongRunningTaskCounter();
					await iOSLongRunningTaskCounterTask.Start();
				}
				else
				{
					await iOSLongRunningTaskCounterTask.Start();
				}
			});

			MessagingCenter.Subscribe<StopLongRunningTaskMessage>(this, "StopLongRunningTimerMessage", message =>
			{
				iOSLongRunningTaskCounterTask.Stop();
			});
		}
	}
}
