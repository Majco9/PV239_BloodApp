namespace BloodApp.Core.Model
{
	public class Demand : Event
	{
		/// <summary>
		/// Instructions for blood donation
		/// </summary>
		public string Instructions { get; set; }
		
		/// <summary>
		/// target bloodGroup
		/// </summary>
		public string BloodGroup { get; set; }
		
		/// <summary>
		/// target RhFactor
		/// if <value>null</value>, then it doesn't matter
		/// </summary>
		public string RhFactor { get; set; } 
	}
}