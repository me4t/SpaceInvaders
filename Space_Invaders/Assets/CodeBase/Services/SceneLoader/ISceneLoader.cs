using System;

namespace CodeBase.Services.SceneLoader
{
	public interface ISceneLoader
	{
		void Load(string name, Action onLoaded = null);
	}
}