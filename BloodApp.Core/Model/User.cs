namespace BloodApp.Core.Model
{
	public class User
	{
		public string Email { get; set; } 

		public string Name { get; set; }

		public BloodType? BloodGroup { get; set; }
	}
}