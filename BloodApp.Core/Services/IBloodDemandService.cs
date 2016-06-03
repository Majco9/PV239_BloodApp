using System.Collections.Generic;
using System.Threading.Tasks;
using BloodApp.Core.Model;

namespace BloodApp.Core.Services
{
	public interface IBloodDemandService
	{
		/// <summary>
		/// Returns list of blood demands
		/// </summary>
		/// <param name="bloodType">Blood type to filtering, when null then no filtering executed</param>
		/// <returns>list contains blood demands</returns>
		Task<IList<BloodDemand>> ListAllBloodDemandsAsync(BloodType? bloodType = null);

		/// <summary>
		/// Returns blood demand with given id
		/// </summary>
		/// <param name="id">blood demand's guid</param>
		/// <returns>blood demand with given id</returns>
		Task<BloodDemand> GetBloodDemandAsync(string id);

		/// <summary>
		/// Updates given blood demand
		/// </summary>
		/// <param name="demand">modified blood demand</param>
		Task UpdateBloodDemandAsync(BloodDemand demand);

		/// <summary>
		/// Removes blood demand with given id
		/// </summary>
		/// <param name="demand">blood demand</param>
		Task RemoveBloodDemandAsync(BloodDemand demand);

		/// <summary>
		/// Create blood demand
		/// </summary>
		/// <param name="demand">blood demand</param>
		/// <returns>crated blood demand with generated guid</returns>
		Task<BloodDemand> CreateBloodDemandAsync(BloodDemand demand);
	}
}