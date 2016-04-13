using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloodApp.Core.Model;

namespace BloodApp.Core.Services
{
	public class TestEventService : IEventService
	{
		public async Task<IList<Event>> ListAllEventsAsync()
		{
			return await Task.Run(() =>
			{
				var result = new List<Event>();

				result.Add(new Event
				{
					Id = 0,
					Name = "Test Event 1",
					Date = DateTime.Now,
					Description = "Lorem ipsum",
					Address = new Address
					{
						Id = 0,
						Street = "Testovacia 10",
						PlaceTitle = "Poliklinka",
						City = "Mordor",
						State = ""
					}
				});

				result.Add(new Event
				{
					Id = 1,
					Name = "Test Event 2",
					Date = DateTime.Now,
					Description = "Lorem ipsum",
					Address = new Address
					{
						Id = 1,
						Street = "Testovacia 2",
						PlaceTitle = "Krcma",
						City = "Mordor",
						State = ""
					}
				});

				return result;
			});
		}

		public Task<IList<BloodDonation>> ListAllBloodDonationsAsync()
		{
			throw new System.NotImplementedException();
		}

		public Task<IList<Demand>> ListAllDemandsAsync()
		{
			throw new System.NotImplementedException();
		}
	}
}