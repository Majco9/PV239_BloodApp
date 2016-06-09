using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.Settings;
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

		public async Task<IList<BloodDonation>> ListAllBloodDonationsAsync(bool filterMyEvents = false, bool includePastEvents = false)
		{
			try {
				var events = await this._client.GetTable<BloodDonation>().OrderByDescending(d => d.Date).ToListAsync();
				
				// filter collection
				if (filterMyEvents || !includePastEvents) {
					var userId = Mvx.Resolve<ISettings>().Get("userId",string.Empty);

					events = events.Where(donation =>
					{
						bool myEventsresult = true;
						bool dateFilterResult = true;

						if (filterMyEvents) {
							myEventsresult = donation.DonorId == userId;
						}

						if (!includePastEvents) {
							dateFilterResult = donation.Date.HasValue && DateTime.Compare(donation.Date.Value, DateTime.Now) >= 0;
						}
						
						return dateFilterResult && myEventsresult;
					}).ToList();
				}

				return events;
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
				donation.CreatedAt = DateTime.Now;
				donation.Deleted = false;
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