using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloodApp.Core.Model;

namespace BloodApp.Core.Services
{
	public interface IBloodDonationService
	{
		/// <summary>
		/// Returns list of blood donation events
		/// </summary>
		/// <returns>list of blood donation events</returns>
		Task<IList<BloodDonation>> ListAllBloodDonationsAsync();

		/// <summary>
		/// Returns blood donation with given id
		/// </summary>
		/// <param name="id">blood donation's guid</param>
		/// <returns>blood donation with given id</returns>
		Task<BloodDonation> GetBloodDonationAsync(string id);

		/// <summary>
		/// Creates blood donation
		/// </summary>
		/// <param name="donation">blood donation to create</param>
		/// <returns>created blood donation event with generated id</returns>
		Task<BloodDonation> CreateBloodDonationAsync(BloodDonation donation);

		/// <summary>
		/// Updates blood donation event
		/// </summary>
		/// <param name="donation">modified blood donation</param>
		Task UpdateBloodDonationAsync(BloodDonation donation);

		/// <summary>
		/// Removes blood donation event with given id
		/// </summary>
		/// <param name="donation">blood donation</param>
		Task RemoveBloodDonationAsync(BloodDonation donation);

	}
}