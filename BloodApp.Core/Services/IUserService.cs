using System.Threading.Tasks;
using BloodApp.Core.Model;

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
		/// <param name="user">user</param>
		/// <param name="password">user's password</param>
		/// <returns><value>true</value> if register was succesfull, otherwise returns <value>false</value></returns>
		Task<bool> RegisterUserAsync(User user, string password);
	}
}