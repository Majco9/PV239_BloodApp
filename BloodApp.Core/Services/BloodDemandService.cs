using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task<IList<BloodDemand>> ListAllBloodDemandsAsync(BloodType? bloodType = null)
		{
			try {
				var demands = await this._client.GetTable<BloodDemand>().OrderBy(d => d.CreatedAt).ToListAsync();

				if (bloodType != null) {
					demands = demands.Where(d => d.BloodGroup != null && bloodType.Value.IsBloodTypeCompatible(d.BloodGroup.Value)).ToList();
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