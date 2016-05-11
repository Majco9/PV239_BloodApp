using BloodApp.Core.Model;
using BloodApp.Core.Model.Util;
using MvvmCross.Core.ViewModels;

namespace BloodApp.Core.ViewModels
{
	public class BloodDemandListItemViewModel : MvxViewModel
	{
		private readonly BloodDemand _bloodDemand;

		public BloodDemandListItemViewModel(BloodDemand bloodDemand)
		{
			this._bloodDemand = bloodDemand;
		}

		public string Created => this._bloodDemand?.CreatedAt.ToString("D");

		public string Title => $"{this._bloodDemand?.BloodGroup.GetBloodTypeFriendyName()} - Žiadosť o krv";

	}
}