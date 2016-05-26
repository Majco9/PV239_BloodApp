using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BloodApp.Core.Services;
using BloodApp.Core.Services.Exceptions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class BloodDonationListViewModel : BaseViewModel
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

		private BloodDonationListItemViewModel _selectedBloodDonation;

		public BloodDonationListItemViewModel SelectedBloodDonation
		{
			get { return this._selectedBloodDonation; }
			set
			{
				this._selectedBloodDonation = value;
				this.ShowViewModel<BloodDonationDetailViewModel>(new { id = this._selectedBloodDonation.Id });
			}
		}


		public BloodDonationListViewModel()
		{
			this._eventService = new Lazy<IBloodDonationService>(Mvx.Resolve<IBloodDonationService>);
			this._donationsCollection = new ObservableCollection<BloodDonationListItemViewModel>();
		}


		protected async Task LoadData()
		{
			this.IsLoading = true;
			try {
				var events = await this._eventService.Value.ListAllBloodDonationsAsync();
				this.DonationsCollection = new ObservableCollection<BloodDonationListItemViewModel>(events
						.Select(e => new BloodDonationListItemViewModel(e)));
			} catch (ServiceException ex) {
				//todo: handle it (some toast)
				var userDialogs = Mvx.Resolve<IUserDialogs>();
				var alertConfig = new AlertConfig
				{
					Title = "Error",
					Message = "Login was unsuccessful!"
				};
				userDialogs.Alert(alertConfig);
				Debug.WriteLine("Error while loading data..message: {0}", ex.Message);
			}
			this.IsLoading = false;
		}

		public override async void Start()
		{
			base.Start();
			await this.LoadData();
		}
	}
}