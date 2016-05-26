using System.Threading.Tasks;
using BloodApp.Core.Model;
using BloodApp.Core.Model.Register;

namespace BloodApp.Core.Services
{
	public interface IUserService
	{
		/// <summary>
		/// Try authenticate user
		/// </summary>
		/// <param name="email">user email</param>
		/// <param name="password">user password</param>
		/// <returns><value>true</value> if autheticate was succesfull, otherwise returns <value>false</value></returns>
		Task<bool> AuthenticateAsync(string email, string password);

		/// <summary>
		/// Try register new user
		/// </summary>
		/// <param name="registerModel"></param>
		/// <returns><value>true</value> if register was succesfull, otherwise returns <value>false</value></returns>
		Task<bool> RegisterUserAsync(RegisterUserModel registerModel);

		/// <summary>
		/// Returns logged in user's id
		/// </summary>
		/// <returns>user's id, when there isn't logged user retruns null</returns>
		string GetIdOfLoggedUser();

		/// <summary>
		/// Checks for valid user token
		/// </summary>
		/// <returns><value>true</value> if user's token is valid, otherwise returns <value>false</value></returns>
		Task<bool> CheckForValidTokenAsync();

		/// <summary>
		/// Logout current user
		/// 
		/// Warning!..methods doesn't navigate to login view
		/// </summary>
		/// <returns></returns>
		void LogoutUser();

	}
}