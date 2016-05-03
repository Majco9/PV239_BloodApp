using System;

namespace BloodApp.Core.Model
{
	public class BloodDonation
	{

		/// <summary>
		/// donation's guid
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Guid of user that created this donation offer
		/// </summary>
		public string DonorId { get; set; }

		/// <summary>
		/// Event organizator's name
		/// </summary>
		public string OrganizatorName { get; set; }

		/// <summary>
		/// Event's name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Event's description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Event's date
		/// </summary>
		public DateTime? Date { get; set; }

		/// <summary>
		/// Event's place
		/// </summary>
		public Address Address { get; set; }

		/// <summary>
		/// Donation's created date
		/// </summary>
		public DateTime CreatedAt { get; set; }
		 
	}
}