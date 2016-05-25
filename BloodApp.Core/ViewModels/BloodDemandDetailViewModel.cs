using System;
using System.Threading.Tasks;
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
			} catch (ServiceException ex) {
				// todo: handle it
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
		
	}
}