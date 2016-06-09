using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.Settings;
using BloodApp.Core.Model;
using BloodApp.Core.Services.Exceptions;
using BloodApp.Core.Utils;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;

namespace BloodApp.Core.Services
{
	public class BloodDemandService : IBloodDemandService
	{
		private readonly IMobileServiceClient _client;

		public BloodDemandService()
		{
			this._client = Mvx.Resolve<IMobileServiceClient>();
		}

		public async Task<IList<BloodDemand>> ListAllBloodDemandsAsync(BloodType? bloodType = null, bool showOnlyMyDemands = false)
		{
			try {
				var demands = await this._client.GetTable<BloodDemand>().OrderByDescending(d => d.CreatedAt).ToListAsync();

				// filter collection
				if (bloodType != null || showOnlyMyDemands) {
					var userId = Mvx.Resolve<ISettings>().Get("userId", string.Empty);
					demands = demands.Where(d =>
					{
						var result = true;

						if (bloodType != null) {
							result = d.BloodGroup != null && bloodType.Value.IsBloodTypeCompatible(d.BloodGroup.Value);
						}

						if (showOnlyMyDemands) {
							result = d.PublisherdId == userId;
						}

						return result;
					}).ToList();
				}

				return demands;
			} catch (Exception ex) {
				throw new ServiceException("Error while getting all blood demands", ex);
			}
		}

		public async Task<BloodDemand> GetBloodDemandAsync(string id)
		{
			try {
				return await this._client.GetTable<BloodDemand>().LookupAsync(id);
			} catch (Exception ex) {
				throw new ServiceException($"Error while getting blood demand with id: {id}", ex);
			}
		}

		public async Task UpdateBloodDemandAsync(BloodDemand demand)
		{
			try {
				await this._client.GetTable<BloodDemand>().UpdateAsync(demand);
			} catch (Exception ex) {
				throw new ServiceException("Error while updating all blood demands", ex);
			}
		}

		public async Task RemoveBloodDemandAsync(BloodDemand demand)
		{
			try {
				await this._client.GetTable<BloodDemand>().DeleteAsync(demand);
			} catch (Exception ex) {
				throw new ServiceException("Error while removing all blood demands", ex);
			}
		}

		public async Task<BloodDemand> CreateBloodDemandAsync(BloodDemand demand)
		{
			try {
				demand.CreatedAt = DateTime.Now;
				demand.Deleted = false;
				await this._client.GetTable<BloodDemand>().InsertAsync(demand);
				return demand;
			} catch (Exception ex) {
				throw new ServiceException("Error while creating new blood demand", ex);
			}
		}
	}
}