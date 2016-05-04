using System;
using System.Threading.Tasks;
using BloodApp.Core.Model;
using BloodApp.Core.Services;
using BloodApp.Core.Services.Exceptions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class BloodDonationDetailViewModel : MvxViewModel
	{
		private readonly Lazy<IBloodDonationService> _donationService;
		private readonly string _donationId;

		private BloodDonation _bloodDonation;

		public BloodDonationDetailViewModel(string donationId)
		{
			this._donationId = donationId;
			this._donationService = new Lazy<IBloodDonationService>(Mvx.Resolve<IBloodDonationService>);
		}

		public BloodDonation BloodDonation
		{
			get { return this._bloodDonation; }
			set
			{
				this._bloodDonation = value;
				this.RaiseAllPropertiesChanged();
			}
		}


		protected async Task LoadData()
		{
			try {
				this.BloodDonation = await this._donationService.Value.GetBloodDonationAsync(this._donationId);
			} catch (ServiceException ex) {
				//todo: handle it
			}
		}

		public string Name => this.BloodDonation?.Name;

		public string Description => this.BloodDonation?.Description;
		public string OrganizatorName => this.BloodDonation?.OrganizatorName;
		public string Address => this.BloodDonation?.Address.ToString();
		public string Date
		{
			get
			{
				if (this.BloodDonation?.Date != null) {
					return this.BloodDonation.Date.Value.ToString("f");
				}

				return string.Empty;
			}
		}
	}
}