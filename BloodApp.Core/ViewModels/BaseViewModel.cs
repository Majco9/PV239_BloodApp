using BloodApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{

		public static BaseViewModel ActiveViewModel;
		private IMvxCommand _backCommand;
		private bool _isLoading;

		public BaseViewModel Parent { get; set; }


		public IMvxCommand BackCommand
		{
			get
			{
				if (this._backCommand == null) {
					this._backCommand = new MvxCommand(() =>
					{
						this.Close(BaseViewModel.ActiveViewModel);
					});
				}
				return this._backCommand;
			}
		}

		public virtual bool IsLoading
		{
			get { return this._isLoading; }
			set
			{
				this._isLoading = value;
				this.RaisePropertyChanged();
			}
		}

		public override void Start()
		{
			base.Start();
			BaseViewModel.ActiveViewModel = this;
		}

		#region Navigation Commands

		private IMvxCommand _goToAddDemandCommand;
		private IMvxCommand _goToAddDonationCommand;
		private IMvxCommand _logoutCommand;

		public IMvxCommand LogoutCommand
		{
			get
			{
				if (this._logoutCommand == null) {
					this._logoutCommand = new MvxCommand(() =>
					{
						var userService = Mvx.Resolve<IUserService>();
						userService.LogoutUser();

						if (this.Parent != null) {
							this.Close(this.Parent);
						} else {
							this.Close(this);
						}
					});
				}

				return this._logoutCommand;
			}
		}

		public IMvxCommand GoToAddDonationCommand
		{
			get
			{
				if (this._goToAddDonationCommand == null) {
					this._goToAddDonationCommand = new MvxCommand(() =>
					{
						this.ShowViewModel<BloodDonationEditViewModel>();
					});
				}

				return this._goToAddDonationCommand;
			}
		}

		public IMvxCommand GoToAddDemandCommand
		{
			get
			{
				if (this._goToAddDemandCommand == null) {
					this._goToAddDemandCommand = new MvxCommand(() =>
					{
						this.ShowViewModel<BloodDemandEditViewModel>();
					});
				}

				return this._goToAddDemandCommand;
			}
		}

		#endregion
	}
}