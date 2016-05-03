using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloodApp.Core.Model;

namespace BloodApp.Core.Services
{
	public interface IBloodDonationService
	{

		Task<IList<BloodDonation>> ListAllBloodDonationsAsync();

		Task<BloodDonation> GetBloodDonation(string id);

	}
}