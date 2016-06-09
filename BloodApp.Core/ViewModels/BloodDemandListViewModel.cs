using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.Settings;
using Acr.UserDialogs;
using BloodApp.Core.Model;
using BloodApp.Core.Services;
using BloodApp.Core.Services.Exceptions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class BloodDemandListViewModel : BaseViewModel
	{
		private ObservableCollection<BloodDemandListItemViewModel> _bloodDemands;
		private readonly Lazy<IBloodDemandService> _demandService;

		public BloodDemandListViewModel()
		{
			this._bloodDemands = new ObservableCollection<BloodDemandListItemViewModel>();
			this._demandService = new Lazy<IBloodDemandService>(Mvx.Resolve<IBloodDemandService>);
		}

		public ObservableCollection<BloodDemandListItemViewModel> BloodDemands
		{
			get { return this._bloodDemands; }
			set
			{
				this._bloodDemands = value;
				this.RaisePropertyChanged();
			}
		}

		private BloodDemandListItemViewModel _selectedBloodDemand;
		public BloodDemandListItemViewModel SelectedBloodDemand
		{
			get { return this._selectedBloodDemand; }
			set
			{
				this._selectedBloodDemand = value;
				if (this._selectedBloodDemand != null) {
					this.ShowViewModel<BloodDemandDetailViewModel>(new { id = this._selectedBloodDemand.Id });
				}
			}
		}

		private bool _showOnlyMyBloodGroups;

		public bool ShowOnlyMyBloodGroups
		{
			get { return this._showOnlyMyBloodGroups; }
			set
			{
				this._showOnlyMyBloodGroups = value;
				this.RaisePropertyChanged();
#pragma warning disable 4014
				this.LoadData();
#pragma warning restore 4014
			}
		}

		private bool _showOnlyMyDemands;

		public bool ShowOnlyMyDemands
		{
			get { return this._showOnlyMyDemands; }
			set
			{
				this._showOnlyMyDemands = value;
				this.RaisePropertyChanged();

#pragma warning disable 4014
				this.LoadData();
#pragma warning restore 4014
			}
		}

		protected async Task LoadData()
		{
			this.IsLoading = true;
			try {
				BloodType? bloodTypeToFilter = null;
				if (this.ShowOnlyMyBloodGroups) {
					var settings = Mvx.Resolve<ISettings>();
					bloodTypeToFilter = settings.Get<BloodType?>("userBloodGroup");
				}

				var demands = await this._demandService.Value.ListAllBloodDemandsAsync(bloodTypeToFilter, this.ShowOnlyMyDemands);
				this.BloodDemands = new ObservableCollection<BloodDemandListItemViewModel>(demands
						.Select(e => new BloodDemandListItemViewModel(e)));
			} catch (ServiceException) {
				var userDialogs = Mvx.Resolve<IUserDialogs>();
				var alertConfig = new AlertConfig
				{
					Title = "Error",
					Message = "Error while loading data!"
				};
				userDialogs.Alert(alertConfig);
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