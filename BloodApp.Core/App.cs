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
		private const string ServerUri = "https://giveblood.azurewebsites.net/";

		public App()
		{
			// todo: setup IoC
			Mvx.RegisterType<IBloodDonationService, BloodDonationService>();
			Mvx.RegisterType<IBloodDemandService, BloodDemandService>();
			Mvx.RegisterType<IUserService, UserService>();

			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<RegisterViewModel>());

			var client = new MobileServiceClient(new Uri(App.ServerUri));
			Mvx.RegisterSingleton<IMobileServiceClient>(client);

		}
	}
}