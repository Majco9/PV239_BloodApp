using BloodApp.Core.Model;

namespace BloodApp.Core.Utils
{
	public static class ExtensionMethods
	{
		public static bool IsBloodTypeCompatible(this BloodType bloodType, BloodType secondType)
		{
			if (bloodType == BloodType.OPositive) {

				return secondType == BloodType.ABPositive || secondType == BloodType.APositive 
					|| secondType == BloodType.BPositive || secondType == BloodType.OPositive;
			} else if (bloodType == BloodType.ONegative) {
				return true;
			} else if (bloodType == BloodType.APositive) {

				return secondType == BloodType.APositive || secondType == BloodType.ABPositive;
			} else if (bloodType == BloodType.ANegative) {

				return secondType == BloodType.APositive || secondType == BloodType.ABPositive
					|| secondType == BloodType.ANegative || secondType == BloodType.ABNegative;
			} else if (bloodType == BloodType.BPositive) {

				return secondType == BloodType.BPositive || secondType == BloodType.ABPositive;
			} else if (bloodType == BloodType.BNegative) {

				return secondType == BloodType.BPositive || secondType == BloodType.ABPositive
					|| secondType == BloodType.BNegative || secondType == BloodType.ABNegative;
			} else if (bloodType == BloodType.ABPositive) {

				return secondType == BloodType.ABPositive;
			} else if (bloodType == BloodType.ABNegative) {

				return secondType == BloodType.ABPositive || secondType == BloodType.ABNegative;
			} else {
				return false;
			}
		}
	}
}