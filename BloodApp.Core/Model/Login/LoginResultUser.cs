namespace BloodApp.Core.Model.Login
{
	public class LoginResultUser
	{
		public string Id { get; set; } 
		public string Email { get; set; }
		public BloodType? BloodGroup { get; set; }
	}
}