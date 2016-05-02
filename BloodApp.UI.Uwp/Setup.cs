using System;
using Windows.UI.Xaml.Controls;
using BloodApp.Core.Services;
using BloodApp.UI.Uwp.Services;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.WindowsUWP.Platform;
using MvvmCross.WindowsUWP.Views;

namespace BloodApp.UI.Uwp
{
	public class Setup : MvxWindowsSetup
	{

		private const string ServerUri = "http://giveblood.azurewebsites.net/";

		//private const string TestToken =
		//	"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxZWVmZTQ2My1hMTI0LTQzNmQtYmZhNy0zMDM3N2ZmMzZjNjEiLCJ2ZXIiOiIzIiwiaXNzIjoiaHR0cHM6Ly9naXZlYmxvb2QuYXp1cmV3ZWJzaXRlcy5uZXQvIiwiYXVkIjoiaHR0cHM6Ly9naXZlYmxvb2QuYXp1cmV3ZWJzaXRlcy5uZXQvIiwiZXhwIjoxNDYyMzEwMzMyLCJuYmYiOjE0NjIyMjM5MzJ9._Rdylm8USFuIZwASdOv86F4PGqTQIhsAeMHEo-fajqU.JwfYgfN2THP1eHqdiPDvS1R3bPepdpinab9fSYIjykk.c74DJ_0bV8QCITQXxD6WBzlsrzodrAcT91rtwF1OQiY";

		//private const string TestUserId = "1eefe463-a124-436d-bfa7-30377ff36c61";

		public Setup(Frame rootFrame, string suspensionManagerSessionStateKey = null) : base(rootFrame, suspensionManagerSessionStateKey)
		{
		}

		public Setup(IMvxWindowsFrame rootFrame) : base(rootFrame)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new Core.App();
		}

		protected override void InitializeIoC()
		{
			base.InitializeIoC();

			// define own services
			Mvx.RegisterType<IUserService, UserService>();

			var client = new MobileServiceClient(new Uri(Setup.ServerUri));
//#if DEBUG
//			client.CurrentUser = new MobileServiceUser(Setup.TestUserId);
//			client.CurrentUser.MobileServiceAuthenticationToken = Setup.TestToken;
//#endif
			Mvx.RegisterSingleton<IMobileServiceClient>(client);
		}
	}
}