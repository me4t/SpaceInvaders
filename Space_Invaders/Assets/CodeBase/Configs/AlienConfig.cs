using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(fileName = "AlienConfig", menuName = "Static Data/Alien")]
	public class AlienConfig : ScriptableObject
	{
		public AlienType Type;
		public ResourcePath Path;
	}
}