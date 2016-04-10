namespace BloodApp.Core.Model
{
	/// <summary>
	/// Class representive address
	/// </summary>
	public class Address
	{
		/// <summary>
		/// Address's identifier
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Address's place title (some building, hostipal, ...)
		/// </summary>
		public string PlaceTitle { get; set; }

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
	}
}