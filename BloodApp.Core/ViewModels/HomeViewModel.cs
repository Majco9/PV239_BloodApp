﻿using MvvmCross.Core.ViewModels;

namespace BloodApp.Core.ViewModels
{
	public class HomeViewModel : MvxViewModel
	{
		public HomeViewModel()
		{
			this.BloodDonationListViewModel = new BloodDonationListViewModel();
			this.BloodDemandListViewModel = new BloodDemandListViewModel();
		}

		public BloodDemandListViewModel BloodDemandListViewModel { get; private set; }

		public BloodDonationListViewModel BloodDonationListViewModel { get; private set; }

		public override void Start()
		{
			base.Start();
			this.BloodDonationListViewModel.Start();
			this.BloodDemandListViewModel.Start();
		}
	}
}