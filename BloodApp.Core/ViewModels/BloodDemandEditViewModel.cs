﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BloodApp.Core.Model;
using BloodApp.Core.Services;
using BloodApp.Core.Services.Exceptions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class BloodDemandEditViewModel : BaseViewModel
	{
		private string _demandId;
		private readonly Lazy<IBloodDemandService> _demandService;
		private EditMode _editMode;
		private BloodDemand _bloodDemand;
		private IMvxCommand _saveCommand;

		public BloodDemandEditViewModel()
		{
			this._demandService = new Lazy<IBloodDemandService>(Mvx.Resolve<IBloodDemandService>);

		}

		public void Init(string demandId = null)
		{
			if (string.IsNullOrEmpty(demandId)) {
				this._editMode = EditMode.Creating;
				this.BloodDemand = new BloodDemand { CreatedAt = DateTime.Now };
			} else {
				this._demandId = demandId;
				this._editMode = EditMode.Modifying;
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

		public IMvxCommand SaveCommand
		{
			get
			{
				if (this._saveCommand == null) {
					this._saveCommand = new MvxCommand(async () =>
					{
						var userDialogs = Mvx.Resolve<IUserDialogs>();
						if (this.ValidForm()) {
							try {
								if (this._editMode == EditMode.Creating) {
									await this._demandService.Value.CreateBloodDemandAsync(this.BloodDemand);
								} else {
									await this._demandService.Value.UpdateBloodDemandAsync(this.BloodDemand);
								}

								this.Close(this);
							} catch (ServiceException) {
								
								var alertConfig = new AlertConfig
								{
									Title = "Error",
									Message = "Error while creating new demand!"
								};
								userDialogs.Alert(alertConfig);
							}
						} else {
							userDialogs.Alert("Some required information is missing or incomplete.", "Error");
						}
					});
				}

				return this._saveCommand;
			}
		}

		private bool ValidForm()
		{
			return !string.IsNullOrEmpty(this.Description) && !string.IsNullOrEmpty(this.Instructions)
					&& this.BloodGroup != null;

		}

		public string Description
		{
			get { return this.BloodDemand?.Description; }
			set
			{
				if (this.BloodDemand == null) {
					return;
				}

				this.BloodDemand.Description = value;
				this.RaisePropertyChanged();
			}
		}

		public string Instructions
		{
			get { return this.BloodDemand?.Instructions; }
			set
			{
				if (this.BloodDemand == null) {
					return;
				}

				this.BloodDemand.Instructions = value;
				this.RaisePropertyChanged();
			}
		}

		public BloodType? BloodGroup
		{
			get { return this.BloodDemand?.BloodGroup; }
			set
			{
				if (this.BloodDemand == null) {
					return;
				}

				this.BloodDemand.BloodGroup = value;
				this.RaisePropertyChanged();
			}
		}

		public List<BloodType> BloodTypes
		{
			get { return Enum.GetValues(typeof(BloodType)).Cast<BloodType>().ToList(); }
		}

		public bool IsCreatingDemand
		{
			get {
				return this._editMode == EditMode.Creating;
			}
		}

		public bool IsModifyingDemand
		{
			get { return this._editMode == EditMode.Modifying; }
		}

		public override async void Start()
		{
			base.Start();
			if (this._editMode == EditMode.Creating) {
				var userService = Mvx.Resolve<IUserService>();
				this.BloodDemand.PublisherdId = userService.GetIdOfLoggedUser();
			} else {
				await this.LoadData();
			}
		}

		protected async Task LoadData()
		{
			try {
				this.BloodDemand = await this._demandService.Value.GetBloodDemandAsync(this._demandId);
			} catch (ServiceException ex) {
				// todo: handle it
			}
		}
	}
}