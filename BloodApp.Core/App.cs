using BloodApp.Core.Services;
using BloodApp.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core
{
	public class App : MvxApplication
	{
		public App()
		{
			// todo: setup IoC
			Mvx.RegisterType<IEventService, TestEventService>();
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<LoginViewModel>());
		}
	}
}