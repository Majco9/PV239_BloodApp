using System;
using System.Collections.Generic;
using System.Linq;
using Acr.Settings;
using BloodApp.Core.Model;
using BloodApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class RegisterViewModel : BaseViewModel
	{
		private string _email;
		private string _confirmPassword;
		private BloodType? _bloodGroup;
		private string _password;
		private IMvxCommand _registerCommand;

		public IMvxCommand RegisterCommand
		{
			get
			{
				if (this._registerCommand == null)
				{
					this._registerCommand = new MvxCommand(async () =>
					{
						if (this.ValidForm()) {

							var user = new User
							{
								Email = this.Email,
								//Name = this.Name,
								BloodGroup = this.BloodGroup
							};

							var registerResult = await Mvx.Resolve<IUserService>().RegisterUserAsync(user, this.Password);

							if (registerResult) {
								//todo: show dialog

								var settings = Mvx.Resolve<ISettings>();
								settings.Set("NotFirstAppAppRun", true);

								this.ShowViewModel<HomeViewModel>();
							} else {
								//todo: show error dialog
							}
						} else {
							// totod: show error dialog
						}

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