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
	public class BloodDonationEditViewModel : BaseViewModel
	{
		private string _donationId;
		private readonly Lazy<IBloodDonationService> _donationService;
		private EditMode _editMode;

		private BloodDonation _bloodDonation;
		private IMvxCommand _saveCommand;
		private DateTimeOffset? _dateTimeOffset;
		private TimeSpan? _timeSpan;

		public BloodDonationEditViewModel()
		{
			this._donationService = new Lazy<IBloodDonationService>(Mvx.Resolve<IBloodDonationService>);
		}

		public void Init(string donationId = null)
		{
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
				if (this._bloodDonation.Date != null) {
					this._dateTimeOffset = new DateTimeOffset(this._bloodDonation.Date.Value);
					this._timeSpan = new TimeSpan(this._bloodDonation.Date.Value.Hour, this._bloodDonation.Date.Value.Minute,
						this._bloodDonation.Date.Value.Second);
				}

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

		public DateTimeOffset? Date
		{
			get { return this._dateTimeOffset; }
			set
			{
				this._dateTimeOffset = value;
				this.RaisePropertyChanged();
			}
		}

		public TimeSpan? Time
		{
			get { return this._timeSpan; }
			set
			{
				this._timeSpan = value;
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
						if (!this.ValidateForm()) {
							// todo: handle it
						}

						//todo: save date and time
							this.BloodDonation.Date = this.PrepareTimeAndDate();

							try {
								if (this._editMode == EditMode.Creating) {
									await this._donationService.Value.CreateBloodDonationAsync(this.BloodDonation);
								} else {
									await this._donationService.Value.UpdateBloodDonationAsync(this.BloodDonation);
								}

								this.Close(this);
							} catch (ServiceException) {
								//todo: handle it
								var userDialogs = Mvx.Resolve<IUserDialogs>();
								userDialogs.Alert(new AlertConfig {Title = "Error", Message = "Error while creating new donation"});
							}
						
					});
				}

				return this._saveCommand;
			}
		}

		private bool ValidateForm()
		{
			return true;
		}

		private DateTime? PrepareTimeAndDate()
		{
			if (this._dateTimeOffset == null || this._timeSpan == null) {
				return null;
			}

			return new DateTime(this._dateTimeOffset.Value.Year, this._dateTimeOffset.Value.Month,
				this._dateTimeOffset.Value.Day, this._timeSpan.Value.Hours, this._timeSpan.Value.Minutes, this._timeSpan.Value.Seconds);
		}

		public bool IsCreatingNewDonation
		{
			get { return this._editMode == EditMode.Creating; }
		}

		public bool IsModifyingDonation
		{
			get { return this._editMode == EditMode.Modifying; }
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