using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MvvmCross.WindowsUWP.Views;

namespace BloodApp.UI.Uwp.Views
{
	public class BaseWinPage : MvxWindowsPage
	{
		protected virtual bool ForceHideBackButton { get; set; }

		protected BaseWinPage()
		{
		}

		protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			Frame rootFrame = Window.Current.Content as Frame;

			if (rootFrame != null && rootFrame.CanGoBack && !this.ForceHideBackButton) {
				SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
			} else {
				SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
			}
		}
	}
}