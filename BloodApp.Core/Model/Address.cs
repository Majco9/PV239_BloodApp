namespace BloodApp.Core.Model
{
	/// <summary>
	/// Class representive address
	/// </summary>
	public class Address
	{
		/// <summary>
		/// Address's place title (some building, hostipal, ...)
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Address's street
		/// </summary>
		public string Street { get; set; }

		/// <summary>
		/// Address's city
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Address's state
		/// </summary>
		public string State { get; set; }

		/// <summary>
		/// Address's zip code
		/// </summary>
		public string ZipCode { get; set; }
	}
}