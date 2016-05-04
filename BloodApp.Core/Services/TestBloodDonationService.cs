using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloodApp.Core.Model;

namespace BloodApp.Core.Services
{
	public class TestBloodDonationService : IBloodDonationService
	{
		
		public async Task<IList<BloodDonation>> ListAllBloodDonationsAsync()
		{
			return await Task.Run(() =>
			{
				var result = new List<BloodDonation>();

				result.Add(new BloodDonation
				{
					Id = Guid.NewGuid().ToString(),
					Name = "Test Event 1",
					Date = DateTime.Now,
					Description = "Lorem ipsum",
					Address = new Address
					{
						Street = "Testovacia 10",
						Title = "Poliklinka",
						City = "Mordor",
						State = ""
					}
				});

				result.Add(new BloodDonation
				{
					Id = Guid.NewGuid().ToString(),
					Name = "Test Event 2",
					Date = DateTime.Now,
					Description = "Lorem ipsum",
					Address = new Address
					{
						Street = "Testovacia 2",
						Title = "Krcma",
						City = "Mordor",
						State = ""
					}
				});

				return result;
			});
		}

		public Task<BloodDonation> GetBloodDonationAsync(string id)
		{
			throw new NotImplementedException();
		}
	}
}