using System;
using LongRunningTask.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LongRunningTask
{
	public partial class App : Application
	{
		public static CounterService counterService;

		public App()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			App.counterService = new CounterService();
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
