using System;
using BloodApp.Core.Services;
using BloodApp.Core.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core
{
	public class App : MvxApplication
	{
		private const string ServerUri = "http://giveblood.azurewebsites.net/";

		private const string TestToken =
			"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxZWVmZTQ2My1hMTI0LTQzNmQtYmZhNy0zMDM3N2ZmMzZjNjEiLCJ2ZXIiOiIzIiwiaXNzIjoiaHR0cHM6Ly9naXZlYmxvb2QuYXp1cmV3ZWJzaXRlcy5uZXQvIiwiYXVkIjoiaHR0cHM6Ly9naXZlYmxvb2QuYXp1cmV3ZWJzaXRlcy5uZXQvIiwiZXhwIjoxNDYyMzkyMTgzLCJuYmYiOjE0NjIzMDU3ODN9.mhdaBadfuaKdEmitX13a-Y7dnf8-sS_H4gyi1lzUhuI";

		private const string TestUserId = "1eefe463-a124-436d-bfa7-30377ff36c61";

		public App()
		{
			// todo: setup IoC
			Mvx.RegisterType<IBloodDonationService, BloodDonationService>();
			Mvx.RegisterType<IBloodDemandService, BloodDemandService>();
			Mvx.RegisterType<IUserService, UserService>();

			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<BloodDonationListViewModel>());

			var client = new MobileServiceClient(new Uri(App.ServerUri));
#if DEBUG
			client.CurrentUser = new MobileServiceUser(App.TestUserId);
			client.CurrentUser.MobileServiceAuthenticationToken = App.TestToken;
#endif
			Mvx.RegisterSingleton<IMobileServiceClient>(client);

		}
	}
}