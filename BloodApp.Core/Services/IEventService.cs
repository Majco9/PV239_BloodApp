using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloodApp.Core.Model;

namespace BloodApp.Core.Services
{
	public interface IEventService
	{
		Task<IList<Event>> ListAllEventsAsync();

		Task<IList<BloodDonation>> ListAllBloodDonationsAsync();

		Task<IList<Demand>> ListAllDemandsAsync();
	}
}