using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Extensions
{
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
	}
}