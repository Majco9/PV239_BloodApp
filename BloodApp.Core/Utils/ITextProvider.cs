namespace BloodApp.Core.Utils
{
	public interface ITextProvider
	{
		/// <summary>
		/// Returns localized string with given key
		/// </summary>
		/// <param name="key">string's key</param>
		/// <returns>localized string or <value>null</value> if key doesn't exist</returns>
		string GetText(string key);
	}
}