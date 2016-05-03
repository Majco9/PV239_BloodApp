using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BloodApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class BloodDonationListViewModel : MvxViewModel
	{

		private ObservableCollection<BloodDonationListItemViewModel> _donationsCollection;
		private readonly Lazy<IBloodDonationService> _eventService;


		public ObservableCollection<BloodDonationListItemViewModel> DonationsCollection
		{
			get { return this._donationsCollection; }
			set
			{
				this._donationsCollection = value;
				this.RaisePropertyChanged();
			}
		}

		public BloodDonationListViewModel()
		{
			this._eventService = new Lazy<IBloodDonationService>(Mvx.Resolve<IBloodDonationService>);
			this._donationsCollection = new ObservableCollection<BloodDonationListItemViewModel>();
		}


		protected async Task LoadData()
		{
			var events = await this._eventService.Value.ListAllBloodDonationsAsync();
			this.DonationsCollection = new ObservableCollection<BloodDonationListItemViewModel>(events.Select(e => new BloodDonationListItemViewModel(e)));
		}

		public override async void Start()
		{
			base.Start();
			await this.LoadData();
		}
	}
}