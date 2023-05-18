using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace CodeBase.Enums
{
	public enum AlienType
	{
		Simple = 1,
		Unusual = 2,
	}
	public static class EnumExtensions
	{
		private static readonly
			ConcurrentDictionary<string, string> DisplayNameCache = new ConcurrentDictionary<string, string>();

		public static string ConvertToString(this Enum value)
		{
			var key = $"{value.GetType().FullName}.{value}";

			var displayName = DisplayNameCache.GetOrAdd(key, x =>
			{
				var name = (DescriptionAttribute[])value
					.GetType()
					.GetTypeInfo()
					.GetField(value.ToString())
					.GetCustomAttributes(typeof(DescriptionAttribute), false);

				return name.Length > 0 ? name[0].Description : value.ToString();
			});

			return displayName;
		}
	}
}