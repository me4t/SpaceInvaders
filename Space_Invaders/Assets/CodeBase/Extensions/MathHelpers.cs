using Unity.Mathematics;

namespace CodeBase.Extensions
{
	public class MathHelpers
	{
		public static float Magnitude(float3 a) =>
			math.sqrt(a.x * a.x + a.y * a.y + a.z * a.z);

		public static float Dot(float3 lhs, float3 rhs) =>
			lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;

		public static bool IsSpheresIntersect(float3 pos1, float3 pos2, float radius1, float radius2)
		{
			float radius = radius1 + radius2;
			var delta = pos2 - pos1;
			return SqrMagnitude(delta) < radius * radius;
		}

		public static float3 MoveTowards(float3 current, float3 target, float maxDistanceDelta)
		{
			float deltaX = target.x - current.x;
			float deltaY = target.y - current.y;
			float deltaZ = target.z - current.z;
			float sqdist = deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ;
			if (sqdist == 0 || sqdist <= maxDistanceDelta * maxDistanceDelta)
				return target;
			var dist = math.sqrt(sqdist);
			return new float3(current.x + deltaX / dist * maxDistanceDelta,
				current.y + deltaY / dist * maxDistanceDelta,
				current.z + deltaZ / dist * maxDistanceDelta);
		}

		public static float SqrMagnitude(float3 a)
		{
			return a.x * a.x + a.y * a.y + a.z * a.z;
		}

		public static float Square(float value)
		{
			return value * value;
		}

		public static float Distance(float3 from, float3 to)
		{
			return math.sqrt(Square(from.x - to.x)) + Square(from.y - to.y) + Square(from.z - to.z);
		}
	}
}