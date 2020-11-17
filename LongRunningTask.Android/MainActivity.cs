using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using LongRunningTask.Messsages;
using Android.Content;
using LongRunningTask.Droid.LongRunningTask;

namespace LongRunningTask.Droid
{
	[Activity(Label = "LongRunningTask", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		private Intent backgroundIntent = null;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			LoadApplication(new App());

			this.WireUpLongRunningTimerTask();
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		/// <summary>
		/// WireUpLongRunningTimerTask
		/// </summary>
		private void WireUpLongRunningTimerTask()
		{
			MessagingCenter.Subscribe<StartLongRunningTaskMessage>(this, "StartLongRunningTimerMessage", message =>
			{
				if (this.backgroundIntent == null)
				{
					this.backgroundIntent = new Intent(this, typeof(DroidLongRunningTaskCounter));
					StartService(this.backgroundIntent);
				}
				else
				{
					StartService(this.backgroundIntent);
				}
			});

			MessagingCenter.Subscribe<StopLongRunningTaskMessage>(this, "StopLongRunningTimerMessage", message => {
				var intent = new Intent(this, typeof(DroidLongRunningTaskCounter));
				StopService(intent);
			});
		}
	}
}