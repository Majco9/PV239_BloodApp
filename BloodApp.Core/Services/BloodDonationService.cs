using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloodApp.Core.Model;
using BloodApp.Core.Services.Exceptions;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;
using Newtonsoft.Json.Linq;

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
			try {
				return await this._client.GetTable<BloodDonation>().ToListAsync();
			} catch (Exception ex) {
				throw new ServiceException("Error while getting list of blood donation", ex);
			}
		}

		public async Task<BloodDonation> GetBloodDonationAsync(string id)
		{
			try {
				return await this._client.GetTable<BloodDonation>().LookupAsync(id);
			} catch (Exception ex) {
				throw new ServiceException($"Error while getting blood donation with id: {id}", ex);
			}
		}

		public async Task<BloodDonation> CreateBloodDonationAsync(BloodDonation donation)
		{
			try {
				donation.Id = Guid.NewGuid().ToString();
				donation.CreatedAt = DateTime.Now;
				await this._client.GetTable<BloodDonation>().InsertAsync(donation);
				return donation;
			} catch (Exception ex) {
				throw new ServiceException("Error while creating new blood donation event", ex);
			}
		}

		public async Task UpdateBloodDonationAsync(BloodDonation donation)
		{
			try {
				await this._client.GetTable<BloodDonation>().UpdateAsync(donation);
			} catch (Exception ex) {
				throw new ServiceException("Error while updating blood donation", ex);
			}
		}

		public async Task RemoveBloodDonationAsync(BloodDonation donation)
		{
			try {
				await this._client.GetTable<BloodDonation>().DeleteAsync(donation);
			} catch (Exception ex) {
				throw new ServiceException($"Error while removing blood donation with id: {donation}", ex);
			}
		}
	}
}