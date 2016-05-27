using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using Acr.Settings;
using BloodApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;
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

		public override void LoadPlugins(IMvxPluginManager pluginManager)
		{
			base.LoadPlugins(pluginManager);
			
			// -- Manually load plugins
			Mvx.RegisterSingleton(Settings.Local);

		}
	}
}