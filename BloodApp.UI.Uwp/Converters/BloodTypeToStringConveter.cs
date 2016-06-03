using System;
using Windows.UI.Xaml.Data;
using BloodApp.Core.Model;

namespace BloodApp.UI.Uwp.Converters
{
	public class BloodTypeToStringConveter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (value == null || !(value is BloodType)) {
				return string.Empty;
			}

			// suffix
			bool longSuffix = parameter != null && parameter.ToString() == "long";

			BloodType bloodType = (BloodType)value;

			switch (bloodType) {
				case BloodType.ABNegative:
					if (longSuffix) {
						return "AB - neg.";
					}
					return "AB-";
				case BloodType.ABPositive:
					if (longSuffix) {
						return "AB - pos.";
					}
					return "AB+";
				case BloodType.ANegative:
					if (longSuffix) {
						return "A - neg.";
					}
					return "A-";
				case BloodType.APositive:
					if (longSuffix) {
						return "A - pos.";
					}
					return "A+";
				case BloodType.BNegative:
					if (longSuffix) {
						return "B - neg.";
					}
					return "B-";
				case BloodType.BPositive:
					if (longSuffix) {
						return "B - pos.";
					}
					return "B+";
				case BloodType.ONegative:
					if (longSuffix) {
						return "0 - neg.";
					}
					return "0-";
				case BloodType.OPositive:
					if (longSuffix) {
						return "0 - pos.";
					}
					return "0+";

				default:
					return string.Empty;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new System.NotImplementedException();
		}
	}
}