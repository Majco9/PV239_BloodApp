using System;
using System.Threading.Tasks;
using BloodApp.Core.Model;
using BloodApp.Core.Services;
using BloodApp.Core.Services.Exceptions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class BloodDemandDetailViewModel : MvxViewModel
	{
		private readonly Lazy<IBloodDemandService> _bloodDemandrService;
		private readonly string _bloodDemandId;

		private BloodDemand _bloodDemand;

		public BloodDemandDetailViewModel(string id)
		{
			this._bloodDemandId = id;
			this._bloodDemandrService = new Lazy<IBloodDemandService>(Mvx.Resolve<IBloodDemandService>);
		}

		protected async Task LoadData()
		{
			if (!string.IsNullOrEmpty(this._bloodDemandId)) {
				return;
			}

			try {
				this.BloodDemand = await this._bloodDemandrService.Value.GetBloodDemandAsync(this._bloodDemandId);
			} catch (ServiceException ex) {
				// todo: handle it
			}
			
		}

		public BloodDemand BloodDemand
		{
			get { return this._bloodDemand; }
			set
			{
				this._bloodDemand = value;
				this.RaiseAllPropertiesChanged();
			}
		}

		public string Description => this._bloodDemand?.Description;

		public string Instructions => this._bloodDemand?.Instructions;

		public BloodType? BloodGroup => this._bloodDemand.BloodGroup;
		
	}
}