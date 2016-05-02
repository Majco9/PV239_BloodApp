using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using BloodApp.Core.Model;
using BloodApp.Core.Model.Login;
using BloodApp.Core.Services;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;
using Newtonsoft.Json;

namespace BloodApp.UI.Uwp.Services
{
	public class UserService : IUserService
	{
		public async Task<bool> AuthenticateAsync(string email, string password)
		{
			var client = Mvx.Resolve<IMobileServiceClient>();
			var userLogin = new LoginUserModel
			{
				Email = email,
				Password = password
			};
			try {
				var result = await client.InvokeApiAsync<LoginUserModel, LoginResult>("auth/login", userLogin);
				
				if (result == null) {
					return false;
				}

				var settings = ApplicationData.Current.LocalSettings;
				settings.Values["token"] = result.Token;
				settings.Values["userId"] = result.User.Id;

				MobileServiceUser user = new MobileServiceUser(result.User.Id);
				user.MobileServiceAuthenticationToken = result.Token;
				client.CurrentUser = user;

				return true;
			} catch (Exception ex) {
				Debug.Fail("Zlyhalo prihlasenie!", ex.Message);
				return false;
			}
		}

		public Task<bool> RegisterUser(User user, string password)
		{
			throw new System.NotImplementedException();
		}
	}
}