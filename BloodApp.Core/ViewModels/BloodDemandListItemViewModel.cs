using BloodApp.Core.Model;
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

		public string Created => this._bloodDemand.CreatedAt.ToString("f");

	}
}