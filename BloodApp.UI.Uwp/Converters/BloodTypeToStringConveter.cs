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

			BloodType bloodType = (BloodType)value;

			switch (bloodType) {
				case BloodType.ABNegative:
					return "AB - Neg.";
				case BloodType.ABPositive:
					return "AB - Pos.";
				case BloodType.ANegative:
					return "A - Neg.";
				case BloodType.APositive:
					return "A - Pos.";
				case BloodType.BNegative:
					return "B - Neg.";
				case BloodType.BPositive:
					return "B - Pos.";
				case BloodType.ONegative:
					return "0 - Neg.";
				case BloodType.OPositive:
					return "0 - Pos.";

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