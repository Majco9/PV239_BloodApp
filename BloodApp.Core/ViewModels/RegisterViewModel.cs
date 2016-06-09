using System;
using System.Collections.Generic;
using System.Linq;
using Acr.Settings;
using Acr.UserDialogs;
using BloodApp.Core.Model;
using BloodApp.Core.Model.Register;
using BloodApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class RegisterViewModel : BaseViewModel
	{
		private string _email;
		private string _name;
		private string _confirmPassword;
		private BloodType? _bloodGroup;
		private string _password;
		private IMvxCommand _registerCommand;

		public IMvxCommand RegisterCommand
		{
			get
			{
				if (this._registerCommand == null) {
					this._registerCommand = new MvxCommand(async () =>
					{

						var userDialogs = Mvx.Resolve<IUserDialogs>();
						this.IsLoading = true;
						if (this.ValidForm()) {

							var registration = new RegisterUserModel
							{
								// ReSharper disable once PossibleInvalidOperationException
								BloodGroup = this.BloodGroup.Value,
								Password = this.Password,
								PasswordVerify = this.ConfirmPassword,
								Email = this.Email,
								Name = this.Name
							};

							var userService = Mvx.Resolve<IUserService>();
							var registerResult = await userService.RegisterUserAsync(registration);

							if (registerResult) {
								
								var settings = Mvx.Resolve<ISettings>();
								settings.Set("NotFirstAppAppRun", true);

								await userService.AuthenticateAsync(this.Email, this.Password);
								this.ShowViewModel<HomeViewModel>();
							} else {
								userDialogs.Alert("Create account failed. Please try again.");
							}
						} else {
							if (this.Password != this.ConfirmPassword) {
								userDialogs.Alert("Password and password confirmation do not match.", "Error");
							} else {
								userDialogs.Alert("Some required information is missing or incomplete.", "Error");
							}
						}

						this.IsLoading = false;

					});
				}

				return this._registerCommand;
			}
		}

		private bool ValidForm()
		{
			return !string.IsNullOrEmpty(this.Email) && !string.IsNullOrEmpty(this.Password) && this.BloodGroup != null &&
					this.Password.Equals(this.ConfirmPassword);
		}

		public string Email
		{
			get { return this._email; }
			set
			{
				this._email = value;
				this.RaisePropertyChanged();
			}
		}

		public string Name
		{
			get { return this._name; }
			set
			{
				this._name = value;
				this.RaisePropertyChanged();
			}
		}

		public BloodType? BloodGroup
		{
			get { return this._bloodGroup; }
			set
			{
				this._bloodGroup = value;
				this.RaisePropertyChanged();
			}
		}

		public string Password
		{
			get { return this._password; }
			set
			{
				this._password = value;
				this.RaisePropertyChanged();
			}
		}

		public string ConfirmPassword
		{
			get { return this._confirmPassword; }
			set
			{
				this._confirmPassword = value;
				this.RaisePropertyChanged();
			}
		}

		public List<BloodType> BloodTypes
		{
			get { return Enum.GetValues(typeof(BloodType)).Cast<BloodType>().ToList(); }
		}

	}
}