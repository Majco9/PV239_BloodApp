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
			this.BloodDonationListViewModel = new BloodDonationListViewModel {Parent = this};
			this.BloodDemandListViewModel = new BloodDemandListViewModel {Parent =  this};
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

		public bool ShowOnlyMyBloodGroups
		{
			get { return this.BloodDemandListViewModel.ShowOnlyMyBloodGroups; }
			set
			{
				this.BloodDemandListViewModel.ShowOnlyMyBloodGroups = value;
				this.RaisePropertyChanged();
			}
		}

		public bool ShowOnlyMyDemands
		{
			get { return this.BloodDemandListViewModel.ShowOnlyMyDemands; }
			set
			{
				this.BloodDemandListViewModel.ShowOnlyMyDemands = value;
				this.RaisePropertyChanged();
			}
		}

		public bool ShowOnlyMyEvents
		{
			get { return this.BloodDonationListViewModel.ShowOnlyMyEvents; }
			set
			{
				this.BloodDonationListViewModel.ShowOnlyMyEvents = value;
				this.RaisePropertyChanged();
			}
		}

		public bool IncludePastEvents
		{
			get { return this.BloodDonationListViewModel.IncludePastEvents; }
			set
			{
				this.BloodDonationListViewModel.IncludePastEvents = value;
				this.RaisePropertyChanged();
			}
		}
		
	}
}