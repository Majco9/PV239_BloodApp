﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BloodApp.Core.Model;
using BloodApp.Core.Services;
using BloodApp.Core.Services.Exceptions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class BloodDemandListViewModel : MvxViewModel
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

		protected async Task LoadData()
		{
			try {
				var demands = await this._demandService.Value.ListAllBloodDemandsAsync();
				this.BloodDemands = new ObservableCollection<BloodDemandListItemViewModel>(demands
						.Select(e => new BloodDemandListItemViewModel(e)));
			} catch (ServiceException ex) {
				// todo: handle it
			}
		}

		public override async void Start()
		{
			base.Start();
			await this.LoadData();
		}
	}
}