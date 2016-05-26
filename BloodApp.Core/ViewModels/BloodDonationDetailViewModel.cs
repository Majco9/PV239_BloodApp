using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BloodApp.Core.Model;
using BloodApp.Core.Services;
using BloodApp.Core.Services.Exceptions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class BloodDonationDetailViewModel : BaseViewModel
	{
		private readonly Lazy<IBloodDonationService> _donationService;
		private string _donationId;
		private IMvxCommand _goToEditCommand;
		private IMvxCommand _deleteCommand;

		private BloodDonation _bloodDonation;

		public BloodDonationDetailViewModel()
		{
			this._donationService = new Lazy<IBloodDonationService>(Mvx.Resolve<IBloodDonationService>);
		}

		public void Init(string id)
		{
			this._donationId = id;
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

		public override async void Start()
		{
			base.Start();
			await this.LoadData();
		}

		protected async Task LoadData()
		{
			if (string.IsNullOrEmpty(this._donationId)) {
				return;
			}

			try {
				this.IsLoading = true;
				this.BloodDonation = await this._donationService.Value.GetBloodDonationAsync(this._donationId);
			} catch (ServiceException) {
				var userDialogs = Mvx.Resolve<IUserDialogs>();
				var alertConfig = new AlertConfig
				{
					Title = "Error",
					Message = "Erro while loading donation event!"
				};
				userDialogs.Alert(alertConfig);
				this.Close(this);
			}

			this.IsLoading = false;
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

		public bool IsUserOwner
		{
			get
			{
				var userService = Mvx.Resolve<IUserService>();
				return this.BloodDonation?.DonorId == userService.GetIdOfLoggedUser();
			}
		}

		public IMvxCommand GoToEditCommand
		{
			get
			{
				if (this._goToEditCommand == null) {
					this._goToEditCommand = new MvxCommand(() =>
					{
						this.ShowViewModel<BloodDonationEditViewModel>(new { donationId = this.BloodDonation.Id });
					});
				}

				return this._goToEditCommand;
			}
		}

		public IMvxCommand DeleteCommand
		{
			get
			{
				if (this._deleteCommand == null) {
					this._deleteCommand = new MvxCommand(() =>
					{
						try {
							this._donationService.Value.RemoveBloodDonationAsync(this.BloodDonation);
							this.Close(this);
							// todo: add undo dialog
						} catch (ServiceException) {
							var userDialogs = Mvx.Resolve<IUserDialogs>();
							var alertConfig = new AlertConfig
							{
								Title = "Error",
								Message = "Error while removing donation event!"
							};
							userDialogs.Alert(alertConfig);
						}
					});
				}

				return this._deleteCommand;
			}
		}
	}
}