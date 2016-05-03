using System;

namespace BloodApp.Core.Model
{
	public class BloodDemand
	{
		/// <summary>
		/// demand's guid
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Guid of user that created this demand
		/// </summary>
		public string PublisherId { get; set; }

		/// <summary>
		/// Demand's description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Instructions for volunteers
		/// </summary>
		public string Instructions { get; set; }

		/// <summary>
		/// patient's blood group
		/// </summary>
		public BloodType? BloodGroup { get; set; }

		/// <summary>
		/// Demand's created time
		/// </summary>
		public DateTime CreatedAt { get; set; }
	}
}