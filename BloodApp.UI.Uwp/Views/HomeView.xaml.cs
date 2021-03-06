﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BloodApp.Core.ViewModels;
using MvvmCross.WindowsUWP.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BloodApp.UI.Uwp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class HomeView : BaseWinPage
	{
		protected override bool ForceHideBackButton { get { return true; } }

		/// <summary>
		/// Flag, that forces reloading data, when user come back to home page
		/// </summary>
		private bool _reloadData = false;

		public HomeView()
		{
			this.InitializeComponent();
			this.NavigationCacheMode = NavigationCacheMode.Required;


		}
		

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			if (this._reloadData && this.DataContext != null) {
				((BaseViewModel)this.DataContext).Start();
				this._reloadData = false;
			}
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);

			this._reloadData = true;

			this.DemandsList.SelectedItem = null;
			this.DonationsList.SelectedItem = null;
		}

		private void PagePivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedPivotItem = e.AddedItems[0];

			// update filter menu flyout
			if (selectedPivotItem.Equals(this.DemandsPage)) {
				this.DemandsForMeFilterMenuItem.Visibility = Visibility.Visible;
				this.MyDemandsFilterMenuItem.Visibility = Visibility.Visible;

				this.MyDonationFilterMenuItem.Visibility = Visibility.Collapsed;
				this.PastEventsFilterMenuItem.Visibility = Visibility.Collapsed;
			} else {
				this.MyDonationFilterMenuItem.Visibility = Visibility.Visible;
				this.PastEventsFilterMenuItem.Visibility = Visibility.Visible;

				this.DemandsForMeFilterMenuItem.Visibility = Visibility.Collapsed;
				this.MyDemandsFilterMenuItem.Visibility = Visibility.Collapsed;
			}
		}
	}
}
