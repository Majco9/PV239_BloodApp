using Acr.UserDialogs;
using BloodApp.Core.Services;
using BloodApp.Core.Utils;
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

						if (this.ValidForm()) {

							var loginService = Mvx.Resolve<IUserService>();
							var loginResult = await loginService.AuthenticateAsync(this._email, this._password);
							if (loginResult) {
								this.ShowViewModel<HomeViewModel>();
							} else {
								var alertConfig = new AlertConfig
								{
									Title = "Error",
									Message = "Sorry, that login was invalid. Please try again."
								};
								userDialogs.Alert(alertConfig);
							}

						} else {
							userDialogs.Alert("Some field is empty.", "Error");
						}
						this.IsLoading = false;
					});
				}

				return this._loginCommand;
			}
		}

		private bool ValidForm()
		{
			return !string.IsNullOrEmpty(this.Email) && !string.IsNullOrEmpty(this.Password);
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

		public override async void Start()
		{
			base.Start();

			this.IsLoading = true;

			var userService = Mvx.Resolve<IUserService>();
			var isValidToken = await userService.CheckForValidTokenAsync();

			this.IsLoading = false;

			if (isValidToken) {
				this.ShowViewModel<HomeViewModel>();
			}
		}
	}
}