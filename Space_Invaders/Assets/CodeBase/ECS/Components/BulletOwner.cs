using System.Collections.Generic;
using CodeBase.Enums;

namespace CodeBase.ECS.Components
{
	public struct BulletOwner
	{
		public Dictionary<BulletType, int> Bullets;
	}
}