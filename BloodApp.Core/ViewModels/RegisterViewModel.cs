using BloodApp.Core.Model;
using BloodApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class RegisterViewModel : MvxViewModel
	{
		private string _email;
		private string _name;
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
						var user = new User
						{
							Email = this.Email,
							Name = this.Name,
							BloodGroup = this.BloodGroup
						};

						var registerResult = await Mvx.Resolve<IUserService>().RegisterUserAsync(user, this.Password);

						if (registerResult)
						{
							//todo: show dialog

							this.ShowViewModel<BloodDonationListViewModel>();
						} else {
							//todo: show error dialog
						}

					});
				}

				return this._registerCommand;
			}
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


	}
}