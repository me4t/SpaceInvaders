using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(menuName = "Static Data/Window static data", fileName = "WindowStaticData")]
	public class WindowStaticConfig : ScriptableObject
	{
		public List<WindowData> Configs;
	}
}