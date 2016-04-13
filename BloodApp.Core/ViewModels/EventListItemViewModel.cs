using System;
using BloodApp.Core.Model;
using MvvmCross.Core.ViewModels;

namespace BloodApp.Core.ViewModels
{
	public class EventListItemViewModel : MvxViewModel
	{
		private readonly Event _event;

		public EventListItemViewModel(Event bloodEvent)
		{
			this._event= bloodEvent;
		}
		
		public string Name => this._event.Name;

		public string Description => this._event.Description;

		public string BloodGroup
		{
			get
			{
				var demand = this._event as Demand;
				return demand != null ? demand.BloodGroup : string.Empty;
			}
		}

		public string RhFactor
		{
			get
			{
				var demand = this._event as Demand;
				return demand != null ? demand.RhFactor : string.Empty;
			}
		}

		public bool IsItemDemand => this._event is Demand;
	}
}