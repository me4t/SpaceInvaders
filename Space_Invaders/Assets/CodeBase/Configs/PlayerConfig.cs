using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Static Data/Player")]
	public class PlayerConfig : ScriptableObject
	{
		public PlayerType Type; 
		public ResourcePath Path;
		public float Speed = 3;
	}
}