using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BloodApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BloodApp.Core.ViewModels
{
	public class EventListViewModel : MvxViewModel
	{

		private ObservableCollection<EventListItemViewModel> _eventsCollection;
		private readonly Lazy<IEventService> _eventService;


		public ObservableCollection<EventListItemViewModel> EventsCollection
		{
			get { return this._eventsCollection; }
			set
			{
				this._eventsCollection = value;
				this.RaisePropertyChanged();
			}
		}

		public EventListViewModel()
		{
			this._eventService = new Lazy<IEventService>(Mvx.Resolve<IEventService>);
			this._eventsCollection = new ObservableCollection<EventListItemViewModel>();
		}


		protected async Task LoadData()
		{

			//todo: load data funcionality

			// test dummy code data
			var events = await this._eventService.Value.ListAllEventsAsync();
			this.EventsCollection = new ObservableCollection<EventListItemViewModel>(events.Select(e => new EventListItemViewModel(e)));
			
		}
		
	}
}