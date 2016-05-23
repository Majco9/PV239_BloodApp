using Acr.UserDialogs;
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

		private IMvxCommand _goToAddDemandCommand;
		private IMvxCommand _goToAddDonationCommand;

		public IMvxCommand GoToAddDonationCommand
		{
			get
			{
				if (this._goToAddDonationCommand == null) {
					this._goToAddDonationCommand = new MvxCommand(() =>
					{
						this.ShowViewModel<BloodDonationEditViewModel>();
					});
				}

				return this._goToAddDonationCommand;
			}
		}

		public IMvxCommand GoToAddDemandCommand
		{
			get
			{
				if (this._goToAddDemandCommand == null) {
					this._goToAddDemandCommand = new MvxCommand(() =>
					{
						this.ShowViewModel<BloodDemandEditViewModel>();
					});
				}

				return this._goToAddDemandCommand;
			}
		}
	}
}