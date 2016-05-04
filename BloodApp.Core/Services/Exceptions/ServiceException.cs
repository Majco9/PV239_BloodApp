using System;

namespace BloodApp.Core.Services.Exceptions
{
	/// <summary>
	/// Represents exception that service layer should thrown
	/// </summary>
	public class ServiceException : Exception
	{
		public ServiceException()
		{
		}

		public ServiceException(string message) : base(message)
		{
		}

		public ServiceException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}