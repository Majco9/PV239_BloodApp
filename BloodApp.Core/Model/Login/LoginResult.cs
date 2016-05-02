namespace BloodApp.Core.Model.Login
{
	public class LoginResult
	{
		public string Token { get; set; } 
		public LoginResultUser User { get; set; }
	}
}