using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.Settings;
using BloodApp.Core.Model;
using BloodApp.Core.Model.Login;
using BloodApp.Core.Model.Register;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;
using Newtonsoft.Json;

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

		/// <summary>
		/// Register new user
		/// </summary>
		/// <param name="registerModel"></param>
		/// <returns></returns>
		public async Task<bool> RegisterUserAsync(RegisterUserModel registerModel)
		{
			var client = Mvx.Resolve<IMobileServiceClient>();
			try {

				await client.InvokeApiAsync("auth/register", JsonConvert.SerializeObject(registerModel), HttpMethod.Post, null);

				return true;
			} catch (Exception ex) {
				Debug.WriteLine("Zlyhala registracia uzivale, chyba: {0}", ex.Message);
				return false;
			}


		}

		public string GetIdOfLoggedUser()
		{
			var settings = Mvx.Resolve<ISettings>();
			return settings.Get<string>("userId");
		}


		public async Task<bool> CheckForValidTokenAsync()
		{
			var client = Mvx.Resolve<IMobileServiceClient>();
			try {
				await client.GetTable<BloodDonation>().Take(1).ToListAsync();
				return true;
			} catch (Exception) {
				return false;
			}
		}
	}
}