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
	public class BloodDemandDetailViewModel : BaseViewModel
	{
		private readonly Lazy<IBloodDemandService> _bloodDemandrService;
		private string _bloodDemandId;
		private IMvxCommand _goToEditCommand;
		private IMvxCommand _deleteCommand;

		private BloodDemand _bloodDemand;

		public BloodDemandDetailViewModel()
		{
			this._bloodDemandrService = new Lazy<IBloodDemandService>(Mvx.Resolve<IBloodDemandService>);
		}

		public void Init(string id)
		{
			this._bloodDemandId = id;
		}

		public override async void Start()
		{
			base.Start();
			await this.LoadData();
		}

		protected async Task LoadData()
		{
			if (string.IsNullOrEmpty(this._bloodDemandId)) {
				return;
			}

			try {
				this.IsLoading = true;
				this.BloodDemand = await this._bloodDemandrService.Value.GetBloodDemandAsync(this._bloodDemandId);
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

		public BloodType? BloodGroup => this._bloodDemand?.BloodGroup;

		public string Date
		{
			get
			{
				if (this._bloodDemand?.CreatedAt != null) {
					return this._bloodDemand.CreatedAt.ToString("f");
				}

				return string.Empty;
			}
		}

		public bool IsUserOwner
		{
			get
			{
				var userService = Mvx.Resolve<IUserService>();
				return this.BloodDemand?.PublisherdId == userService.GetIdOfLoggedUser();
			}
		}

		public IMvxCommand GoToEditCommand
		{
			get
			{
				if (this._goToEditCommand == null) {
					this._goToEditCommand = new MvxCommand(() =>
					{
						this.ShowViewModel<BloodDemandEditViewModel>(new { demandId = this.BloodDemand.Id });
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
							this._bloodDemandrService.Value.RemoveBloodDemandAsync(this.BloodDemand);
							this.Close(this);
							// todo: add undo dialog
						} catch (ServiceException) {
							//todo: ohlasit chybu
						}
					});
				}

				return this._deleteCommand;
			}
		}
		
	}
}