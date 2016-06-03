using BloodApp.Core.Model;
using BloodApp.Core.Model.Util;
using MvvmCross.Core.ViewModels;

namespace BloodApp.Core.ViewModels
{
	public class BloodDemandListItemViewModel : BaseViewModel
	{
		private readonly BloodDemand _bloodDemand;

		public BloodDemandListItemViewModel(BloodDemand bloodDemand)
		{
			this._bloodDemand = bloodDemand;
		}

		public string Id => this._bloodDemand?.Id;

		public string Created => this._bloodDemand?.CreatedAt.ToString("D");

		public BloodType? BloodGroup => this._bloodDemand?.BloodGroup;

	}
}