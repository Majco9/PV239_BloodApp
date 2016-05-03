using System.Collections.Generic;
using System.Threading.Tasks;
using BloodApp.Core.Model;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;

namespace BloodApp.Core.Services
{
	public class BloodDonationService : IBloodDonationService
	{
		private readonly IMobileServiceClient _client;

		public BloodDonationService()
		{
			this._client = Mvx.Resolve<IMobileServiceClient>();
		}

		public async Task<IList<BloodDonation>> ListAllBloodDonationsAsync()
		{
			return await this._client.GetTable<BloodDonation>().ToListAsync();
		}

		public async Task<BloodDonation> GetBloodDonation(string id)
		{
			return await this._client.GetTable<BloodDonation>().LookupAsync(id);
		}
	}
}