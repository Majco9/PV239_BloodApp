using MvvmCross.Core.ViewModels;

namespace BloodApp.Core.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{

		public static BaseViewModel ActiveViewModel;
		private IMvxCommand _backCommand;

		public IMvxCommand BackCommand
		{
			get
			{
				if (this._backCommand == null) {
					this._backCommand = new MvxCommand(() =>
					{
						this.Close(BaseViewModel.ActiveViewModel);
					});
				}
				return this._backCommand;
			}
		}

		public override void Start()
		{
			base.Start();
			BaseViewModel.ActiveViewModel = this;
		}
	}
}