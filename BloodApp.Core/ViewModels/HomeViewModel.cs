using Acr.UserDialogs;
using BloodApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class HomeViewModel : BaseViewModel
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

		public override bool IsLoading
		{
			get { return this.BloodDemandListViewModel.IsLoading || this.BloodDonationListViewModel.IsLoading; }
		}

		

	}
}