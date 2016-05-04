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
		/// <returns>list contains blood demands</returns>
		Task<List<BloodDemand>> ListAllBloodDemandsAsync();

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
		/// <param name="id">blood demand's guid</param>
		Task RemoveBloodDemandAsync(string id);

		/// <summary>
		/// Create blood demand
		/// </summary>
		/// <param name="demand">blood demand</param>
		/// <returns>crated blood demand with generated guid</returns>
		Task<BloodDemand> CreateBloodDemandAsync(BloodDemand demand);
	}
}