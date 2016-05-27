using Windows.ApplicationModel.Resources;
using BloodApp.Core.Utils;

namespace BloodApp.UI.Uwp.Utils
{
	public class TextProvider : ITextProvider
	{
		
		public string GetText(string key)
		{
			if (string.IsNullOrEmpty(key)) {
				return null;
			}

			var res = ResourceLoader.GetForCurrentView();
			return res.GetString(key);
		}
	}
}