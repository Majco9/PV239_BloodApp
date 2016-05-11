using System;
using System.Threading.Tasks;
using BloodApp.Core.Model;
using BloodApp.Core.Services;
using BloodApp.Core.Services.Exceptions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class BloodDonationEditViewModel : BaseViewModel
	{
		private readonly string _donationId;
		private readonly Lazy<IBloodDonationService> _donationService;
		private readonly EditMode _editMode;

		private BloodDonation _bloodDonation;
		private IMvxCommand _saveCommand;

		public BloodDonationEditViewModel(string donationId = null)
		{
			this._donationService = new Lazy<IBloodDonationService>(Mvx.Resolve<IBloodDonationService>);
			if (string.IsNullOrEmpty(donationId)) {
				this._editMode = EditMode.Creating;
				this.BloodDonation = new BloodDonation { CreatedAt = DateTime.Now, Address = new Address() };
			} else {
				this._editMode = EditMode.Modifying;
				this._donationId = donationId;
			}
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

		public string OrganizatorName
		{
			get { return this.BloodDonation?.OrganizatorName; }
			set
			{
				if (this.BloodDonation == null) {
					return;
				}

				this.BloodDonation.OrganizatorName = value;
				this.RaisePropertyChanged();
			}
		}

		public string Name
		{
			get { return this.BloodDonation?.Name; }
			set
			{
				if (this.BloodDonation == null) {
					return;
				}

				this.BloodDonation.Name = value;
				this.RaisePropertyChanged();
			}
		}

		public string Description
		{
			get { return this.BloodDonation?.Description; }
			set
			{
				if (this.BloodDonation == null) {
					return;
				}

				this.BloodDonation.Description = value;
				this.RaisePropertyChanged();
			}
		}

		public DateTime? Date
		{
			get { return this.BloodDonation?.Date; }
			set
			{
				if (this.BloodDonation == null) {
					return;
				}

				this.BloodDonation.Date = value;
				this.RaisePropertyChanged();
			}
		}

		public string PlaceTitle
		{
			get { return this.BloodDonation?.Address?.Title; }
			set
			{
				if (this.BloodDonation?.Address == null) {
					return;
				}

				this.BloodDonation.Address.Title = value;
				this.RaisePropertyChanged();
			}
		}

		public string Street
		{
			get { return this.BloodDonation?.Address?.Street; }
			set
			{
				if (this.BloodDonation?.Address == null) {
					return;
				}

				this.BloodDonation.Address.Street = value;
				this.RaisePropertyChanged();
			}
		}

		public string City
		{
			get { return this.BloodDonation?.Address?.City; }
			set
			{
				if (this.BloodDonation?.Address == null) {
					return;
				}

				this.BloodDonation.Address.City = value;
				this.RaisePropertyChanged();
			}
		}

		public string State
		{
			get { return this.BloodDonation?.Address?.State; }
			set
			{
				if (this.BloodDonation?.Address == null) {
					return;
				}

				this.BloodDonation.Address.State = value;
				this.RaisePropertyChanged();
			}
		}

		public string ZipCode
		{
			get { return this.BloodDonation?.Address?.ZipCode; }
			set
			{
				if (this.BloodDonation?.Address == null) {
					return;
				}

				this.BloodDonation.Address.ZipCode = value;
				this.RaisePropertyChanged();
			}
		}

		public IMvxCommand SaveCommand
		{
			get
			{
				if (this._saveCommand == null) {
					this._saveCommand = new MvxCommand(async () =>
					{
						// todo: validate form

						try {
							if (this._editMode == EditMode.Creating) {
								await this._donationService.Value.CreateBloodDonationAsync(this.BloodDonation);
							} else {
								await this._donationService.Value.UpdateBloodDonationAsync(this.BloodDonation);
							}

							this.Close(this);
						} catch (ServiceException ex) {
							//todo: handle it
						}
					});
				}

				return this._saveCommand;
			}
		}

		protected async Task LoadData()
		{
			try {
				this.BloodDonation = await this._donationService.Value.GetBloodDonationAsync(this._donationId);
			} catch (ServiceException) {
				//todo: handle it
			}
		}

		public override async void Start()
		{
			base.Start();
			if (this._editMode == EditMode.Creating) {
				var userService = Mvx.Resolve<IUserService>();
				this.BloodDonation.DonorId = userService.GetIdOfLoggedUser();
			} else {
				await this.LoadData();
			}
		}
	}
}