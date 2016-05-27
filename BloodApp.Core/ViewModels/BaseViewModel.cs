using System.Resources;
using BloodApp.Core.Strings.en_US;
using MvvmCross.Core.ViewModels;

namespace BloodApp.Core.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{

		public static BaseViewModel ActiveViewModel;
		private IMvxCommand _backCommand;
		private bool _isLoading;

		public string this[string key] => Resources.ResourceManager.GetString(key);

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

		public virtual bool IsLoading
		{
			get { return this._isLoading; }
			set
			{
				this._isLoading = value;
				this.RaisePropertyChanged();
			}
		}

		public override void Start()
		{
			base.Start();
			BaseViewModel.ActiveViewModel = this;
		}
	}
}