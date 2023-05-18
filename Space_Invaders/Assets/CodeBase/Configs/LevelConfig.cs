using System;
using System.Collections.Generic;
using CodeBase.Enums;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(fileName = "LevelConfig", menuName = "Static Data/Level")]
	public class LevelConfig : ScriptableObject
	{
		 public string Key;
		 public List<AlienSpawnPoint> aliens;
	}

	[Serializable]
	public class AlienSpawnPoint
	{
		public float3 Position;
		public AlienType AlienType;
	}
}