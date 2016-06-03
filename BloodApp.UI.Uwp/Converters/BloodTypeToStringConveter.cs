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
					return "AB-";
				case BloodType.ABPositive:
					return "AB+";
				case BloodType.ANegative:
					return "A-";
				case BloodType.APositive:
					return "A+";
				case BloodType.BNegative:
					return "B-";
				case BloodType.BPositive:
					return "B+";
				case BloodType.ONegative:
					return "0-";
				case BloodType.OPositive:
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