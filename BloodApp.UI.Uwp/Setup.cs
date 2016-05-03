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
		}
	}
}