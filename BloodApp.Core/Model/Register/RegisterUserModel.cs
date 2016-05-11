namespace BloodApp.Core.Model.Register
{
	public class RegisterUserModel
	{
		public string Email { get; set; }

		public string Name { get; set; }

		public string Password { get; set; }

		public string PasswordVerify { get; set; }

		public BloodType BloodGroup { get; set; }
	}
}