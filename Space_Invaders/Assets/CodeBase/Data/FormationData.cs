using System;
using UnityEngine;

namespace CodeBase.Data
{
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