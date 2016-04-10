using System;

namespace BloodApp.Core.Model
{
	/// <summary>
	/// Base class for blood donation and blood demand
	/// </summary>
	public class Event
	{
		/// <summary>
		/// Event identifier
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Event's name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Event's date
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Event's description
		/// </summary>
		public string Description { get; set; }
		 
	}
}