using CodeBase.Configs;
using CodeBase.Enums;

namespace CodeBase.Services.StaticData
{
	public interface IStaticDataService
	{
		void Load();
		LevelConfig ForLevel(string sceneName);
		AlienConfig ForAlien(AlienType id);
		PlayerConfig ForPlayer(PlayerType spawnPointPlayerType);
	}
}