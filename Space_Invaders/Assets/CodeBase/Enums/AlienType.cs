using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace CodeBase.Enums
{
	public enum AlienType
	{
		Simple = 1,
		Unusual = 2,
	}
	public enum PlayerType
	{
		Green = 1,
		Blue = 2,
	}
	public enum BulletType
	{
		Fire = 1,
		Ice = 2,
		Magic = 3,
	}
	public static class FormationExtensions
	{
		public static Vector3 GetNoise(Vector3 pos, float noise)
		{
			var perlinNoise = Mathf.PerlinNoise(pos.x * noise, pos.z * noise);

			return new Vector3(noise, 0, noise);
		}

		public static IEnumerable<Vector3> EvaluatePoints(FormationData data)
		{
			var amountPerRing = data.Amount / data.Rings;
			var ringOffset = 0f;
			for (var i = 0; i < data.Rings; i++)
			{
				for (var j = 0; j < amountPerRing; j++)
				{
					var angle = j * Mathf.PI * (2 * data.Rotations) / amountPerRing + (i % 2 != 0 ? data.NthOffset : 0);

					var radius = data.Radius + ringOffset + j * data.RadiusGrowthMultiplier;
					var x = Mathf.Cos(angle) * radius;
					var z = Mathf.Sin(angle) * radius;

					var pos = new Vector3(x, 0, z);

					pos += GetNoise(pos, data.Noise);

					pos *= data.Spread;

					yield return pos;
				}

				ringOffset += data.RingOffset;
			}
		}

		[Serializable]
		public class FormationData
		{
			public int Amount = 10;
			public float Radius = 1;
			public float RadiusGrowthMultiplier = 0;
			public float Rotations = 1;
			public int Rings = 1;
			public float RingOffset = 1;
			public float NthOffset = 0;
			public float Spread = 1;
			public float Noise = 0;
			public Vector3 UnitGizmoSize = new Vector3(1, 1, 1);
			public Color GizmoColor = Color.red;
		}
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