using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.SceneLoader;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(menuName = "Static Data/Window static data", fileName = "WindowStaticData")]
	public class WindowStaticData : ScriptableObject
	{
		public List<WindowConfig> Configs;
	}
	[Serializable]
	public class WindowConfig
	{
		public WindowId WindowId;
		public ResourcePath Path;
	}

	public enum WindowId
	{
		None = 0,
		GameOver = 1,
	}
}