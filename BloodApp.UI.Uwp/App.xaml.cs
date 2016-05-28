using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BloodApp.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.WindowsUWP.Views;

namespace BloodApp.UI.Uwp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

		/// <summary>
		/// Names of view models, that back button should "exit" app
		/// </summary>
		private readonly List<string> _rootPages = new List<string>
		{
			nameof(HomeViewModel), nameof(BloodDonationListViewModel),
			nameof(BloodDemandListViewModel), nameof(LoginViewModel),
			nameof(RegisterViewModel)
		};

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = false;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;

				// Title bar button colors
	            var primaryColor = Color.FromArgb(255, 239, 35, 88);
				ApplicationView.GetForCurrentView().TitleBar.ButtonForegroundColor = primaryColor;
				ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.White;
				ApplicationView.GetForCurrentView().TitleBar.ButtonHoverBackgroundColor = primaryColor;
				ApplicationView.GetForCurrentView().TitleBar.ButtonHoverForegroundColor = Colors.White;
	            ApplicationView.GetForCurrentView().TitleBar.ButtonPressedBackgroundColor = primaryColor;
				ApplicationView.GetForCurrentView().TitleBar.ButtonPressedForegroundColor = Colors.White;

				SystemNavigationManager.GetForCurrentView().BackRequested += (sender, args) =>
				{
					// Hardware back button hanling
					if (this._rootPages.Contains(BaseViewModel.ActiveViewModel.GetType().Name)) {
						args.Handled = false;
					} else {
						args.Handled = true;
					}

					BaseViewModel.ActiveViewModel.BackCommand.Execute();
				};
				
			}

            if (rootFrame.Content == null)
            {
               var setup = new Setup(rootFrame);
				setup.Initialize();

	            var start = Mvx.Resolve<IMvxAppStart>();
				start.Start();
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

	    private void OnBackRequested(object sender, BackRequestedEventArgs e)
	    {
		    
	    }

	    /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
