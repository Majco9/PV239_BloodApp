namespace BloodApp.Core.Model.Util
{
	public static class ExtensionMethods
	{
		public static string GetBloodTypeFriendyName(this BloodType? bloodType)
		{
			if (!bloodType.HasValue) {
				return string.Empty;
			}

			switch (bloodType.Value) {
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
	}
}