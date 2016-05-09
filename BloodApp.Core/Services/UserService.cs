using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.Settings;
using BloodApp.Core.Model;
using BloodApp.Core.Model.Login;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BloodApp.Core.Services
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
				//var result = await client.InvokeApiAsync<LoginUserModel, LoginResult>("auth/login", userLogin);
				var clientResult = await client.InvokeApiAsync("auth/login", JsonConvert.SerializeObject(userLogin), HttpMethod.Post, null);

				if (clientResult == null) {
					return false;
				}

				var result = JsonConvert.DeserializeObject<LoginResult>(clientResult.ToString());

				if (result == null) {
					return false;
				}
				
				var settings = Mvx.Resolve<ISettings>();
				settings.Set("token", result.Token);
				settings.Set("userId", result.User.Id);
				
				MobileServiceUser user = new MobileServiceUser(result.User.Id);
				user.MobileServiceAuthenticationToken = result.Token;
				client.CurrentUser = user;

				return true;
			} catch (Exception ex) {
				Debug.WriteLine("Zlyhalo prihlasenie!...chyba: {0}", ex.Message);
				return false;
			}
		}

		public Task<bool> RegisterUserAsync(User user, string password)
		{
			throw new NotImplementedException();
		}

		public string GetIdOfLoggedUser()
		{
			var settings = Mvx.Resolve<ISettings>();
			return settings.Get<string>("userId");
		}
	}
}