using System;
using System.Collections.Generic;
using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(menuName = "Static Data/Window static data", fileName = "WindowStaticData")]
	public class WindowStaticData : ScriptableObject
	{
		public List<WindowData> Configs;
	}
	[Serializable]
	public class WindowData
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