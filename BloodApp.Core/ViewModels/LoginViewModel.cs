using Acr.UserDialogs;
using BloodApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private string _email;

		public string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				this._email = value;
				this.RaisePropertyChanged();
			}
		}

		private string _password;

		public string Password
		{
			get { return this._password; }
			set
			{
				this._password = value;
				this.RaisePropertyChanged();
			}
		}

		private IMvxCommand _loginCommand;
		private IMvxCommand _goToRegisterCommand;

		public IMvxCommand LoginCommand
		{
			get
			{
				if (this._loginCommand == null) {
					this._loginCommand = new MvxCommand(async () =>
					{
						this.IsLoading = true;
						var userDialogs = Mvx.Resolve<IUserDialogs>();

						var loginService = Mvx.Resolve<IUserService>();
						var loginResult = await loginService.AuthenticateAsync(this._email, this._password);
						if (loginResult) {
							this.ShowViewModel<HomeViewModel>();
						} else {
							//todo: show dialog with error
							var alertConfig = new AlertConfig
							{
								Title = "Chyba",
								Message = "Prihlasenie bolo neuspesne!"
							};
							userDialogs.Alert(alertConfig);
						}
						this.IsLoading = false;
					});
				}

				return this._loginCommand;
			}
		}

		public IMvxCommand GoToRegisterCommand
		{
			get
			{
				if (this._goToRegisterCommand == null) {
					this._goToRegisterCommand = new MvxCommand(() =>
					{
						this.ShowViewModel<RegisterViewModel>();
					});
				}

				return this._goToRegisterCommand;
			}
		}
		
	}
}