﻿using System;
using BloodApp.Core.Model;
using MvvmCross.Core.ViewModels;

namespace BloodApp.Core.ViewModels
{
	public class BloodDonationListItemViewModel : BaseViewModel
	{
		private readonly BloodDonation _event;

		public BloodDonationListItemViewModel(BloodDonation bloodEvent)
		{
			this._event= bloodEvent;
		}
		
		public string Id => this._event?.Id;

		public string Name => this._event.Name;

		public string Date => this._event.Date != null ? this._event.Date.Value.ToString("f") : string.Empty;
	}
}