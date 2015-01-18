using System;
using System.Text.RegularExpressions;

namespace DesktopBackgroundChanger.Extensions
{
	public static class RegexExtensions
	{
		public static string GetGroupValueOrThrow(this Match match, int groupCount, string errorMessage)
		{
			if (match.Groups.Count < groupCount - 1)
			{
				throw new Exception(errorMessage);
			}

			return match.Groups[groupCount].Value;
		}

		public static TEnum GetGroupValueAsEnumOrThrow<TEnum>(this Match match, int groupCount, string errorMessage) where TEnum : struct
		{
			var enumType = typeof(TEnum);
			if (!enumType.IsEnum) throw new ArgumentException(string.Format("The type {0} is not an enum!", enumType.Name));

			if (match.Groups.Count < groupCount - 1)
			{
				throw new Exception(errorMessage);
			}

			var matchValue = match.Groups[groupCount].Value;

			TEnum enumTarget;
			if (!Enum.TryParse(matchValue, true, out enumTarget))
			{
				throw new Exception(string.Format("Can't parse '{0}' into enum type {1}!", matchValue, enumType.Name));
			}

			return enumTarget;
		}

		public static string GetGroupValueOrDefault(this Match match, int groupCount, string defaultValue)
		{
			if (match.Groups.Count < groupCount - 1)
			{
				return defaultValue;
			}

			return match.Groups[groupCount].Value;
		}
	}
}